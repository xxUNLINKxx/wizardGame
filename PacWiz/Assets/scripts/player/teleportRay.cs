using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportRay : MonoBehaviour
{
    public float speed =10f;
    private Vector3 mousePos;
    public bool teleport;
    public float distance;
    teleportScript TeleportScript;

    //for raycast
    public GameObject firePoint;
    public LayerMask block;
    public LayerMask ground;
    private GameObject CENTER;

    public LineRenderer lr;

    private void Start()
    {
        TeleportScript = GameObject.Find("Player").GetComponent<teleportScript>();
        CENTER = GameObject.Find("raycast");
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 10;    
        distance = Vector2.Distance(TeleportScript.tel.transform.position, CENTER.transform.position);
        Vector2 direction = Camera.main.ScreenToWorldPoint(mousePos)-transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
        ShootRayCast();   
    }

    void ShootRayCast()
    {
        lr.SetPosition(0, firePoint.transform.position);
        Debug.DrawRay(CENTER.transform.position, CENTER.transform.right, Color.red);
        //Searches for block layerMask
        RaycastHit2D blockHitInfo = Physics2D.Raycast(CENTER.transform.position, CENTER.transform.right, distance,block);

        //Searches for ground LayerMask
        RaycastHit2D groundHitInfo = Physics2D.Raycast(CENTER.transform.position, CENTER.transform.right, distance, ground);

        if (distance > TeleportScript.Range) {
            lr.enabled = false;
        }
        else
        {
            lr.enabled = true;
            if (groundHitInfo||blockHitInfo)
            {
                teleport = false;
                if (groundHitInfo)
                {
                    lr.SetPosition(1, groundHitInfo.point);
                }
                if (blockHitInfo)
                {
                    lr.SetPosition(1, blockHitInfo.point);
                }
            
            }
            else
            {
                teleport = true;
                lr.SetPosition(1, TeleportScript.tel.transform.position);
            }
        }

    }
}
