using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public string sceneName;
    private GameObject llobj;
    void Start()
    {
        llobj = GameObject.Find("LevelLoader").gameObject;
        LevelLoader ll = llobj.GetComponent<LevelLoader>();
        StartCoroutine(ll.LoadScene(sceneName));
    }
}
