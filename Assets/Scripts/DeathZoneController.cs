using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneController : MonoBehaviour
{
    // Start is called before the first frame update
    public float DropSpeed = 0.1f;
    private float StartTime;
    private BoxCollider2D boxCollider;
    void Start()
    {
        StartTime = Time.time;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.down * DropSpeed * (Time.time - StartTime);
        var boundsMin = boxCollider.bounds.min;
        //销毁挖掘洞
        foreach (var item in DiggedHole.Instances)
        {
            if (item.GetComponent<Collider2D>().bounds.max.y > boundsMin.y + 10)
            {
                Destroy(item.gameObject);
            }
        }
        //销毁土豆
        foreach (Transform item in PotatoContainer.Instance.transform)
        {
            if (item.GetComponent<Collider2D>().bounds.max.y > boundsMin.y + 10)
            {
                Destroy(item.gameObject);
            }
        }
        if(GameManager.endGame) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Rabbit_Entity rabbit;
        if(other.TryGetComponent<Rabbit_Entity>(out rabbit)){
            other.GetComponent<Rabbit_Entity>().Death();
        }
    }
}
