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
    private Player player;
    private RectTransform labelX;
    private RectTransform labelY;
    private RectTransform gridLabelY;
    public RectTransform avgLine;
    private RectTransform BarContainer;
    private RectTransform LineContainer;
    private RectTransform GridContainer;

    public float sum;
    public float average;
    public float yMax;
    public float graphHeight;


    private void OnEnable()
   
    {
        graphContaner = transform.Find("GraphContaner").GetComponent<RectTransform>();
        labelX = graphContaner.Find("LabelX").GetComponent<RectTransform>();
        labelY = graphContaner.Find("LabelY").GetComponent<RectTransform>();
        player = NetworkManager.instance.localPlayer;
        gridLabelY = graphContaner.Find("GridY").GetComponent<RectTransform>();
        BarContainer = graphContaner.Find("BarObject").GetComponent<RectTransform>();
        LineContainer = graphContaner.Find("LineObject").GetComponent<RectTransform>();
        GridContainer = graphContaner.Find("GridObject").GetComponent<RectTransform>();



     

        List<int> values = player.decisionTimeHistory;
        




        List<int> avg = player.GetAverageTimeHistory();
       

        graphHeight = graphContaner.sizeDelta.y;
        avgLine.anchorMin = new Vector2(0, 0);



        yMax = player.decisionTimeHistory.Max() * 1.1f;

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
       


        ShowGraph(avg,values,(int _i)=> ""+(_i+1),(float _f) => Mathf.RoundToInt(_f)+ "s" );


    }

   

    private void ShowGraph( List<int> avg ,List<int> values, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
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

            Color barColor = Color.white;

            if (!player.winHistory[i])
            {
                barColor = new Color32(0x11,0x40,0x4D,0xff);
            }
            


            CreateBar(new Vector2(xPos, yPos), xSize*.9f,barColor );

            if(i< values.Count - 1)
            {
                float avgPosX = xPos;
                float avgPosY = (avg[i] / yMax * graphHeight);
                float endPointX = .5f * xSize + (i + 1) * xSize;
                float endPointY = (avg[i + 1] / yMax * graphHeight);
                CreateAverageLine(new Vector2(avgPosX, avgPosY), new Vector2(endPointX, endPointY), 3f, Color.cyan);
            }
          
            

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
            gridY.SetParent(GridContainer, false);
            gridY.gameObject.SetActive(true);
            gridY.anchoredPosition = new Vector2(graphWidth*.5f, normalizedY * graphHeight);
        }
    }
  
    private GameObject CreateBar(Vector2 graphPosition, float barWidth,Color color)
    {
        GameObject gameObject = new GameObject("bar", typeof(Image));
        gameObject.transform.SetParent(BarContainer, false);
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
        rectTransform.sizeDelta = new Vector2(barWidth,graphPosition.y);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.pivot = new Vector2(.5f, 0f);


        

        return gameObject;
    }

    private GameObject CreateAverageLine(Vector2 endPoint,Vector2 startPoint, float lineWidth, Color color)
    {

         GameObject gameObject = new GameObject("line", typeof(Image));
        gameObject.transform.SetParent(LineContainer, false);
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        int angle = UtilsClass.GetAngleFromVector(endPoint - startPoint);
        float distance = Vector2.Distance(startPoint, endPoint);
        rectTransform.sizeDelta = new Vector2(distance,lineWidth);
        rectTransform.anchoredPosition = startPoint;
        rectTransform.Rotate(0,0,angle);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.pivot = new Vector2(0f, 0f);
        GameObject GameObject = new GameObject("line", typeof(Image));
        GameObject.transform.SetParent(LineContainer, false);
        GameObject.GetComponent<Image>().color = color;
        RectTransform RectTransform = GameObject.GetComponent<RectTransform>();
        RectTransform.sizeDelta = new Vector2(lineWidth, lineWidth);
        RectTransform.anchoredPosition = startPoint;
        RectTransform.anchorMin = new Vector2(0, 0);
        RectTransform.anchorMax = new Vector2(0, 0);
        RectTransform.pivot = new Vector2(.5f, 1f);


        return gameObject;
    }
   

}
