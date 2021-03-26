using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomingPaningRotating : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float zoomOutMin = 1;
    [SerializeField] private float zoomOutMax = 8;
    private Vector3 prevPos;

    public Text logText;
    public Text diffText;

    [SerializeField] private float slowDownTime = 0.005f;

    Vector3 dir;

    // Update is called once per frame
    void Update()
    {
        Zoom(Input.GetAxis("Mouse ScrollWheel"));

        //Rotation();


        //Two fingers on screen
        if (Input.touchCount == 2)
        {
            logText.text = ("Two fingers on screen");
            Touch touchFirst = Input.GetTouch(0);
            Touch touchSecond = Input.GetTouch(1);

            Vector2 firstTouchPrevPos = touchFirst.position - touchFirst.deltaPosition;
            Vector2 secondTouchPrevPos = touchSecond.position - touchSecond.deltaPosition;

            Debug.Log("touchFirstPos " + touchFirst.position);
            Debug.Log("touchSecondPos " + touchSecond.position);



            float prevMagnitude = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            float currentMagnitude = (touchFirst.position - touchSecond.position).magnitude;


            Debug.Log("prevMagnitude " + prevMagnitude);
            Debug.Log("currentMagnitude " + currentMagnitude);


            float difference = currentMagnitude - prevMagnitude;
            Zoom(difference * slowDownTime);
            diffText.text = (difference.ToString());

           
        }
        else
        {
            Rotation();
        }



    }
    void Rotation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            prevPos = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            dir = prevPos - cam.ScreenToViewportPoint(Input.mousePosition);
            cam.transform.position = target.position;

            cam.transform.Rotate(new Vector3(1, 0, 0) * dir.y * 180);
            cam.transform.Rotate(new Vector3(0, 1, 0) * -dir.x * 180, Space.World);
            cam.transform.Translate(new Vector3(0, 0, -10));

            prevPos = cam.ScreenToViewportPoint(Input.mousePosition);

        }
      
       

    }
    void Zoom(float difference)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - difference, zoomOutMin, zoomOutMax);

    }
}
