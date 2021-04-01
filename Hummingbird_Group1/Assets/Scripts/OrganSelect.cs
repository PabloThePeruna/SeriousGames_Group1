using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganSelect : MonoBehaviour
{
    public List<GameObject> organsList = new List<GameObject>();

    private GameObject selectedOrgan;



    [Range(1, 4)]
    public int organSelector;

    public int hummingBirdOrganNumber;


    //public int maleOrganSelector = Mathf.Clamp(0, 0, 4);

    public Material transpMaterial;
    public Material normalSkinMaterial;
    public Material alwaysVisible;

    private ZoomingPaningRotating zPR;


    // Start is called before the first frame update
    void Start()
    {
        zPR = FindObjectOfType<ZoomingPaningRotating>();

        organSelector = hummingBirdOrganNumber;

    }

    // Update is called once per frame
    void Update()
    {

        //When other organ than skin is selected, make body transparent

        //scrollOrgans(organSelector); //Also the function is commented. Is this necessary, works without this


        //Show different material in enlargened organ
        if (zPR.IsOrganErlargened == true)
        {
            organsList[hummingBirdOrganNumber].GetComponent<MeshRenderer>().material = normalSkinMaterial;

        }
        else if (zPR.IsOrganErlargened != true)
        {
            //Otherwise show alwaysVisible
            organsList[hummingBirdOrganNumber].GetComponent<MeshRenderer>().material = alwaysVisible;

        }

    }

 
       
    //Works fine without this, is it still necessary?
    /*
    void scrollOrgans(int organNumber)
    {
        organSelector = organNumber;
        Debug.Log(organsList[organNumber]);
    }
    */
}
