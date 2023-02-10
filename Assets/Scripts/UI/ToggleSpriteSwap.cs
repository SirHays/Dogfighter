using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleSpriteSwap: MonoBehaviour
{
    [SerializeField] private Image targetImage;

    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private bool isOn;

    public bool IsOn { 
        get => isOn;
        set { isOn = value; UpdateValue(); }
    }
    
    public event Action<bool> onValueChanged;

    private Button button;

    // to set initial value and skip onValueChanged notification
    public void Initialize(bool value)
    {
        isOn = value;
        UpdateValue(false);
    }

    // Use this for initialization
    void Start ()
    {
        button = GetComponent<Button>();
        button.transition = Selectable.Transition.None;
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        isOn = !isOn;
        UpdateValue();
    }

    private void UpdateValue(bool notifySubscribers = true)
    {
        if(notifySubscribers && onValueChanged != null)
            onValueChanged(isOn);

        if (targetImage == null)
            return;

        targetImage.sprite = isOn ? onSprite : offSprite;
    }
}