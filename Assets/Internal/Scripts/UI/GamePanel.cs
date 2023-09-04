using Internal.Scripts.Raft;
using Internal.Scripts.WaveSpawner;
using UnityEngine;
using Zenject;

public class GamePanel : MonoBehaviour
{
	[SerializeField] private GameObject LosePanel;
	[SerializeField] private GameObject winPanel;

	private NewWaveSpawner newWaveSpawner;
	private RaftHealth raftHealth;

    [Inject]
	private void Construct(NewWaveSpawner newWaveSpawner, RaftHealth raftHealth)
	{
		this.newWaveSpawner = newWaveSpawner;
		this.raftHealth = raftHealth;
	}

	private void OnEnable()
	{
		newWaveSpawner.OnWin += ShowWinPanel;
		raftHealth.OnDie += ShowlosePanel;
	}

	private void OnDisable()
	{
        newWaveSpawner.OnWin -= ShowWinPanel;
        raftHealth.OnDie += ShowlosePanel;
	}

	private void ShowWinPanel()
	{
		gameObject.SetActive(false);
		winPanel.SetActive(true);
		Debug.Log("ShowWinPanel");
	}

	private void ShowlosePanel()
	{
		gameObject.SetActive(false);
		LosePanel.SetActive(true);
	}
}