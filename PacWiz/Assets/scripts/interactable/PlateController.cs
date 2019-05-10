using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    [Header("Parent of all pp's")]
    public GameObject[] activateObjects;
    public PlateScript[] otherPlates;

    private void Update()
    {

        foreach(PlateScript ps in otherPlates)
        {
            if (ps.activate)
            {
                Activate();
            }
            else
            {
                Deactivate();
                break;
            }
        }
    }

    void Activate()
    {

        foreach (GameObject obj in activateObjects)
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
