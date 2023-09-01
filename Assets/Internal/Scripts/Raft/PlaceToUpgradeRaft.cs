using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using Zenject;
using UnityEngine.UIElements;

public class PlaceToUpgradeRaft : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject raftPiecePrefab;

    [SerializeField] private int cost;
    [SerializeField] private TMP_Text costText;
    private Color littleMoney, enoughMoney;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject[] patterns;

    public static event Action<int> OnBuyPiece;
    public static event Action<GameObject> OnInstantiatePiece;

    private float rayDistance = 0.02f;
    [SerializeField] private float rayOffset;

    [SerializeField] private LayerMask raftLayer;
    private Transform newParent;
    private GameObject currentPattern;

    [SerializeField] private DiContainer container;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        container = diContainer;
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
        else spriteRenderer.color = littleMoney;
        Debug.DrawRay(transform.position + Vector3.right * rayOffset, transform.right, Color.green);
        Debug.DrawRay(transform.position - Vector3.right * rayOffset, -transform.right, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * rayOffset, transform.up, Color.blue);
        Debug.DrawRay(transform.position - Vector3.up * rayOffset, -transform.up, Color.yellow);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ScoreCollector.scoreCollected >= cost)
        {
            GameObject piece = container.InstantiatePrefab(raftPiecePrefab, transform.position, Quaternion.identity, null);
            OnInstantiatePiece?.Invoke(piece);
            ShowNewPieces();
            Destroy(gameObject);
            OnBuyPiece?.Invoke(-cost);
        }
    }
    private void ShowNewPieces()
    {
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position + Vector3.right * rayOffset, transform.right, rayDistance, raftLayer);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position - Vector3.right * rayOffset, -transform.right, rayDistance, raftLayer);
        RaycastHit2D upHit = Physics2D.Raycast(transform.position + Vector3.up * rayOffset, transform.up, rayDistance, raftLayer);
        RaycastHit2D downHit = Physics2D.Raycast(transform.position - Vector3.up * rayOffset, -transform.up, rayDistance, raftLayer);

        //one piece
        if (upHit.collider != null && leftHit.collider != null && downHit.collider != null && rightHit.collider == null)
        {
            //currentPattern = Instantiate(patterns[8], transform.position, Quaternion.identity);
            currentPattern = container.InstantiatePrefab(patterns[8], transform.position, Quaternion.identity, null);
        }

        if (upHit.collider != null && downHit.collider != null && rightHit.collider != null && leftHit.collider == null)
        {
            currentPattern = container.InstantiatePrefab(patterns[9], transform.position, Quaternion.identity, null);
        }

        if (upHit.collider != null && downHit.collider == null && rightHit.collider != null && leftHit.collider != null)
        {
            currentPattern = container.InstantiatePrefab(patterns[10], transform.position, Quaternion.identity, null);
        }

        if (upHit.collider == null && downHit.collider != null && rightHit.collider != null && leftHit.collider != null)
        {
            currentPattern = container.InstantiatePrefab(patterns[11], transform.position, Quaternion.identity, null);
        }

        //diagonal
        if (downHit.collider != null && leftHit.collider != null && rightHit.collider == null && upHit.collider == null)
        {
            currentPattern = container.InstantiatePrefab(patterns[4], transform.position, Quaternion.identity, null);
        }

        if (downHit.collider != null && rightHit.collider != null && leftHit.collider == null && upHit.collider == null)
        {
            currentPattern = container.InstantiatePrefab(patterns[5], transform.position, Quaternion.identity, null);
        }

        if (upHit.collider != null && leftHit.collider != null && rightHit.collider == null && downHit.collider == null)
        {
            currentPattern = container.InstantiatePrefab(patterns[6], transform.position, Quaternion.identity, null);
        }

        if (upHit.collider != null && rightHit.collider != null && leftHit.collider == null && downHit.collider == null)
        {
            currentPattern = container.InstantiatePrefab(patterns[7], transform.position, Quaternion.identity, null);
        }

        //base patterns
        if (leftHit.collider != null && rightHit.collider == null && upHit.collider == null && downHit.collider == null)
        {
            currentPattern = container.InstantiatePrefab(patterns[1], transform.position, Quaternion.identity, null);
        }

        if (rightHit.collider != null && leftHit.collider == null && upHit.collider == null && downHit.collider == null)
        {
            currentPattern = container.InstantiatePrefab(patterns[0], transform.position, Quaternion.identity, null);
        }


        if (upHit.collider != null && rightHit.collider == null && leftHit.collider == null && downHit.collider == null)
        {
            currentPattern = container.InstantiatePrefab(patterns[2], transform.position, Quaternion.identity, null);
        }

        if (downHit.collider != null && rightHit.collider == null && leftHit.collider == null && upHit.collider == null)
        {
            currentPattern = container.InstantiatePrefab(patterns[3], transform.position, Quaternion.identity, null);
        }


        newParent = FindObjectOfType<RaftPlaces>().transform;
        currentPattern.transform.SetParent(newParent, true);
    }

}
