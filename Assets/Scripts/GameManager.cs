using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameObject chosenOne;
    public GameObject rabbitPrefab;
    public GameObject initPos;
    public GameObject workPrefab;
    public static int totalIndex = 0;
    private void Start() {
        GenerateRabbit(2);
    }
    private void Update() {
        if(Input.GetMouseButtonDown(0)){
            //Debug.Log("clicked");
            chosenOne = null;
            Vector3 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Ray aim = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] rabbit = new RaycastHit2D[5];
            //Physics2D.RaycastNonAlloc(
            int numbers = Physics2D.RaycastNonAlloc(aim, Vector2.zero, rabbit);
            Debug.Log(numbers);
            for(int i =0;i<numbers;i++){
                if(rabbit[i].collider.GetComponent<Rabbit_Entity>()!=null){
                    chosenOne = rabbit[i].collider.gameObject;
                    return;
                }
            }
        }
        if(chosenOne!=null && Input.GetMouseButtonDown(1)){
            //Debug.Log("work");
            Vector3 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] rabbit = new RaycastHit2D[5];
            int numbers = Physics2D.RaycastNonAlloc(aim, Vector2.zero, rabbit);
            Debug.Log(numbers);
            for(int i =0;i<numbers;i++){
                if(rabbit[i].collider.GetComponent<Work_Entity>()!=null){
                    chosenOne.GetComponent<Rabbit_Entity>().task = rabbit[i].collider.gameObject;
                    rabbit[i].collider.GetComponent<Work_Entity>().worker = chosenOne;
                    return;
                }
            }
        }

    }
    public void GenerateRabbit(int num){
        bool gender = true;
        for(int i = 0;i<num;i++){
            GameObject item = Instantiate(rabbitPrefab,initPos.transform.position, Quaternion.identity);
            item.transform.SetParent(GameObject.Find("Rabbits").transform);
            item.GetComponent<Rabbit_Entity>().mother = item.GetComponent<Rabbit_Entity>();
            item.GetComponent<Rabbit_Entity>().father = item.GetComponent<Rabbit_Entity>();
            item.GetComponent<Rabbit_Entity>().isMale = gender;
            gender = !gender;
            item.GetComponent<Rabbit_Entity>().gene =Color.white;
            item.GetComponent<Rabbit_Entity>().generation = 0;
            item.GetComponent<Rabbit_Entity>().index = totalIndex;
            item.GetComponent<Rabbit_Entity>().status = Rabbit_Entity.RabbitStatus.middle;
            totalIndex+=1;
            RabbitMap.MakeANode(item,item.GetComponent<Rabbit_Entity>().generation);
        }
    }

    public void DigButton(){
        chosenOne.GetComponent<Rabbit_Entity>().toDo = Rabbit_Entity.Assignment.Dig;

    }
    public enum WorkType{
        Dig
    }
    public void GenerateWork(Transform value){
        GameObject item = Instantiate(workPrefab,value.position, Quaternion.identity);
        item.GetComponent<Work_Entity>();
    }
}
