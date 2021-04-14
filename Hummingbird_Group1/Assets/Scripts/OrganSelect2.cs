using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganSelect2 : MonoBehaviour
{
    public List<GameObject> organsList2 = new List<GameObject>();

    private GameObject selectedOrgan2;

    //Numbers
    [Range(1, 4)]
    public int organSelector2;

    public int hummingBirdOrganNumber2;

    private float maxOutline2 = 1.3f;
    private float minOutline2 = 1.15f;
    public float outlineSize2;
    private float waitTime = 0.05f;

    //Materials
    public Material transpMaterial;
    public Material normalSkinMaterial;

    //Shaders
    public Shader showAlwaysShader;
    public Shader standardShader;

    private ZoomingPaningRotating2 zPR2;

    //-

    // Start is called before the first frame update
    void Start()
    {
        zPR2 = FindObjectOfType<ZoomingPaningRotating2>();

        StartCoroutine(OutlineSize2());

        organSelector2 = hummingBirdOrganNumber2;
    }

    // Update is called once per frame
    void Update()
    {


        outlineSize2 = organsList2[hummingBirdOrganNumber2].GetComponent<Renderer>().sharedMaterial.GetFloat("_OutlineWidth");

        //Debug.Log("Outlinesize = " + outlineSize);
        //When other organ than skin is selected, make body transparent

        //scrollOrgans(organSelector); //Also the function is commented. Is this necessary, works without this


        //Show different material in enlargened organ
        if (zPR2.IsOrganErlargened == true)
        {
            //When organ is enlargened
            //Set standard shader of the game object
            organsList2[hummingBirdOrganNumber2].GetComponent<Renderer>().material.shader = standardShader;
            outlineSize2 = 1.15f;

        }
        else if (zPR2.IsOrganErlargened != true)
        {
            //In full body view show set see through with outlines -shader
            organsList2[hummingBirdOrganNumber2].GetComponent<Renderer>().material.shader = showAlwaysShader;

        }

    }

    IEnumerator OutlineSize2()
    {
        float timer = 0;

        while (true) // this could also be a condition indicating "alive or dead"
        {
            // we scale all axis, so they will have the same value, 
            // so we can work with a float instead of comparing vectors
            while (outlineSize2 < maxOutline2)
            {
                timer += Time.deltaTime;
                organsList2[hummingBirdOrganNumber2].GetComponent<Renderer>().sharedMaterial.SetFloat("_OutlineWidth", outlineSize2 + Time.deltaTime / 8);
                yield return null;
            }
            // reset the timer

            yield return new WaitForSeconds(waitTime);

            timer = 0;
            while (outlineSize2 > minOutline2)
            {
                timer += Time.deltaTime;
                organsList2[hummingBirdOrganNumber2].GetComponent<Renderer>().sharedMaterial.SetFloat("_OutlineWidth", outlineSize2 - Time.deltaTime / 8);
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
