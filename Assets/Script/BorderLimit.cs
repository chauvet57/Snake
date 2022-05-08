using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderLimit : MonoBehaviour
{
    /*private void OnCollisionEnter(Collision collision)
    {
        //si une collision detecte player 
        if (collision.gameObject.tag == "Player")
        {
            //alors on bloque le jeu
            Time.timeScale = 0f;

            //recuperation de la fonction perdu dans le script snake controller
            SnakeController sc = collision.gameObject.GetComponentInParent<SnakeController>();
            sc.Loose();
        }
    }*/

    private void OnCollisionStay(Collision collision)
    {
        //si une collision detecte player 
        if (collision.gameObject.tag == "Player")
        {
            //alors on bloque le jeu
            Time.timeScale = 0f;

            //recuperation de la fonction perdu dans le script snake controller
            SnakeController sc = collision.gameObject.GetComponentInParent<SnakeController>();
            sc.Loose();
        }
    }
}
