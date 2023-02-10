using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BorderEnforcement : MonoBehaviour
{
    private bool trigger;
    private float timer = 10f;
    public Text timerText;
    public GameObject uiArt;
    
    void Update()
    {
        if (trigger)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("f1");
            if (!(timer <= 0)) return;
            trigger = false;
            gameObject.GetComponent<Respawn>().Hit();
        }

    }

    public void ExitPlayArea()
    {
        //uiArt.SetActive(true);
        trigger = true;
    }

    public void EnterPlayArea()
    {
        trigger = false;
        uiArt.SetActive(false);
        timer = 10f;
    }
}
