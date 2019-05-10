using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class MechDoorScript : MonoBehaviour
{
    
    private Animator anim;
    private BoxCollider2D box;
    public bool inverse;

    private void Start()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }
    public void Activate()
    {
        if (inverse)
        {
            anim.SetBool("open", false);
            box.enabled = true;
        }
        else
        {
            anim.SetBool("open", true);
            box.enabled = false;
        }

    }
    public void Deactivate()
    {
        if (inverse)
        {
            anim.SetBool("open", true);
            box.enabled = false;
        }
        else
        {
            anim.SetBool("open", false);
            box.enabled = true;
        }

    }
}
