using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    List<int> mList = new List<int>();
    void Start()
    {
        mList.Add(5);
        int temp;
        temp = mList[0];
        print(temp);
        
    }
    void Update()
    {
        
    }
}
