using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    public plateScriptable GetPlate;
    public GameObject[] activateObjects;
    [Tooltip("First Gameobject must always be this")]
    public GameObject[] otherPlates;

    private bool activate=false;    

    private void Update()
    {
        for(int i = 0; i < otherPlates.Length; i++)
        {
            if (otherPlates[i].GetComponent<PlateScript>().activate)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
        }
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.attachedRigidbody.mass >= GetPlate.minWeight)
        {
            activate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody.mass >= GetPlate.minWeight)
        {
            activate = false;
        }
    }


    void Activate()
    {

        foreach(GameObject obj in activateObjects)
        {
            //for Moving Platforms only
            if (obj.GetComponent<MovingPlatformScript>() != null)
            {
                obj.GetComponent<MovingPlatformScript>().enabled = true;
            }
            //for MechDoors
            if (obj.GetComponent<MechDoorScript>() != null)
            {
                obj.GetComponent<MechDoorScript>().Activate();
            }       
        }
    }
    void Deactivate()
    {
        foreach (GameObject obj in activateObjects)
        {
            //for Moving Platforms only
            if (obj.GetComponent<MovingPlatformScript>() != null)
            {
                obj.GetComponent<MovingPlatformScript>().enabled = false;
            }
            //for MechDoors
            if (obj.GetComponent<MechDoorScript>() != null)
            {
                obj.GetComponent<MechDoorScript>().Deactivate();
            }
        }
    }

}
