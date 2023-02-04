using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigController : MonoBehaviour
{
    public static DigController Instance;
    SpriteRenderer Triangle;
    Vector2 potatoPoint;
    Vector2 holePoint;

    [Tooltip("可以播种")]
    public bool CanSow;

    [Tooltip("土豆预制体")]
    public List<GameObject> PotatoPrefebs;

    [Tooltip("胡萝卜预制体")]
    public List<GameObject> CarrotPrefebs;

    [Tooltip("挖掘坑洞预制体")]
    public List<GameObject> HolePrefebs;

    void Start()
    {
        Instance = this;
        Triangle = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        //找到离鼠标最近的挖掘点
        float minDistance = float.MaxValue;
        if (DiggedHole.Instances.Count > 0)
        {
            DiggedHole targetHole = DiggedHole.Instances[0];
            foreach (var diggedHole in DiggedHole.Instances)
            {
                var distance = Vector2.Distance(diggedHole.transform.position, mousePosition);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    targetHole = diggedHole;
                }
            }
            //射线判断挖掘点
            var hitResult = Physics2D.Raycast(targetHole.transform.position, mousePosition - targetHole.transform.position, 4, LayerMask.GetMask("Hole"));
            var hitPotato = Physics2D.Raycast(
                mousePosition + (mousePosition - targetHole.transform.position).normalized * 50,
                targetHole.transform.position - mousePosition, 100, LayerMask.GetMask("Potato")
            );
            potatoPoint = hitPotato.point;
            holePoint = hitResult.point;
            CanSow = false;
            Vector2 direction = Vector2.zero;
            if (hitPotato.point != Vector2.zero)
            {
                if (Vector2.Distance(hitPotato.point, targetHole.transform.position) < hitResult.distance)
                {
                    transform.position = hitPotato.point;
                    direction = hitPotato.normal * -1;
                    transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
                    CanSow = true;

                }
                else
                {
                    CanSow = Vector2.Distance(hitPotato.point, hitResult.point) < 0.2f;
                    direction = (Vector2)targetHole.transform.position - hitResult.point;
                    transform.position = hitResult.point;

                }
                Triangle.color = CanSow ? Color.green : Color.red;
            }
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
            //鼠标直接挖掘
            if (Input.GetMouseButtonDown(0))
            {
                if (!CanSow)
                {
                    MakeAHole(holePoint, direction);
                }
                else
                {
                    MakeAPotato(potatoPoint, direction);
                }
            }
        }



    }

    public void MakeAHole(Vector2 holePosition, Vector2 direction)
    {
        //挖掘土豆
        var hole = Instantiate<GameObject>(HolePrefebs[Random.Range(0, HolePrefebs.Count)], holePoint - direction * 0.2f, Quaternion.identity);
        hole.transform.SetParent(HoleContainer.Instance.transform);
    }

    public void MakeAPotato(Vector2 potatoPosition, Vector2 direction)
    {
        //种植土豆
        var potato = Instantiate<GameObject>(PotatoPrefebs[Random.Range(0, PotatoPrefebs.Count)], potatoPosition, Quaternion.identity);
        potato.transform.SetParent(PotatoContainer.Instance.transform);

        potato.transform.localRotation = Quaternion.identity;
        potato.transform.localScale = Vector3.one * Random.Range(0.8f, 2f);

        var anchors = potato.GetComponentsInChildren<Anchor>();

        var targetAnchor = anchors[Random.Range(0, anchors.Length)];
        var dir1 = targetAnchor.transform.position - potato.transform.position;

        potato.transform.localPosition -= dir1;
        var deg1 = Mathf.Atan2(-dir1.y, -dir1.x);
        var deg2 = Mathf.Atan2(direction.y, direction.x);
        Debug.Log(180 - (deg1 - deg2) * Mathf.Rad2Deg);

        potato.transform.RotateAround((Vector3)potatoPosition, Vector3.forward, 180 - (deg1 - deg2) * Mathf.Rad2Deg);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere((Vector3)holePoint, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere((Vector3)potatoPoint, 0.1f);
    }
}
