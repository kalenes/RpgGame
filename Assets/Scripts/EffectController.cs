using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public ParticleSystem healEffect;

    public ParticleSystem hitEffect;

    public void PlayHealEffect()
    {
        healEffect.Play();
    }

    public void PlayHitEffect()
    {
        hitEffect.Play();
    }
}
