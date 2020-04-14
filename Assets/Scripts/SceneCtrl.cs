using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCtrl : MonoBehaviour
{
    public GameObject master;
    private int masterStatus;
    private GameObject startPoint;
    private GameObject finishPoint;
    public GameObject VictoryObj;
    public Vector3 masterPosition;
    void Start()
    {
        startPoint = GameObject.FindGameObjectWithTag("Start Point");
        finishPoint = GameObject.FindGameObjectWithTag("Finish Point");
        // print("Start Point is: " + startPoint.name);
        // print("Finish Point is: " + finishPoint.name);
        masterPosition = startPoint.transform.position;
        master.SetActive(true);
    }
    void Update()
    {
        masterStatus = master.GetComponent<MasterCtrl>().status;
        if(masterStatus == 0)
        {
            master.transform.position = masterPosition;
        }
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
