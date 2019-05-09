using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MovingPlatformScript : MonoBehaviour
{
    
    public enum Movement
    {
        BACKFORTH,
        FORWARD
    }
    [Header("disabled before playing")]
    [Header("This component must be")]
    private loadNextRoom GetLoad;
    public Movement platformMovement;

    public float moveSpeed;
    public float minDistance;
    private Vector3 originalPos;
    public Transform newPos;
    bool atOGPos = true;
    private float timer;
    private bool fwd = false;

    private void Start()
    {
        originalPos = transform.position;
        fwd = !fwd;
        GetLoad = GameObject.Find("LoadLevel").GetComponent<loadNextRoom>();
    }

    private void Update()
    {
        switch (platformMovement)
        {
            case Movement.BACKFORTH:
                BackForth();
                break;
            case Movement.FORWARD:
                Forward();
                break;
        }
    }

    void BackForth()
    {
        float distanceNew = Vector2.Distance(transform.position, newPos.position);
        float distanceOld = Vector2.Distance(transform.position, originalPos);
        
        if (atOGPos)
        {         
            if (timer <= 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, newPos.position, moveSpeed*Time.deltaTime);
                if (distanceNew<minDistance)
                {
                    timer = 0.5f;
                    atOGPos = false;
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }           
        }
        else
        {        
            if (timer <= 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, originalPos, moveSpeed*Time.deltaTime);
                if (distanceOld < minDistance)
                {
                    timer = 0.5f;
                    atOGPos = true;
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
    }
    void Forward()
    {
        float distanceNew = Vector2.Distance(transform.position, newPos.position);
        float distanceOld = Vector2.Distance(transform.position, originalPos);

        if (fwd)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPos.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, originalPos, moveSpeed * Time.deltaTime);  
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }      
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            if (GetLoad.Base.name.Contains("BASE"))
            {
                SceneManager.MoveGameObjectToScene(other.gameObject, GetLoad.Base);
            }
        }      
    }
}
