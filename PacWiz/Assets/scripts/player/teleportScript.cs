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

    public Transform telLine;
    public float Range;
    public float distance;

    private void Start()
    {
        magicCircle = GameObject.Find("magicC").GetComponent<Animator>();
        teleportRayScript = GameObject.Find("raycast").GetComponent<teleportRay>();
        
    }
    void Update()
    {
        Teleport();
        CastLine();
    }
    void Teleport()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 10;
        tel.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        distance = Vector2.Distance(tel.transform.position, transform.position);



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
    void CastLine()
    {
        if (distance < Range)
        {
            telLine.localScale = new Vector3(Mathf.Abs(distance), 1, 1);
        }
        else
        {
            telLine.localScale = new Vector3(6, 1, 1);
        }

        if (!teleportRayScript.teleport|| distance>Range || startTimeBtwTel > 0)
        {
            telLine.GetComponentInChildren<SpriteRenderer>().color = new Color32(255,0,0,100);
        }
        else
        {
            telLine.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }
}
