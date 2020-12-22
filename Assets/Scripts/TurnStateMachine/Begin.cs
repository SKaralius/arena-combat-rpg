using System.Collections;
using UnityEngine;
using Unit;
using Battle;

namespace TurnFSM
{
    public class Begin : State
    {
        GameObject enemyHealthBarGO;
        GameObject playerHealthBarGO;
        public Begin(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            SetUpBattle();
            MessageSystem.Print("The battle has begun");
            yield return new WaitForSeconds(0.1f);
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }
        private void SetUpBattle()
        {
            SpawnHealthBars();
            BattleSystem.Player.ResetHealth();
        }
        private void SpawnHealthBars()
        {
            // Enemy
            enemyHealthBarGO = Object.Instantiate(BattleSystem.healthBarPrefab, GameObject.Find("UI").transform);

            enemyHealthBarGO.transform.position = new Vector2(enemyHealthBarGO.transform.position.x * -1, enemyHealthBarGO.transform.position.y);
            enemyHealthBarGO.GetComponent<RectTransform>().anchorMin = new Vector2(1, 1);
            enemyHealthBarGO.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            HealthBar enemyHealthBar = enemyHealthBarGO.GetComponentInChildren<HealthBar>();
            enemyHealthBar.unitStats = BattleSystem.Enemy.GetComponent<UnitStats>();
            BattleSystem.Enemy.GetComponent<Controller>().healthBar = enemyHealthBar;

            // Player
            playerHealthBarGO = Object.Instantiate(BattleSystem.healthBarPrefab, GameObject.Find("UI").transform);
            HealthBar playerHealthBar = playerHealthBarGO.GetComponentInChildren<HealthBar>();
            playerHealthBar.unitStats = BattleSystem.Player.GetComponent<UnitStats>();
            BattleSystem.Player.GetComponent<Controller>().healthBar = playerHealthBar;
        }
    }
}