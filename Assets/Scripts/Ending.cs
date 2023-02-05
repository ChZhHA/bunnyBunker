using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable() {
        transform.position=(Vector2)GameObject.Find("Main Camera").transform.position;
        GetComponent<Animator>().SetBool("isWin",GameObject.Find("Manager").GetComponent<GameManager>().isWin);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=(Vector2)GameObject.Find("Main Camera").transform.position;
    }
}
