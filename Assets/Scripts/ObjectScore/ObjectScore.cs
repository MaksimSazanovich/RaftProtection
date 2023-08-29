using System;
using UnityEngine;

public abstract class ObjectScore : MonoBehaviour
{
    public static event Action<int> OnChanged;

    [SerializeField] protected int score;

    public virtual void Activate()
    {
        OnChanged?.Invoke(score);
    }

    public virtual void SetScore(int score)
    {
        this.score = score;
        Activate();
    }    
}
