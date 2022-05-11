using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private AudioSource audioSource = null;
    public float transitionTime = 1f;
    private string sceneCharge;

    private void Awake()
    {
        animator.GetComponent<Animator>();
        audioSource.GetComponent<AudioSource>();
    }

    public void LoadNextScene(string sceneCharge)
    {
        StartCoroutine(LoadScene(sceneCharge));
        StartCoroutine(StartFade(audioSource, 1f, 0));
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

    IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
        {
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
            yield break;
        }
    }
