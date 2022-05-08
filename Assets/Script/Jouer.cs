using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jouer : MonoBehaviour
{
    [SerializeField]
    private string SceneCharge = null;

    public void PlayGame() {
        SceneManager.LoadScene(SceneCharge);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
}
