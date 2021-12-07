using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    /// <summary>
    /// 功能一：此腳本以SceneManager.sceneLoaded紀錄已經呼叫過的場景，以提升老舊設備運行時切換場景效能
    /// 功能二：呼叫轉場
    /// </summary>

    public static bool isClone = false; //用來確定是否已經有dont destroy 的 level loader

    private void Start()
    {
        if (isClone == true) 
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            isClone = true;
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(_LoadScene(sceneName));
    }
    public IEnumerator _LoadScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        while(!asyncLoad.isDone)
        {
            if(asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            yield return null;
        }
        yield return null;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        print(scene.name);
    }
    public string getSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
