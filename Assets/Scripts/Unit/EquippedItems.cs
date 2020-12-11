using UnityEngine;

namespace Unit
{
    public abstract class EquippedItems : MonoBehaviour
    {
        public EquippableItem[] equippedItems { get; protected set; } = new EquippableItem[5];
        protected ItemEquipGraphics itemEquipGraphics;
        protected CharacterSkills characterSkills;
        protected HealthBar healthBar;

        protected void Start()
        {
            itemEquipGraphics = GetComponent<ItemEquipGraphics>();
            characterSkills = GetComponent<CharacterSkills>();
            healthBar = GetComponent<HealthBar>();
            //if (!isPlayer)
            //{
            //    //Equip(new EquippableItem(EquipSlot.RightWeapon, "Knockbacking Staff", ("Weapon", "Staff"), _sellPrice: 69, _attackRange: 10, _damage: 5, _evasion: 5, _skill: ESkills.Knockback));
            //    Equip(new EquippableItem(EquipSlot.LeftWeapon, "Twice Sword", ("Weapon", "Sword"), _sellPrice: 69, _damage: 5, _evasion: 5, _skill: ESkills.HitTwice));
            //}
        }

        public virtual void Equip(EquippableItem item)
        {
            equippedItems[(int)item.Slot] = item;
            MessageSystem.Print($"{item.Name} is equiped on {gameObject.name}.");
            itemEquipGraphics.EquipItem(item);
            characterSkills.AddToSkillList(item);
            if (healthBar)
                healthBar.UpdateHealthBar(GetComponent<Controller>().Health);
        }

        public virtual void Unequip(EquipSlot Slot)
        {
            EquippableItem item = equippedItems[(int)Slot];
            equippedItems[(int)Slot] = null;
            MessageSystem.Print($"{item.Name} is unequiped on {gameObject.name}.");
            itemEquipGraphics.UnequipItem(Slot);
            characterSkills.RemoveFromSkillList(item);
            if (healthBar)
                healthBar.UpdateHealthBar(GetComponent<Controller>().Health);
        }
    }
}