using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Classement : MonoBehaviour
{
    [SerializeField]
    private string URL = "";
    [SerializeField]
    private Transform ligneClassement = null;
    [SerializeField]
    private GameObject uiClassement = null;
    [SerializeField]
    private Transform contentListParent = null;
    public List<string> classement = new List<string>();
    

    public void ClassementList()
    {
        uiClassement.SetActive(true);
        
        StartCoroutine(StartClassement());
    }

    IEnumerator StartClassement()
    {
       string newurl = URL + "?game=" + UnityWebRequest.EscapeURL("snake");
       UnityWebRequest data = UnityWebRequest.Get(newurl);
        
        yield return data.SendWebRequest();

        JSONNode parsedata = JSON.Parse(data.downloadHandler.text);
        
        float ligneHeight = 60f;
        for (int i = 0; i < parsedata.Count; i++)
        {
            Transform transformClassement = Instantiate(ligneClassement, contentListParent);
            RectTransform rectTransformClassement = transformClassement.GetComponent<RectTransform>();
            rectTransformClassement.anchoredPosition = new Vector2(0, -ligneHeight * i);

            transformClassement.Find("Position").GetComponent<Text>().text = (i + 1).ToString();
            transformClassement.Find("Score").GetComponent<Text>().text = parsedata[i]["score"].Value;
            transformClassement.Find("Pseudo").GetComponent<Text>().text = parsedata[i]["pseudo"].Value;
            transformClassement.Find("Date").GetComponent<Text>().text = parsedata[i]["date"].Value;
        }
        ligneClassement.gameObject.SetActive(false);
    }

    
}
