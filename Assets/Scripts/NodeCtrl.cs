using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCtrl : MonoBehaviour
{
    private SceneCtrl sc;
    void Start()
    {
       sc = GameObject.Find("SceneCtrl").GetComponent<SceneCtrl>();
    }
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider Other)
    {
        print(this.transform.position);
        if(Other.gameObject.name == "Master")
        {
            print(this.gameObject.name);
            sc.masterPosition = this.transform.position;
        }
        if (Other.gameObject.name == "Master" && this.gameObject.tag == "obstacle")
        {
            sc.Lose();
        }
    }
}
