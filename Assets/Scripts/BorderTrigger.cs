using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderTrigger : MonoBehaviour
{
    private bool inArea;
    private BorderEnforcement borderEnforcement;
    public GameObject uiArt;
    public Enemy enemy;
        private void Awake()
    {
        inArea = true;
    }

    private void Start()
    {
        borderEnforcement = GameObject.Find("respawn").GetComponent<BorderEnforcement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.transform.root.gameObject;
        if (obj.CompareTag("Enemy"))
        {
           enemy.Collision(obj);
        }

        if (inArea)
        {
            uiArt.SetActive(true);
            borderEnforcement.ExitPlayArea();
            inArea = false;
        }

        if (!inArea)
        {
            borderEnforcement.EnterPlayArea();
            inArea = true;
        }
    }
}
