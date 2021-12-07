using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressController : MonoBehaviour
{
    [SerializeField]    Image progressImg;
    [SerializeField]    Text progressText;
    public void setProgess(float i)
    {
        progressImg.fillAmount = i/100;
        //progressImg.fillAmount = i ;
        progressText.text = ((int)(i)).ToString() + "%";
        //progressText.text = ((int)(i * 100)).ToString()+"%";
    }
}
