using UnityEngine;
using UnityEngine.Events;

public class Gun : WeaponBase
{
    [SerializeField] private GunButton gunButton;
    private Transform nearestEnemy;
    private float nearestEnemyDistance;
    private float currentDistance;
    private Vector3 raftPosition;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Vector2 attackSize;

    [SerializeField] private Player player;

    [SerializeField] private UnityEvent OnShot;



    private void Awake()
    {
        gunButton.OnClick.AddListener(Rotate);
        raftPosition = FindObjectOfType<RaftHealth>().transform.position;
    }

    private void Update()
    {
        SearchTarget();
    }

    private void SearchTarget()
    {
        nearestEnemy = null;
        nearestEnemyDistance = Mathf.Infinity;

        //Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        Collider2D[] hitObjects = Physics2D.OverlapBoxAll(raftPosition, attackSize, 0, enemyLayer);      
        foreach (Collider2D enemy in hitObjects)
        {
            currentDistance = Vector2.Distance(transform.position, enemy.GetComponent<Transform>().transform.position);

            if (currentDistance < nearestEnemyDistance)
            {
                nearestEnemy = enemy.GetComponent<Transform>().transform;
                nearestEnemyDistance = currentDistance;
            }
        }
    }

    public void Rotate()
    {
        if (Time.time > nextShotTime)
        {
            if(nearestEnemy != null)
            targetPosition = nearestEnemy.transform.position;

            if (targetPosition.x < 0)
            {
                player.Rotate(targetPosition);
                if(transform.rotation.eulerAngles.y == 180)
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180f /* изменить!!! */, Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
                else transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
                transform.localScale = new Vector3(1f, -1f, 1f);
            }
            else if (targetPosition.x > 0)
            {
                player.Rotate(targetPosition);
                if (transform.rotation.eulerAngles.y == 180f)
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 180f, Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
                else transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            OnShot.Invoke();
            Shoot();
            nextShotTime = Time.time + timeBetweenShots;
        }
    }

    protected override void Shoot()
    {
        //Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        BulletActivate(firePoint, transform);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(raftPosition, attackSize);
    }
}