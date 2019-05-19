using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator transitionCanvas;
    public Button start;
    public Button Continue;
    public Button Options;
    public Button Exit;

    private SAVE GetSAVE;

    private void Start()
    {
        GetSAVE = GameObject.Find("SAVE").GetComponent<SAVE>();
        if (GetSAVE != null)
        {
            GetSAVE.LoadData();
        }
    }

    private void Update()
    {
        if (GetSAVE.saved)
        {
            Continue.enabled = true;
        }
        else
        {
            Continue.enabled = false;
        }
    }
    public void StartGame()
    {
        if (GetSAVE.saved)
        {
            GetSAVE.ResetData();
            GetSAVE.LoadData();
        }

        SceneManager.LoadScene("BASE.tow");
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene(SaveSystem.GetString("lastBase"));
    }
}
