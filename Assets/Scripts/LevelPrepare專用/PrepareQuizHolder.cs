using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareQuizHolder : MonoBehaviour
{
    public string Game_ID;//id
    public string Game_Name;//名字
    public string[] Game_pos_map;

    void Awake()
    {
        Game_pos_map = new string[25];
        for (int i = 0; i < Game_pos_map.Length; i++) Game_pos_map[i] = "";
    }

    public void Set(string _id, string _Name, string[] _pos_map)
    {
        Game_ID = _id;
        Game_Name = _Name;
        Game_pos_map = _pos_map;
    }

    public void Reset()
    {
        Game_ID = "";
        Game_Name = "";
        Game_pos_map = new string[25];
        for (int i = 0; i < Game_pos_map.Length; i++) Game_pos_map[i] = "";
    }
}
