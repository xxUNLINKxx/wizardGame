using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SAVE : MonoBehaviour
{

    [SerializeField] private string fileName = "wiz.SaveFile"; // file to save with the specified resolution
    [SerializeField] private bool dontDestroyOnLoad; // the object will move from one scene to another (you only need to add it once)

    void Awake()
    {
        SaveSystem.Initialize(fileName);
        if (dontDestroyOnLoad) DontDestroyOnLoad(transform.gameObject);
    }

    // if the object is present in all game scenes, auto save before exiting
    // on some platforms there may not be an exit function, see the Unity help
    void OnApplicationQuit()
    {
        SaveSystem.SaveToDisk();
    }

    
    private loadNextRoom GetLoadNext;

    //temporary save
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            SaveData();
            Debug.Log("Saved Data");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetData();
            Debug.Log("Reset Data");
        }
        GetLoadNext = GameObject.Find("LoadLevel").GetComponent<loadNextRoom>();
        if (GetLoadNext != null)
        {
            EndOfLevel();
        }
    }
    public void SaveData()
    {
        SaveScene();
    }
    public void LoadData()
    {
        LoadScene();
    }
    public void ResetData()
    {
        ResetScene();
    }

    [Header("SceneSave")]
    public bool saved;
    public string[] Bases;
    public level[] floors;
    public int currentFloor;

    public void SaveScene()
    {
        saved = true;
        SaveSystem.SetString("lastBase", GetLoadNext.Base.name);
        SaveSystem.SetString("lastfloor", GetLoadNext.floor.name);
        SaveSystem.SetInt("lastfloorIndex", GetLoadNext.floorIndex);
        SaveSystem.SetInt("currentFloor", currentFloor);
        SaveSystem.SetBool("hasSaved", saved);
    }
    public void ResetScene()
    {
        saved = false;
        SaveSystem.SetString("lastBase", "BASE.tow");
        SaveSystem.SetString("lastfloor", "tow.Floor1");
        SaveSystem.SetInt("lastfloorIndex", 0);
        SaveSystem.SetInt("currentFloor", 0);
        SaveSystem.SetBool("hasSaved", saved);
    }
    public void LoadScene()
    {
        saved = SaveSystem.GetBool("hasSaved");
        GetBase();
        GetFloor();
        GetLoadNext.floorIndex = SaveSystem.GetInt("lastfloorIndex");
        currentFloor = SaveSystem.GetInt("currentFloor");
    }
    void GetBase()
    {
        foreach (string Base in Bases)
        {
            if (Base == SaveSystem.GetString("lastBase"))
            {
                GetLoadNext.Base = SceneManager.GetSceneByName(Base);
            }
        }
    }
    void GetFloor()
    {
        foreach(level floor in floors)
        {
            if (floor.name == SaveSystem.GetString("lastfloor"))
            {
                GetLoadNext.floor = floor;
            }
        }
    }
    void EndOfLevel()
    {
        if (GetLoadNext.floorIndex > GetLoadNext.scenesInFloor.Length)
        {
            GetLoadNext.floor = floors[currentFloor+=1];
            GetLoadNext.LoadNext();
        }
    }
}
