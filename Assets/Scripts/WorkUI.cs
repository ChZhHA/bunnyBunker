using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkUI : MonoBehaviour
{
    Vector2 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = Vector2.right * (GameManager.chosenOne==null? 50:-50);
        if(GetComponent<RectTransform>().anchoredPosition!=targetPos) GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition,targetPos,0.05f);
    }
}
