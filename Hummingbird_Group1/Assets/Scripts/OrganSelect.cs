using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganSelect : MonoBehaviour
{
    public List<GameObject> maleOrgans = new List<GameObject>();

    private GameObject selectedOrgan;
    public GameObject groins;

    [Range(0, 4)]
    public int maleOrganSelector;

    //public int maleOrganSelector = Mathf.Clamp(0, 0, 4);

    public Material transpMaterial;
    public Material normalSkinMaterial;
    public Material alwaysVisible;

    private ZoomingPaningRotating zPR;


    // Start is called before the first frame update
    void Start()
    {
        zPR = FindObjectOfType<ZoomingPaningRotating>();


        //maleOrganSelector = Mathf.Clamp(0, 0, 4);

    }

    // Update is called once per frame
    void Update()
    {

        //When other organ than skin is selected, make body transparent
        scrollOrgans(maleOrganSelector);

        //When other organ than skin is selected, make body transparent
        if (maleOrganSelector > 0)
        {
            maleOrgans[0].GetComponent<MeshRenderer>().material = transpMaterial;
            groins.SetActive(false);
        }
        else
        {
            maleOrgans[0].GetComponent<MeshRenderer>().material = normalSkinMaterial;
            groins.SetActive(true);
        }
        //If brains is selected
        if (maleOrganSelector == 1)
        {
            maleOrgans[1].GetComponent<MeshRenderer>().material = alwaysVisible;
        }
        else
        {
            maleOrgans[1].GetComponent<MeshRenderer>().material = normalSkinMaterial;
        }

        //If heart is selected
        if (maleOrganSelector == 2)
        {
            maleOrgans[2].GetComponent<MeshRenderer>().material = alwaysVisible;
        }
        else
        {
            maleOrgans[2].GetComponent<MeshRenderer>().material = normalSkinMaterial;
        }

        //If liver is selected
        if (maleOrganSelector == 3)
        {
            maleOrgans[3].GetComponent<MeshRenderer>().material = alwaysVisible;
        }
        else
        {
            maleOrgans[3].GetComponent<MeshRenderer>().material = normalSkinMaterial;
        }

        //If lungs is selected
        if (maleOrganSelector == 4)
        {
            maleOrgans[4].GetComponent<MeshRenderer>().material = alwaysVisible;
        }
        else
        {
            maleOrgans[4].GetComponent<MeshRenderer>().material = normalSkinMaterial;
        }

        if (zPR.IsOrganErlargened == true)
        {
            maleOrgans[1].GetComponent<MeshRenderer>().material = normalSkinMaterial;

        }

    }

 
       
    

    void scrollOrgans(int organNumber)
    {
        maleOrganSelector = organNumber;
        Debug.Log(maleOrgans[organNumber]);
    }

}
