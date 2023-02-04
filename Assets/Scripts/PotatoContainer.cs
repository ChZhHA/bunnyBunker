using UnityEngine;

public class PotatoContainer : MonoBehaviour
{
    public static PotatoContainer Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        // foreach(Transform item in transform){
        //     item.GetComponent<BoxCollider2D>()
        // }
        var range = GetComponent<CompositeCollider2D>().bounds;
        PotatoRangeContainer.Instance.box.size = range.size;
        PotatoRangeContainer.Instance.box.offset = new Vector2(0, -range.size.y / 2 + range.max.y );
        //  .bounds.SetMinMax(range.min, range.max);
    }
}