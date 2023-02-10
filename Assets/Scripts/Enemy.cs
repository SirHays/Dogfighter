using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    private int hitCount;
    private GameObject boom;
    public GameObject explosionPrefab;

    public event EventHandler<CollisionEventArgs> onCollision;
    
    public class CollisionEventArgs : EventArgs
    {
        public GameObject eventArgsCollision;
    }

    public float pitchPower, rollPower, yawPower, enginePower;

    private float activeRoll, activePitch, activeYaw;

    private float vertical, horizontal, yaw;

    private bool readyToCollide, allowInvoke;

    private Rigidbody enemy_Rigidbody;

    private void Awake()
    {
        readyToCollide = true;
        allowInvoke = true;
        enemy_Rigidbody = GetComponent<Rigidbody>();
    }

    public float GetRoll(){
        return horizontal;
    }
    public float GetPitch(){
        return vertical;
    }
    public float GetYaw(){
        return yaw;
    }
    private void FixedUpdate()
    {
            
            //transform.position += transform.forward * enginePower * Time.deltaTime;
            Transform enemyTransform = transform;
            enemy_Rigidbody.MovePosition(enemyTransform.position+enemyTransform.forward * (Time.deltaTime * enginePower));

            // no need for manual control of agent.
            
            // vertical = Input.GetAxisRaw("Vertical");
            // horizontal = Input.GetAxisRaw("Horizontal");
            //
            //
            // RotateShip(vertical,horizontal);
        
    }
    
    public void RotateShip(float vertical, float horizontal){

        activePitch = vertical * pitchPower * Time.deltaTime;
        activeRoll = horizontal * rollPower * Time.deltaTime;
        //activeYaw = yaw * yawPower * Time.deltaTime;
        Quaternion rot = Quaternion.Euler(activePitch * pitchPower * Time.deltaTime, 0, -activeRoll * rollPower * Time.deltaTime);
        enemy_Rigidbody.MoveRotation(enemy_Rigidbody.rotation * rot);
        //transform.Rotate(activePitch * pitchPower * Time.deltaTime, 0, -activeRoll * rollPower * Time.deltaTime, Space.Self);
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (readyToCollide)
        {
            Collision(other.gameObject);
            readyToCollide = false;
        }
        if (!allowInvoke) return;
        Invoke(nameof(AllowCollision), 0.1f);
        allowInvoke = false;
        
    }

    public void Collision(GameObject objInCollision)
    {
        onCollision?.Invoke(this, new CollisionEventArgs {eventArgsCollision = objInCollision});
    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (readyToCollide)
    //     {
    //         // Debug.Log("invoke");
    //         onCollision?.Invoke(this, new CollisionEventArgs {eventArgsCollision = null});
    //         readyToCollide = false;
    //     }
    //     if (!allowInvoke) return;
    //     Invoke(nameof(AllowCollision), 0.1f);
    //     allowInvoke = false;
    // }
    
    private void AllowCollision()
    {
        //Allow colliding and invoking again
        readyToCollide = true;
        allowInvoke = true;
    }
}