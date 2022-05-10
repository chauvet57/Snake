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
        Transition.Out(sceneCharge);
        Cursor.visible = true;
    }

    public void LoadScene()
    {
        Transition.Out(sceneCharge);
        Cursor.visible = true;
    }
}
