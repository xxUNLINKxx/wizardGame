using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactable : MonoBehaviour
{
    public loadNextRoom load;
    

    //for interactables
    public interactableScriptableObject interact;
    private bool enter;
    private bool pickUp;
    private bool chest;

    [Header("Chest")]
    public bool locked;
    public int lootAmt;
    public GameObject[] content;

    void Start()
    {
        load = GameObject.Find("LoadLevel").GetComponent<loadNextRoom>();
           
    }

    private void Update()
    {
        //checks for type "Enter"
        if(interact.Type == "Enter")
        {
            enter = true;
        }
        else
        {
            enter = false;
        }

        //checks for type "Pickup"
        if (interact.Type == "Pickup")
        {
            pickUp = true;
        }
        else
        {
            pickUp = false;
        }

        //checks for type "Chest"
        if(interact.Type == "Chest")
        {
            chest = true;
        }
        else
        {
            chest = false;
        }
            
    }

    void Enter()
    {
        load.LoadNext();
    }

    void Chest()
    {
        Transform playerPos = GameObject.Find("Player").transform;
        Vector2 distance = new Vector2(Mathf.Abs(playerPos.position.x+1), playerPos.position.y);
        if (!locked)
        {
            if (lootAmt > 0)
            {
                if (content.Length > 1)
                {
                    int rand = Random.Range(0, content.Length + 1);
                    Instantiate(content[rand], distance, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                else
                {
                    Instantiate(content[0],distance,Quaternion.Euler(new Vector3(0,0,0)));
                }
            } 
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (enter)
            {
                Enter();
            }

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")||other.CompareTag("Fairy"))
        {
            if (chest)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Chest();
                    lootAmt -= 1;
                }
            }
        }
    }
}
