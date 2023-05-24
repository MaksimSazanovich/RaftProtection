using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetAndShootAI : AI
{

    [SerializeField] private float minimumDistance;

    [SerializeField] private UnityEvent OnStop;
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
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, targetPosition) < minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, -speed * Time.deltaTime);
            OnStop.Invoke();
        }
    }
}
