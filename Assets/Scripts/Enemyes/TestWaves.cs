using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWaves : MonoBehaviour
{
    private WaveSpawner waveSpawner;

	private void Awake()
	{
		waveSpawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
	}
	private  void OnDestroy()
	{
		int enemiesLeft = 0;
		enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
		if (enemiesLeft == 0)
		{
			waveSpawner.LaunchWave();
		}
	}
}