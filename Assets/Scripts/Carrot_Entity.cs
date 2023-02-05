using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot_Entity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Eaten());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Eaten(){
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
