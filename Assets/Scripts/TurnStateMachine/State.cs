using System.Collections;
using Unit;
using UnityEngine;
using Battle;
using UnityEngine.SceneManagement;

namespace TurnFSM
{
    public abstract class State
    {
        protected BattleSystem BattleSystem;

        public State(BattleSystem battleSystem)
        {
            BattleSystem = battleSystem;
        }

        // Start is called before the first frame update
        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator UseSkill(UseSkillHandler skill)
        {
            yield break;
        }

        public virtual IEnumerator Equip(EquippableItem item)
        {
            // Base implementation reverts the actions, if it's not the player's turn.
            BattleSystem.Player.GetComponent<PlayerEquippedItems>().Unequip(item.Slot);
            MessageSystem.Print("Not player turn");
            yield break;
        }

        protected virtual void DecideNextState()
        {
            return;
        }
        protected void CleanScene()
        {
            HealthBar[] healthBars = Object.FindObjectsOfType<HealthBar>();
            foreach (HealthBar healthBar in healthBars)
            {
                Object.Destroy(healthBar.transform.parent.gameObject);
            }
        }
        protected void ResetCharacters()
        {
            BattleSystem.Player.GetComponent<UnitStats>().ResetModifiers();
            BattleSystem.Player.GetComponent<CharacterActiveEffects>().Reset();
            BattleSystem.Enemy.GetComponent<UnitStats>().ResetModifiers();
            BattleSystem.Enemy.GetComponent<CharacterActiveEffects>().Reset();
        }
        protected void LoadTown()
        {
            SaverLoader.instance.SaveInventory();
            SceneManager.LoadSceneAsync("TownScene", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("CombatScene");
        }
    }
}