using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragCtrl : MonoBehaviour
{
    public Transform startPos;
    private bool isClicking = false;
    private Vector3 lastPos;
    private PrepareNodeCtrl prepareodeCtrl;

    void Awake()
    {
        lastPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //拖曳功能
        if (isClicking)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 point = touch.position;
                this.transform.position = new Vector3(point.x, point.y, transform.position.z);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 point = Input.mousePosition;
                this.transform.position = new Vector3(point.x, point.y, transform.position.z);
            }  
        }
    }

    public void clickDown()
    {
        isClicking = true;

        //點擊時改變為階層中的最後一項，如此可以避免被其他圖片擋住
        transform.SetAsLastSibling();

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 50f);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "RunwayNode")
            {
                if (collider.GetComponent<PrepareNodeCtrl>().nodeStatus != "")
                {
                    collider.GetComponent<PrepareNodeCtrl>().SetNodeStatus("");
                }

                break;
            }
        }
    }
    public void clickUp()
    {
        isClicking = false;

        bool isPutOnNode = false;
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 50f);

        foreach(Collider collider in colliders)
        {
            if(collider.tag == "RunwayNode")
            {
                isPutOnNode = true;

                //如果此格目前是空的
                if(collider.GetComponent<PrepareNodeCtrl>().nodeStatus == "" || collider.GetComponent<PrepareNodeCtrl>().nodeStatus == null)
                {
                    this.transform.position = new Vector3(collider.transform.position.x, collider.transform.position.y, this.transform.position.z);
                    lastPos = this.transform.position;

                    collider.GetComponent<PrepareNodeCtrl>().SetNodeStatus(this.gameObject.name);
                }
                else
                {
                    this.transform.position = lastPos;
                }

                break;
            }
        }
        if(isPutOnNode == false)
        {
            this.transform.position = startPos.position;

            if (prepareodeCtrl)
            {
                prepareodeCtrl.nodeStatus = "";
                prepareodeCtrl = null;
            }
        }
        
    }


}
