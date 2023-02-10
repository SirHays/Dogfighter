using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float initTime;
    private float timeSinceInit;
    public GameObject source;
    public ProjectilePool projectilePool;
    
    void Start() 
    {
        //game time when object was initialized.
        initTime = Time.timeSinceLevelLoad;
    }

    private void OnEnable()
    {
        initTime = Time.timeSinceLevelLoad; 
    }

    void Update()
    {
        if (Time.frameCount % 5 != 0) return;
        timeSinceInit = Time.timeSinceLevelLoad - initTime;
        if (timeSinceInit > 1f)
        {
            ReturnToPool(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        GameObject obj = other.gameObject;
        //source is null if projectile came from player. null for enemy source.
        if ((obj.CompareTag("Enemy") || obj.CompareTag("Player")) && source != null)
        {
            source.GetComponent<AgentProjectileController>().OnHit(obj);
        }
        //if statement for hits with player projectiles. (if the player hit something).
        //if (source == null)
    

        //play effect.
        ReturnToPool(gameObject);
            
    }

    public void ReturnToPool(GameObject obj)
    {
        projectilePool.ReturnProjectile(obj);
    }

}
