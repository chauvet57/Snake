using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    //panel Login
    [SerializeField]
    private GameObject panelInscription = null;
    [SerializeField]
    private GameObject panelRestoreMdp = null;
    [SerializeField]
    private GameObject panelLogin = null;
    [SerializeField]
    private GameObject panelMessage = null;
    [SerializeField]
    private GameObject panelMessageRegister = null;
    [SerializeField]
    private GameObject panelMessageRestore = null;
    //panel Menu
    [SerializeField]
    private GameObject panelClassement = null;
    [SerializeField]
    private GameObject panelOption = null;
    [SerializeField]
    private GameObject panelMenu = null;
    [SerializeField]
    private GameObject panelChangementMdp = null;
    [SerializeField]
    private GameObject panelSupport = null;
    [SerializeField]
    private GameObject panelMessageSupport = null;
    [SerializeField]
    private GameObject panelMessageChangementMdp = null;

    // action panel login
    public void Register() {
        panelLogin.SetActive(false);
        panelInscription.SetActive(true);
    }

    public void RestorePassword() {
        panelLogin.SetActive(false);
        panelRestoreMdp.SetActive(true);
    }

    public void RegisterToLogin() {
        panelLogin.SetActive(true);
        panelInscription.SetActive(false);
    }

    public void Login() {
        panelLogin.SetActive(false);
    }

    public void MessageToLogin() {
        panelLogin.SetActive(true);
        panelMessage.SetActive(false);
    }

    public void RestoreToLogin() {
        panelLogin.SetActive(true);
        panelRestoreMdp.SetActive(false);
    }

    public void MessageRegisterToRegister() {
        panelInscription.SetActive(true);
        panelMessageRegister.SetActive(false);
    }

    public void MessageRestoreToRestore() {
        panelRestoreMdp.SetActive(true);
        panelMessageRestore.SetActive(false);
    }

    // action panel menu
    public void ClassementClose() {
        panelClassement.SetActive(false);
    }

    public void OptionsOpen() {
        panelOption.SetActive(true);
        panelMenu.SetActive(false);
    }

    public void OptionsToMenu() {
        panelOption.SetActive(false);
        panelMenu.SetActive(true);
    }

    public void ChangementMdpOpen() {
        panelOption.SetActive(false);
        panelChangementMdp.SetActive(true);
    }

    public void ChangementMdpToMenu() {
        panelOption.SetActive(true);
        panelChangementMdp.SetActive(false);
    }

    public void MessageChangementMdpToChangementMdp() {
        panelMessageChangementMdp.SetActive(false);
        panelChangementMdp.SetActive(true);
    }

    public void MessageSupportToSupport() {
        panelMessageSupport.SetActive(false);
        panelSupport.SetActive(true);
    }

    public void SupportOpen() {
        panelOption.SetActive(false);
        panelSupport.SetActive(true);
    }

    public void SupportToMenu() {
        panelOption.SetActive(true);
        panelSupport.SetActive(false);
    }
}

