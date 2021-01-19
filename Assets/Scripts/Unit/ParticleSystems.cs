using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;

namespace Unit
{
    public class ParticleSystems : MonoBehaviour
    {
        public bool evaded = true;
        public bool amPlayer;

        [SerializeField] private ParticleSystem[] particleSystemPrefabs;
        [SerializeField] private GameObject spellParticleLocation;
        private UnitMovement spellParticleMovement;
        private Transform spellParticleLocationParent;
        public static readonly float projectileTravelDuration = 0.2f;

        private List<ParticleSystem> particleSystems = new List<ParticleSystem>();

        public void Start()
        {
            spellParticleLocationParent = spellParticleLocation.transform.parent;
            spellParticleMovement = spellParticleLocation.GetComponent<UnitMovement>();
            foreach (ParticleSystem ps in particleSystemPrefabs)
            {
                ParticleSystem instantiatedPs = Instantiate(ps, spellParticleLocation.transform);
                instantiatedPs.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                particleSystems.Add(instantiatedPs);
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
            particleSystems[1].Play();
        }
        public void ShootProjectile()
        {
            GameObject battleSystemGO = GameObject.Find("BattleSystem");
            BattleSystem battleSystem = battleSystemGO.GetComponent<BattleSystem>();
            if (amPlayer)
            {
                StartCoroutine(ShootProjectile(battleSystem.Enemy.transform.position.x));
            } 
            else
            {
                StartCoroutine(ShootProjectile(battleSystem.Player.transform.position.x));
            }
        }
        private IEnumerator ShootProjectile(float posX)
        {
            spellParticleLocation.transform.SetParent(null);
            yield return spellParticleMovement.MoveUnit(posX, projectileTravelDuration);
            foreach(ParticleSystem ps in particleSystems)
            {
                ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
            if (!evaded)
            {
                // TODO: Explosion
            } else
            {
                // Fail sound?
            }
            spellParticleLocation.transform.SetParent(spellParticleLocationParent);
            spellParticleLocation.transform.localPosition = Vector3.zero;
        }
    }

}