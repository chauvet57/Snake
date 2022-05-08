using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChangeEmail : MonoBehaviour {
    public InputField emailTxt = null;
    public InputField emailConfirmTxt = null;
    public GameObject uichangementEmail = null;
    public GameObject uiMess = null;
    public Text textPass = null; 
    [SerializeField]
    private string url = "";
    
    public void ChangementEmail () {

        string email = emailTxt.text;
        int idUser = PlayerPrefs.GetInt("id");
        string emailConfirm = emailConfirmTxt.text;
        
        StartCoroutine (ModificationMdp (email, idUser,emailConfirm));
    }

    IEnumerator ModificationMdp (string email, int id,string emailConfirm) {

    if (email == emailConfirm) {

        
        string newurl = url + "?_email=" + UnityWebRequest.EscapeURL (email) 
                            + "&_id=" + UnityWebRequest.EscapeURL (id.ToString());

        //creer une url qui va appeller login.php
        UnityWebRequest data = UnityWebRequest.Get (newurl);

        yield return data.SendWebRequest ();

        JSONNode parsedata = JSON.Parse (data.downloadHandler.text);
        Debug.Log(parsedata["res"].Value);
        if (parsedata["res"].Value == "1")
                    {
                        textPass.text = "Adresse Email changé avec succès\n Good Game !!";
                        uiMess.SetActive(true);
                        StartCoroutine(redirectOption());
                    }
                    else{

                        textPass.text = "Erreur lors du changement\n Veuillez réessayer !!";
                        uiMess.SetActive(true);
                        StartCoroutine(closeMess());
                    }
         } else {
            //Debug.Log ("error mdp non identique !!");
            textPass.text = "Erreur adresse Email non identique\n Veuillez vérifier !! ";
            uiMess.SetActive(true);
            StartCoroutine(closeMess());
        }
    }

    IEnumerator closeMess()
    {
     yield return new WaitForSeconds(2);
        uiMess.SetActive(false);
    }

     IEnumerator redirectOption()
    {
     yield return new WaitForSeconds(2);
        uiMess.SetActive(false);
        uichangementEmail.SetActive(false);
    }




}