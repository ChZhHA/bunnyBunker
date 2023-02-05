using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrotCounter : MonoBehaviour
{   
    public Sprite none;
    public Sprite less;
    public Sprite some;
    public Sprite more;
    void Update()
    {
        if(GameManager.carrotCounter<=0){
            GetComponent<Image>().sprite = none;
        }else if(GameManager.carrotCounter==1){
            GetComponent<Image>().sprite = less;
        }else if(GameManager.carrotCounter>1&&GameManager.carrotCounter<=3){
            GetComponent<Image>().sprite = some;
        }else{
            GetComponent<Image>().sprite = more;
        }
    }
}
