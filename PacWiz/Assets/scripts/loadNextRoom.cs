using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class loadNextRoom : MonoBehaviour
{
    public Scene Base;
    public level floor;
    public String[] scenesInFloor;
    public int floorIndex;
    public Animator transitionCanvas;

    private void Awake()
    {
        Base = gameObject.scene;
    }
    void Start()
    {
        
        scenesInFloor = floor.scenesInFloor;      
        if (Application.isEditor)
        {
            for (int i = 0; i < floorIndex; i++)
            {
                Scene loadedScene = SceneManager.GetSceneByName(scenesInFloor[i]);
                if (loadedScene.name.Contains("floor."))
                {
                    SceneManager.SetActiveScene(loadedScene);
                    return;
                }
            }
        }
        StartCoroutine(LoadNextRoom(floorIndex));     
    }

    //loads next room in floor
    public void LoadNext()
    {
        StartCoroutine(LoadNextRoom(floorIndex+=1));
    }

    public IEnumerator LoadNextRoom(int sceneArrayIndex)
    {
        
        yield return new WaitForSecondsRealtime(0.7f);
        transitionCanvas.SetBool("loading", true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1.2f);
        if (sceneArrayIndex > 0)
        {
            yield return SceneManager.UnloadSceneAsync(scenesInFloor[(sceneArrayIndex-1)]);//unloads previous scene
        }      
        enabled = false;
        yield return SceneManager.LoadSceneAsync(scenesInFloor[sceneArrayIndex], LoadSceneMode.Additive);//adds new scene       
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scenesInFloor[sceneArrayIndex]));
        yield return new WaitForSecondsRealtime(1.5f);
        transitionCanvas.SetBool("loading", false);
        yield return new WaitForSecondsRealtime(1.2f);
        
        Time.timeScale = 1;
        enabled = true;
    }
}
