using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
    public float FadeRate;
    private Image image;
    private float targetAlpha;
    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
        if(image==null)
        {
            Debug.LogError("Error: No image on "+name);
        }
        targetAlpha = 0.95f;
    }
      
    // Update is called once per frame
    void Update () {
        Color curColor = image.color;
        float alphaDiff = Mathf.Abs(curColor.a-targetAlpha);
        if (alphaDiff>=0.05f)
        {
            curColor.a = Mathf.Lerp(curColor.a,targetAlpha,FadeRate*Time.deltaTime);
            image.color = curColor;
        }
    }
  
    public void FadeOut()
    {
        targetAlpha = 0.0f;
    }
  
    public void FadeIn()
    {
        targetAlpha = 1.0f;
    }
}