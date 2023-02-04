using UnityEngine;

public class PotatoRangeContainer : MonoBehaviour
{
    public static PotatoRangeContainer Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;

    }
}
