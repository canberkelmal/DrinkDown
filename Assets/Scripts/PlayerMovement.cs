using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    
    Rigidbody rb;
    string trigger;
    bool gate=true;
    Vector3 Movement;
    public float sensivity=100f;
    public int health;
    public Joystick joystick;
    public Text score;
    public GameObject healEffect;
    public GameObject damageEffect;
    public Image healthBar;
    public Animator animator;

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


        Movement = new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += Movement * sensivity * Time.deltaTime;

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

        trigger=other.gameObject.tag;
        
        Debug.Log("----" + trigger);

        if(trigger=="Gate"){
            gate= gameObject.transform.position.x>0 ? false : true;
        }

        if( health<100 && (trigger=="Heal" || (trigger=="Gate" && gate)) ){
            Heal();
        }
        if(health>0 && (trigger=="Fire" || (trigger=="Gate" && !gate) || trigger=="Obs")){
            Damage();
        }

        if(trigger=="Heal" || trigger=="Fire"){
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

    public void Heal(){

        animator.SetTrigger("Heal");

        health+=10;
        score.text=health + "/100";
        healthBar.fillAmount+=0.1f;
        
        healEffect.gameObject.GetComponent<ParticleSystem>().Play();

        Debug.Log("from " + health + " to " + (health+10));
        Debug.Log("----------------");

    }

    public void Damage(){

        animator.SetTrigger("Damage");

        health-=10;
        score.text=health + "/100";
        healthBar.fillAmount-=0.1f;
        
        damageEffect.gameObject.GetComponent<ParticleSystem>().Play();

        Debug.Log("from " + health + " to" + (health-10));
        Debug.Log("----------------");
    }
    

}
