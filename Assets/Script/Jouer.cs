using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jouer : MonoBehaviour
{
    [SerializeField]
    private string sceneCharge = null;

    public void PlayGame() {
        Transition.Out(sceneCharge);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
}
