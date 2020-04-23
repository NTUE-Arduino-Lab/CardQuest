using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private void Start()
    {
        print(SceneManager.GetActiveScene().name);
        StartCoroutine(LoadScene("Menu"));
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            StartCoroutine(LoadScene("Level1"));
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public IEnumerator LoadScene(string sceneName)
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
