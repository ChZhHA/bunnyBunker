using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject finish;
    public static GameManager Instance;
    public static bool endGame;
    public GameObject rootPanel;
    public GameObject carrotPrefab;
    public static int carrotCounter = 1;
    public static GameObject chosenOne;
    public GameObject rabbitPrefab;
    public GameObject initPos;
    public GameObject workPrefab;
    public static int totalIndex = 0;
    public WorkType current = WorkType.Potato;
    public bool isWin=false;
    private void Start()
    {
        Instance = this;
        GenerateRabbit(2);
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            switch (current)
            {
                case WorkType.Feed:
                    if (carrotCounter > 0)
                    {
                        carrotCounter--;
                        Vector2 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        GameObject carrot = Instantiate(carrotPrefab, aim, Quaternion.identity);
                        carrot.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-5f, 5f));
                    }

                    break;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space)&&!endGame) {
            endGame=true;
            EndGame(false);
        }
        if(GameObject.Find("Rabbit(Clone)")==null &&!endGame){
            endGame=true;
            isWin=false;
            EndGame(false);
        }
        if(GameObject.Find("Main Camera").transform.position.y<-80){
            endGame=true;
            EndGame(true);
        }
        /*
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
        }*/

    }
    public void GenerateRabbit(int num)
    {
        bool gender = true;
        for (int i = 0; i < num; i++)
        {
            GameObject item = Instantiate(rabbitPrefab, initPos.transform.position, Quaternion.identity);
            item.transform.SetParent(GameObject.Find("Rabbits").transform);
            item.GetComponent<Rabbit_Entity>().mother = item.GetComponent<Rabbit_Entity>();
            item.GetComponent<Rabbit_Entity>().father = item.GetComponent<Rabbit_Entity>();
            item.GetComponent<Rabbit_Entity>().isMale = gender;
            gender = !gender;
            item.GetComponent<Rabbit_Entity>().gene = Color.white;
            item.GetComponent<Rabbit_Entity>().generation = 0;
            item.GetComponent<Rabbit_Entity>().index = totalIndex;
            item.GetComponent<Rabbit_Entity>().status = Rabbit_Entity.RabbitStatus.middle;
            totalIndex += 1;
            RabbitMap.MakeANode(item, item.GetComponent<Rabbit_Entity>().generation);
        }
    }
    public void EndGame(bool win){
        Debug.Log(win);
        endGame = true;
        isWin = win;
        StartCoroutine(Ending());
    }
    IEnumerator Ending(){
        yield return new WaitForEndOfFrame();
        finish.SetActive(true);
        yield return new WaitForSeconds(2);
        rootPanel.SetActive(true);
        rootPanel.GetComponentInChildren<RabbitRoot>().GenerateRoot(totalIndex-1);
    }

    public void DigButton(int i)
    {
        //chosenOne.GetComponent<Rabbit_Entity>().toDo = Rabbit_Entity.Assignment.Dig;
        if ((int)current != i)
        {
            current = (WorkType)i;
        }
        DigController.Instance.mode = (DigController.OperateMode)i;
    }
    public enum WorkType
    {
        Feed, Carrot, Potato
    }
    public void GenerateWork(Transform value)
    {
        GameObject item = Instantiate(workPrefab, value.position, Quaternion.identity);
        item.GetComponent<Work_Entity>();
    }
}
