using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;


public class Insert : MonoBehaviour
{
    [SerializeField]
    private InputField pseudoTxt = null;
    [SerializeField]
    private InputField passwordTxt = null;
    [SerializeField]
    private InputField passwordConfirmTxt = null;
    [SerializeField]
    private InputField emailTxt = null;
    [SerializeField]
    private GameObject panelMessageRegister = null;
    [SerializeField]
    private GameObject panelRegister = null;
    [SerializeField]
    private Text txtMessageRegister = null; 
    [SerializeField]
    private string url = "";
    [SerializeField]
    private string sceneCharge = null;

    bool IsValidEmail(string email) {
        return Regex.IsMatch(email, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z");
    }

    public void insertUsers() {

        string pseudo = pseudoTxt.text;
        string password = passwordTxt.text;
        string passwordConfirm = passwordConfirmTxt.text;
        string email = emailTxt.text;

        StartCoroutine(AddUser(pseudo,password,passwordConfirm,email)); 
    }

    IEnumerator AddUser(string pseudo, string password, string passwordConfirm, string email) {

        if (pseudo != "" && password != "" && passwordConfirm != "" && email !="" ) {
            if(IsValidEmail(email) == true) {
                if (password == passwordConfirm) { 
                    string newurl = url + "?pseudo=" + UnityWebRequest.EscapeURL(pseudo) 
                                        + "&password=" + UnityWebRequest.EscapeURL(password) 
                                        + "&email=" + UnityWebRequest.EscapeURL(email);
                    UnityWebRequest data = UnityWebRequest.Get(newurl);
                    
                    yield return data.SendWebRequest();
                    
                    JSONNode parsedata = JSON.Parse(data.downloadHandler.text);
                    if (parsedata["user"] != null) {
                        PlayerPrefs.SetInt("id", int.Parse(parsedata["user"]["id"]));
                        PlayerPrefs.SetString("pseudo",pseudo);
                        PlayerPrefs.SetInt("score", 0);
                        SceneManager.LoadScene(sceneCharge);
                    } else if (parsedata["error"] != null) {
                        OpenMessage();
                        txtMessageRegister.text = parsedata["error"];
                    } else {
                        OpenMessage();
                        txtMessageRegister.text = "Problème de connexion, essayer à nouveau !";
                    }
                } else {
                    OpenMessage();
                    txtMessageRegister.text = "Les mots de passe ne sont pas indentique !!!"; 
                }
            } else {
                OpenMessage();
                txtMessageRegister.text = "Adresse Email non valide !!!";
            }
        } else {
            OpenMessage();
            txtMessageRegister.text = "Tout les champs ne sont pas rempli !!!";
        }
    }

    public void OpenMessage() {
        panelRegister.SetActive(false);
        panelMessageRegister.SetActive(true);
    }
}
