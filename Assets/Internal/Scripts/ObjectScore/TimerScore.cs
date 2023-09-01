using Internal.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.ObjectScore
{
    public class TimerScore : ObjectScore
    {
        private WaveTimerButton _waveTimerButton;

        [SerializeField] private int coef;

        [Inject]
        private void Construct(WaveTimerButton waveTimerButton)
        { 
            _waveTimerButton = waveTimerButton;
        }

        private void OnEnable()
        {
            _waveTimerButton.OnTimerPrematurelyEnd += SetScore;
        }

        private void OnDisable()
        {
            _waveTimerButton.OnTimerPrematurelyEnd -= SetScore;
        }

        public override void SetScore(int score)
        {
            this.score = score * coef;
            Activate();
        }
    }
}