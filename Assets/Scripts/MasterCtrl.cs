using UnityEngine;

public class MasterCtrl : MonoBehaviour
{   
    public int status;
    // float time;
    void Start()
    {

    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            status = 1;
        }
        switch(status)
        {
            case 0:
                
                break;
            case 1:
                InvokeRepeating("forward1",2.0f,1.0f);
                status = 0;
                break;
            case 2:
                
                break;
            case 3:
                
                break;
        }
    }
    public void forward1()
    {   
        this.transform.Translate(Vector3.forward * Time.deltaTime);
    }
}
