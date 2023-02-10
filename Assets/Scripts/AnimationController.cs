using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    
    private Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
        anim.Play("inside out");
        
    }
    void Update()
    {
        // leave to complete before changing
        if (anim.isPlaying)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.Play("inside in");
        }

        
    }
}
