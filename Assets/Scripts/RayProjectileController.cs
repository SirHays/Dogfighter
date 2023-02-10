
using UnityEngine;
using TMPro;

public class RayProjectileController : MonoBehaviour
{
    
    public GameObject bullet;
    public ProjectilePool projectilePool;
   
    public float shootForce, upwardForce;

    
    public float timeBetweenShooting, timeBetweenShots;
    public bool allowButtonHold;
    
    
   
    bool shooting, readyToShoot;

   
    public Camera mainCam;
    public Transform attackPointTL;
    public Transform attackPointTR;
    public Transform attackPointBL;
    public Transform attackPointBR;
    
    
    public bool allowInvoke = true;

    private void Awake()
    {
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
        
    }
    private void MyInput()
    {
        
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //Find the exact hit position using a raycast
        // Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        // RaycastHit hit;
        //
        // //check if ray hits something
        // Vector3 targetPoint;
        // if (Physics.Raycast(ray, out hit))
        //     targetPoint = hit.point;
        // else
        //     targetPoint = ray.GetPoint(75); //Just a point far away from the player
        //
        // //Calculate direction from attackPoint to targetPoint
        // Vector3 direction1 = targetPoint - attackPointTL.position;
        // Vector3 direction2 = targetPoint - attackPointTR.position;
        // Vector3 direction3 = targetPoint - attackPointBL.position;
        // Vector3 direction4 = targetPoint - attackPointBR.position;
        //Instantiate projectile
        Quaternion rot1 = attackPointTL.rotation;
        Quaternion rot2 = attackPointTR.rotation;
        Quaternion rot3 = attackPointBL.rotation;
        Quaternion rot4 = attackPointBR.rotation;
        
        GameObject bulletTL = projectilePool.GetProjectile(attackPointTL.position, rot1);
        GameObject bulletTR = projectilePool.GetProjectile(attackPointTR.position, rot2);
        GameObject bulletBL = projectilePool.GetProjectile(attackPointBL.position, rot3);
        GameObject bulletBR = projectilePool.GetProjectile(attackPointBR.position, rot4);
        
        //Rotate projectile to shoot direction
         bulletTL.transform.rotation *= Quaternion.Euler(0,0,120);
         bulletTR.transform.rotation *= Quaternion.Euler(0,0,60);
         bulletBL.transform.rotation *= Quaternion.Euler(0,0,120);
         bulletBR.transform.rotation *= Quaternion.Euler(0,0,60);
         

        //Add forces to projectile
        bulletTL.GetComponent<Rigidbody>().AddForce(attackPointTL.forward * shootForce, ForceMode.Impulse);
        bulletTR.GetComponent<Rigidbody>().AddForce(attackPointTR.forward * shootForce, ForceMode.Impulse);
        bulletBL.GetComponent<Rigidbody>().AddForce(attackPointBL.forward * shootForce, ForceMode.Impulse);
        bulletBR.GetComponent<Rigidbody>().AddForce(attackPointBR.forward * shootForce, ForceMode.Impulse);
        
        
        if (!allowInvoke) return;
        Invoke(nameof(ResetShot), timeBetweenShooting);
        allowInvoke = false;

    }
    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }
}
