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
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] private float _reachDistance = 0.5f;
    [SerializeField] private int _pointsToResearch = 3;
    [SerializeField] private float _researchRadius = 3f;
    
    private NavMeshAgent _agent;
    public float ChaseSpeed => _chaseSpeed;
    public float PatrolSpeed => _patrolSpeed;
    public Transform[] PatrolPoints => _patrolPoints;
    public float AttackCooldownTime => _attackCooldownTime;
    public int AttackDamage => _attackDamage;
    public float VisionRange => _visionRange;
    public float VisionAngle => _visionAngle;
    public float AttackRange => _attackRange;
    public float ReachDistance => _reachDistance;
    public int PointsToResearch => _pointsToResearch;
    public float ResearchRadius => _researchRadius;
    public Blackboard Blackboard => _blackboard;
    public NavMeshAgent Agent => _agent;

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
        _root = new NodeRoot(this);
        NodeSelector selector = new NodeSelector(this);
        _root.Child = selector;

        // Chase and attack sequence
        NodeSequence a = new NodeSequence(this);
        selector.Children.Add(a);
        NodeLeaf aa = new IsPlayerVisible(this);
        NodeLeaf ab = new Chase(this);
        NodeDecorator ac = new CooldownDecorator(this);
        NodeLeaf aca = new Attack(this);
        ac.Child = aca;
        a.Children.Add(aa);
        a.Children.Add(ab);
        a.Children.Add(ac);
        
        // Investigate noise et research sequence
        NodeSequence b = new NodeSequence(this);
        selector.Children.Add(b);
        NodeLeaf ba = new DetectNoise(this);
        NodeLeaf bb = new InvestigateNoise(this);
        NodeLeaf bc = new ResearchArea(this);
        b.Children.Add(ba);
        b.Children.Add(bb);
        b.Children.Add(bc);

        // Follow footsteps sequence
        NodeSequence c = new NodeSequence(this);
        selector.Children.Add(c);
        NodeLeaf ca = new DetectFootsteps(this);
        NodeLeaf cb = new FollowFootsteps(this);
        c.Children.Add(ca);
        c.Children.Add(cb);
        
        NodeLeaf d = new Patrol(this);
        selector.Children.Add(d);
    }

    private void Update()
    {
        _root.ExecuteAndDebug();
    }
}