using System;
using System.Collections.Generic;
using UnityEngine;

public class Raft : MonoBehaviour
{
	[SerializeField] private List<GameObject> raftPices = new List<GameObject>();
	public List<GameObject> RaftPices { get => raftPices; }

	public event Action OnAddRaftPiece;
    private void OnEnable()
	{
		PlaceToUpgradeRaft.OnInstantiatePiece += AddPiece;
	}

	private void OnDisable()
	{
        PlaceToUpgradeRaft.OnInstantiatePiece -= AddPiece;
    }

	private void AddPiece(GameObject piece)
	{ 
		raftPices.Add(piece);
		OnAddRaftPiece?.Invoke();
	}
}