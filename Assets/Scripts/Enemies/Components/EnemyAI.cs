using BehaviorTree;
using Enemies.Nodes;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Components
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Blackboard _blackboard;

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

        private NodeRoot _root;
        private EnemySensor _sensor;
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
        public NavMeshAgent Agent { get; private set; }

        private void Start()
        {
            // Get NavMeshAgent and set patrol speed
            Agent = GetComponent<NavMeshAgent>();
            Agent.speed = _patrolSpeed;
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
            var selector = new NodeSelector(this);
            _root.Child = selector;

            // Chase and attack sequence
            var a = new NodeSequence(this);
            selector.Children.Add(a);

            NodeLeaf aa = new IsPlayerVisible(this);
            NodeLeaf ab = new Chase(this);
            NodeDecorator ac = new CooldownDecorator(this);
            NodeLeaf aca = new Attack(this);
            ac.Child = aca;
            a.Children.Add(aa);
            a.Children.Add(ab);
            a.Children.Add(ac);

            // Research Area sequence
            var b = new NodeSequence(this);
            selector.Children.Add(b);

            NodeLeaf ba = new HasInvestigatedNoise(this);
            NodeLeaf bb = new ResearchArea(this);
            b.Children.Add(ba);
            b.Children.Add(bb);

            // Investigate noise et research sequence
            var c = new NodeSequence(this);
            selector.Children.Add(c);

            NodeLeaf ca = new DetectNoise(this);
            NodeLeaf cb = new InvestigateNoise(this);
            NodeLeaf cc = new ResearchArea(this);
            c.Children.Add(ca);
            c.Children.Add(cb);
            c.Children.Add(cc);

            // Follow footsteps sequence
            var d = new NodeSequence(this);
            selector.Children.Add(d);

            NodeLeaf da = new DetectFootsteps(this);
            NodeLeaf db = new FollowFootsteps(this);
            d.Children.Add(da);
            d.Children.Add(db);

            NodeLeaf e = new Patrol(this);
            selector.Children.Add(e);
        }

        private void Update()
        {
            _root.ExecuteAndDebug();
        }
    }
}