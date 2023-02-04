using UnityEngine;

public class PotatoContainer : MonoBehaviour
{
    public static PotatoContainer Instance;
    private void Awake()
    {
        Instance = this;
    }
}