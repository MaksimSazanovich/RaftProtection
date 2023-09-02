using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Internal.Scripts.Raft
{
    public class PlaceToCreateStationaryWeapon : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject weaponPrefab;

        [SerializeField] private int cost;
        [SerializeField] private TMP_Text costText;
        private Color littleMoney, enoughMoney;
        private SpriteRenderer spriteRenderer;

        public static event Action<int> OnBuyPiece;

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("qwerty");
            if (ScoreCollector.scoreCollected >= cost)
            {
                Instantiate(weaponPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                OnBuyPiece?.Invoke(-cost);
            }
        }

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            costText.text = cost.ToString();
            littleMoney = new Color(235f / 255, 7f / 255f, 0f, 194f / 255);
            enoughMoney = new Color(8f / 255f, 41f / 255, 0f, 107f / 255);
            spriteRenderer.color = littleMoney;

        }

        private void Update()
        {
            if (ScoreCollector.scoreCollected >= cost)
                spriteRenderer.color = enoughMoney;
            else
                spriteRenderer.color = littleMoney;
        }
    }
}