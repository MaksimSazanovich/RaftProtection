using Internal.Scripts.Raft;
using Internal.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Internal.Scripts.Weapon.ObjectWeapon.PlayerWeapon
{
    public class Gun : WeaponBase
    {
        [SerializeField] private GunButton gunButton;
        private Transform nearestEnemy;
        private float nearestEnemyDistance;
        private float currentDistance;
        private Vector3 raftPosition;

        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private Vector2 attackSize;

        [SerializeField] private Player.Player player;

        [SerializeField] private UnityEvent OnShot;

        private TouchDetector touchDetector;

        [Inject]
        private void Construct(TouchDetector touchDetector)
        { 
            this.touchDetector = touchDetector;
        }

        private void Awake()
        {
            //gunButton.OnClick.AddListener(Rotate);
            raftPosition = FindObjectOfType<RaftHealth>().transform.position;
        }

        private void OnEnable()
        {
            touchDetector.OnTouch += Rotate;
        }

        private void OnDisable()
        {
            touchDetector.OnTouch -= Rotate;
        }

        private void Update()
        {
        
        }

        //private void SearchTarget()
        //{
        //    nearestRaftPiece = null;
        //    nearestRaftPieceDistance = Mathf.Infinity;

        //    //Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        //    Collider2D[] hitObjects = Physics2D.OverlapBoxAll(raftPosition, attackSize, 0, enemyLayer);
        //    foreach (Collider2D enemy in hitObjects)
        //    {
        //        currentDistance = Vector2.Distance(transform.position, enemy.GetComponent<Transform>().transform.position);

        //        if (currentDistance < nearestRaftPieceDistance)
        //        {
        //            nearestRaftPiece = enemy.GetComponent<Transform>().transform;
        //            nearestRaftPieceDistance = currentDistance;
        //        }
        //    }
        //}

        public void Rotate(Vector3 targetPosition)
        {
            if (Time.time > nextShotTime)
            {
                //if(nearestRaftPiece != null)
                //targetPosition = nearestRaftPiece.transform.position;

                if (targetPosition.x < 0)
                {
                    player.Rotate(targetPosition);
                    if(transform.rotation.eulerAngles.y == 180)
                        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180f /* ��������!!! */, Mathf.Atan2(targetPosition.y - transform.position.y, targetPosition.x - transform.position.x) * Mathf.Rad2Deg);
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
}