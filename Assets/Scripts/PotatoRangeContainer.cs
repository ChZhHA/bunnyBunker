using UnityEngine;

public class PotatoRangeContainer : MonoBehaviour
{
    public static PotatoRangeContainer Instance;
    public BoxCollider2D box;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        box=GetComponent<BoxCollider2D>();
    }
}
