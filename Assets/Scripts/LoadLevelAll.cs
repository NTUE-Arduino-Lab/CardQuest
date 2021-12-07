using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class LoadLevelAll : MonoBehaviour
{
    [Header("載入相關")]
    public ProgressController progressController;
    public GameObject loadScreen;

    [Header("題目展示")]
    public GameObject Content_G;
    public GameObject buttonTemplate;
    
    public struct Game
    {
        public string ID;
        public string Name;
        public string[] pos_map;
    }
    List<Game> allGames = new List<Game>();

    void Start()
    {
        StartCoroutine(ReadAll());
    }

    IEnumerator ReadAll()
    {
        loadScreen.SetActive(true);
        loadScreen.GetComponent<CanvasGroup>().alpha = 1f;

        int displayProgress = 0;
        int toProgress = 0;

        WWWForm form = new WWWForm();

        //傳輸的資料
        form.AddField("method", "read_all"); //read_all取得所有資料
        UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/a/grad.ntue.edu.tw/macros/s/AKfycbx60_AnB6P9pPqvv3ltoBzZRCSXpbOP9RhCeJ9twR_hWpoMkd4DVOa47UBqR7HVErcOPg/exec", form);
        www.SendWebRequest();

        while (!www.isDone)
        {
            //print("www.downloadProgress" + www.downloadProgress);
            toProgress = (int)www.downloadProgress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                progressController.setProgess(displayProgress);
                yield return new WaitForEndOfFrame();
            }
            //progressController.setProgess(www.downloadProgress);
            yield return new WaitForFixedUpdate();
        }
        if (www.isNetworkError || www.isHttpError) //錯誤檢查
        {
            Debug.Log(www.error);
            loadScreen.transform.GetChild(0).gameObject.SetActive(false);
            loadScreen.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "載入失敗!";
            loadScreen.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            //print(www.downloadHandler.text);//讀取回傳ㄉ資料
            string output = www.downloadHandler.text;
            output = output.Replace("[", "");
            output = output.Substring(0, output.Length - 2);
            string[] GetData_row = Regex.Split(output, "],", RegexOptions.IgnoreCase);

            for (int i = 0; i < GetData_row.Length; i++)
            {
                GetData_row[i] = GetData_row[i].Trim();//移除所有出現在目前字串開頭和結尾的指定字元集
                string[] single_int = GetData_row[i].Split(','); //separating integers by ","
                var cd = new Game();
                for (int j = 0; j < single_int.Length; j++)
                {
                    if (j == 0)
                    {
                        cd.ID = single_int[j].TrimStart('"').TrimEnd('"');
                        cd.pos_map = new string[25];

                    }
                    else if (j == 1)
                    {
                        cd.Name = single_int[j].TrimStart('"').TrimEnd('"');
                    }
                    else
                    {
                        cd.pos_map[j - 2] = single_int[j].TrimStart('"').TrimEnd('"');
                    }
                }
                allGames.Add(cd);
            }
            if (www.isDone) //完成
            {
                //print("www.isDone" + www.downloadProgress);
                toProgress = (int)www.downloadProgress * 100;
                while (displayProgress < toProgress)
                {
                    ++displayProgress;
                    progressController.setProgess(displayProgress);
                    //print(displayProgress);
                    yield return new WaitForEndOfFrame();
                }
                //progressController.setProgess(1);
                int N = allGames.Count;
                for (int i = 0; i < N; i++)
                {
                    GameObject g = Instantiate(buttonTemplate, Content_G.transform);
                    g.GetComponent<LevelButton>().SetContent(allGames[i].ID, allGames[i].Name, allGames[i].pos_map);
                }

                buttonTemplate.SetActive(false);

                StartCoroutine(LoadScreenFadeOut());
            }
        }
    }

    IEnumerator LoadScreenFadeOut()
    {
        CanvasGroup canvasGroup = loadScreen.GetComponent<CanvasGroup>();

        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= 0.03f;
            yield return null;
        }

        loadScreen.SetActive(false);
    }
    public void Reload()
    {
        loadScreen.transform.GetChild(1).gameObject.SetActive(false);
        loadScreen.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(ReadAll());
    }
}
