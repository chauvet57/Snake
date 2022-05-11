using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class selectionInput : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjects;
    private int i = 0;

    void Start()
    {
        SelectInput();
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            if ( i >= gameObjects.Length - 1)
            {
                i = 0;
            }
            else
            {
                i++;
            }
            SelectInput();
        }
    }

    public void SelectInput() 
    {
        gameObjects = GameObject.FindGameObjectsWithTag("input");
        EventSystem.current.SetSelectedGameObject(gameObjects[i], null);
    }
}
