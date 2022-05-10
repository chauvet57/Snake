using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using System.Text.RegularExpressions;

public class recupMdp : MonoBehaviour
{

    [SerializeField]
    private Text email = null;
    [SerializeField]
    private string url = "";
    [SerializeField]
    private GameObject panelMessageRestore = null;
    [SerializeField]
    private GameObject panelRestoreMdp = null;
    [SerializeField]
    private Text txtMessageRestore = null;

    public void controlMail() {
        
        if(email.text != "" && IsValidEmail(email.text) == true){
            string mail = email.text;
            StartCoroutine(ResetPassword(mail));
        } else {
            OpenMessage();
            txtMessageRestore.text = "Veuillez saisir votre adresse email valide !";
        }
    }

    IEnumerator ResetPassword(string mail){

    string newurl = url + "?email=" + UnityWebRequest.EscapeURL(mail)
                        + "&game=" + UnityWebRequest.EscapeURL("snake 3D");
        
        UnityWebRequest data = UnityWebRequest.Get(newurl);
        yield return data.SendWebRequest();

        switch (data.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                OpenMessage();
                txtMessageRestore.text = "Mdp non envoyé il y a une erreur " + data.error;
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(": HTTP Error: " + data.error);
                break;
            case UnityWebRequest.Result.Success:
                JSONNode parsedata = JSON.Parse(data.downloadHandler.text);

                if (parsedata["success"] != null)
                {
                    OpenMessage();
                    txtMessageRestore.text = parsedata["success"] + mail;

                }
                else if (parsedata["error"] != null)
                {
                    OpenMessage();
                    txtMessageRestore.text = parsedata["error"];
                }
                else
                {
                    OpenMessage();
                    txtMessageRestore.text = "Problème de connexion a votre compte, essayer à nouveau !";
                }
                break;
        }
    }

    bool IsValidEmail(string email) {
        return Regex.IsMatch(email, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z");
    }

    public void OpenMessage() {
        panelRestoreMdp.SetActive(false);
        panelMessageRestore.SetActive(true);
    }
}
