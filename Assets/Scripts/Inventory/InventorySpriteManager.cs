﻿using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySpriteManager : MonoBehaviour
    {
        [SerializeField] private Image[] spriteSlots = new Image[13];
        [SerializeField] private GameObject skillIndicator;
        [SerializeField] private Image backgroundImage;

        public void CreateAndDisplaySprite(EquippableItem item)
        {
            // Goes first
            HideSprite();

            ChangeAlpha(spriteSlots[(int)item.Slot], 1);
            spriteSlots[(int)item.Slot].sprite = ItemEquipGraphics.RetrieveSprite(item.SpriteCategoryLabel.Item1, item.SpriteCategoryLabel.Item2);
            ChestAndLegAdditionalSprites(item);
            SetColorBasedOnRarity(item);
            if (item.Skill != ESkills.None)
                skillIndicator.SetActive(true);
            else
                skillIndicator.SetActive(false);
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

        private void SetColorBasedOnRarity(EquippableItem item)
        {
            switch (item.Rarity)
            {
                case "Uncommon":
                    backgroundImage.color = new Color(0.1176471f, 1, 0);
                    break;
                case "Rare":
                    backgroundImage.color = new Color(0, 0.4392157f, 0.8666667f);
                    break;
                case "Epic":
                    backgroundImage.color = new Color(0.6392157f, 0.2078431f, 0.9333333f);
                    break;
                case "Legendary":
                    backgroundImage.color = new Color(1f, 0.5019608f, 0);
                    break;
                default:
                    backgroundImage.color = Color.white;
                    break;
            }
        }

        public void HideSprite()
        {
            foreach (Image image in spriteSlots)
            {
                ChangeAlpha(image, 0);
            }
            skillIndicator.SetActive(false);
            backgroundImage.color = Color.white;
        }

        private void ChangeAlpha(Image image, float alpha)
        {
            Color tmp = image.color;
            tmp.a = alpha;
            image.color = tmp;
        }

    }
}