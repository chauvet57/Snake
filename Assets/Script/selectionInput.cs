﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class selectionInput : MonoBehaviour
{
    public EventSystem system = null;
    public InputField inputField = null;
    
    
    void Start() {
        SelectInput();
        system = EventSystem.current;
        
    }

    void Update() {
          if (Input.GetKeyDown(KeyCode.Tab)) {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null) {
             
                InputField inputfield = next.GetComponent<InputField>();
                if (inputfield != null){
                    inputfield.OnPointerClick(new PointerEventData(system));
                }
                system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
            }
            SelectInput();
        }
    }

    public void SelectInput() {
        EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
        inputField.OnPointerClick(new PointerEventData(EventSystem.current));
    }
}
