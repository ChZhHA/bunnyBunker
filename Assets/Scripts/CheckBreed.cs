using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBreed : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //transform.parent.GetComponent<Rabbit_Entity>().Breed(other.transform.parent.GetComponent<Rabbit_Entity>());
        //other.transform.parent.GetComponent<Rabbit_Entity>().Breed(transform.parent.GetComponent<Rabbit_Entity>());
    }
    private void OnTriggerStay2D(Collider2D other) {
        if(other.GetComponentInParent<Rabbit_Entity>()==null) return;
        if(Random.Range(0,10)==0) transform.parent.GetComponent<Rabbit_Entity>().Breed(other.transform.parent.GetComponent<Rabbit_Entity>());
        transform.parent.GetComponent<Rabbit_Entity>().Babysit(other.transform.parent.GetComponent<Rabbit_Entity>(),true);
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.GetComponentInParent<Rabbit_Entity>()==null) return;
        transform.parent.GetComponent<Rabbit_Entity>().Babysit(other.transform.parent.GetComponent<Rabbit_Entity>(),false);
    }
}
