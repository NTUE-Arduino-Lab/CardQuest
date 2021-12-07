using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public string sceneName;

    
    public void Load()
    {
        string this_scene = GameObject.Find("LevelLoader").GetComponent<LevelLoader>().getSceneName();
        if (this_scene != "MainMenu")
        {
            GameObject quizCtrl = GameObject.Find("QuizCtrl");
            if(quizCtrl) quizCtrl.GetComponent<QuizCtrl>().Reset();
        }
        GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadScene(sceneName);
    }
    public void ToGameScene()
    {
       GameObject.Find("LevelLoader").GetComponent<LevelLoader>().LoadScene(sceneName);
    }
}
