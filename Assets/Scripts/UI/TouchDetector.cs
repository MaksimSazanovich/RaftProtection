using System;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    private Vector3 targetPosition;
    private Camera cam;

    public event Action<Vector3> OnTouch;

    private void Start()
    {
        cam = Camera.main;
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
        
        OnTouch(targetPosition);
    }
}
