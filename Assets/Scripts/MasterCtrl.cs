using UnityEngine;
using System.Collections.Generic;
public class MasterCtrl : MonoBehaviour
{
	public int status;
	public int steps;
	public float speed;
	public GameObject fire;
	private float startTime;
	private bool getTarget;
	private Vector3 targetPoint;
	private Quaternion rotateTargetPoint;
	public List<int> act = new List<int>();
	public List<int> tempList = new List<int>();
	public bool go;
	void Start()
	{

	}
	void Update()
	{
		switch (status)
		{
			case 0:
				tempLoopEnd = false;
				if (!go)
				{
					if (Input.GetKeyUp(KeyCode.W))
						getForward();
					if (Input.GetKeyUp(KeyCode.A))
						getLeft();
					if (Input.GetKeyUp(KeyCode.D))
						getRight();
					if (Input.GetKeyUp(KeyCode.Space))
						getGo();
					if (Input.GetKeyUp(KeyCode.L))
						getLoop3();
					if (Input.GetKeyUp(KeyCode.K))
						getLoop2();
				}
				if (go)
				{
					this.transform.rotation = rotateTargetPoint;
					startTime = Time.time;
					getTarget = false;
					if (steps < act.Count)
						status = act[steps];
				}
				break;
			case 1:
				forward();
				break;
			case 2:
				turnLeft();
				break;
			case 3:
				turnRight();
				break;
			case 4:
				addLoop();
				if (Input.GetKeyUp(KeyCode.W))
					getForward();
				if (Input.GetKeyUp(KeyCode.A))
					getLeft();
				if (Input.GetKeyUp(KeyCode.D))
					getRight();
				if (Input.GetKeyUp(KeyCode.Space))
					getEndLoop();
				break;
			case 5:
				fire.SetActive(true);
				break;
		}
	}
	private void forward()
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
			steps++;
			status = 0;
		}
	}
	public void getForward()
	{
		if (status == 0)
			act.Add(1);
		else if (status == 4)
			tempList.Add(1);
	}
	private void turnLeft()
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
			steps++;
			status = 0;
		}
	}
	public void getLeft()
	{
		if (status == 0)
			act.Add(2);
		else if (status == 4)
			tempList.Add(2);
	}
	private void turnRight()
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
			steps++;
			status = 0;
		}
	}
	public void getRight()
	{
		if (status == 0)
			act.Add(3);
		else if (status == 4)
			tempList.Add(3);
	}
	private bool tempLoopEnd;
	public void getEndLoop()
	{
		tempLoopEnd = true;
	}
	private int loopTimes;
	public void getLoop2()
	{
		status = 4;
		loopTimes = 2;
	}
	public void getLoop3()
	{
		status = 4;
		loopTimes = 3;
	}
	private void addLoop()
	{
		if(tempLoopEnd == true)
		{
			for (int i = 0; i < loopTimes; i++)
			{
				for (int j = 0; j < tempList.Count; j++)
					act.Add(tempList[j]);
			}
			tempList.Clear();
			status = 0;
		}
	}
	public void getGo()
	{
		go = true;
	}
}
