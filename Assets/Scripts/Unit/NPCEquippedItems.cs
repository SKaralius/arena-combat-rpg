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
            int tier = Mathf.CeilToInt((float)gameManager.nextEncounterNumber / 10);
            if (gameManager.nextEncounterNumber % 10 == 0)
            {
                this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 1.5f, gameObject.transform.localScale.y * 2f, gameObject.transform.localScale.z);
                tier += 1;
            }
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.RightWeapon));
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.LeftWeapon));
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.Legs));
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.Chest, ESkills.EarthStrike));
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.Head, ESkills.Lightning));
        }
    }

}