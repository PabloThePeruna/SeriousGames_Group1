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
    

    private void Awake()
    {
        graphContaner = transform.Find("GraphContaner").GetComponent<RectTransform>();
        labelX = graphContaner.Find("LabelX").GetComponent<RectTransform>();
        labelY = graphContaner.Find("LabelY").GetComponent<RectTransform>();
     
        gridLabelY = graphContaner.Find("GridY").GetComponent<RectTransform>();


        List<int> values = new List<int>() { 7, 76, 9, 50, 10, 70, 65, 76, 42, 90,54,15,76,24,23 };

        List<int> avg = new List<int>();
        avg.Add(values[0]);
       

        ShowGraph(values,(int _i)=> ""+(_i+1),(float _f) => Mathf.RoundToInt(_f)+ "s" );


    }

   

    private void ShowGraph( List<int> values, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
    {
        if(getAxisLabelX== null)
        {
            getAxisLabelX = delegate (int _i) { return _i.ToString(); };
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }
        float graphHeight = graphContaner.sizeDelta.y;
        float yMax = 110f;
        float graphWidth = graphContaner.sizeDelta.x;
        float xSize = graphWidth/ values.Count;
       
        

        for (int i = 0; i < values.Count; i++)
        {
            float frac = 1f / (i + 1);
           // avg.Add((int)(i * frac * avg[i - 1] + frac * values[i]));
            float xPos = .5f *xSize+ i * xSize;
            float yPos = (values[i] / yMax) * graphHeight;
            float avgXPos = xSize + i * xSize;
            //float avgYPos = (avg[i] / yMax) * graphHeight;
            CreateBar(new Vector2(xPos, yPos), xSize*.9f);
            //CreateAverage(new Vector2(avgXPos, avgYPos), 3f, Color.cyan);
            

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

    private GameObject CreateAverage(Vector2 graphPosition, float lineWidth, Color color)
    {
        GameObject gameObject = new GameObject("bar", typeof(Image));
        gameObject.transform.SetParent(graphContaner, false);
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(graphPosition.x, graphContaner.sizeDelta.y*.5f);
        rectTransform.sizeDelta = new Vector2(lineWidth, graphPosition.y);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.pivot = new Vector2(.5f, 0f);

        return gameObject;
    }

}
