using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuizCtrl : MonoBehaviour
{
    public string Game_ID;//id
    public string Game_Name;//名字
    public string[] Game_pos_map;
    public static bool QuizCtrl_isClone = false; //用來確定是否已經有dont destroy 的 QuizCtrl

    void Awake()
    {
        Game_pos_map = new string[25];

        for (int i = 0; i < Game_pos_map.Length; i++) Game_pos_map[i] = "";
    }

    private void Start()
    {
        if (QuizCtrl_isClone == true)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            QuizCtrl_isClone = true;
        }
    }

    public void send(string id,string Name, string[] pos_map)
    {
        Game_ID = id;
        Game_Name = Name;
        Game_pos_map = pos_map;
    }

    public void Reset()
    {
        Game_ID = "";
        Game_Name = "";
        Game_pos_map = new string[25];
        for (int i = 0; i < Game_pos_map.Length; i++) Game_pos_map[i] = "";
    }
}
