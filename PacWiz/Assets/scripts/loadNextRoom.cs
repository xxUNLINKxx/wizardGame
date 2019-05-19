using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class loadNextRoom : MonoBehaviour
{
    private SAVE GetSAVE;
    public Scene Base;
    public level floor;
    public String[] scenesInFloor;
    public int floorIndex;
    public Animator transitionCanvas;
    private void Awake()
    {
        Base = gameObject.scene;
        //GetSAVE = GameObject.Find("SAVE").GetComponent<SAVE>();
        //if (GetSAVE != null && GetSAVE.saved)
        //{
            //GetSAVE.LoadData();
            //scenesInFloor = floor.scenesInFloor;
        //}
    }
    void Start()
    {

        //if (GetSAVE.saved)
        //{
        //scenesInFloor = floor.scenesInFloor;
        //StartCoroutine(LoadSavedRoom(floorIndex));
        //}
        //else 
        //{   
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
        //}   
    }

    public IEnumerator LoadSavedRoom(int sceneArrayIndex)
    {
        yield return new WaitForSecondsRealtime(0.7f);
        transitionCanvas.SetBool("loading", true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1.5f);
        enabled = false;
        yield return SceneManager.LoadSceneAsync(scenesInFloor[sceneArrayIndex], LoadSceneMode.Additive);//adds new scene       
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scenesInFloor[sceneArrayIndex]));
        yield return new WaitForSecondsRealtime(1.5f);
        transitionCanvas.SetBool("loading", false);
        yield return new WaitForSecondsRealtime(1.2f);

        Time.timeScale = 1;
        enabled = true;
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
        yield return new WaitForSecondsRealtime(1.5f);
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
