using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tempMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadSceneAsync("BASE.tow");
    }
}
