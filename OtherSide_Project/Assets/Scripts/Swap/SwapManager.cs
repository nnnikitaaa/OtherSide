using UnityEngine;
using System;

public class SwapManager : MonoBehaviour
{
    public Color normalColor;
    public Color insideColor;
    public SpriteRenderer maskSprite;
    public Action onSwap;
    public static SwapManager i { get; set; }
    private void Awake()
    {
        i = this;
    }
    public void Swap()
    {
        onSwap?.Invoke();
        if (maskSprite.color == normalColor)
        {
            maskSprite.color = insideColor;
        }
        else
        {
            maskSprite.color = normalColor;
        }
        if (Camera.main.backgroundColor == normalColor)
        {
            Camera.main.backgroundColor = insideColor;
        }
        else
        {
            Camera.main.backgroundColor = normalColor;
        }
    }
}
