using System;
using System.Collections;
using Internal.Scripts.WaveSpawner;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Scripts.UI
{
    public class WaveTimerButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        private NewWaveSpawner _waveSpawner;

        private float _totalTime;
        private float _currentTime;

        private Coroutine _timerCor;

        public event Action OnTimerStart;
        public event Action OnTimerEnd;
        public event Action<int> OnTimerPrematurelyEnd;

        [Inject]
        private void Construct(NewWaveSpawner newWaveSpawner)
        {
            _waveSpawner = newWaveSpawner;
        }

        private void OnEnable()
        {
            _waveSpawner.OnWaveEnd += StartTimer;
            _button.onClick.AddListener(FinishTimer);
        }

        private void OnDisable()
        {
            _waveSpawner.OnWaveEnd -= StartTimer;
            _button.onClick.RemoveListener(FinishTimer);
        }

        private void Start()
        {
            _button.gameObject.SetActive(false);
        }
        private void StartTimer()
        {
            if (_waveSpawner.CurrentWave != _waveSpawner.LevelMapConfig.LevelConfigs[_waveSpawner.CurrentLevel].Waves.Length - 1)
            {
                int _nextWave = _waveSpawner.CurrentWave + 1;
                _totalTime = _waveSpawner.LevelMapConfig.LevelConfigs[_waveSpawner.CurrentLevel].Waves[_nextWave].MiniWaves[0].SpawnDelay;
                _timerCor = StartCoroutine(Timer());
                _button.gameObject.SetActive(true);
            }       
        }

        private void FinishTimer()
        {
            StopCoroutine(_timerCor);
            OnTimerEnd?.Invoke();
            OnTimerPrematurelyEnd?.Invoke((int)(_totalTime - _currentTime)*100/100);
            Debug.Log((int)(_totalTime - _currentTime) * 100 / 100);
            _button.gameObject.SetActive(false);
        }

        IEnumerator Timer()
        {
            OnTimerStart?.Invoke();
            _currentTime = 0;
            _image.fillAmount = 0;
            Debug.Log("_totalTime: " + _totalTime);
            while (_currentTime < _totalTime)
            {
                _image.fillAmount = _currentTime / _totalTime;
                _currentTime += Time.deltaTime;
                yield return null;
            }
            _image.fillAmount = 1;
            _button.gameObject.SetActive(false);
        }
    }
}