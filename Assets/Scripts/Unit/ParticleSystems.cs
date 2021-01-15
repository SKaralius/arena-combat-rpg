using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class ParticleSystems : MonoBehaviour
    {
        public bool evaded = true;

        [SerializeField] private ParticleSystem[] particleSystemPrefabs;
        [SerializeField] private GameObject spellParticleLocation;

        private List<ParticleSystem> particleSystems = new List<ParticleSystem>();

        public void Start()
        {
            foreach(ParticleSystem ps in particleSystemPrefabs)
            {
                particleSystems.Add(Instantiate(ps, spellParticleLocation.transform));
            }
        }
        public void PlaySparks()
        {
            if (!evaded)
            {
                particleSystems[0].Play();
            }
            evaded = true;
        }
        public void FireLoad()
        {

        }
    }

}