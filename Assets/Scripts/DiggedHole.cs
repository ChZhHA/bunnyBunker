using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggedHole : MonoBehaviour
{
    // Start is called before the first frame update
    public static List<DiggedHole> Instances = new();

    void Start()
    {
        Instances.Insert(0, this);
    }
    private void OnDestroy()
    {
        Instances.Remove(this);
    }


}
