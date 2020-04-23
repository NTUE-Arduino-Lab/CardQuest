using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnsCtrl : MonoBehaviour
{
	private Dictionary<string, List<List<int>>> ans = new Dictionary<string, List<List<int>>>();
	LevelLoader ll;
	private SceneCtrl sc;
	void Start()
	{
		ll = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
	}
	void Update()
	{
		DontDestroyOnLoad(this.gameObject);
	}
	public void check(List<int> act)
	{
		string scene = ll.getSceneName();
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
