using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganSelect : MonoBehaviour
{
    public List<GameObject> organsList = new List<GameObject>();

    private GameObject selectedOrgan;

    //Numbers
    [Range(1, 4)]
    public int organSelector;

    public int hummingBirdOrganNumber;

    private float maxOutline = 1.3f;
    private float minOutline = 1.15f;
    public float outlineSize;
    private float waitTime = 0.05f;

    //Materials
    public Material transpMaterial;
    public Material normalSkinMaterial;
    private Material ownMaterial;

    //Shaders
    public Shader showAlwaysShader;
    public Shader standardShader;

    private ZoomingPaningRotating zPR;

    //-

    // Start is called before the first frame update
    void Start()
    {
        

        zPR = FindObjectOfType<ZoomingPaningRotating>();

        StartCoroutine(OutlineSize());

        organSelector = hummingBirdOrganNumber;

        ownMaterial = organsList[hummingBirdOrganNumber].GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {



        //Debug.Log("Outlinesize = " + outlineSize);
        //When other organ than skin is selected, make body transparent

        //scrollOrgans(organSelector); //Also the function is commented. Is this necessary, works without this


        //Show different material in enlargened organ
        if (zPR.IsOrganErlargened == true)
        {
            //When organ is enlargened
            //Set standard shader of the game object
            organsList[hummingBirdOrganNumber].GetComponent<Renderer>().material.shader = standardShader;
            outlineSize = 1.15f;

        }
        else if (zPR.IsOrganErlargened != true)
        {
            //In full body view show set see through with outlines -shader
            outlineSize = organsList[hummingBirdOrganNumber].GetComponent<Renderer>().sharedMaterial.GetFloat("_OutlineWidth");

            organsList[hummingBirdOrganNumber].GetComponent<Renderer>().material.shader = showAlwaysShader;

        }

    }

    IEnumerator OutlineSize()
    {
        float timer = 0;

        while (true) // this could also be a condition indicating "alive or dead"
        {
            // we scale all axis, so they will have the same value, 
            // so we can work with a float instead of comparing vectors
            while (outlineSize < maxOutline)
            {
                timer += Time.deltaTime;
                organsList[hummingBirdOrganNumber].GetComponent<Renderer>().sharedMaterial.SetFloat("_OutlineWidth", outlineSize + Time.deltaTime / 8);
                yield return null;
            }
            // reset the timer

            yield return new WaitForSeconds(waitTime);

            timer = 0;
            while (outlineSize > minOutline)
            {
                timer += Time.deltaTime;
                organsList[hummingBirdOrganNumber].GetComponent<Renderer>().sharedMaterial.SetFloat("_OutlineWidth", outlineSize - Time.deltaTime / 8);
                yield return null;
            }

            timer = 0;
            yield return new WaitForSeconds(waitTime);
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
