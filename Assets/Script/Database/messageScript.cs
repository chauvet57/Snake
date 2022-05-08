using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.Networking;

public class messageScript : MonoBehaviour
{
    private string mail;
    private string message;
    [SerializeField]
	private string url = "";
    [SerializeField]
    private Text txtMessage = null;
    public GameObject panelSupport = null;
    public GameObject panelMessageSupport = null;
    public Text txtMessageSupport = null;

    
    public void messageSend() {

        mail = PlayerPrefs.GetString("email");
        message = txtMessage.text;

        if(mail != ""){
            if(message != "") {
                StartCoroutine(SendMessageSupport(mail, message));
            } else {
                OpenMessage();
                txtMessageSupport.text = "Veuillez saisir votre message avant d'envoyer merci.";
            }
        } else {
            OpenMessage();
            txtMessageSupport.text = "Veuillez vous connecter pour envoyer un message au support!";
        }
    }

    IEnumerator SendMessageSupport(string mail, string message){

    string newurl = url + "?email=" + UnityWebRequest.EscapeURL(mail)
                        + "&message=" + UnityWebRequest.EscapeURL(message)
                        + "&game=" + UnityWebRequest.EscapeURL("snake 3D");
        
        UnityWebRequest data = UnityWebRequest.Get(newurl);
        yield return data.SendWebRequest();
        
        if (data.isNetworkError) { 
            OpenMessage();
            txtMessageSupport.text = "Mdp non envoyé il y a une erreur " + data.error;

        } else {
            JSONNode parsedata = JSON.Parse(data.downloadHandler.text);
           
            if (parsedata["success"] != null) {
                OpenMessage();
                txtMessageSupport.text = parsedata["success"] ;

            } else if (parsedata["error"] != null) {
                OpenMessage();
                txtMessageSupport.text = parsedata["error"];
            } else {
                OpenMessage();
                txtMessageSupport.text = "Problème de connexion a votre compte, essayer à nouveau !";
            }
        }
	}

    public void OpenMessage() {
        panelSupport.SetActive(false);
        panelMessageSupport.SetActive(true);
    }
}
