using Enemies;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NodeRoot _root;
    private EnemySensor _sensor;
    private Blackboard _blackboard;
    
    
    [SerializeField] private float _chaseSpeed = 5f;
    [SerializeField] private float _patrolSpeed = 3f;
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _attackCooldownTime = 1f;
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private float _visionRange = 10f;
    [SerializeField] private float _visionAngle = 120f;

    private NavMeshAgent _agent;

private void Start()
{
        // Get NavMeshAgent and set patrol speed
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _patrolSpeed;
        // Initialize blackboard 
        _blackboard = new Blackboard();
        
        // Get sensor component and link blackboard
        _sensor = GetComponentInChildren<EnemySensor>();
        _sensor.Blackboard = _blackboard;
        
        //Get hearing component and link blackboard
        var hearing = GetComponentInChildren<EnemyHearing>();
        hearing.Blackboard = _blackboard;
        
        // Add node root and selector
        _root = new NodeRoot();
        NodeSelector selector = new NodeSelector();
        _root.Child = selector;

      // Chase and attack sequence
        NodeSequence a = new NodeSequence();
        selector.Children.Add(a);
        NodeLeaf aa = new IsPlayerVisible(_blackboard);
        NodeLeaf ab = new Chase(transform, _agent, _blackboard, 1f);
        NodeDecorator ac = new CooldownDecorator(new Attack(transform, _blackboard, _attackDamage), _attackCooldownTime);
        a.Children.Add(aa);
        a.Children.Add(ab);
        a.Children.Add(ac);
       
        // Investigate noise et research sequence
        NodeSequence b = new NodeSequence();
        selector.Children.Add(b);
        NodeLeaf ba = new DetectNoise(_blackboard);
        NodeLeaf bb = new InvestigateNoise(_agent, _blackboard, 0.5f);
        NodeLeaf bc = new ResearchArea(_agent, _blackboard, 5f, 3);
        b.Children.Add(ba);
        b.Children.Add(bb);
        b.Children.Add(bc);

        // Follow footsteps sequence
        NodeSequence c = new NodeSequence();
        selector.Children.Add(c);
        NodeLeaf ca = new DetectFootsteps(transform, _blackboard, _visionRange, _visionAngle );
        NodeLeaf cb = new FollowFootsteps(_agent, _blackboard, 0.5f);
        c.Children.Add(ca);
        c.Children.Add(cb);
        
        NodeLeaf d = new Patrol(transform, _agent, _patrolPoints, 0.2f);
        selector.Children.Add(d);
    }

    private void Update()
    {
        _root.Execute();
    }
}