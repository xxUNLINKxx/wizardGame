﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyMovement : MonoBehaviour
{
    private GameObject Player;
    [Header("Components")]
    public SpriteRenderer sr;
    private Rigidbody2D rb;
    //fairypositions
    private GameObject[] fairyPos;
    private int Rand;
    private float randTimeB4NextPos;

    
    private teleportScript GetTeleportScript;
    private interactable GetInteractable;
    [Header("Interactables")]
    public LayerMask interact;

    [Header("Fairy")]
    public float speed;
    RaycastHit2D mouse;
    private bool selected;
    private bool grabbed;
    public Transform holdpoint;
    private bool lastpos;
    private Vector3 lastPosition;
    private GameObject objectInHold;

    private Vector3 mousePos;
    private Transform tel;
    
    public bool toggle = true;
    private bool facingLeft = true;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        GetTeleportScript = Player.GetComponent<teleportScript>();
        rb = GetComponent<Rigidbody2D>();
        fairyPos = GameObject.FindGameObjectsWithTag("fairyPos");
    }

    private void Update()
    {
        if (GetTeleportScript.enabled)
        {
            tel = GetTeleportScript.tel.transform;
            mousePos = GetTeleportScript.mousePos;
            mouse = Physics2D.Raycast(tel.position, tel.forward, 0.3f);
            Toggle();
            SelectedObject();
        }
        else
        {
            FollowPlayer();
        }       
    }

    void Toggle()
    {
        if (!selected) {
            if (Input.GetMouseButtonDown(1))
            {

                //toggles boolean toggle
                toggle = !toggle;              
            }
            if (toggle)
            {
                FollowPlayer();
                lastpos = false;
            }
            else
            {
                Lastpos();
            }
            
        }
        else
        {
            if (!grabbed)
            {
                Lastpos();
            }
            else
            {
                transform.position = Vector2.Lerp(transform.position, tel.position, (speed * 2) * Time.deltaTime);
                objectInHold.gameObject.GetComponent<Rigidbody2D>().velocity= (holdpoint.position-objectInHold.transform.position)*20;
            }
                
        }
        
    }
    void FollowPlayer()
    {
        Flip();
        if (randTimeB4NextPos <= 0)
        {
            Rand = Random.Range(0, 2);
            randTimeB4NextPos = Random.Range(1, 2.6f);
        }
        else
        {
            randTimeB4NextPos -= Time.deltaTime;
        }

        transform.position = Vector2.Lerp(transform.position, fairyPos[Rand].transform.position, speed * Time.deltaTime);       
    }    
    void Lastpos()
    {
        if (!lastpos)
        {
            lastPosition = tel.position;
            lastpos = true;
        }
        else
        {
            transform.position = Vector2.Lerp(transform.position,lastPosition,speed*Time.deltaTime);
        }
    }
    void SelectedObject()
    {
        if (mouse)
        {
            if (Input.GetMouseButtonDown(1) && mouse.transform.CompareTag("pickUp"))
            {
                 selected = true;
            }
        }
        if (grabbed)
        {
            if (Input.GetMouseButtonDown(1))
            {
                selected = false;
                grabbed = false;
                toggle = true;
                objectInHold.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }          
    }
    void Flip()
    {
        if (facingLeft == true && Player.transform.position.x < transform.position.x)
        {
            sr.flipX = false;
            facingLeft = !facingLeft;
        }
        else if (facingLeft == false && Player.transform.position.x > transform.position.x)
        {
            sr.flipX = true;
            facingLeft = !facingLeft;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("pickUp") && selected)
        {
            objectInHold = other.gameObject;
            grabbed = true;
        }
    }

}
