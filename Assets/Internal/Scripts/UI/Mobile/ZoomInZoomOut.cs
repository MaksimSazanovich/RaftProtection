using UnityEngine;

//using UnityEngine.UI;

namespace Internal.Scripts.UI.Mobile
{
    public class ZoomInZoomOut : MonoBehaviour
    {

        private UnityEngine.Camera mainCamera;

        private float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

        private Vector2 firstTouchPrevPos, secondTouchPrevPos;

        [SerializeField] private 
            float zoomModifierSpeed = 0.1f;

        [SerializeField]
        //Text text;

        // Use this for initialization
        void Start()
        {
            mainCamera = GetComponent<UnityEngine.Camera>();
        }

        // Update is called once per frame
        void LateUpdate()
        {

            if (Input.touchCount == 2)
            {
                Touch firstTouch = Input.GetTouch(0);
                Touch secondTouch = Input.GetTouch(1);

                firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
                secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

                touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
                touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

                zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude * zoomModifierSpeed;

                if (touchesPrevPosDifference > touchesCurPosDifference)
                    mainCamera.orthographicSize += zoomModifier;
                if (touchesPrevPosDifference < touchesCurPosDifference)
                    mainCamera.orthographicSize -= zoomModifier;

            }

            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 2f, 10f);

            if (mainCamera.orthographicSize <= 5)
                mainCamera.orthographicSize = 5;
            if (mainCamera.orthographicSize >= 7)
                mainCamera.orthographicSize = 7;
            //text.text = "Camera size " + mainCamera.orthographicSize;

        }
    }
}