using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySnakeController : MonoBehaviour
{

    [Header("Life variables")]
    [SerializeField]
    private float timeImmunised = 5f;
    private float currentTime;
    private bool canLoose;


    
    void Start() {
        //initialisation du temps de l'immunite
        currentTime = 0f;
        canLoose = false;
    }

    
    void Update() {
        //retire l'immunite au bout de 5 secondes
        currentTime += Time.deltaTime;
        if (currentTime > timeImmunised)
        {
            canLoose = true;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        //si une collision detecte player / player et qu'il y a plus immunite
        if (collision.gameObject.tag == "Player" && canLoose == true)
        {
            //alors on bloque le jeu
            //Time.timeScale = 0f;

            //recuperation de la fonction perdu dans le script snake controller
            SnakeController sc = collision.gameObject.GetComponentInParent<SnakeController>();
            sc.Loose();
        }
    }
}
