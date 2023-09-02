using UnityEngine;
using UnityEngine.Events;

namespace Internal.Scripts.UI
{
    public class RaftButton : MonoBehaviour
    {
        private bool isActive = false;
        [SerializeField] private UnityEvent Activate;
        [SerializeField] private UnityEvent Deactivate;

        public void ChangeState()
        {
            if (isActive)
            {
                Deactivate.Invoke();
                isActive = false;
            }
            else
            {
                Activate.Invoke();
                isActive = true;
            }
        }
    }
}