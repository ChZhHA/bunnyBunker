using UnityEngine;

public class SeedControl : MonoBehaviour
{
    void Start()
    {
        var anchor = transform.parent.GetComponentsInChildren<SeedAnchor>();
        transform.position = anchor[Random.Range(0, anchor.Length)].transform.position;
    }
    private void Update()
    {
        if (DiggedHole.Instances.FindIndex(item => Vector2.Distance(item.transform.position, transform.position) < 2f) > -1)
        {
            //TODO: 查找最近的小兔子 把种子给他
            Debug.Log("得到种子");
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
