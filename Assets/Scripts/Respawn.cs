using System.Collections;
using UnityEngine;

// explosion effect not getting destroyed after respawn. fix plz/ fixed


public class Respawn : MonoBehaviour
{
    public GameObject ship;
    public GameObject explosionPrefab;
    private GameObject pow;
    public Camera TP;
   
    public void Hit(){
        
        StartCoroutine(Waiter()); 
    }

    private IEnumerator Waiter()
    {
        pow = Instantiate(explosionPrefab, ship.transform.position, ship.transform.rotation);
        ship.SetActive(false);
        if (TP != null)
        {
            if (!TP.enabled)
            {
                TP.enabled = true;
                yield return new WaitForSeconds(2);
                TP.enabled = false;
            }
            else
            {
                yield return new WaitForSeconds(2);
            }
        }

        GameObject[] explosions = GameObject.FindGameObjectsWithTag("Explosion");
            foreach (GameObject exp in explosions)
            {
                Destroy(exp);
            }
            
            ship.transform.position = Vector3.zero;
            ship.transform.rotation = Quaternion.identity;
            ship.GetComponent<Rigidbody>().velocity =Vector3.zero;
            ship.GetComponent<Rigidbody>().angularVelocity =Vector3.zero;
            ship.SetActive(true);
    }
}
