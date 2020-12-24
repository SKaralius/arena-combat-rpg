using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystems : MonoBehaviour
{
    public ParticleSystem WeaponSparks;
    public bool evaded = true;
    public void PlaySparks()
    {
        if (!evaded)
            WeaponSparks.Play();
        evaded = true;
    }
}
