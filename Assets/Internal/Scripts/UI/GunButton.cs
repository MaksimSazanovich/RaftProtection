using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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