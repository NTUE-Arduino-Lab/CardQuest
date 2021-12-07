using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UploadButton : MonoBehaviour
{
    public GameObject AlertText;
    public GameObject UploadPage;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(isClicked);
    }

    void isClicked()
    {
        if (Check())
        {
            UploadPage.SetActive(true);
        }
    }

    bool Check()
    {
        string[] nodeStatusArray = PrepareNodeCtrl.GET_nodeStatusArray();

        bool startIsPlaced = false, endIsPlaced = false;
        for (int i = 0; i < nodeStatusArray.Length; i++)
        {
            if (nodeStatusArray[i] == "Start") startIsPlaced = true;
            if (nodeStatusArray[i] == "End") endIsPlaced = true;
        }

        if (startIsPlaced && endIsPlaced) return true;
        else
        {
            string alerText = "";
            if (startIsPlaced == false && endIsPlaced == false) alerText = "Start及End";
            else if (startIsPlaced == false) alerText = "Start";
            else if (endIsPlaced == false) alerText = "End";

            StartCoroutine(Alert(alerText));
        }

        return false;
    }

    IEnumerator Alert(string _text)
    {
        AlertText.GetComponent<Text>().text = "跑道上缺少" + _text;

        AlertText.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        AlertText.SetActive(false);
    }
}
