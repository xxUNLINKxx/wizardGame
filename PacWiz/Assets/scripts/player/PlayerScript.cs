using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject tel;
    private bool facingRight = true;
    private SpriteRenderer sr;

    public bool canTeleport;
    public GameObject arrow;

    [Space]
    public float deathHeight;

    //scripts
    private teleportScript GetTeleportScript;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        GetTeleportScript = GetComponent<teleportScript>();
    }  
    void Update()
    {
        Flip();
        teleportEnabled();
    }
    void Flip()
    {
        if (facingRight == true && tel.transform.position.x > transform.position.x)
        {
            sr.flipX = false;
            facingRight = !facingRight;
        }
        else if (facingRight == false && tel.transform.position.x < transform.position.x)
        {
            sr.flipX = true;
            facingRight = !facingRight;
        }
    }
    void teleportEnabled()
    {
        
        if (canTeleport)
        {
            GetTeleportScript.enabled = true;
            arrow.SetActive(true);
        }
        else
        {
            GetTeleportScript.enabled = false;
            arrow.SetActive(false);
        }
    }
}
