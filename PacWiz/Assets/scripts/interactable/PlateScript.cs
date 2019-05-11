using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    public plateScriptable GetPlate;
    

    public bool activate=false;
    private int entityCount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody.mass >= GetPlate.minWeight)
        {
            entityCount += 1;
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
            entityCount -= 1;
        }
        if (entityCount == 0)
        {
            activate = false;
        }
            
    }
}
