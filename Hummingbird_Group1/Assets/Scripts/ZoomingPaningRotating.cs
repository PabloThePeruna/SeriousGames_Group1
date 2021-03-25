using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomingPaningRotating : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float zoomOutMin = 1;
    [SerializeField] private float zoomOutMax = 8;
    private Vector3 pervPos;

    [SerializeField] private float slowDownTime = 0.01f;

    Vector3 dir;

    // Update is called once per frame
    void Update()
    {
        
        Rotation();
        if (Input.touchCount == 2)
        {
            Touch touchFirst = Input.GetTouch(0);
            Touch touchSecond = Input.GetTouch(1);

            Vector2 firstTouchPrevPos = touchFirst.position - touchFirst.deltaPosition;
            Vector2 secondTouchPrevPos = touchSecond.position - touchSecond.deltaPosition;

            float prevMagnitude = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            float currentMagnitude = (firstTouchPrevPos - secondTouchPrevPos).magnitude;

            float difference = currentMagnitude - prevMagnitude;
            Zoom(difference * slowDownTime);

        }
    }
    void Rotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pervPos = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            dir = pervPos - cam.ScreenToViewportPoint(Input.mousePosition);
            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0) * dir.y * 180);
            cam.transform.Rotate(new Vector3(0, 1, 0) * -dir.x * 180, Space.World);
            cam.transform.Translate(new Vector3(0, 0, -10));

            pervPos = cam.ScreenToViewportPoint(Input.mousePosition);

        }
        if (Input.touchCount == 2)
        {
            Touch touchFirst = Input.GetTouch(0);
            Touch touchSecond = Input.GetTouch(1);

            Vector2 firstTouchPrevPos = touchFirst.position - touchFirst.deltaPosition;
            Vector2 secondTouchPrevPos = touchSecond.position - touchSecond.deltaPosition;

            float prevMagnitude = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            float currentMagnitude = (firstTouchPrevPos - secondTouchPrevPos).magnitude;

            float difference = currentMagnitude - prevMagnitude;


        }
       

    }
    void Zoom(float diffrence)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - diffrence, zoomOutMin, zoomOutMax);

    }
}
