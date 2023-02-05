using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RabbitRoot : MonoBehaviour
{
    public GameObject element;
    public Image line;
    public GameObject generations;
    public Vector2 pos;
    public Image scroll;
    float a;
    float tarA=0;
    float s;
    float tarS=0;
    private List<GameObject> elements = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        a = Mathf.Lerp(a,tarA,0.002f);
        scroll.color = new Color(1,1,1,a);
        s= Mathf.Lerp(s,tarS,0.002f);
        GetComponent<RectTransform>().localScale =Vector3.one* (1-s);
        
    }
    public void GenerateRoot(int index){
        for(int i =0;i<RabbitMap.rootTree[RabbitMap.rootTree.Count-1].generation;i++){
            Instantiate(generations,transform);
        }
        for(int i=0;i<RabbitMap.rootTree.Count;i++){
            GameObject item = Instantiate(element);
            if(transform.GetChild(1+RabbitMap.rootTree[i].generation)!=null) item.transform.SetParent(transform.GetChild(1+RabbitMap.rootTree[i].generation));
            Debug.Log(RabbitMap.rootTree[i].generation);
            item.GetComponent<UnityEngine.UI.Image>().color = RabbitMap.rootTree[i].gene;
            elements.Add(item);
            item.GetComponent<Counter>().count = i;
            item.GetComponent<Counter>().motherIndex = RabbitMap.rootTree[i].mother;
            item.GetComponent<Counter>().generation = RabbitMap.rootTree[i].generation;
            item.GetComponent<Counter>().parent = elements[RabbitMap.rootTree[i].mother];
            //Image lineElement = Instantiate(line,transform);
            //Debug.Log(lineElement);
            //lineElement.transform.position = item.GetComponent<RectTransform>().position;
            //lineElement.transform.localRotation = Quaternion.AngleAxis(-GetAngle(elements[RabbitMap.rootTree[i].mother].GetComponent<RectTransform>(),item.GetComponent<RectTransform>()), Vector3.forward);
            //var distance = Vector2.Distance(elements[RabbitMap.rootTree[i].mother].GetComponent<RectTransform>().anchoredPosition, item.GetComponent<RectTransform>().anchoredPosition);
            //lineElement.rectTransform.sizeDelta = new Vector2(10, Mathf.Max(1, distance - 30));
            //Debug.Log(Camera.main.ViewportToWorldPoint((item.GetComponent<RectTransform>().position)));
            //Debug.Log(item.GetComponent<RectTransform>().position);
            //Debug.Log(elements[RabbitMap.rootTree[i].mother].GetComponent<RectTransform>().position);

            /*
            item.AddComponent<LineRenderer>();
            LineRenderer line = item.GetComponent<LineRenderer>();
            //Camera.main.ScreenToViewportPoint()
            Vector3[] pathPoints = { Camera.main.ViewportToScreenPoint(item.GetComponent<RectTransform>().position), Camera.main.ViewportToScreenPoint(elements[RabbitMap.rootTree[i].mother].GetComponent<RectTransform>().position)};
            line .positionCount = 2;
            line .SetPositions(pathPoints);
            line.startWidth= 0.1f;
            Debug.Log(item.GetComponent<RectTransform>().position);
            Debug.Log(elements[RabbitMap.rootTree[i].mother].GetComponent<RectTransform>().position);
            //Gizmos.DrawLine(item.GetComponent<RectTransform>().anchoredPosition,elements[RabbitMap.rootTree[i].mother].GetComponent<RectTransform>().anchoredPosition);
            */
        }
        StartCoroutine(View());

    }

    IEnumerator View(){
        yield return new WaitForSeconds(3);
        tarA = 0.7f;
        tarS=0.7f;
    }
    
}
