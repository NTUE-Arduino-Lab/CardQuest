using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareNodeCtrl : MonoBehaviour
{
    private int nodeID;
    private static string[] nodeStatusArray = new string[25];
    public string nodeStatus = "";

    public PrepareQuizHolder prepareQuizHolder;


    void Awake()
    {
        nodeID = transform.GetSiblingIndex();

        prepareQuizHolder = GameObject.Find("PrepareQuizHolder").GetComponent<PrepareQuizHolder>();
    }

    private void OnEnable()
    {
        SetNodeStatus(prepareQuizHolder.Game_pos_map[nodeID]);
    }

    public void SetNodeStatus(string status)
    {
        nodeStatus = status;
        nodeStatusArray[nodeID] = status;
    }

    static public string[] GET_nodeStatusArray()
    {
        return nodeStatusArray;
    }
}
