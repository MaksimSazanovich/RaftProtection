using UnityEngine;
using UnityEngine.Events;

public class ScoreCollector : MonoBehaviour
{
	[SerializeField] private UnityEvent<int> ScoreChanged;
	public static int scoreCollected;

	private void Awake()
	{
		scoreCollected = 0;
        ScoreChanged.Invoke(scoreCollected);
    }
	private void OnEnable()
	{
		EnemyScore.OnChanged += ObjectScore_OnChanged;
		PlaceToUpgradeRaft.OnBuyPiece += ObjectScore_OnChanged;
		PlaceToCreateStationaryWeapon.OnBuyPiece += ObjectScore_OnChanged;
	}
	private void OnDisable()
	{
		EnemyScore.OnChanged -= ObjectScore_OnChanged;
        PlaceToUpgradeRaft.OnBuyPiece -= ObjectScore_OnChanged;
		PlaceToCreateStationaryWeapon.OnBuyPiece -= ObjectScore_OnChanged;
    }

	private void ObjectScore_OnChanged(int value)
	{
		scoreCollected += value;
		ScoreChanged.Invoke(scoreCollected);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			scoreCollected += 1000;
            ScoreChanged.Invoke(scoreCollected);
        }
	}
}