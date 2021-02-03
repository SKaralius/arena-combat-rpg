using System.Collections;
using Unit;
using UnityEngine;
using Battle;
using Inventory;
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
            BattleSystem.Player.UnitStats.ResetModifiers();
            BattleSystem.Player.CharacterActiveEffects.Reset();
            BattleSystem.Player.ResetPosition();

            BattleSystem.Enemy.UnitStats.ResetModifiers();
            BattleSystem.Enemy.CharacterActiveEffects.Reset();
            BattleSystem.Enemy.ResetPosition();
        }
        protected void LoadTown()
        {
            GameObject.Find("Shop").transform.GetChild(0).GetComponent<Shop>().GenerateItems();
            SaverLoader.instance.SaveInventory();
            SceneManager.LoadSceneAsync("TownScene", LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync("CombatScene");
        }
    }
}