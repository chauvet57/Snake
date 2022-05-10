using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Result : MonoBehaviour
{
    private int score;
    private int scoreNew;
    private int idUser;
    [SerializeField]
    private string url = "";

    [SerializeField]
    private Text TxtResultat = null;
    [SerializeField]
    private Text TxtResultatScore = null;

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("score");
        scoreNew = PlayerPrefs.GetInt("scoreNew");
        idUser = PlayerPrefs.GetInt("id");

        if (scoreNew > score)
        {
            TxtResultat.text = "Bien jouer";
            TxtResultatScore.text = "Nouveau Record !!\n Votre score est de : " + scoreNew;
            PlayerPrefs.SetInt("score", scoreNew);
            StartCoroutine(SaveScore());
        }
        else
        {
            TxtResultat.text = "Perdu";
            TxtResultatScore.text = "Dommage !!\n Votre score est de : " + scoreNew;
        }
    }

    IEnumerator SaveScore()
    {
        string newurl = url + "?id=" + UnityWebRequest.EscapeURL(idUser.ToString())
                            + "&score=" + UnityWebRequest.EscapeURL(scoreNew.ToString())
                            + "&game=" + UnityWebRequest.EscapeURL("snake");

        //creer une url qui va appeller /score
        UnityWebRequest data = UnityWebRequest.Get(newurl);
        yield return data.SendWebRequest();
    }


}
