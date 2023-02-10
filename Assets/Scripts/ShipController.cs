using System.Xml.Schema;
using System.Net.Sockets;
using System.Xml;
//using System.Threading.Tasks.Dataflow;
using System;
//using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //set map border. destroy enemy when crossing(training phase 1)
    //display bf return to the combat area. look at note on phone.
    
    
    
    //option for boost by pressing shift soon
    public Respawn respawn;
    private Rigidbody ship_RigidBody;

    private float forwardSpeed = 80f;
    private float activeForwardSpeed;
    private float forwardAcceleration = 10f;

    private float lookRateSpeed = 120f;

    private Vector2 lookInput, screenCenter, mouseDistance;

    private int hitCounter;

    private float rollInput;
    private float rollSpeed =300f, rollAcceleration =5f;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
        
        Physics.IgnoreLayerCollision(9,10,true);
        ship_RigidBody = GetComponent<Rigidbody>();
            
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lookInput.x = Input.mousePosition.x;
       lookInput.y = Input.mousePosition.y;

       mouseDistance.x = (lookInput.x-screenCenter.x) / screenCenter.y;
       mouseDistance.y = (lookInput.y-screenCenter.y) / screenCenter.y;

       mouseDistance = Vector2.ClampMagnitude(mouseDistance, 2f);

       rollInput = Mathf.Lerp(rollInput,Input.GetAxisRaw("Horizontal"), rollAcceleration * Time.deltaTime);
        
       Quaternion rot = Quaternion.Euler(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime);
       ship_RigidBody.MoveRotation(ship_RigidBody.rotation * rot);

       activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
       
       // if(activeForwardSpeed<0){
       //     // transform.position += transform.forward * (activeForwardSpeed/4) * Time.deltaTime;     
       //     ship_RigidBody.MovePosition(transform.position + transform.forward * (Time.deltaTime * (activeForwardSpeed/4)));
       // }
       // else{
       //     // position += trans.forward * activeForwardSpeed * Time.deltaTime;
       //     ship_RigidBody.MovePosition(transform.position + transform.forward * Time.deltaTime * activeForwardSpeed);
       // }
       ship_RigidBody.MovePosition(transform.position + transform.forward * (Time.deltaTime * activeForwardSpeed));
    }

    private void Hit()
    {
        if(respawn.enabled)
            respawn.Hit();
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("projectile"))
        {
           //if(obj.GetComponent<Projectile>().source == null) return;
            
            hitCounter++;
            
            if(hitCounter ==0) return;
            if (hitCounter == 1)
            {
                Debug.Log("warning");
                return;
            }
            if(hitCounter>1)
                hitCounter = 0;
        }
        Hit();
    }
}
