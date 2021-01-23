using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySpriteManager : MonoBehaviour
    {
        public Image[] spriteSlots = new Image[13];

        public void CreateAndDisplaySprite(EquippableItem item)
        {
            HideSprite();
            ChangeAlpha(spriteSlots[(int)item.Slot], 1);
            spriteSlots[(int)item.Slot].sprite = ItemEquipGraphics.RetrieveSprite(item.SpriteCategoryLabel.Item1, item.SpriteCategoryLabel.Item2);
            ChestAndLegAdditionalSprites(item);
        }

        private void ChestAndLegAdditionalSprites(EquippableItem item)
        {
            if (item.Slot == EquipSlot.Chest)
            {
                ShowSlot("Right_Arm", item.SpriteCategoryLabel.Item2, SpriteRenderSlots.ChestRightArm);
                ShowSlot("Right_Hand", item.SpriteCategoryLabel.Item2, SpriteRenderSlots.ChestRightHand);
                ShowSlot("Left_Arm", item.SpriteCategoryLabel.Item2, SpriteRenderSlots.ChestLeftArm);
                ShowSlot("Left_Hand", item.SpriteCategoryLabel.Item2, SpriteRenderSlots.ChestLeftHand);
            }
            else if (item.Slot == EquipSlot.Legs)
            {
                ShowSlot("Right_Leg", item.SpriteCategoryLabel.Item2, SpriteRenderSlots.RightLeg);
                ShowSlot("Right_Calf", item.SpriteCategoryLabel.Item2, SpriteRenderSlots.RightCalf);
                ShowSlot("Left_Leg", item.SpriteCategoryLabel.Item2, SpriteRenderSlots.LeftLeg);
                ShowSlot("Left_Calf", item.SpriteCategoryLabel.Item2, SpriteRenderSlots.LeftCalf);
            }
        }
        private void ShowSlot(string nameOfSlot, string label, SpriteRenderSlots slot)
        {
            Sprite sprite = ItemEquipGraphics.RetrieveSprite(nameOfSlot, label);
            if (sprite == null)
                ChangeAlpha(spriteSlots[(int)slot], 0);
            else
            {
                spriteSlots[(int)slot].sprite = sprite;
                ChangeAlpha(spriteSlots[(int)slot], 1);
            }

        }

        public void HideSprite()
        {
            foreach (Image image in spriteSlots)
            {
                ChangeAlpha(image, 0);
            }
        }

        private void ChangeAlpha(Image image, float alpha)
        {
            Color tmp = image.color;
            tmp.a = alpha;
            image.color = tmp;
        }

    }
}