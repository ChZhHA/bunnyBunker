using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rabbit_Entity : MonoBehaviour
{
    /*
    public enum Assignment{
        Idle,Dig
    }
    public Assignment toDo;
    */
    public float scale = 0.4f;
    float tarScale;
    public RabbitStatus status;
    public int generation;
    public int index;
    public int motherIndex;
    float age;
    public bool isDead=false;
    float grow = 10f;
    int lifeSpan = 20;
    float hunger = 0f;
    public bool isMale;
    [SerializeField]bool isPreg = false;
    bool isBabysit = false;
    bool isRight = true;
    //float laziness =1f;
    public Color gene;
    public GameObject task;
    [SerializeField] Collider2D face;
    public Rabbit_Entity father = null;
    public Rabbit_Entity mother = null;
    GameObject[] kids=new GameObject[3];

    private void OnEnable() {
        //GetComponent<SpriteRenderer>().color = gene;
        StartCoroutine(DeathTimer(lifeSpan));
        transform.localScale=Vector3.one*0.1f;
        status = RabbitStatus.child;
    }
    private void Update() {
        if(isDead) Destroy(gameObject);
        if(isDead) return;
        tarScale = status==RabbitStatus.child? 0.1f:0.4f;
        scale = Mathf.Lerp(scale,tarScale,0.01f);
        age+=Time.deltaTime;
        if(isBabysit) grow-=Time.deltaTime;
        if(grow<=0&&status==RabbitStatus.child) status = RabbitStatus.middle;
        if(status==RabbitStatus.middle && age>=40){
            status = RabbitStatus.old;
        }
        GetComponent<SpriteRenderer>().material.SetInt("_Enable",1);
        GetComponent<SpriteRenderer>().material.SetColor("_EdgeColor",Color.black);
        //if(gameObject==GameManager.chosenOne) GetComponent<SpriteRenderer>().material.SetColor("_EdgeColor",Color.red);
        //GetComponent<SpriteRenderer>().material.SetColor("_EdgeColor",Color.black);

        //GetComponent<SpriteRenderer>().color = GameManager.chosenOne == gameObject? Color.red:Color.white;
        //GetComponent<SpriteRenderer>().color = gene;
        /*
        if(task!=null && status == RabbitStatus.middle){
            Move(task);
        }*/

        transform.localScale=scale*(isRight?Vector3.one:new Vector3(-1,1,1));
    }
    public void Babysit(Rabbit_Entity other,bool isNear){
        if(GetComponent<Rabbit_Entity>().status==RabbitStatus.child && other.status!=RabbitStatus.child){
            isBabysit = isNear;
        }
    }

    public void Breed(Rabbit_Entity other){
        if(!GetComponent<Rabbit_Entity>().isMale && other.isMale && GetComponent<Rabbit_Entity>() && other.status==RabbitStatus.middle 
        && GetComponent<Rabbit_Entity>().status==RabbitStatus.middle
        && GetComponent<Rabbit_Entity>().isPreg == false){
            GetComponent<Rabbit_Entity>().isPreg = true;
            StartCoroutine(ProduceTimer(5,other));
        }
    }
    public void Reproduce(Rabbit_Entity other){
        int num = Random.Range(1,3);
        for(int i=0;i<num;i++){
        //if(kids[i]!=null) {
            GameObject born = Instantiate(GameObject.Find("Manager").GetComponent<GameManager>().rabbitPrefab,transform.position,Quaternion.identity);
            born.transform.SetParent(GameObject.Find("Rabbits").transform);
            born.GetComponent<Rabbit_Entity>().isMale = Random.Range(0,3)==0;
            Color rdm = new Color(Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f),1f);
            born.GetComponent<Rabbit_Entity>().mother = gameObject.GetComponent<Rabbit_Entity>();
            born.GetComponent<Rabbit_Entity>().gene = Random.Range(0,5)!=0? (gameObject.GetComponent<Rabbit_Entity>().gene*0.5f+other.gene*0.5f):rdm;
            born.GetComponent<Rabbit_Entity>().father = other;
            born.GetComponent<Rabbit_Entity>().motherIndex = gameObject.GetComponent<Rabbit_Entity>().index;
            born.GetComponent<SpriteRenderer>().color = born.GetComponent<Rabbit_Entity>().gene;
            born.GetComponent<SpriteRenderer>().material.SetColor("_Color",born.GetComponent<Rabbit_Entity>().gene);
            born.GetComponent<Rabbit_Entity>().generation = 1+(other.generation>gameObject.GetComponent<Rabbit_Entity>().generation? other.generation:gameObject.GetComponent<Rabbit_Entity>().generation);
            Debug.Log(born.GetComponent<Rabbit_Entity>().generation);
            born.GetComponent<Rabbit_Entity>().index = RabbitMap.rootTree.Count;
            RabbitMap.MakeANode(born,born.GetComponent<Rabbit_Entity>().generation);
        //}
        }
        kids = new GameObject[3];
    }
    IEnumerator ProduceTimer(int secs,Rabbit_Entity other){
        yield return new WaitForSeconds(secs);
        isPreg = false;
        Reproduce(other);
    }
    IEnumerator DeathTimer(int secs){
        yield return new WaitForSeconds(secs);
        GetComponent<Animator>().SetBool("isDead",true);
        yield return new WaitForSeconds(1);
        isDead=true;
    }



    void Move(GameObject target){
        if(Vector2.Distance(GetComponent<Rigidbody2D>().position,target.transform.position)>=1){
            isRight = GetComponent<Rigidbody2D>().position.x<target.transform.position.x;
            GetComponent<Rigidbody2D>().AddForce(Vector2.right* (isRight? 2:-2));
            if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x)>5) GetComponent<Rigidbody2D>().velocity = new Vector2((isRight? 5:-5), GetComponent<Rigidbody2D>().velocity.y);
            //GetComponent<Rigidbody2D>().velocity = new Vector2((isRight? 5:-5), GetComponent<Rigidbody2D>().velocity.y);
        }
        else{
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
    public void Jump(Collider2D other){
        //if(GetComponent<Collider2D>().IsTouching(other)){
            //Debug.Log("jump");
            GetComponent<Rigidbody2D>().velocity = new Vector2(isRight?-1:1,5);
            //GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
        //}
    }
    public void Death(){
        isDead=true;
    }

    public enum RabbitStatus{
        child,middle,old
    }
}
