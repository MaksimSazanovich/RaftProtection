using Internal.Scripts.WaveSpawner;
using UnityEngine;
using Zenject;

public class GamePanel : MonoBehaviour
{
	[SerializeField] private GameObject LosePanel;
	[SerializeField] private GameObject winPanel;

	private NewWaveSpawner newWaveSpawner;

    [Inject]
	private void Construct(NewWaveSpawner newWaveSpawner)
	{
		this.newWaveSpawner = newWaveSpawner;
	}

	private void OnEnable()
	{
		newWaveSpawner.OnWin += ShowWinPanel;
    }

	private void OnDisable()
	{
        newWaveSpawner.OnWin -= ShowWinPanel;
    }

	private void ShowWinPanel()
	{
		gameObject.SetActive(false);
		winPanel.SetActive(true);
		Debug.Log("ShowWinPanel");
	}
}