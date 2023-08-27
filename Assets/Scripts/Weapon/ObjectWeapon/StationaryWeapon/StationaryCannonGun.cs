using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StationaryCannonGun : WeaponBase
{
    private bool shootLock = false;
    private Transform nearestEnemy;
    private float nearestEnemyDistance;
    private float currentDistance;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackSize;

    protected override void Start()
    {
        base.Start();

        if (transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (transform.position.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void SearchTarget()
    {
        nearestEnemy = null;
        nearestEnemyDistance = Mathf.Infinity;

        //Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, attackSize,enemyLayer);
        foreach (Collider2D enemy in hitObjects)
        {
            currentDistance = Vector2.Distance(transform.position, enemy.GetComponent<Transform>().transform.position);

            if (currentDistance < nearestEnemyDistance)
            {
                nearestEnemy = enemy.GetComponent<Transform>().transform;
                nearestEnemyDistance = currentDistance;
                UnlockShoot();
            }
        }


    }

    protected virtual void Update()
    {
        SearchTarget();
        if (Time.time > nextShotTime && shootLock)
        {
            Rotate();
        }
    }

    public void Rotate()
    {
        if (Time.time > nextShotTime)
        {
            if (nearestEnemy == null)
            { 
                shootLock = false;
                return;
            }
            if (nearestEnemy != null)
                targetPosition = nearestEnemy.transform.position;

            if (targetPosition.x < 0)
            {
                //if (transform.rotation.eulerAngles.y == 180)
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
                transform.localScale = new Vector3(1f, -1f, 1f);
                //else transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(_targetPosition.y - transform.position.y, _targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
            }
            else if (targetPosition.x > 0)
            {
                //if (transform.rotation.eulerAngles.y == 180f)
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
                //else transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(_targetPosition.y - transform.position.y, _targetPosition.x - transform.position.x) * Mathf.Rad2Deg);s
            }

            if (nearestEnemy != null)
                Shoot();
            nextShotTime = Time.time + timeBetweenShots;
        }
    }
    protected override void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
    }

    public void UnlockShoot() => shootLock = true;

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawSphere(transform.position, attackSize);
    //}
}