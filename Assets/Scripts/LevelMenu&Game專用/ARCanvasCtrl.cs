using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCanvasCtrl : MonoBehaviour
{
    private GameObject[] nodeObjs = new GameObject[25];

    public GameObject nodeFolder;
    public GameObject ObjFolder;

    private void Start()
    {
        for (int i = 0; i < nodeFolder.transform.childCount; i++)
        {
            nodeObjs[i] = nodeFolder.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < ObjFolder.transform.childCount; i++)
        {
            ObjFolder.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void SetObjPos(string objName, int toNode)
    {
        if(toNode >= 0 && toNode < 25)
        {
            GameObject obj = ObjFolder.transform.Find(objName).gameObject;
            obj.SetActive(true);
            Debug.Log(nodeObjs[toNode].transform.position);
            obj.GetComponent<RectTransform>().localPosition = new Vector3(nodeObjs[toNode].GetComponent<RectTransform>().localPosition.x, nodeObjs[toNode].GetComponent<RectTransform>().localPosition.y, obj.transform.localPosition.z);
        }
        else
        {
            ObjFolder.transform.Find(objName).gameObject.SetActive(false);
        }
    }
}
