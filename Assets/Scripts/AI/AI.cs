using UnityEngine;
using Zenject;

public abstract class AI : MonoBehaviour
{
    protected Vector3 _targetPosition;
    protected float _speed;
    public float Speed { get => _speed; set => _speed = value; }

    [SerializeField] protected float _timeToTarget;

    [Inject]
    protected virtual void Construct(RaftHealth raftHeath)
    {
        _targetPosition = raftHeath.transform.localPosition;
    }

    protected virtual void Start()
    {
        _speed = Vector2.Distance(transform.position, _targetPosition) / _timeToTarget;
    }
}