using TMPro;
using UnityEngine;

namespace Internal.Scripts.UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        public void ShowValue(int value)
        { 
            scoreText.text = value.ToString();
        }
    }
}