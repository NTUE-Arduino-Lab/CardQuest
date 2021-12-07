using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnsCtrl : MonoBehaviour
{
	private Dictionary<string, List<List<int>>> ans = new Dictionary<string, List<List<int>>>();
	//LevelLoader ll;
	private SceneCtrl sc;
    QuizCtrl quizctrl;
    public static bool AnsCtrl_isClone = false; //用來確定是否已經有dont destroy 的 AnsCtrl
    void Start()
	{
        //ll = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        if (AnsCtrl_isClone == true)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            AnsCtrl_isClone = true;
        }
    }
	/*void Update()
	{
		DontDestroyOnLoad(this.gameObject);
	}*/
	public void check(List<int> act)
	{
        //string scene = ll.getSceneName();
        quizctrl = GameObject.Find("QuizCtrl").GetComponent<QuizCtrl>();
        string scene = quizctrl.Game_Name;
        sc = GameObject.Find("SceneCtrl").GetComponent<SceneCtrl>();
		if (!ans.ContainsKey(scene))
		{
			print(scene);
			ans.Add(scene, new List<List<int>>() { act });
            sc.NewWin();
			return;
		}
		foreach(List<int> l in ans[scene])
		{
			if(l.SequenceEqual(act))
			{
				sc.Win();
				return;
			}
		}
		ans[scene].Add(act.ToList());
		sc.NewWin();
	}
}
