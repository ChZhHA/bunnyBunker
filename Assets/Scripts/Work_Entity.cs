using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Work_Entity : MonoBehaviour
{
    public GameObject worker;
    public bool inWork=false;
    public float timer = 0f;
    public float totTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(worker!=null && Vector2.Distance(worker.transform.position,gameObject.transform.position)<1f){
            inWork=true;
            timer+=Time.deltaTime;
            if(timer>=totTime){
                timer=0f;
                inWork=false;
                worker=null;
                //invoke
            }
        }

        if(inWork && worker!=null && Vector2.Distance(worker.transform.position,gameObject.transform.position)>=1f){
            timer = 0f;
            inWork=false;
            worker=null;
        }
    }
}
