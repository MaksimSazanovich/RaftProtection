using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class TargetAndShootAI : AI
{

    [SerializeField] private float minimumDistance;

    [SerializeField] private UnityEvent OnStop;

    [Inject]
    private void Constuct(RaftHealth raftHeath)
    {
        _targetPosition = raftHeath.gameObject.transform.position;
    }

    protected override void Start()
    {
        base.Start();
        if (transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    protected virtual void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, _targetPosition) < minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPosition, -_speed * Time.deltaTime);
            OnStop.Invoke();
        }
    }
}
