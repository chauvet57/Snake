using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChangeMdp : MonoBehaviour {
    [SerializeField]
    private InputField passwordOld = null;
    [SerializeField]
    private InputField passwordNew = null;
    [SerializeField]
    private InputField passwordNewConfirm = null;
    [SerializeField]
    private GameObject panelChangementMdp = null;
    [SerializeField]
    private GameObject panelMessageChangementMdp = null;
    [SerializeField]
    private Text txtMessageChangementMdp = null; 
    [SerializeField]
    private string url = "";
    
    public void ChangementMdp () {
        string pwdOld = passwordOld.text;
        string pwdNew = passwordNew.text;
        string pwdConfirm = passwordNewConfirm.text;
        int idUser = PlayerPrefs.GetInt("id");
        
        StartCoroutine (ModificationMdp (pwdOld, pwdNew, pwdConfirm, idUser));
    }

    IEnumerator ModificationMdp (string pwdOld,string pwdNew,string pwdConfirm, int id) {

        if (pwdNew == pwdConfirm) {
            string newurl = url + "?passwordOld=" + UnityWebRequest.EscapeURL (pwdOld) 
                                + "&passwordNew=" + UnityWebRequest.EscapeURL (pwdNew) 
                                + "&id=" + UnityWebRequest.EscapeURL (id.ToString());
            UnityWebRequest data = UnityWebRequest.Get (newurl);
            yield return data.SendWebRequest ();

            JSONNode parsedata = JSON.Parse (data.downloadHandler.text);
            if (parsedata["success"] != null) {
                OpenMessage();
                txtMessageChangementMdp.text = parsedata["success"];
            } else if (parsedata["error"] != null) {
                OpenMessage();
                txtMessageChangementMdp.text = parsedata["error"];
            } else {
                OpenMessage();
                txtMessageChangementMdp.text = "Erreur lors du changement du mot de passe veuillez vérifier!";
            }
        }
    }

    public void OpenMessage() {
        panelChangementMdp.SetActive(false);
        panelMessageChangementMdp.SetActive(true);
    }
}