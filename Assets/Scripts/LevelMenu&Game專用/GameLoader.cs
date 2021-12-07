using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public Text title;
    public GameObject SceneCtrl;
    //public GameObject RunWay; //格子
    [SerializeField]
    Transform[] Nodes;
    [SerializeField]
    GameObject ObjCanvas; // 障礙
    [SerializeField]
    Transform[] objects;
    [SerializeField]
    GameObject quizcrtl;
    [SerializeField]
    string Game_ID;
    [SerializeField]
    string[] Game_pos_map;
    void Start()
    {
        quizcrtl = GameObject.Find("QuizCtrl");
        //Nodes = RunWay.GetComponentsInChildren<Transform>();
        objects = ObjCanvas.GetComponentsInChildren<Transform>();
        title.text=quizcrtl.GetComponent<QuizCtrl>().Game_Name;
        Game_ID = quizcrtl.GetComponent<QuizCtrl>().Game_ID;
        Game_pos_map = quizcrtl.GetComponent<QuizCtrl>().Game_pos_map;
        int i = 0;
        foreach (string pos in Game_pos_map)
        {
            foreach (Transform each_object in objects)
            {
                if (pos == each_object.name)
                {
                    //print(pos + "  " + each_object.name + "  " + i);
                    each_object.transform.parent = Nodes[i].transform;
                    //這裡不用+1，因為節點是我手動拉進去ㄉ
                    if (each_object.name == "End")
                    {
                        Nodes[i].tag = "Finish Point";
                    }
                    else if (each_object.name == "Start")
                    {
                        Nodes[i].tag = "Start Point";
                    }
                    else
                    {
                        Nodes[i].tag = "obstacle";
                    }
                    each_object.transform.position = Nodes[i].transform.position;
                }
            }
            //print(i);
            Nodes[i].gameObject.SetActive(true);
            i++;
        }
        SceneCtrl.SetActive(true);
    }
}
