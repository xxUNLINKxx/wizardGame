using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public loadNextRoom GetLoad;
    public doorScriptable GetDoor;
    private bool locked;
    private bool seal;
    void Start()
    {
        GetLoad = GameObject.Find("LoadLevel").GetComponent<loadNextRoom>();
        bool locked = GetDoor.Locked;
        bool seal = GetDoor.Sealed;
    }
    void Unlock()
    {
        locked = false;
    }
    void Unseal()
    {
        seal = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   
            if (!locked || !seal)
            {
                GetLoad.LoadNext();
            }
        }
        
    }
}
