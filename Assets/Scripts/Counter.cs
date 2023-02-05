using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public int count;
    public int motherIndex;
    public int generation;
    public GameObject parent;

    public Image arrow;
    public RectTransform pa;
    public RectTransform pb;



    public float GetAngle()
    {
        var dir = pb.position - pa.position;
        var dirV2 = new Vector2(dir.x, dir.y);
        var angle = Vector2.SignedAngle(dirV2, Vector2.down);
        return angle;
    }
    private void OnEnable() {
        //StartCoroutine(CoWaitForPosition());
        pa = GetComponent<RectTransform>();
        pb = parent.GetComponent<RectTransform>();
        if(pa.GetComponent<Counter>().generation!=0){
            var rP = pa.anchoredPosition;
            var tp = pa.position;
            Image line = Instantiate(arrow,GameObject.Find("Lines").transform);
            line.transform.position = pa.position;
            line.transform.localRotation = Quaternion.AngleAxis(-GetAngle(), Vector3.forward);
            
            //var distance = Vector2.Distance(pb.anchoredPosition, pa.anchoredPosition);
            line.rectTransform.sizeDelta = new Vector2(5, 100+generation*10);
        }
    }

    IEnumerator CoWaitForPosition()
    {
        yield return new WaitForEndOfFrame();
        pa = GetComponent<RectTransform>();
        pb = parent.GetComponent<RectTransform>();
        if(pa.GetComponent<Counter>().generation!=0){
            var rP = pa.anchoredPosition;
            var tp = pa.position;
            Image line = Instantiate(arrow,GameObject.Find("Lines").transform);
            line.transform.position = pa.position;
            line.transform.localRotation = Quaternion.AngleAxis(-GetAngle(), Vector3.forward);
            
            //var distance = Vector2.Distance(pb.anchoredPosition, pa.anchoredPosition);
            line.rectTransform.sizeDelta = new Vector2(5, 100+generation*10);
        }
    }
}
