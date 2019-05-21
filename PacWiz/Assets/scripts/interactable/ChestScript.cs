using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public chestScriptable GetChest;
    private bool locked;
    public int lootAmt;
    public GameObject[] content;

    private void Start()
    {
        locked = GetChest.Locked;
    }
    void Chest()
    {
        Transform playerPos = GameObject.Find("Player").transform;
        Vector2 spawnLocation = new Vector2(transform.position.x + 1.5f, transform.position.y+1);
        if (!locked)
        {
            if (lootAmt > 0)
            {
                if (content.Length > 1)
                {
                    int rand = Random.Range(0, content.Length + 1);
                    Instantiate(content[rand], spawnLocation, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                else
                {
                    Instantiate(content[0], spawnLocation, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
            }

        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Fairy"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Chest();
                lootAmt -= 1;
            }
        }
    }
}
