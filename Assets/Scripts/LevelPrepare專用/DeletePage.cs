using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DeletePage : MonoBehaviour
{

    public void delete()
    {
        PrepareQuizHolder prepareQuizHolder = GameObject.Find("PrepareQuizHolder").GetComponent<PrepareQuizHolder>();
        StartCoroutine(Delete(prepareQuizHolder.Game_ID));
    }

    IEnumerator Delete(string id)
    {
        WWWForm form = new WWWForm();
        //傳輸的資料
        form.AddField("method", "delete"); //read取得特定資料
        form.AddField("id", id);

        UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/a/grad.ntue.edu.tw/macros/s/AKfycbwzuKnjhxDNGBDjr4e2br9Aqgfeg99HnmkH7xlV9P5e3cr_dflv0CQ9JuhtJakNWSfiVg/exec", form);
        www.SendWebRequest();
        while (!www.isDone)
        {
            //progressController.setProgess(www.downloadProgress);
            this.transform.GetChild(3).GetComponent<Text>().text = "刪除中...";
            yield return new WaitForFixedUpdate();
        }
        if (www.isNetworkError || www.isHttpError) //錯誤檢查
        {
            Debug.Log(www.error);
            this.transform.GetChild(3).GetComponent<Text>().text = "刪除失敗!";
        }
        else
        {
            print(www.downloadHandler.text);//讀取回傳ㄉ資料
            if (www.isDone) //完成
            {
                //progressController.setProgess(1);
                print("刪除成功");
                this.transform.GetChild(3).GetComponent<Text>().text = "刪除成功!";

                yield return new WaitForSeconds(1); //顯示刪除成功字樣後，移除原本的quizctrl，重新載入這個scene

                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            }
        }
    }
    
}
