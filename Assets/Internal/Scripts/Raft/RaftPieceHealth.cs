using Internal.Scripts.Enemyes;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Raft
{
	public class RaftPieceHealth : MonoBehaviour, IDamageable
	{
		private RaftHealth raftHealth;
		[Inject]
		private void Construct(RaftHealth raftHealth)
		{ 
			this.raftHealth = raftHealth;
		}
		public void ApplyDamage(int damageValue)
		{
			Debug.Log("damageValue " + damageValue);
			raftHealth.ApplyDamage(damageValue);
		}

		public void Die()
		{
		
		}

	}
}