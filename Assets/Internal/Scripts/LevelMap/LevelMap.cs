using System.Diagnostics.CodeAnalysis;
using Internal.Scripts.LocalStorage;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Internal.Scripts.LevelMap
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public sealed class LevelMap : MonoBehaviour
    {
        private const string GameScene = "Game";

        [SerializeField] private Progress progress;
        private IStorageService storageService;

        [Inject]
        private void Construct(IStorageService storage) =>
            storageService = storage;

        private void Start() =>
            storageService.Load<Progress>(SaveKey.LevelIndex, Loaded);

        private void Loaded(Progress data) =>
            progress = data ?? new Progress(0);

        public void GetIndex(int index)
        {
            progress.index = index;

            // Нужно сохранять не значение типа int "progress.index", а объект целиком "progress".
            storageService.Save(SaveKey.LevelIndex, progress, LoadGameScene);
            
            // Local Storage - C:\Users\UsersName\AppData\LocalLow\Blebagames\RaftProtection\Database -> LevelIndex
        }

        private static void LoadGameScene(bool isSuccessSave) =>
            SceneManager.LoadSceneAsync(GameScene);
    }
}