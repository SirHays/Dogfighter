using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;

public class TargetingSystem : MonoBehaviour
{
    
    //donzo. configure usage in agent script to start phase 2 training.
    public EventHandler<TargetEventArgs> onTargetLeave;
    private Transform player;
    
    
    public class TargetEventArgs : EventArgs
    {
        public GameObject targetLeft;
    }
    
    
    private readonly List<GameObject> targets = new List<GameObject>();
    private int targetsInRange = 0;
    // private GameObject target;


    private void Awake()
    {
        player = transform.parent;
    }


    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.transform.root.gameObject;
        
        //add the targetable object to the list of targets
        targets.Add(obj);
        targetsInRange++;
        
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject obj = other.transform.root.gameObject;
        onTargetLeave?.Invoke(this,new TargetEventArgs{targetLeft = obj});
        //remove the targetable object from the list of targets
        targets.Remove(obj);
        targetsInRange--;
        
    }

    public GameObject PickTarget()
    {
        if (targetsInRange > 0)
        {
            foreach (GameObject target in targets)
            {
                Transform targetTransform = target.transform;
                float ang = Vector3.Dot(player.forward, (targetTransform.position - player.position).normalized);
                if (ang > 0.6f)
                {
                    return target;
                }
            }
        }
        // else if (targetsInRange > 0)
        // {
        //
        //     List<GameObject> sortedList = targets.OrderByDescending(gameObjects =>
        //     {
        //         Transform targetTransform = gameObjects.transform;
        //         float ang = Vector3.Dot(player.forward, (targetTransform.position - player.position).normalized);
        //         //returns value between 1 (facing) and -1 (facing away)
        //         
        //         return ang;
        //
        //     }).ToList();
    
            // foreach (GameObject target in sortedList)
            // {
            //     Vector3 direction = target.transform.position - player.position;
            //     //try removing the raycast check
            //     if (Physics.Raycast(player.position, direction, out var hit))
            //     {
            //         //works (checked usage of hit.transform.gameObject)
            //         if (hit.transform.gameObject == target)
            //         {
            //             return target;
            //         }
            //     }
            // }
            // return sortedList[0];
        // }
        return null;
    }
}
