using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPreparePage : MonoBehaviour
{
    public GameObject ObjCanvas, RunWay;
    private Transform[] objects, Nodes;
    private Dictionary<string, Vector3> Origin_GameObjects_pos = new Dictionary<string, Vector3>();
    private PrepareQuizHolder prepareQuizHolder;

    private void Awake()
    {
        Nodes = RunWay.GetComponentsInChildren<Transform>();
        objects = ObjCanvas.GetComponentsInChildren<Transform>();
        foreach (Transform each_object in objects)
        {
            Origin_GameObjects_pos.Add(each_object.name, each_object.transform.position);
        }

        prepareQuizHolder = GameObject.Find("PrepareQuizHolder").GetComponent<PrepareQuizHolder>();
    }
    private void OnEnable()
    {
        //重置所有物件位置
        foreach (Transform each_object in objects)
            each_object.transform.position = Origin_GameObjects_pos[each_object.name];

        if(prepareQuizHolder.Game_ID != "")
        {
            for(int i = 0; i < prepareQuizHolder.Game_pos_map.Length; i++)
            {
                if(prepareQuizHolder.Game_pos_map[i] != "")
                {
                    //設定grid內容
                    RunWay.transform.GetChild(i).GetComponent<PrepareNodeCtrl>().SetNodeStatus(prepareQuizHolder.Game_pos_map[i]);

                    //設定Obj位置
                    GameObject gameObject = ObjCanvas.transform.Find(prepareQuizHolder.Game_pos_map[i]).gameObject;
                    gameObject.GetComponent<RectTransform>().position = RunWay.transform.GetChild(i).position;
                }
            }
        }
    }
}
