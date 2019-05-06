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
        Vector2 distance = new Vector2(Mathf.Abs(playerPos.position.x + 1), playerPos.position.y);
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
                    Instantiate(content[0], distance, Quaternion.Euler(new Vector3(0, 0, 0)));
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
