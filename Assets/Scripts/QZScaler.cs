using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QZScaler : MonoBehaviour
{
    void Start()
    {
        QuizScaler();
    }

    //适配
    void QuizScaler()
    {
        //当前画布尺寸
         Vector2 canvasSize = gameObject.GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta;
        //当前画布尺寸长宽比
        // float screenxyRate = canvasSize.x / canvasSize.y;
        //if (Screen.width < 500.0)
        if(canvasSize.x<500.0)
        {
           transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3(0.75f, 0.75f, 1);
        }
        else
        {
            transform.GetChild(0).GetComponent<RectTransform>().localScale = new Vector3( 1, 1, 1);

        }
    }
    void  Update()
    {
        QuizScaler();
    }

}
