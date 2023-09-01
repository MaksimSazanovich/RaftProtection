using Internal.Scripts.Raft;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Scripts.UI
{
	public class RaftHeathBar : MonoBehaviour
	{
		[SerializeField] private Image _healhBar;
		private RaftHealth _raftHealth;

		[Inject]
		private void Construct(RaftHealth raftHealth)
		{
			_raftHealth = raftHealth;
		}
		private void Start()
		{
			_healhBar.fillAmount = _raftHealth.Maxhealth;

		}

		private void Update()
		{
			_healhBar.fillAmount = _raftHealth.Health / _raftHealth.Maxhealth;
		}
	}
}