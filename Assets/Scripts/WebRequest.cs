using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class WebRequest : MonoBehaviour
{
    //[SerializeField] ProgressController progressController;
    //[SerializeField] GameObject loadScreen;
    //可能要限定檔名不能有,[]之類的!
    //string[] pos_map = new string[25];
    string[,] my_matrics;

    static public bool isLoading = false;
    void Start()
    {
        //print("start");
        //loadScreen.transform.localScale = new Vector3(1, 1, 1);
    }

    public void WriteData(string levelname, string[] leveldata)
    {
        StartCoroutine(Write(levelname, leveldata));
        Debug.Log(leveldata[24]);
    }

    IEnumerator Write(string levelname, string[] leveldata)
    {
        isLoading = true;

        WWWForm form = new WWWForm();
        //傳輸的資料
        form.AddField("method", "write");
        form.AddField("id", GetUniqueIdentifier().ToString());
        form.AddField("name", levelname);
        //設定位置

        //將陣列轉成字串才能傳到google app script
        string str ="[";
        for (var i = 0; i < leveldata.Length; i++)
        {
            if (i!= leveldata.Length-1)
            {
                str = str + "'" + leveldata[i].ToString() + "',";
            }
            else
            {
                str = str + "'" + leveldata[i].ToString() + "'";
            }
        }
        str = str + "]";
        //print(str);
        form.AddField("map", str);
        UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/a/grad.ntue.edu.tw/macros/s/AKfycbwzuKnjhxDNGBDjr4e2br9Aqgfeg99HnmkH7xlV9P5e3cr_dflv0CQ9JuhtJakNWSfiVg/exec", form);
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
        }
        else
        {
            print(www.downloadHandler.text);//讀取回傳ㄉ資料
        }
        if (www.isDone) //完成
        {
            //progressController.setProgess(1);
            isLoading = false;
        }
        
    }
    IEnumerator Read(string id)
    {
        isLoading = true; 

        WWWForm form = new WWWForm();
        //傳輸的資料
        form.AddField("method", "read"); //read取得特定資料
        form.AddField("id", id);
        form.AddField("name", "李淯萱");
        UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/a/grad.ntue.edu.tw/macros/s/AKfycbwzuKnjhxDNGBDjr4e2br9Aqgfeg99HnmkH7xlV9P5e3cr_dflv0CQ9JuhtJakNWSfiVg/exec", form);
        www.SendWebRequest();
        while (!www.isDone)
        {
            //progressController.setProgess(www.downloadProgress);
            yield return new WaitForFixedUpdate();
        }
        if (www.isNetworkError || www.isHttpError) //錯誤檢查
        {
            Debug.Log(www.error);
        }
        else
        {
            print(www.downloadHandler.text);//讀取回傳ㄉ資料
            string output = www.downloadHandler.text;
            string[] GetData = output.TrimStart('[').TrimEnd(']').Split(',');
            print(GetData[1].TrimStart('"').TrimEnd('"'));
        }
        if (www.isDone) //完成
        {
            //progressController.setProgess(1);
            isLoading = false;
        }
    }

    IEnumerator Put(string id, string levelname, string[] leveldata)
    {

        WWWForm form = new WWWForm();
        //傳輸的資料
        form.AddField("method", "put");
        //抓id
        form.AddField("id", id);

        //form.AddField("name", "左上&右上");//現在的名字(因為沒有id所以目前抓欄位是用名字)
        //新名字
        form.AddField("new_name", levelname);
        //更新的位置資訊
        //將陣列轉成字串才能傳到google app script
        string str = "[";
        for (var i = 0; i < leveldata.Length; i++)
        {
            if (i != leveldata.Length - 1)
            {
                str = str + "'" + leveldata[i].ToString() + "',";
            }
            else
            {
                str = str + "'" + leveldata[i].ToString() + "'";
            }
        }
        str = str + "]";
        //print(str);
        form.AddField("map", str);
        UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/a/grad.ntue.edu.tw/macros/s/AKfycbwzuKnjhxDNGBDjr4e2br9Aqgfeg99HnmkH7xlV9P5e3cr_dflv0CQ9JuhtJakNWSfiVg/exec", form);
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
        }
        else
        {
            print(www.downloadHandler.text);//讀取回傳ㄉ資料
        }
        if (www.isDone) //完成
        {
            //progressController.setProgess(1);
           // isLoading = false;
        }
    }
    IEnumerator Delete(string id)
    {
        WWWForm form = new WWWForm();
        //傳輸的資料
        form.AddField("method", "delete"); //read取得特定資料
        form.AddField("id", id);
        //form.AddField("name", name);
        UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/a/grad.ntue.edu.tw/macros/s/AKfycbwzuKnjhxDNGBDjr4e2br9Aqgfeg99HnmkH7xlV9P5e3cr_dflv0CQ9JuhtJakNWSfiVg/exec", form);
        www.SendWebRequest();
        while (!www.isDone)
        {
            //progressController.setProgess(www.downloadProgress);
            yield return new WaitForFixedUpdate();
        }
        if (www.isNetworkError || www.isHttpError) //錯誤檢查
        {
            Debug.Log(www.error);
        }
        else
        {
            print(www.downloadHandler.text);//讀取回傳ㄉ資料
            this.transform.GetChild(3).GetComponent<Text>().text = "刪除失敗!";
        }
        if (www.isDone) //完成
        {
            //progressController.setProgess(1);
            print("刪除成功");
            this.transform.GetChild(3).GetComponent<Text>().text = "刪除成功!";
            yield return new WaitForSeconds(1); //顯示刪除成功字樣後，移除原本的quizctrl，重新載入這個scene
            //Destroy(GameObject.Find("QuizCtrl"));

            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
    public void delete()
    {
        QuizCtrl quizctrl = GameObject.Find("QuizCtrl").GetComponent<QuizCtrl>();
        print("已刪除: "+quizctrl.Game_ID);
        StartCoroutine(Delete(quizctrl.Game_ID));
    }
    //產生隨機ID
    public static string GetUniqueIdentifier()
    {
        return System.Guid.NewGuid().ToString();
    }
}
