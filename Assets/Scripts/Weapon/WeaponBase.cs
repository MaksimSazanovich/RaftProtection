using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected Vector3 targetPosition;

    [SerializeField] protected float timeBetweenShots;
    protected float nextShotTime;

    [SerializeField] protected int damage;

    [SerializeField] protected int bulletsCount;
    protected PlayerBulletsPool bulletsPool;

    protected virtual void Start()
    {
        if (bulletsPool == null)
            bulletsPool = FindObjectOfType<PlayerBulletsPool>();
        if (bulletsCount > 0)
            bulletsPool.AddBullets(bulletPrefab, bulletsCount);
        bulletPrefab.GetComponent<Bullet>().Damage = damage;
    }

    protected void BulletActivate(Transform bulletStartPosition, Transform weapon)
    {
        var bullet = bulletsPool.GetBullet(bulletPrefab);
        bullet.transform.position = bulletStartPosition.position;
        bullet.transform.rotation = weapon.rotation;
        bullet.SetActive(true);
    }

    protected abstract void Shoot();
}