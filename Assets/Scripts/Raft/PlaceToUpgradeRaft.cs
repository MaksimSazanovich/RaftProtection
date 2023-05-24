using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;
using System;

public class PlaceToUpgradeRaft : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject raftPiecePrefab;

    [SerializeField] private int cost;
    [SerializeField] private TMP_Text costText;
    private Color littleMoney, enoughMoney;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject[] patterns;

    public static event Action<int> OnBuyPiece;

    private float rayDistance = 0.02f;
    [SerializeField] private float rayOffset;

    [SerializeField] private LayerMask raftLayer;
    private Transform newParent;
    private GameObject currentPattern;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ScoreCollector.scoreCollected >= cost)
        {
            Instantiate(raftPiecePrefab, transform.position, Quaternion.identity);
            ShowNewPieces();
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
        else spriteRenderer.color = littleMoney;
        Debug.DrawRay(transform.position + Vector3.right * rayOffset, transform.right, Color.green);
        Debug.DrawRay(transform.position - Vector3.right * rayOffset, -transform.right, Color.red);
        Debug.DrawRay(transform.position + Vector3.up * rayOffset, transform.up, Color.blue);
        Debug.DrawRay(transform.position - Vector3.up * rayOffset, -transform.up, Color.yellow);
    }

    private void ShowNewPieces()
    {
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position + Vector3.right * rayOffset, transform.right, rayDistance, LayerMask.GetMask("Raft"));
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position - Vector3.right * rayOffset, -transform.right, rayDistance, LayerMask.GetMask("Raft"));
        RaycastHit2D upHit = Physics2D.Raycast(transform.position + Vector3.up * rayOffset, transform.up, rayDistance, LayerMask.GetMask("Raft"));
        RaycastHit2D downHit = Physics2D.Raycast(transform.position - Vector3.up * rayOffset, -transform.up, rayDistance, LayerMask.GetMask("Raft"));

        //one piece
        if (upHit.collider != null && leftHit.collider != null && downHit.collider != null && rightHit.collider == null)
        {
            currentPattern = Instantiate(patterns[8], transform.position, Quaternion.identity);
        }

        if (upHit.collider != null && downHit.collider != null && rightHit.collider != null && leftHit.collider == null)
        {
            currentPattern = Instantiate(patterns[9], transform.position, Quaternion.identity);
        }

        if (upHit.collider != null && downHit.collider == null && rightHit.collider != null && leftHit.collider != null)
        {
            currentPattern = Instantiate(patterns[10], transform.position, Quaternion.identity);
        }

        if (upHit.collider == null && downHit.collider != null && rightHit.collider != null && leftHit.collider != null)
        {
            currentPattern = Instantiate(patterns[11], transform.position, Quaternion.identity);
        }

        //diagonal
        if (downHit.collider != null && leftHit.collider != null && rightHit.collider == null  && upHit.collider == null)
        {
            currentPattern = Instantiate(patterns[4], transform.position, Quaternion.identity);
        }

        if (downHit.collider != null && rightHit.collider != null && leftHit.collider == null && upHit.collider == null)
        {
            currentPattern = Instantiate(patterns[5], transform.position, Quaternion.identity);
        }

        if (upHit.collider != null && leftHit.collider != null && rightHit.collider == null && downHit.collider == null)
        {
            currentPattern = Instantiate(patterns[6], transform.position, Quaternion.identity);
        }

        if (upHit.collider != null && rightHit.collider != null && leftHit.collider == null && downHit.collider == null)
        {
            currentPattern = Instantiate(patterns[7], transform.position, Quaternion.identity);
        }

        //base patterns
        if (leftHit.collider != null && rightHit.collider == null && upHit.collider == null && downHit.collider == null)
        {
            currentPattern = Instantiate(patterns[1], transform.position, Quaternion.identity);
        }

        if (rightHit.collider != null && leftHit.collider == null && upHit.collider == null && downHit.collider == null)
        {
            currentPattern = Instantiate(patterns[0], transform.position, Quaternion.identity);
        }


        if (upHit.collider != null && rightHit.collider == null && leftHit.collider == null && downHit.collider == null)
        {
            currentPattern = Instantiate(patterns[2], transform.position, Quaternion.identity);
        }

        if (downHit.collider != null && rightHit.collider == null && leftHit.collider == null && upHit.collider == null)
        {
            currentPattern = Instantiate(patterns[3], transform.position, Quaternion.identity);
        }


        newParent = FindObjectOfType<RaftPlaces>().transform;
        currentPattern.transform.SetParent(newParent, true);
    }
}