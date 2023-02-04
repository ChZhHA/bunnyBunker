using UnityEngine;

public class PotatoRangeBuilder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(transform.parent.GetComponent<SpriteMask>().bounds.extents);
        GetComponent<BoxCollider2D>().size = transform.parent.GetComponent<SpriteMask>().bounds.extents * new Vector2(5, 1.5f);
        transform.SetParent(PotatoRangeContainer.Instance.transform);
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        // transform.rotation = Quaternion.identity;
    }

}
