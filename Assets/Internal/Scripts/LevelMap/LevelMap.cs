using AbyssMoth.Codebase.Infrastructure.Services.Storage;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelMap : MonoBehaviour
{
	[SerializeField] private Progress progress;

	private IStorageService storageService = new JsonToFileStorageService();

    [Inject]
	private void Construct(IStorageService storageService)
	{ 
		this.storageService = storageService;
	}

	private void Awake()
	{
        print(storageService == null);
    }

	private void Start()
	{       
        storageService.Load<Progress>(SaveKey.LevelIndex, Loaded);
    }

	private void Loaded(Progress progress)
	{
        this.progress = progress ?? new Progress(0);
    }

	public void GetIndex(int index)
	{
        if (progress == null) { progress = new Progress(index); } else { progress.index = index; }
        storageService.Save(SaveKey.LevelIndex, progress.index);
		SceneManager.LoadScene("Game");
	}
}