using UnityEngine;

public class HoleContainer : MonoBehaviour {
    public static HoleContainer Instance;
    private void Awake() {
        Instance=this;
    }
}