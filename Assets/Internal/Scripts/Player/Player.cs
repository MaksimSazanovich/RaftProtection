using Internal.Scripts.UI;
using UnityEngine;

namespace Internal.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GunButton touchDetector;
        private bool isFasingRight;
        private void Start()
        {
            isFasingRight = true;
        }

        public void Rotate(Vector3 targetposition)
        {
            if (targetposition.x < 0f && isFasingRight)
            {
                transform.Rotate(0f, -180f, 0f);
                isFasingRight = false;
            }
            else if (targetposition.x > 0f && isFasingRight == false)
            {
                transform.Rotate(0f, 180f, 0f);
                isFasingRight = true;
            }
        }

    }
}