using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] foodPrefab = null;

    private float timeStep = 5f;
    private float heightZone = 3f;
    private float radiusZone = 40f;
    private float currentTime = 0f;
    public int[] table = { 750, 150, 100 };
    public int total;
    public int random;

    void Start() {
        //initialisation du temps
        currentTime = 0f;
        foreach (var item in table) {
            total += item;
        }
        random = Random.Range(0, total);
        SpawnFood(random);
    }


    void Update() {
        //mise a jour du temps
        currentTime += Time.deltaTime;
        //boucle de spawn par rapport au temps
        if (currentTime > timeStep) {
            currentTime -= timeStep;
            random = Random.Range(0, total);
            SpawnFood(random);
        }
    }

    private void SpawnFood(int random) {
        //random des coordonees
        float x = Random.Range(-radiusZone, radiusZone);
        float z = Random.Range(-radiusZone, radiusZone);

        for (int i =0; i < table.Length; i++) {
            if (random <= table[i]) {
                GameObject _foodPrefab = foodPrefab[i];
                Instantiate(_foodPrefab, new Vector3(x, heightZone, z), _foodPrefab.transform.rotation);
                return;
            } else {
                random -= table[i];
            }
        }
        return;
    }
}

