using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class ItemEquipGraphics : MonoBehaviour
    {
        private enum SpriteRenderSlots
        {
            Helmet,
            ChestMain,
            Pelvis,
            LeftWeapon,
            RightWeapon,
            ChestRightArm,
            ChestRightHand,
            ChestLeftArm,
            ChestLeftHand,
            RightLeg,
            RightFoot,
            LeftLeg,
            LeftFoot
        }
        private EquippedItems eqItems;
        public SpriteRenderer[] equipSlots = new SpriteRenderer[13];
        void Awake()
        {
            eqItems = GetComponent<EquippedItems>();
            EventManager.OnItemEquipped += EquipItem;
            EventManager.OnItemUnequipped += UnequipItem;
        }
        private void EquipItem(IItem item, int who)
        {
            if (who != gameObject.GetHashCode())
                return;
            IEquipable equipableItem = (IEquipable)item;
            equipSlots[(int)equipableItem.Slot].sprite = RetrieveSprite(equipableItem.SpriteCategoryLabel.Item1, equipableItem.SpriteCategoryLabel.Item2);
            if (equipableItem.Slot == EquipSlot.Chest)
            {
                equipSlots[(int)SpriteRenderSlots.ChestRightArm].sprite = RetrieveSprite("Right_Arm", equipableItem.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.ChestRightHand].sprite = RetrieveSprite("Right_Hand", equipableItem.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.ChestLeftArm].sprite = RetrieveSprite("Left_Arm", equipableItem.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.ChestLeftHand].sprite = RetrieveSprite("Left_Hand", equipableItem.SpriteCategoryLabel.Item2);
            }
            else if (equipableItem.Slot == EquipSlot.Legs)
            {
                equipSlots[(int)SpriteRenderSlots.RightLeg].sprite = RetrieveSprite("Right_Leg", equipableItem.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.RightFoot].sprite = RetrieveSprite("Right_Foot", equipableItem.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.LeftLeg].sprite = RetrieveSprite("Left_Leg", equipableItem.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.LeftFoot].sprite = RetrieveSprite("Left_Foot", equipableItem.SpriteCategoryLabel.Item2);
            }
            MessageSystem.Print($"{name} equips item");
        }
        private void UnequipItem(IItem item, int who)
        {
            if (who != gameObject.GetHashCode())
                return;
            IEquipable equipableItem = (IEquipable)item;
            equipSlots[(int)equipableItem.Slot].sprite = null;
            if (equipableItem.Slot == EquipSlot.Chest)
            {
                equipSlots[(int)SpriteRenderSlots.ChestRightArm].sprite = null;
                equipSlots[(int)SpriteRenderSlots.ChestRightHand].sprite = null;
                equipSlots[(int)SpriteRenderSlots.ChestLeftArm].sprite = null;
                equipSlots[(int)SpriteRenderSlots.ChestLeftHand].sprite = null;
            }
            else if (equipableItem.Slot == EquipSlot.Legs)
            {
                equipSlots[(int)SpriteRenderSlots.RightLeg].sprite = null;
                equipSlots[(int)SpriteRenderSlots.RightFoot].sprite = null;
                equipSlots[(int)SpriteRenderSlots.LeftLeg].sprite = null;
                equipSlots[(int)SpriteRenderSlots.LeftFoot].sprite = null;
            }
            MessageSystem.Print($"{name} unequips item");
        }
        private Sprite RetrieveSprite(string category, string label)
        {
            Sprite sprite = GameManager.instance.itemSpriteLibrary.GetSprite(category, label: label);
            return sprite;
        }
    }
}