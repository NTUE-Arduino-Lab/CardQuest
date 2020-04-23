using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCtrl : MonoBehaviour
{
    public GameObject master;
    private int masterStatus;
    private GameObject startPoint;
    private GameObject finishPoint;
    public GameObject victoryObj, loseObj, newAns;
    public Vector3 masterPosition;
    private AnsCtrl ac;
    void Start()
    {
        startPoint = GameObject.FindGameObjectWithTag("Start Point");
        finishPoint = GameObject.FindGameObjectWithTag("Finish Point");
        print("Start Point is: " + startPoint.name);
        print("Finish Point is: " + finishPoint.name);
        masterPosition = startPoint.transform.position;
        master.SetActive(true);
        ac = GameObject.Find("AnsCtrl").GetComponent<AnsCtrl>();
    }
    void Update()
    {
        masterStatus = master.GetComponent<MasterCtrl>().status;
        if(masterStatus == 0 || masterStatus == 6)
        {
            master.transform.position = masterPosition;
            if(masterStatus == 6)
            {
                check();
            }
        }
        
    }
    private void check()
    {
        if(master.transform.position.x == finishPoint.transform.position.x &&
        master.transform.position.z == finishPoint.transform.position.z)
        {
            ac.check(master.GetComponent<MasterCtrl>().ans);
            
        }
        else
        {
            Lose();
        }
    }
    public void Win()
    {
        victoryObj.SetActive(true);
    }
    public void NewWin()
    {
        victoryObj.SetActive(true);
        newAns.SetActive(true);
    }
    public void Lose()
    {
        loseObj.SetActive(true);
        master.GetComponent<MasterCtrl>().status = 5;
    }
}
