using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCollider : MonoBehaviour
{
    public bool CanBuild;
    private List<Transform> bumpList = new List<Transform>();
    private SpriteRenderer sprite;
    private Color color;
    private Color banColor;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        color = sprite.color;
        banColor = Color.red;
        banColor.a = 0.1f;
    }
    private void Update()
    {
        for (int i = 0; i < bumpList.Count; i++)
        {
            if (bumpList[i] == null)
            {
                bumpList.RemoveAt(i);
                i--;
            }
        }
        CanBuild = bumpList.Count == 0;
        sprite.color = CanBuild ? color : banColor;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bumpList.Contains(collision.transform)) bumpList.Add(collision.transform);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        bumpList.Remove(collision.transform);
    }
}
