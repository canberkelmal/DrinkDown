using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    
    Rigidbody rb;
    public float sensivity=100f;
    public int health;
    public Joystick joystick;
    public Text score;
    public GameObject healEffect;
    public GameObject damageEffect;
    public Image healthBar;

    void Start()
    {
        health=100;
        rb=gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.velocity=new Vector3(joystick.Horizontal*sensivity,
                                0,
                                joystick.Vertical*sensivity);

        /* if(joystick.Vertical>=0){
            rb.velocity=new Vector3(joystick.Horizontal*sensivity,
                                    0,
                                    joystick.Vertical*sensivity);
        }
        else{
            rb.velocity=new Vector3(joystick.Horizontal*sensivity, 0, 0);
        } */



    }

    void OnTriggerEnter(Collider other){
        
        Debug.Log("----" + other.gameObject.tag);

        if( health<100 && (other.gameObject.tag=="Heal" || other.gameObject.tag=="HealGate") ){
            health+=10;
            score.text=health + "/100";
            healthBar.fillAmount+=0.1f;

            Debug.Log("from " + health + " to " + (health+10));
            Debug.Log("----------------");
        }

        if(other.gameObject.tag=="Fire" || other.gameObject.tag=="DamageGate"){
            Debug.Log("from " + health + " to" + (health-10));
            Debug.Log("----------------");
            health-=10;
            score.text=health + "/100";
            healthBar.fillAmount-=0.1f;
        }

        if(other.gameObject.tag=="Heal"){
            Instantiate(healEffect, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag=="Fire"){   
            Instantiate(damageEffect, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
        }


        if(health>=70){
            gameObject.GetComponent<Renderer>().material.color=Color.green;
        }
        else if(health<70 && health>40){
            gameObject.GetComponent<Renderer>().material.color=Color.yellow;
        }        
        else if(health<=40){
            gameObject.GetComponent<Renderer>().material.color=Color.red;
        }

    }
}
