using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Internal.Scripts.AI
{
    public abstract class AI : MonoBehaviour
    {
        protected Vector3 _targetPosition;
        public float Speed { get => _agent.speed; set => _agent.speed = value; }

        private Transform nearestRaftPiece;
        private float nearestRaftPieceDistance;
        private float currentDistance;

        private Raft.Raft raft;

        [SerializeField] protected NavMeshAgent _agent;

        [Inject]
        protected virtual void Construct(Raft.Raft raft)
        {
            //_targetPosition = raft.transform.position;
            //float targetPositionX;
            //if (_targetPosition.x > transform.position.x)
            //    targetPositionX = -1;
            //else
            //    targetPositionX = 1;

            //float targetPositionY = (transform.position.y + 0.01f) / 12;

            this.raft = raft;

            //_targetPosition = new Vector3(targetPositionX, targetPositionY, 0);
        }

        private void OnEnable()
        {
            raft.OnAddRaftPiece += Move;
        }

        private void OnDisable()
        {
            raft.OnAddRaftPiece -= Move;
        }

        protected virtual void Start()
        {        
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;

            Move();
        }

        protected virtual void Move()
        {
            _agent.SetDestination(SearchTarget());
        }

        private Vector3 SearchTarget()
        {
            nearestRaftPiece = null;
            nearestRaftPieceDistance = Mathf.Infinity;

            //Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        
            foreach (GameObject raftPiece in raft.RaftPices)
            {
                currentDistance = Vector2.Distance(transform.position, raftPiece.transform.position);

                if (currentDistance < nearestRaftPieceDistance)
                {
                    nearestRaftPiece = raftPiece.transform;
                    nearestRaftPieceDistance = currentDistance;
                }
            }
            //Debug.Log(nearestRaftPiece);
            _targetPosition = nearestRaftPiece.position;
            //Debug.Log(_targetPosition);

            return nearestRaftPiece.position;
        }
    }
}