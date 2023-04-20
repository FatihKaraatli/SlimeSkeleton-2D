using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MainMenuCapsuleController : MonoBehaviour
{
    public Light2D spriteLight;
    public Light2D spotLight;
    public Light2D capsuleLightning;

    public void FlashEffect()
    {
        spriteLight.enabled = true;
        spotLight.intensity = 1.5f;
        Invoke(nameof(CloseLightFlash),0.15f);
    }
    
    public void FlashEffectSmall()
    {
        capsuleLightning.enabled = true;
        Invoke(nameof(CloseLightFlashSmall),0.15f);
    }

    public void CloseLightFlash()
    {
        spriteLight.enabled = false;
        spotLight.intensity = 0.75f;
    }
    
    public void CloseLightFlashSmall()
    {
        capsuleLightning.enabled = false;
    }
}
