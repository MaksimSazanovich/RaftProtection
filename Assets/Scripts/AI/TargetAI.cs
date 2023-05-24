using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Drawing;

public class TargetAI : AI
{
    protected override void Start()
    {
        targetPosition = FindObjectOfType<Raft>().transform.position;
        if (transform.position.x < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
        }
        else if (transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 360f, transform.rotation.eulerAngles.y + 360f, Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
            transform.localScale = new Vector3(1f, -1f, 1f);
        }

        //transform.DOMove(targetPosition, speed).SetSpeedBased().SetEase(Ease.Linear);//.OnComplete(() => Destroy(gameObject));
    }

    protected virtual void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }


}