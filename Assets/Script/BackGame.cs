using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGame : MonoBehaviour
{
    public void backGame() {
        SnakeController.FindObjectOfType<SnakeController>().Toggle();
        Cursor.visible = false;
    }
}
