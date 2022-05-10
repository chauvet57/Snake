using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    public float transitionTime = 1f;
    private string sceneCharge;

    private void Awake()
    {
        animator.GetComponent<Animator>();
    }

    public void LoadNextScene(string sceneCharge)
    {
        StartCoroutine(LoadScene(sceneCharge));
    }

    IEnumerator LoadScene(string sceneCharge)
    {
        animator.SetTrigger("Out");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneCharge);
    }

    public static void Out(string sceneCharge)
    {
        GameObject.Find("ImTransition").GetComponent<Transition>().LoadNextScene(sceneCharge);
    }
}
