using UnityEngine;

namespace Unit
{
        public enum SpriteRenderSlots
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
            RightCalf,
            LeftLeg,
            LeftCalf,
            LeftFoot,
            RightFoot
        }
    public class ItemEquipGraphics : MonoBehaviour
    {
        public SpriteRenderer[] equipSlots = new SpriteRenderer[15];

        public void EquipItem(EquippableItem item)
        {
            equipSlots[(int)item.Slot].sprite = RetrieveSprite(item.SpriteCategoryLabel.Item1, item.SpriteCategoryLabel.Item2);
            if (item.Slot == EquipSlot.Chest)
            {
                equipSlots[(int)SpriteRenderSlots.ChestRightArm].sprite = RetrieveSprite("Right_Arm", item.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.ChestRightHand].sprite = RetrieveSprite("Right_Hand", item.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.ChestLeftArm].sprite = RetrieveSprite("Left_Arm", item.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.ChestLeftHand].sprite = RetrieveSprite("Left_Hand", item.SpriteCategoryLabel.Item2);
            }
            else if (item.Slot == EquipSlot.Legs)
            {
                equipSlots[(int)SpriteRenderSlots.RightLeg].sprite = RetrieveSprite("Right_Leg", item.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.RightCalf].sprite = RetrieveSprite("Right_Calf", item.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.RightFoot].sprite = RetrieveSprite("Right_Foot", item.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.LeftLeg].sprite = RetrieveSprite("Left_Leg", item.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.LeftCalf].sprite = RetrieveSprite("Left_Calf", item.SpriteCategoryLabel.Item2);
                equipSlots[(int)SpriteRenderSlots.LeftFoot].sprite = RetrieveSprite("Left_Foot", item.SpriteCategoryLabel.Item2);
            }
        }

        public void UnequipItem(EquipSlot slot)
        {
            equipSlots[(int)slot].sprite = null;
            if (slot == EquipSlot.Chest)
            {
                equipSlots[(int)SpriteRenderSlots.ChestRightArm].sprite = null;
                equipSlots[(int)SpriteRenderSlots.ChestRightHand].sprite = null;
                equipSlots[(int)SpriteRenderSlots.ChestLeftArm].sprite = null;
                equipSlots[(int)SpriteRenderSlots.ChestLeftHand].sprite = null;
            }
            else if (slot == EquipSlot.Legs)
            {
                equipSlots[(int)SpriteRenderSlots.RightLeg].sprite = null;
                equipSlots[(int)SpriteRenderSlots.RightCalf].sprite = null;
                equipSlots[(int)SpriteRenderSlots.LeftLeg].sprite = null;
                equipSlots[(int)SpriteRenderSlots.LeftCalf].sprite = null;
            }
        }

        public static Sprite RetrieveSprite(string category, string label)
        {
            Sprite sprite = GameManager.instance.itemSpriteLibrary.GetSprite(category, label: label);
            return sprite;
        }
    }
}