using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public string ID;
    public string Name;
    public string[] pos_map;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(itemClicked);
    }

    void itemClicked()
    {
        GameObject.Find("LevelDemoManager").GetComponent<LevelDemoManager>().ShowLevelDemo(ID, Name, pos_map);
    }

    public void SetContent(string _ID, string _Name, string[] _pos_map)
    {
        ID = _ID;
        Name = _Name;
        pos_map = _pos_map;

        transform.GetChild(0).GetComponent<Text>().text = Name;
    }
}
