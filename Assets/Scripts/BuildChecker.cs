using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildChecker : MonoBehaviour
{
    public static BuildChecker Instance { get; private set; }
    public GameObject CarrotEdge;
    public GameObject PotatoEdge;
    public bool CanBuild;
    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (DigController.Instance.CanSow)
        {
            CarrotEdge.SetActive(DigController.Instance.mode == DigController.OperateMode.Carrot);
            PotatoEdge.SetActive(DigController.Instance.mode == DigController.OperateMode.Potato);

            transform.position = DigController.Instance.potatoPoint;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(DigController.Instance.direction.y, DigController.Instance.direction.x) * Mathf.Rad2Deg - 90);

            if (DigController.Instance.mode == DigController.OperateMode.Carrot)
            {
                CanBuild = CarrotEdge.GetComponentInChildren<BuildCollider>().CanBuild;
            }
            else
            {

                CanBuild = PotatoEdge.GetComponentInChildren<BuildCollider>().CanBuild;
            }
        }

        else
        {
            CarrotEdge.SetActive(false);
            PotatoEdge.SetActive(false);
        }
    }
}
