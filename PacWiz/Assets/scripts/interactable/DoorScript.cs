using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public loadNextRoom GetLoad;
    public FairyMovement GetFairy;
    public SpriteRenderer Lock;
    public Sprite[] lockSprites;
    public bool locked;
    public bool seal;
    void Start()
    {
        GetLoad = GameObject.Find("LoadLevel").GetComponent<loadNextRoom>();
        GetFairy = GameObject.Find("Fairy").GetComponent<FairyMovement>();
    }

    private void Update()
    {
        if (locked)
        {
            Lock.sprite = lockSprites[0];
        }

        if (seal)
        {
            Lock.sprite = lockSprites[1];
        }

        if (!locked && !seal)
        {
            Lock.sprite = null;
        }

    }
    void Unlock()
    {
        locked = false;
        Lock.sprite = null;
    }
    void Unseal()
    {
        seal = false;
        Lock.sprite = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   
            if (!locked && !seal)
            {
                GetFairy.selected = false;
                GetFairy.toggle = true;
                GetFairy.FollowPlayer();
                GetLoad.LoadNext();
            }
        }
        
    }
}
