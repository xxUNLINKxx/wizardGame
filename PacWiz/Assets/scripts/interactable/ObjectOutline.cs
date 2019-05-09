using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOutline : MonoBehaviour
{
    public Material[] outlineMat;
    public LayerMask interactable;

    private void Update()
    {
        RaycastHit2D HitInfo = Physics2D.Raycast(transform.position, transform.forward, 10, interactable);
        if (HitInfo)
        {
            if (HitInfo.transform.CompareTag("pickUp"))
            {
                HitInfo.transform.GetComponent<SpriteRenderer>().material = outlineMat[1];
            }
        }
        else
        {
            GameObject[] interact = GameObject.FindGameObjectsWithTag("pickUp");
            foreach(GameObject obj in interact)
            {
                obj.GetComponent<SpriteRenderer>().material = outlineMat[0];
            }
        }
    }

}
