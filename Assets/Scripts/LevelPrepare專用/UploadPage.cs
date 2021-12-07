using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UploadPage : MonoBehaviour {

    string googlesheetURL = "https://script.google.com/a/grad.ntue.edu.tw/macros/s/AKfycbx60_AnB6P9pPqvv3ltoBzZRCSXpbOP9RhCeJ9twR_hWpoMkd4DVOa47UBqR7HVErcOPg/exec";

    void Start()
    {
        transform.Find("UploadButton").GetComponent<Button>().onClick.AddListener(UploadLevel);
    }

    private void Update()
    {
        if (WebRequest.isLoading)
        {
            transform.Find("SignText").GetComponent<Text>().text = "上傳中...";
        }
    }
    private void OnEnable()
    {
        PrepareQuizHolder prepareQuizHolder = GameObject.Find("PrepareQuizHolder").GetComponent<PrepareQuizHolder>();
        transform.Find("LevelNameField").GetComponent<InputField>().text = prepareQuizHolder.Game_Name;
    }

    void UploadLevel()
    {
        string levelName = transform.Find("LevelNameField").GetComponent<InputField>().text;
        if(levelName == "") { }
        else if (levelName.Contains(",") || levelName.Contains("[") || levelName.Contains("]") || levelName.Contains("\"") || levelName.Contains("\'"))
        {
            StartCoroutine(UploadFalse("請勿輸入含 , [ ] \" \' 等特殊符號！"));
        }
        else
        {
            WriteData(levelName, PrepareNodeCtrl.GET_nodeStatusArray());
            transform.Find("LevelNameField").GetComponent<InputField>().text = "";
        }
    }

    void WriteData(string levelname, string[] leveldata)
    {
        transform.Find("SignText").GetComponent<Text>().text = "上傳中...";

        PrepareQuizHolder prepareQuizHolder = GameObject.Find("PrepareQuizHolder").GetComponent<PrepareQuizHolder>();
        string Game_ID = prepareQuizHolder.Game_ID;

        StartCoroutine(Write(Game_ID, levelname, leveldata));
    }

    IEnumerator Write(string _Game_ID, string _levelname, string[] _leveldata)
    {
        WWWForm form = new WWWForm();
        //傳輸的資料

        if(_Game_ID == "")
        {
            form.AddField("method", "write");
            form.AddField("id", WebRequest.GetUniqueIdentifier().ToString());
            form.AddField("name", _levelname);
        }
        else
        {
            form.AddField("method", "put");
            form.AddField("id", _Game_ID);
            form.AddField("new_name", _levelname);
        }

        //設定位置

        //將陣列轉成字串才能傳到google app script
        string str = "[";
        for (var i = 0; i < _leveldata.Length; i++)
        {
            if (i != _leveldata.Length - 1)
            {
                str = str + "'" + _leveldata[i].ToString() + "',";
            }
            else
            {
                str = str + "'" + _leveldata[i].ToString() + "'";
            }
        }
        str = str + "]";

        form.AddField("map", str);

        UnityWebRequest www = UnityWebRequest.Post(googlesheetURL, form);
        www.SendWebRequest();
        while (!www.isDone)
        {
            //進度條
            //progressController.setProgess(www.downloadProgress);
            yield return new WaitForFixedUpdate();
        }
        if (www.isNetworkError || www.isHttpError) //錯誤檢查
        {
            Debug.Log(www.error);
            StartCoroutine(UploadFalse(www.error));
        }
        else
        {
            print(www.downloadHandler.text);//讀取回傳ㄉ資料
            print(str);
        
            if (www.isDone) //完成
            {
                //progressController.setProgess(1);
                StartCoroutine(UploadSuccess());
            }
        }
    }

    IEnumerator UploadSuccess()
    {
        transform.Find("SignText").GetComponent<Text>().text = "上傳成功";
        yield return new WaitForSeconds(1f);
        transform.Find("SignText").GetComponent<Text>().text = "";

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

    }
    IEnumerator UploadFalse(string error)
    {
        transform.Find("SignText").GetComponent<Text>().text = error;
        yield return new WaitForSeconds(5f);
        transform.Find("SignText").GetComponent<Text>().text = "";
    }
}
