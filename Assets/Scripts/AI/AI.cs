using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class AI : MonoBehaviour
{
    [SerializeField] protected Vector3 targetPosition;
    [SerializeField] protected float speed;
    public float Speed { get => speed; set => speed = value; }
    protected virtual void Start()
	{
        targetPosition = FindObjectOfType<Raft>().transform.position;
    }
}