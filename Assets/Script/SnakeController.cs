using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("Mouvement Variable")]
    private float speedMove = 10f;
    private float speedRotation = 90f;
    private float curveSpeed = 1f;

    [Header("Corp Variable")]
    [SerializeField]
    private GameObject headPart = null;
    [SerializeField]
    private List<Transform> bodyParts = null;
    [SerializeField]
    private GameObject bodyPartPrefab = null;
    private float minDistance = 0.6f;

    [Header("Eating variables")]
    [SerializeField]
    private Text scoreText = null;
    [SerializeField]
    private Text pseudoText = null;
    [SerializeField]
    private Text recordText = null;
    private int score = 0;
    [SerializeField]
    private AudioClip eatingSound = null;
    private new AudioSource audio;

    private int appelClassic = 10;
    private int appelGold = 60;
    private int appelRotten = 15;

    [SerializeField]
    private GameObject panelPause = null;

    private bool isLoose = false;
    private int idUser = -1;
    private int scoreUser = 0;
    private string pseudoUser = "";
    private float res = 0;

    void Start()
    {

        //ajout a la tete du serpent
        bodyParts.Add(headPart.transform);

        //initialisation du score
        score = 0;

        //initialisation defaite
        isLoose = false;

        //recuperation du composant audio
        audio = this.GetComponent<AudioSource>();

        idUser = PlayerPrefs.GetInt("id");
        pseudoUser = PlayerPrefs.GetString("pseudo");
        scoreUser = PlayerPrefs.GetInt("score");

        pseudoText.text = "Pseudo : " + pseudoUser;
        recordText.text = "Record : " + scoreUser;
    }


    void Update()
    {
        MoveBody();
        //detection de la touche pour mettre en pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isLoose == false)
            {
                Toggle();
            }
        }


    }

    public void Toggle()
    {
        //permet d'activer le ui pause ou de le déactiver et vice versa
        panelPause.SetActive(!panelPause.activeSelf);

        //si le menu est activé  le jeu et figé en mode pause
        if (panelPause.activeSelf == true)
        {
            //active le curseur du champs de vision
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    public void AddBodyPart(int _appel)
    {
        StartCoroutine(WaitBodySnake(_appel));
        if (_appel == 1)
        {
            //incrementation du score a chaque pomme avale
            score = score + appelClassic;
        }

        if (_appel == 3)
        {
            //incrementation du score a chaque pomme avale
            score = score - appelRotten;
            speedMove += 0.05f;
        }

        if (_appel == 5)
        {
            //incrementation du score a chaque pomme avale
            score = score + appelGold;
        }

        scoreText.text = "Score : " + score;

        //jouer le son manger
        audio.PlayOneShot(eatingSound);

        //augmentation de la vitesse
        AddSpeed();
    }

    private void AddSpeed()
    {
        if (score / 100 > res)
        {
            res += 1;
            speedMove += 0.2f;
        }
    }

    private void MoveBody()
    {
        //gestion de la translation
        float move = speedMove * Time.deltaTime;
        bodyParts[0].Translate(Vector3.forward * speedMove * Time.deltaTime);

        //gestion de la rotation
        float rotation = Input.GetAxis("Horizontal") * speedRotation * Time.deltaTime;

        bodyParts[0].Rotate(new Vector3(0, rotation, 0));

        //gestion du mouvement des autres parties
        for (int i = 1; i < bodyParts.Count; i++)
        {

            Transform currentBodyPart = bodyParts[i];
            Transform previousBodyPart = bodyParts[i - 1];
            float distance = Vector3.Distance(previousBodyPart.position, currentBodyPart.position);

            //calcul de l'interpolation
            Vector3 newPosition = previousBodyPart.position;
            newPosition.y = bodyParts[0].transform.position.y;
            float t = Time.deltaTime * distance / minDistance * curveSpeed;

            if (t > 0.5f)
            {
                t = 0.5f;
            }
            currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPosition, t);
            currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, previousBodyPart.rotation, t);
        }
    }

    public void Loose()
    {
        isLoose = true;
        Transition.Out("GameOver");
        Cursor.visible = true;
        PlayerPrefs.SetInt("scoreNew", score);
    }

    IEnumerator WaitBodySnake(int _appel)
    {
        for (int i = 0; i < _appel; i++)
        {
            //creation d'une partie du corps, puis on l'ajoute au corp
            GameObject newPartObj = Instantiate(bodyPartPrefab, new Vector3(
                                                                    bodyParts[bodyParts.Count - 1].position.x,
                                                                    bodyParts[bodyParts.Count - 1].position.y,
                                                                    bodyParts[bodyParts.Count - 1].position.z + 1f
                                                                    ),
                                                                    bodyParts[bodyParts.Count - 1].rotation) as GameObject;
            Transform newPart = newPartObj.transform;
            newPart.SetParent(this.transform);
            bodyParts.Add(newPart);

            yield return new WaitForSeconds(0.2f);
        }
    }
}

