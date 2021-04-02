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

    //Materials
    public Material transpMaterial;
    public Material normalSkinMaterial;


    //Shaders
    public Shader showAlwaysShader;
    public Shader standardShader;

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
            //When organ is enlargened
            //Set standard shader of the game object
            organsList[hummingBirdOrganNumber].GetComponent<Renderer>().material.shader = standardShader;


        }
        else if (zPR.IsOrganErlargened != true)
        {
            //In full body view show set see through -shader
            organsList[hummingBirdOrganNumber].GetComponent<Renderer>().material.shader = showAlwaysShader;

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
