using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDemoManager : MonoBehaviour
{
    public GameObject QuizDemoScreen;

    public GameObject RunWay; //格子
    public GameObject ObjCanvas; // 障礙
    private Transform[] Nodes;
    private Transform[] objects;

    Dictionary<string, Vector3> Origin_GameObjects_pos = new Dictionary<string, Vector3>();

    void Start()
    {
        Nodes = RunWay.GetComponentsInChildren<Transform>();
        objects = ObjCanvas.GetComponentsInChildren<Transform>();
        foreach (Transform each_object in objects)
        {
            Origin_GameObjects_pos.Add(each_object.name, each_object.transform.position);
        }
    }


    public void ShowLevelDemo(string _ID, string _Name, string[] _pos_map)
    {
        //重置所有物件位置
        foreach (Transform each_object in objects)
            each_object.transform.position = Origin_GameObjects_pos[each_object.name];

        QuizDemoScreen.transform.GetChild(1).GetComponent<Text>().text = _Name;
        int i = 0;
        foreach (string pos in _pos_map)
        {
            foreach (Transform each_object in objects)
            {
                if (pos == each_object.name)
                {
                    //因為會獲取到節點本身所以多一個，所以要+1
                    each_object.transform.position = new Vector3(Nodes[i + 1].transform.position.x, each_object.transform.position.y, Nodes[i + 1].transform.position.z);
                }
            }
            i++;
        }

        QuizDemoScreen.SetActive(true);

        SetQuizHolder(_ID, _Name, _pos_map);
    }

    public void CloseLevelDemo()
    {
        QuizDemoScreen.SetActive(false);

        ResetQuizHolder();
    }

    void SetQuizHolder(string _ID, string _Name, string[] _pos_map)
    {
        GameObject prepareQuizHolder = GameObject.Find("PrepareQuizHolder");
        if (prepareQuizHolder) prepareQuizHolder.GetComponent<PrepareQuizHolder>().Set(_ID, _Name, _pos_map);

        GameObject quizCtrl = GameObject.Find("QuizCtrl");
        if (quizCtrl) quizCtrl.GetComponent<QuizCtrl>().send(_ID, _Name, _pos_map);
    }
    private void ResetQuizHolder()
    {
        GameObject prepareQuizHolder = GameObject.Find("PrepareQuizHolder");
        if (prepareQuizHolder) prepareQuizHolder.GetComponent<PrepareQuizHolder>().Reset();

        GameObject quizCtrl = GameObject.Find("QuizCtrl");
        if (quizCtrl) quizCtrl.GetComponent<QuizCtrl>().Reset();
    }
}
