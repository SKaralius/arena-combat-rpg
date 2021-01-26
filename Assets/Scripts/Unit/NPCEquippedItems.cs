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
            int tier = GameManager.instance.nextEncounterNumber;
            // Boss
            if (tier % 10 == 0)
            {
                this.gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 1.5f, gameObject.transform.localScale.y * 2f, gameObject.transform.localScale.z);
                tier += 5;
            }
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.RightWeapon, guaranteeSkill: true));
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.LeftWeapon, guaranteeSkill: true));
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.Legs, guaranteeSkill: true));
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.Chest, guaranteeSkill: true));
            Equip(ItemGenerator.GenerateItem(tier, EquipSlot.Head, guaranteeSkill: false));
        }
    }

}