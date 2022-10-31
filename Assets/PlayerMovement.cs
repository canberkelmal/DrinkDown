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

    void Start()
    {
        health=100;
        rb=gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(joystick.Vertical>=0){
            rb.velocity=new Vector3(joystick.Horizontal*sensivity,
                                    0,
                                    joystick.Vertical*sensivity);
        }
        else{
            rb.velocity=new Vector3(joystick.Horizontal*sensivity, 0, 0);
        }



    }

    void OnTriggerEnter(Collider other){
        
        Debug.Log("----" + other.gameObject.tag);

        if(other.gameObject.tag=="Heal" || other.gameObject.tag=="HealGate"){
            Debug.Log("----from " + health);
            health+=10;
            Debug.Log("----to " + health);
            Debug.Log("----------------");
            score.text="Health: " + health;
        }

        if(other.gameObject.tag=="Fire" || other.gameObject.tag=="DamageGate"){
            Debug.Log("----from " + health);
            health-=10;
            Debug.Log("----to " + health);
            Debug.Log("----------------");
            score.text="Health: " + health;
        }

        if(other.gameObject.tag=="Fire" || other.gameObject.tag=="Heal"){
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
