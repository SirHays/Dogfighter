
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class AgentProjectileController : MonoBehaviour
{
    public event EventHandler<EnemyHitEventArgs> onEnemyHit;
    
    public class EnemyHitEventArgs
    {
        public GameObject enemyHit;
    }
   
    public float shootForce;
    public float timeBetweenShooting;


    public Camera mainCam;
    public Transform attackPointL;
    public Transform attackPointR;
    public ProjectilePool projectilePool;
    private bool canShoot = true;
    
    public void Shoot()
    {
        if(!canShoot) return;
        
        //Find the exact hit position using a raycast
        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //a point far away from the player

        
        Vector3 leftPosition = attackPointL.position;
        Vector3 rightPosition = attackPointR.position;
        //Calculate direction from attackPoint to targetPoint
        Vector3 direction1 = targetPoint - leftPosition;
        Vector3 direction2 = targetPoint - rightPosition;
        
        //Instantiate projectile
        Quaternion rot = transform.rotation;

        GameObject bulletL = projectilePool.GetProjectile(leftPosition,rot);
        GameObject bulletR = projectilePool.GetProjectile(rightPosition, rot);
        
        //Rotate projectile to shoot direction
        bulletL.transform.rotation *= Quaternion.Euler(0,0,120);
        bulletR.transform.rotation *= Quaternion.Euler(0,0,60);
        
        //Add forces to projectile
        bulletL.GetComponent<Rigidbody>().AddForce(direction1.normalized * shootForce, ForceMode.Impulse);
        bulletR.GetComponent<Rigidbody>().AddForce(direction2.normalized * shootForce, ForceMode.Impulse);
        
        //access projectile scripts for two bullets
        Projectile p1 = bulletL.GetComponent<Projectile>();
        Projectile p2 = bulletR.GetComponent<Projectile>();
        
        //if script isn't null, set source of projectile to be current gameobject.
        if (p1 != null) p1.source = gameObject;
        if (p2 != null) p2.source = gameObject;
        
        

        canShoot = false;
        Invoke(nameof(ResetShot), timeBetweenShooting);
    }

    public void OnHit(GameObject shipHit)
    {
        //this event exists in this class because i need the sender to be the gameobject which fired the projectile.
        onEnemyHit?.Invoke(this,new EnemyHitEventArgs{enemyHit = shipHit});
    }

    public void ResetShot()
    {
        //Allow shooting again
        canShoot = true;
    }
}
