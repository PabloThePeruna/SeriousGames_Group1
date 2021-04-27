using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomingPaningRotating2 : MonoBehaviour
{
    [SerializeField] private Camera cam2;
    [SerializeField] private Transform target2;
    [SerializeField] private float zoomOutMin2 = 0.05f; //max zoom in value
    [SerializeField] private float zoomOutMax2 = 2f; //Max zoom out value
    bool IsZooming = false;
    public bool CanDoubleCliclk = false;
    public bool IsTransparentBody = false;
    public bool IsOrganErlargened = false;
    private OrganSelect2 oS2;


    [SerializeField] private float slowDownTime = 0.005f;

    Vector3 dir;
    private Vector3 prevPos;


    private void Start()
    {
        //Camera view in split screen
        cam2.rect = new Rect(0.5f, 0, 0.25f, 1f);

        oS2 = FindObjectOfType<OrganSelect2>();

        //Set halo on around selected gameobject
        //oS.organsList[oS.hummingBirdOrganNumber].transform.GetChild(0).gameObject.SetActive(true);


    }




    // Update is called once per frame
    void Update()
    {
        //PLEASE DON'T REMOVE THIS LINE EASIER TO DEBUG ON PC

        Zoom2(Input.GetAxis("Mouse ScrollWheel"));

        ZoomCalculations2();

        HighDetail2();

        Body2();





    }
    void Rotation()
    {
        if (Input.GetMouseButtonDown(0) && cam2.pixelRect.Contains(Input.mousePosition))
        {

            prevPos = cam2.ScreenToViewportPoint(Input.mousePosition);

        }

        if (Input.GetMouseButton(0) && cam2.pixelRect.Contains(Input.mousePosition))
        {
            dir = prevPos - cam2.ScreenToViewportPoint(Input.mousePosition); //calculate the difference between the old and new finger position
            cam2.transform.position = target2.position; // make sure that camera rotates around the targets origin

            cam2.transform.Rotate(new Vector3(1, 0, 0) * dir.y * 180); //calculate the amount of rotation applied
            cam2.transform.Rotate(new Vector3(0, 1, 0) * -dir.x * 180, Space.World);
            cam2.transform.Translate(new Vector3(0, 0, -10)); //offset camera

            prevPos = cam2.ScreenToViewportPoint(Input.mousePosition); // assign the new position calculated to be the old position

        }

    }
    void Zoom2(float difference)
    {
        cam2.orthographicSize = Mathf.Clamp(cam2.orthographicSize - difference, zoomOutMin2, zoomOutMax2);

    }

    void ZoomCalculations2()
    {
        if (Input.touchCount == 2 && cam2.pixelRect.Contains(Input.mousePosition)) //Check if two fingers are on screen
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
            Zoom2(difference * slowDownTime);

        }

        //if we have only one finger curently active we can sart to rotate
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && cam2.pixelRect.Contains(Input.mousePosition))
        {
            IsZooming = false;

        }
        if (IsZooming == false)
        {
            Rotation();
        }
    }

    void Body2()
    {
        //Change the body to transparent and highlight specific organ
        if (Input.touchCount == 1 && Input.GetTouch(0).tapCount == 2 && cam2.pixelRect.Contains(Input.mousePosition))
        {
            Ray ray = cam2.ScreenPointToRay(Input.touches[0].position);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.CompareTag("male"))
                {

                    oS2.organsList2[oS2.hummingBirdOrganNumber2].tag = "selected";

                    foreach (GameObject organs in oS2.organsList2)
                    {
                        if (organs.tag == "unselected")
                        {
                            organs.GetComponent<Renderer>().enabled = false;

                            organs.SetActive(false);
                            IsOrganErlargened = true;
                            CanDoubleCliclk = true;
                        }
                    }

                    target2 = oS2.organsList2[oS2.hummingBirdOrganNumber2].transform;
                    cam2.orthographicSize = 0.2f;
                }


            }


        }
    }
    public void BackToFullBody2()
    {
        //Get back to full body-perspective


        oS2.organSelector2 = oS2.hummingBirdOrganNumber2;

        foreach (GameObject organs in oS2.organsList2)
        {
            if (organs.tag == "unselected")
            {
                organs.GetComponent<Renderer>().enabled = true;

                oS2.outlineSize2 = 1.15f;

                organs.SetActive(true);
                IsOrganErlargened = false;
                CanDoubleCliclk = false;
                oS2.organsList2[oS2.hummingBirdOrganNumber2].SetActive(true);


            }
        }

        //Set camera back to original position after return to full body view
        target2 = GameObject.Find("male_body").transform;
        cam2.orthographicSize = 1;

        dir = prevPos - cam2.ScreenToViewportPoint(Input.mousePosition); //calculate the difference between the old and new finger position
        cam2.transform.position = target2.position; // make sure that camera rotates around the targets origin

        cam2.transform.Rotate(new Vector3(1, 0, 0) * dir.y * 180); //calculate the amount of rotation applied
        cam2.transform.Rotate(new Vector3(0, 1, 0) * -dir.x * 180, Space.World);
        cam2.transform.Translate(new Vector3(0, 0, -10)); //offset camera

    }

    void HighDetail2()
    {

        if (IsOrganErlargened == true && CanDoubleCliclk == true && oS2.organSelector2 == 2)
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).tapCount == 2)
            {
                //oS2.organsList2[oS2.hummingBirdOrganNumber2].SetActive(false);
                Debug.Log("Show heart interiors");

            }
        }


    }


}

