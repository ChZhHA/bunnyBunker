using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractRabbit : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        if(other.GetComponentInParent<Rabbit_Entity>()!=null){
            other.GetComponentInParent<Rigidbody2D>().AddForce(20*(transform.position-other.transform.position).normalized);///Vector2.Distance(transform.position,other.transform.position));
        }
    }
}
