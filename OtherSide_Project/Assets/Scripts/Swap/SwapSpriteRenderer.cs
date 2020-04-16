using UnityEngine;

public class SwapSpriteRenderer : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SwapManager.i.onSwap += Swap;
    }
    public void Swap()
    {
        
        if (spriteRenderer.maskInteraction == SpriteMaskInteraction.VisibleInsideMask)
        {
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
        else
        {
            spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    }
    public void OnDisable()
    {
        SwapManager.i.onSwap -= Swap;
    }
}
