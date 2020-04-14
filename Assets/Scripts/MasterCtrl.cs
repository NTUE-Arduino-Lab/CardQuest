using UnityEngine;

public class MasterCtrl : MonoBehaviour
{
    public int status;
    public float speed;
    private float startTime;
    private bool getTarget;
    private Vector3 targetPoint;
    private Quaternion rotateTargetPoint;
    void Start()
    {

    }
    void Update()
    {

        switch (status)
        {
            case 0:
                if (Input.GetKeyUp(KeyCode.W))
                {
                    status = 1;
                }
                if (Input.GetKeyUp(KeyCode.A))
                {
                    status = 2;
                }
                if (Input.GetKeyUp(KeyCode.D))
                {
                    status = 3;
                }
                this.transform.rotation = rotateTargetPoint;
                startTime = Time.time;
                getTarget = false;
                break;
            case 1:
                forward1();
                break;
            case 2:
                turnLeft();
                break;
            case 3:
                turnRight();
                break;
        }
    }
    public void forward1()
    {
        float distCovered = (Time.time - startTime) * speed;
        Vector3 startPoint = this.transform.position;
        if (!getTarget)
        {
            targetPoint = this.transform.position + this.transform.forward * 2;
            getTarget = true;
        }
        this.transform.position = Vector3.Lerp(startPoint, targetPoint, distCovered);
        if (this.transform.position == targetPoint)
        {
            status = 0;
        }
    }

    public void turnLeft()
    {
        float distCovered = (Time.time - startTime) * speed;
        Quaternion rotateStartPoint = this.transform.rotation;
        if (!getTarget)
        {
            rotateTargetPoint = Quaternion.Euler(0.0f, this.transform.rotation.eulerAngles.y - 90f, 0.0f);
            getTarget = true;
        }
        this.transform.rotation = Quaternion.Lerp(rotateStartPoint, rotateTargetPoint, distCovered);
        if (this.transform.rotation.eulerAngles == rotateTargetPoint.eulerAngles)
        {
            status = 0;
        }
    }
    public void turnRight()
    {
        float distCovered = (Time.time - startTime) * speed;
        Quaternion rotateStartPoint = this.transform.rotation;
        if (!getTarget)
        {
            rotateTargetPoint = Quaternion.Euler(0.0f, this.transform.rotation.eulerAngles.y + 90f, 0.0f);
            getTarget = true;
        }
        this.transform.rotation = Quaternion.Lerp(rotateStartPoint, rotateTargetPoint, distCovered);
        if (this.transform.rotation.eulerAngles == rotateTargetPoint.eulerAngles)
        {
            status = 0;
        }
    }
}
