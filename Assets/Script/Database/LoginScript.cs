using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour {

	public InputField email = null;
	public InputField password = null;
    [SerializeField]
	private string url = "";
    [SerializeField]
	private string sceneCharge = null;

    public GameObject panelLogin = null;
    public GameObject panelMessage = null;
    public Text txtMessage = null;
    
	public void LoginAc(){
		
		string mail = email.text;
		string pwd = password.text;
        StartCoroutine(SubmitLogin (mail, pwd));
	}

	IEnumerator SubmitLogin(string mail, string pwd){
        string newurl = url + "?email=" + UnityWebRequest.EscapeURL(mail) 
                            + "&password=" + UnityWebRequest.EscapeURL(pwd) 
                            + "&game=" + UnityWebRequest.EscapeURL("snake");
        UnityWebRequest data = UnityWebRequest.Get(newurl);
        yield return data.SendWebRequest();

        switch (data.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                OpenMessage();
                txtMessage.text = "Erreur dans le Pseudo et/ou Mot de passe \n Veuillez vérifier !";
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError(": HTTP Error: " + data.error);
                break;
            case UnityWebRequest.Result.Success:
                JSONNode parsedata = JSON.Parse(data.downloadHandler.text);
                if (parsedata["user"] != null)
                {
                    PlayerPrefs.SetInt("id", parsedata["user"]["id"]);
                    PlayerPrefs.SetString("pseudo", parsedata["user"]["pseudo"]);
                    PlayerPrefs.SetInt("score", parsedata["user"]["score"]);
                    PlayerPrefs.SetString("email", parsedata["user"]["email"]);

                    SceneManager.LoadScene(sceneCharge);
                }
                else if (parsedata["error"] != null)
                {
                    OpenMessage();
                    txtMessage.text = parsedata["error"];
                }
                else
                {
                    OpenMessage();
                    txtMessage.text = "Problème de connexion a votre compte, essayer à nouveau !";
                }
                break;
        }
    }

    public void OpenMessage() {
        panelLogin.SetActive(false);
        panelMessage.SetActive(true);
    }
}
