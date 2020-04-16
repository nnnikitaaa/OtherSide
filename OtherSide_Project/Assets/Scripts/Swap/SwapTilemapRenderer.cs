using UnityEngine;
using UnityEngine.Tilemaps;
public class SwapTilemapRenderer : MonoBehaviour
{
    TilemapRenderer tilemapRenderer;
    private void Start()
    {
        tilemapRenderer = GetComponent<TilemapRenderer>();
        SwapManager.i.onSwap += Swap;
    }
    public void Swap()
    {

        if (tilemapRenderer.maskInteraction == SpriteMaskInteraction.VisibleInsideMask)
        {
            tilemapRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
        else
        {
            tilemapRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    }
    public void OnDisable()
    {
        SwapManager.i.onSwap -= Swap;
    }
}
