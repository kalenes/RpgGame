using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcEffects : MonoBehaviour
{
    public ParticleSystem hitEffect;

    public void GetHitEffect()
    {
        hitEffect.Play();
    }

}
