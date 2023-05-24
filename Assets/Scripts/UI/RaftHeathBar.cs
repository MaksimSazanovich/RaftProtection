using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaftHeathBar : MonoBehaviour
{
	private Image healhBar;
	[SerializeField] private Raft raft;
	private void Start()
	{
		healhBar = GetComponent<Image>();
		healhBar.fillAmount = raft.Maxhealth;

    }

	private void Update()
	{
		healhBar.fillAmount = raft.Health / raft.Maxhealth;
	}
}