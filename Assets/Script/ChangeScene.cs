using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string sceneCharge = null;

    public void MenuGame() {
        FindObjectOfType<SnakeController>().Toggle();
        SceneManager.LoadScene(sceneCharge);
        Cursor.visible = true;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneCharge);
        Cursor.visible = true;
    }
}
