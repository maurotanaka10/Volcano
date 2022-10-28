using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEfect : MonoBehaviour
{
    public static SoundEfect instance;
    public AudioSource sJump, sExplosion, sMusic, sSwordHit, sHitHero, sSwordSwing, sMeteorSpawn, sDie;

    private void Awake()
    {
        instance = this;
    }
}
