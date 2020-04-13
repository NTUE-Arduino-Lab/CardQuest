using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private void Start()
    {
        print(SceneManager.GetActiveScene().name);
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(LoadScene("Menu"));
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
}
