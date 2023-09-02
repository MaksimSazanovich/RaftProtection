using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Internal.Scripts.UI
{
    public class GunButton : MonoBehaviour, IPointerDownHandler
    {
        public UnityEvent OnClick;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnClick.Invoke();
        }

        //private void OnMouseDown()
        //{
        //    TouchPosition = camera.ScreenToWorldPoint(touch.position);
        //    touchPosition.z = 0f;
        //    FindObjectOfType<WeaponBase>().Rotate();
        //}


    }
}