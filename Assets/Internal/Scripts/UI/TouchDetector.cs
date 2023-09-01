using System;
using UnityEngine;

namespace Internal.Scripts.UI
{
    public class TouchDetector : MonoBehaviour
    {
        private Vector3 targetPosition;
        private UnityEngine.Camera cam;

        public event Action<Vector3> OnTouch;

        private void Start()
        {
            cam = UnityEngine.Camera.main;
        }
        private void Update()
        {

            if (Input.GetMouseButton(0))
            {
                SetTargetPosition();
            }
        }

        private void SetTargetPosition()
        {
            targetPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;
        
            OnTouch?.Invoke(targetPosition);
        }
    }
}
