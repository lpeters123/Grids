using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color defaultColor;
    public Color highlightColor;
    public Color pressedColor;
    public Color disabledColor;

    public bool _isDisabled;
    public bool _isPressed;
    public bool _isHighlighted;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();           
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHighlightedState(bool isHighlighted)
    {
        _isHighlighted = isHighlighted;
        UpdateColor();
    }

    public void SetDisabledState(bool isDisabled)
    {
        _isDisabled = isDisabled;
        UpdateColor();
    }
    
    public void SetPressedState(bool isPressed)
    {
        _isPressed = isPressed;
        UpdateColor();
    }


    void UpdateColor() 
    {
        sr.color = GetTileColor();
    }

    private Color GetTileColor() 
    {
        if (_isDisabled) 
        {
            return disabledColor;
        }
        else if (_isPressed) 
        {
            return pressedColor;
        }
        else if(_isHighlighted) 
        {
            return highlightColor;
        }
        else 
        {
            return defaultColor;
        }
    }
}
