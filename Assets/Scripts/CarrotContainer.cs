using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotContainer : MonoBehaviour
{
    public static CarrotContainer instance;
    // Start is called before the first frame update
    void Start()
    {
        instance= this;
    }
}
