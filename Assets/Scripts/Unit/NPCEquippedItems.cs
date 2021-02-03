using UnityEngine;

namespace Unit
{
    public class NPCEquippedItems : EquippedItems
    {
        GameManager gameManager;
        private new void Awake()
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            base.Awake();
        }
        private void Start()
        {
            // TODO: This can be used to implement difficulty
            int opponentStrengthModifier = 30;
            int tier = GameManager.instance.nextEncounterNumber + opponentStrengthModifier;
            // Boss
            if (tier % 10 == 0)
            {
                this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 1.5f, gameObject.transform.localScale.y * 2f, gameObject.transform.localScale.z);
                tier += 5;
            }
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.RightWeapon));
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.LeftWeapon));
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.Legs));
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.Chest));
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.Head));
        }
    }

}