using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public GameObject bullet;
    
    private readonly Queue<GameObject> projectiles = new Queue<GameObject>();
    public Projectile projectile;
    private int poolStartSize = 35;
    
    // Start is called before the first frame update
    void Start()
    {
        InitPool();
    }

    private void InitPool()
    {
        for (int i = 0; i < poolStartSize; i++)
        {
            GameObject proj = Instantiate(bullet, Vector3.zero, Quaternion.identity);
            projectile.ReturnToPool(proj);
        }
    }

    public GameObject GetProjectile(Vector3 pos, Quaternion rot)
    {
        if (projectiles.Count>0)
        {
            GameObject obj = projectiles.Dequeue();
            obj.SetActive(true);
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            return obj;
        }
        GameObject proj = Instantiate(bullet, pos, rot);
        return proj;
    }


    public void ReturnProjectile(GameObject proj)
    {
        proj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        proj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        projectiles.Enqueue(proj);
        proj.SetActive(false);
    }
}
