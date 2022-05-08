using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{


    [SerializeField]
    private int appelGold = 5;
    [SerializeField]
    private int appelClassic = 1;
    [SerializeField]
    private int appelRotten = 3;

    private void OnCollisionEnter(Collision collision) {

        if (gameObject.tag == "AppelGold") {
            //si une collision detecter avec un objet taggé player
            if (collision.gameObject.tag == "Player") { 
                //alors on recup le script snakeController pour lui ajouter un body
                SnakeController sc = collision.gameObject.GetComponentInParent<SnakeController>();
                sc.AddBodyPart(appelGold);
                
                //puis on detruit l'aliment touché
                Destroy(this.gameObject);
            }
        }

        if (gameObject.tag == "AppelClassic") {
            if (collision.gameObject.tag == "Player") {
                SnakeController sc = collision.gameObject.GetComponentInParent<SnakeController>();
                sc.AddBodyPart(appelClassic);
                Destroy(this.gameObject);
            }
        }

        if (gameObject.tag == "AppelRotten") {
            if (collision.gameObject.tag == "Player") {
                SnakeController sc = collision.gameObject.GetComponentInParent<SnakeController>();
                sc.AddBodyPart(appelRotten);
                Destroy(this.gameObject);
            }
        }
    }
}
