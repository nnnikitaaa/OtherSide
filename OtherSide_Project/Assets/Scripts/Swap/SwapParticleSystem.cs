using UnityEngine;

public class SwapParticleSystem : MonoBehaviour
{
    ParticleSystemRenderer particleSystemRenderer;
    private void Start()
    {
        particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
        SwapManager.i.onSwap += Swap;
    }
    public void Swap()
    {
        
        if (particleSystemRenderer.maskInteraction == SpriteMaskInteraction.VisibleInsideMask)
        {
            particleSystemRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        }
        else
        {
            particleSystemRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
    }
    private void OnDisable()
    {
        SwapManager.i.onSwap -= Swap;
    }
}
