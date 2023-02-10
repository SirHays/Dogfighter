using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //ship
    public Transform target;
    //time for camera to align itself back with the ship
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;
    //reference point for the camera to look at for better angle
    public GameObject lookat;
    //third person camera
    public Camera TP;
    //first person camera
    public Camera FP;

    void Start()
    {
      TP.enabled =true;
      FP.enabled =false;
    }
    
    void LateUpdate()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 7, -25));
        
        // Smoothly move the camera towards that target position
        TP.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
        //set camera to look at a target above the ship
        TP.transform.LookAt(lookat.transform,target.up);
        
        
        //move to first person view
        if (Input.GetKeyDown(KeyCode.V) && TP.enabled) 
        {
            Cursor.visible = false;
            TP.enabled =false;
            FP.enabled =true;
            lookat.SetActive(false);
        }
        //move to third person view
        else if(Input.GetKeyDown(KeyCode.V) && !TP.enabled)
        {
            Cursor.visible = true;
            TP.enabled =true;
            FP.enabled =false;
            lookat.SetActive(true);
        }
        
    }
}
