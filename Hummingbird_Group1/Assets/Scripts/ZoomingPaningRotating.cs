using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomingPaningRotating : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private float zoomOutMin = 0.05f; //max zoom in value
    [SerializeField] private float zoomOutMax = 2f; //Max zoom out value
    bool IsZooming = false;
    public bool CanDoubleCliclk = false;
    public bool IsTransparentBody = false;
    public bool IsOrganErlargened = false;
    private OrganSelect oS;


    [SerializeField] private float slowDownTime = 0.005f;

    Vector3 dir;
    private Vector3 prevPos;


    private void Start()
    {
        //Camera view in split screen

        oS = FindObjectOfType<OrganSelect>();


        //Set halo on around selected gameobject
        //oS.organsList[oS.hummingBirdOrganNumber].transform.GetChild(0).gameObject.SetActive(true);


    }




    // Update is called once per frame
    void Update()
    {
        //PLEASE DON'T REMOVE THIS LINE EASIER TO DEBUG ON PC

        Zoom(Input.GetAxis("Mouse ScrollWheel"));

        ZoomCalculations();

        HighDetail();

        Body();

       

       

    }
    void Rotation()
    {
        if (Input.GetMouseButtonDown(0) && cam.pixelRect.Contains(Input.mousePosition))
        {

            prevPos = cam.ScreenToViewportPoint(Input.mousePosition);

        }

        if (Input.GetMouseButton(0) && cam.pixelRect.Contains(Input.mousePosition))
        {
            dir = prevPos - cam.ScreenToViewportPoint(Input.mousePosition); //calculate the difference between the old and new finger position
            cam.transform.position = target.position; // make sure that camera rotates around the targets origin

            cam.transform.Rotate(new Vector3(1, 0, 0) * dir.y * 180); //calculate the amount of rotation applied
            cam.transform.Rotate(new Vector3(0, 1, 0) * -dir.x * 180, Space.World);
            cam.transform.Translate(new Vector3(0, 0, -10)); //offset camera

            prevPos = cam.ScreenToViewportPoint(Input.mousePosition); // assign the new position calculated to be the old position

        }

    }
    void Zoom(float difference)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - difference, zoomOutMin, zoomOutMax);

    }

    void ZoomCalculations()
    {
        if (Input.touchCount == 2 && cam.pixelRect.Contains(Input.mousePosition)) //Check if two fingers are on screen
        {
            IsZooming = true;
            Touch touchFirst = Input.GetTouch(0); //store touch
            Touch touchSecond = Input.GetTouch(1);

            Vector2 firstTouchPrevPos = touchFirst.position - touchFirst.deltaPosition; // Calculate the fingers position on screen
            Vector2 secondTouchPrevPos = touchSecond.position - touchSecond.deltaPosition;

            Debug.Log("touchFirstPos " + touchFirst.position);
            Debug.Log("touchSecondPos " + touchSecond.position);

            float prevMagnitude = (firstTouchPrevPos - secondTouchPrevPos).magnitude; //calculate to difference beetween previous positions
            float currentMagnitude = (touchFirst.position - touchSecond.position).magnitude; //Calculate the difference between the current positions

            Debug.Log("prevMagnitude " + prevMagnitude);
            Debug.Log("currentMagnitude " + currentMagnitude);

            float difference = currentMagnitude - prevMagnitude; // Calculate the difference and Zoom by that amount
            Zoom(difference * slowDownTime);

        }

        //if we have only one finger curently active we can sart to rotate
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && cam.pixelRect.Contains(Input.mousePosition))
        {
            IsZooming = false;

        }
        if (IsZooming == false)
        {
            Rotation();
        }
    }

    void Body()
    {
        //Change the body to transparent and highlight specific organ
        if (Input.touchCount==1 && Input.GetTouch(0).tapCount== 2 && cam.pixelRect.Contains(Input.mousePosition))
        {
            Ray ray = cam.ScreenPointToRay(Input.touches[0].position);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                
                if (hit.collider.CompareTag("male"))
                {

                    oS.organsList[oS.hummingBirdOrganNumber].tag = "selected";

                    foreach (GameObject organs in oS.organsList)
                    {
                        if (organs.tag == "unselected")
                        {
                            organs.GetComponent<Renderer>().enabled = false;

                            organs.SetActive(false);
                            IsOrganErlargened = true;
                            CanDoubleCliclk = true;
                        }
                    }

                    target = oS.organsList[oS.hummingBirdOrganNumber].transform;
                    cam.orthographicSize = 0.2f;
                }

                
            }
           
            
        }
    }
     public void BackToFullBody()
     {
        //Get back to full body-perspective


        oS.organSelector = oS.hummingBirdOrganNumber;

        foreach (GameObject organs in oS.organsList)
        {
            if (organs.tag == "unselected")
            {
                organs.GetComponent<Renderer>().enabled = true;

                oS.outlineSize = 1.15f;

                organs.SetActive(true);
                IsOrganErlargened = false;
                CanDoubleCliclk = false;
                oS.organsList[oS.hummingBirdOrganNumber].SetActive(true);


            }
        }

        //Set camera back to original position after return to full body view
        target = GameObject.Find("male_body").transform;
        cam.orthographicSize = 1;

        dir = prevPos - cam.ScreenToViewportPoint(Input.mousePosition); //calculate the difference between the old and new finger position
        cam.transform.position = target.position; // make sure that camera rotates around the targets origin

        cam.transform.Rotate(new Vector3(1, 0, 0) * dir.y * 180); //calculate the amount of rotation applied
        cam.transform.Rotate(new Vector3(0, 1, 0) * -dir.x * 180, Space.World);
        cam.transform.Translate(new Vector3(0, 0, -10)); //offset camera

    }

    void HighDetail()
    {
        
        if (IsOrganErlargened == true && CanDoubleCliclk== true)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase== TouchPhase.Began && Input.GetTouch(0).tapCount == 2)
            {
                oS.organsList[oS.hummingBirdOrganNumber].SetActive(false);

            }
        }
        
       
    }


}
      
