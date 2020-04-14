using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCtrl : MonoBehaviour
{
    public GameObject master;
    private GameObject startPoint;
    private GameObject finishPoint;
    public GameObject VictoryObj;
    void Start()
    {
        startPoint = GameObject.FindGameObjectWithTag("Start Point");
        finishPoint = GameObject.FindGameObjectWithTag("Finish Point");
        // print("Start Point is: " + startPoint.name);
        // print("Finish Point is: " + finishPoint.name);
        float masterStartPointX = startPoint.transform.position.x;
        float masterStartPointZ = startPoint.transform.position.z;
        master.transform.position = new Vector3(masterStartPointX,0.5f,masterStartPointZ);
        master.SetActive(true);
    }
    void Update()
    {
        if(master.transform.position.x == finishPoint.transform.position.x &&
        master.transform.position.z == finishPoint.transform.position.z)
        {
            Win();
        }
    }
    void Win()
    {
        VictoryObj.SetActive(true);
    }
}
