using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLimit : MonoBehaviour
{
    [SerializeField]
    private GameObject panelWarning = null;

    private void OnTriggerEnter(Collider collider)
    {
        //si une collision detecte player 
        if (collider.gameObject.tag == "Player")
        {
            panelWarning.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        //si une collision detecte player 
        if (collider.gameObject.tag == "Player")
        {
            panelWarning.SetActive(false);
        }
    }
}
