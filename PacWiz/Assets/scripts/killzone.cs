using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class killzone : MonoBehaviour
{
    private loadNextRoom GetLoadNext;

    private void Start()
    {
        GetLoadNext = GameObject.Find("LoadLevel").GetComponent<loadNextRoom>();
    }
    //loads same level
    public void LoadRetry()
    {
        StartCoroutine(RetryLevel(GetLoadNext.floorIndex));
    }

    public IEnumerator RetryLevel(int sceneArrayIndex)
    {
        Time.timeScale = 0;
        if (sceneArrayIndex > 0)
        {
            yield return SceneManager.UnloadSceneAsync(GetLoadNext.scenesInFloor[sceneArrayIndex]);//unloads same scene
        }
        enabled = false;
        if (SceneManager.GetSceneByName(GetLoadNext.scenesInFloor[sceneArrayIndex]) != null)
        {
            yield return SceneManager.LoadSceneAsync(GetLoadNext.scenesInFloor[sceneArrayIndex], LoadSceneMode.Additive);//loads same scene 
        }

        Time.timeScale = 1;
        enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LoadRetry();
        }
    }
}
