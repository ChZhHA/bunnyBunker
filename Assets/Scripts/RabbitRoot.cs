using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RabbitRoot : MonoBehaviour
{
    public GameObject element;
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
        if(Input.GetKeyDown(KeyCode.Space)) GenerateRoot(GameManager.totalIndex-1);
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
            item.transform.SetParent(transform.GetChild(RabbitMap.rootTree[i].generation));
            Debug.Log(RabbitMap.rootTree[i].generation);
            item.GetComponent<UnityEngine.UI.Image>().color = RabbitMap.rootTree[i].gene;
            elements.Add(item);
            item.GetComponent<Counter>().count = i;
            Debug.Log(Camera.main.ViewportToWorldPoint((item.GetComponent<RectTransform>().position)));
            //item.transform.GetChild(0).LookAt(elements[RabbitMap.rootTree[i].mother].GetComponent<RectTransform>().anchoredPosition);
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
        tarA = 0.2f;
        tarS=0.7f;
    }
    
}
