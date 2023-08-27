using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class EnemyGun : WeaponBase
{
    private bool shootLock = false;

    protected override void Start()
    {
        base.Start();
        targetPosition = new Vector3(0, -1f, 0.7505713f);
        if (transform.position.x < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
        }
        else if (transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 360f, transform.rotation.eulerAngles.y + 360f, Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
            transform.localScale = new Vector3(-1f, -1f, 1f);
        }

        //transform.DOMove(_targetPosition, _speed).SetSpeedBased().SetEase(Ease.Linear);//.OnComplete(() => Destroy(gameObject));
    }

    protected virtual void Update()
	{
        if (Time.time > nextShotTime && shootLock)
        {
            Shoot();
            nextShotTime = Time.time + timeBetweenShots;
        }
    }
    protected override void Shoot()
    {
        //Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        BulletActivate(firePoint, transform);
    }

    public void UnlockShoot() => shootLock = true;
}