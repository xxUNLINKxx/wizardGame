using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateScript : MonoBehaviour
{
    public plateScriptable GetPlate;
    

    public bool activate=false;    

    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.attachedRigidbody.mass >= GetPlate.minWeight)
        {
            activate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null)
        {
            if (other.attachedRigidbody.mass >= GetPlate.minWeight)
            {
                activate = false;
            }
        }      
    }
}
