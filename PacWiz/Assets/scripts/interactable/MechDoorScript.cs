using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class MechDoorScript : MonoBehaviour
{
    
    private Animator anim;
    private BoxCollider2D box;

    private void Start()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }
    public void Activate()
    {
        anim.SetBool("open", true);
        box.enabled = false;
    }
    public void Deactivate()
    {
        anim.SetBool("open", false);
        box.enabled = true;
    }
}
