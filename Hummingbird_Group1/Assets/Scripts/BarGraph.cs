using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using CodeMonkey.Utils;
using System;
using TMPro;

public class BarGraph : MonoBehaviour
{
    private RectTransform graphContaner;
    [SerializeField] private Sprite dotSprite;
    public Player player;
    private RectTransform labelX;
    private RectTransform labelY;
    private RectTransform gridLabelY;
    public RectTransform avgLine;

    public float sum;
    public float average;
    public float yMax;
    public float graphHeight;


    private void Awake()
    {
        graphContaner = transform.Find("GraphContaner").GetComponent<RectTransform>();
        labelX = graphContaner.Find("LabelX").GetComponent<RectTransform>();
        labelY = graphContaner.Find("LabelY").GetComponent<RectTransform>();
     
        gridLabelY = graphContaner.Find("GridY").GetComponent<RectTransform>();


        List<int> values = new List<int>() { 7, 76, 9, 50, 10, 70, 65, 76, 42, 90,54,15,76,24,23, 48, 22, 78, 90, 87 };

        List<int> avg = new List<int>();
       

        graphHeight = graphContaner.sizeDelta.y;
        avgLine.anchorMin = new Vector2(0, 0);
       


        yMax = 110f;

        for (int i = 0; i < values.Count; i++)
        {
            sum = sum + values[i];
        }

        average = sum / values.Count;
        float averageLinePosY = (average / yMax) * graphHeight;

        Debug.Log("Sum " + sum);
        Debug.Log("Average " + average);
        Debug.Log("yMax " + yMax);
        Debug.Log("Graph height " + graphHeight);
        Debug.Log("AverageLineLinePosY " + averageLinePosY);


       
        avgLine.transform.localPosition = new Vector2(0, averageLinePosY -(graphContaner.sizeDelta.y/2));
       


        ShowGraph(values,(int _i)=> ""+(_i+1),(float _f) => Mathf.RoundToInt(_f)+ "s" );


    }

   

    private void ShowGraph( List<int> values, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
    {
        if(getAxisLabelX== null)
        {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }
       
        float graphWidth = graphContaner.sizeDelta.x;
        float xSize = graphWidth/ values.Count;
       
        

        for (int i = 0; i < values.Count; i++)
        {
          
            float xPos = .5f *xSize+ i * xSize;
            float yPos = (values[i] / yMax) * graphHeight;
            float avgXPos = xSize + i * xSize;
           
            CreateBar(new Vector2(xPos, yPos), xSize*.9f);
            
            Debug.Log("yPos " + yPos);

            RectTransform labelXDublicated = Instantiate(labelX);
            labelXDublicated.SetParent(graphContaner,false);
            labelXDublicated.gameObject.SetActive(true);
            labelXDublicated.anchoredPosition = new Vector2(xPos, -labelXDublicated.sizeDelta.y*0.5f);
            labelXDublicated.GetComponent<TextMeshProUGUI>().text = getAxisLabelX(i);

          
            

        }
       
        int separatorCount = 10;
        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelYDublicated = Instantiate(labelY);
            labelYDublicated.SetParent(graphContaner, false);
            labelYDublicated.gameObject.SetActive(true);
            float normalizedY = i * 1f / separatorCount;
            labelYDublicated.anchoredPosition = new Vector2(labelYDublicated.sizeDelta.x*-0.5f, normalizedY * graphHeight);
            labelYDublicated.GetComponent<TextMeshProUGUI>().text = getAxisLabelY(normalizedY * yMax);


            RectTransform gridY = Instantiate(gridLabelY);
            gridY.SetParent(graphContaner, false);
            gridY.gameObject.SetActive(true);
            gridY.anchoredPosition = new Vector2(graphWidth*.5f, normalizedY * graphHeight);
        }
    }
  
    private GameObject CreateBar(Vector2 graphPosition, float barWidth)
    {
        GameObject gameObject = new GameObject("bar", typeof(Image));
        gameObject.transform.SetParent(graphContaner, false);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
        rectTransform.sizeDelta = new Vector2(barWidth,graphPosition.y);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.pivot = new Vector2(.5f, 0f);

        return gameObject;
    }

   

}
