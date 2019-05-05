using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportScript : MonoBehaviour{
    public Vector3 mousePos;
    public Animator magicCircle;
    
    public float timeBtwTel;
    private float startTimeBtwTel;

    private teleportRay teleportRayScript;
    private GameObject CENTER;
    public GameObject tel;
   
    public float Range;
    public float distance;

    private void Start()
    {
        magicCircle = GameObject.Find("magicC").GetComponent<Animator>();
        teleportRayScript = GameObject.Find("raycast").GetComponent<teleportRay>();
        CENTER = GameObject.Find("raycast");
    }
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 10;
        tel.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        distance = Vector2.Distance(tel.transform.position, CENTER.transform.position);



        if (startTimeBtwTel <= 0)
        {
            magicCircle.SetBool("charging", false);
            if (teleportRayScript.teleport)
            {
                if (distance < Range)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
                        startTimeBtwTel = timeBtwTel;
                        magicCircle.SetBool("charging", true);
                    }   
                }                 
            }              
        }
        else
        {
            startTimeBtwTel -= Time.deltaTime;
        }                            
        mousePos.Normalize();       
    }
}
