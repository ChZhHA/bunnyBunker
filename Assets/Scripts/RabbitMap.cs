using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMap : MonoBehaviour
{
    //public static Dictionary<int, Tree> root = new Dictionary<int, Tree>();
    [SerializeField]public static List<Tree> rootTree = new List<Tree>();

    public static void MakeANode(GameObject rabbit,int generation){
        Tree value = new Tree();
        value.gene = rabbit.GetComponent<Rabbit_Entity>().gene;
        //value.father = rabbit.GetComponent<Rabbit_Entity>().fatherIndex;
        value.mother = rabbit.GetComponent<Rabbit_Entity>().motherIndex;
        value.isIn = false;
        value.generation = generation;
        //root.Add(myIndex,value);
        rootTree.Add(value);
    }
    public struct Tree{
        public int mother;
        public Color gene;
        public bool isIn;
        public int generation;
    }
}
