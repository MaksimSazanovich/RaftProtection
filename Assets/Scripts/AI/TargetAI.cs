using UnityEngine;

public class TargetAI : AI
{
    protected override void Start()
    {
        base.Start();
        if (transform.position.x < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(_targetPosition.y - transform.position.y, _targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
        }
        else if (transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 360f, transform.rotation.eulerAngles.y + 360f, Mathf.Atan2(_targetPosition.y - transform.position.y, _targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
            transform.localScale = new Vector3(1f, -1f, 1f);
        }
    }

    protected virtual void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }
}