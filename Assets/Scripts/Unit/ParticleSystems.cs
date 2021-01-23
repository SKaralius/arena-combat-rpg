using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;

namespace Unit
{
    public class ParticleSystems : MonoBehaviour
    {
        public enum ESpellLoads
        {
            None,
            Fire,
            Air,
            Earth,
            Lightning
        }
        public enum EExplosions
        {
            None,
            Fire,
            Air,
            Earth,
            Lightning
        }
        public bool evaded = true;
        public ESpellLoads nextSpellLoad = ESpellLoads.None;
        public EExplosions nextExplosion = EExplosions.None;
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
            switch (nextSpellLoad)
            {
                case ESpellLoads.Fire:
                    particleSystems[1].Play();
                    break;
                case ESpellLoads.Air:
                    particleSystems[3].Play();
                    break;
                case ESpellLoads.Earth:
                    particleSystems[6].Play();
                    break;
                case ESpellLoads.Lightning:
                    particleSystems[5].Play();
                    break;
                default:
                    break;
            }
            
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
                PlayExplosion();
                yield return new WaitForSeconds(0.1f);
            } else
            {
                // Fail sound?
            }
            spellParticleLocation.transform.SetParent(spellParticleLocationParent);
            spellParticleLocation.transform.localPosition = Vector3.zero;
            Reset();
        }
        private void PlayExplosion()
        {
            switch (nextExplosion)
            {
                case EExplosions.Fire:
                    particleSystems[2].Play();
                    break;
                case EExplosions.Air:
                    particleSystems[4].Play();
                    break;
                case EExplosions.Lightning:
                    particleSystems[4].Play();
                    break;
                case EExplosions.Earth:
                    particleSystems[7].Play();
                    break;
                default:
                    break;
            }
        }
        private void Reset()
        {
            nextExplosion = EExplosions.None;
            nextSpellLoad = ESpellLoads.None;
            evaded = true;
        }
    }

}