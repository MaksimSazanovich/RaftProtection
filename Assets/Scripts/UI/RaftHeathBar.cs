using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RaftHeathBar : MonoBehaviour
{
	private Image _healhBar;
	private RaftHealth _raftHealth;

	[Inject]
	private void Construct(RaftHealth raftHealth)
	{
		_raftHealth = raftHealth;
	}
	private void Start()
	{
		_healhBar = GetComponent<Image>();
		_healhBar.fillAmount = _raftHealth.Maxhealth;

    }

	private void Update()
	{
		_healhBar.fillAmount = _raftHealth.Health / _raftHealth.Maxhealth;
	}
}