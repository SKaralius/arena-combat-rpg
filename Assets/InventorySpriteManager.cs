using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unit;

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
    private void ChestAndLegAdditionalSprites(EquippableItem item)
    {
        if (item.Slot == EquipSlot.Chest)
        {
            spriteSlots[(int)SpriteRenderSlots.ChestRightArm].sprite = ItemEquipGraphics.RetrieveSprite("Right_Arm", item.SpriteCategoryLabel.Item2);
            ChangeAlpha(spriteSlots[(int)SpriteRenderSlots.ChestRightArm], 1);
            spriteSlots[(int)SpriteRenderSlots.ChestRightHand].sprite = ItemEquipGraphics.RetrieveSprite("Right_Hand", item.SpriteCategoryLabel.Item2);
            ChangeAlpha(spriteSlots[(int)SpriteRenderSlots.ChestRightHand], 1);
            spriteSlots[(int)SpriteRenderSlots.ChestLeftArm].sprite = ItemEquipGraphics.RetrieveSprite("Left_Arm", item.SpriteCategoryLabel.Item2);
            ChangeAlpha(spriteSlots[(int)SpriteRenderSlots.ChestLeftArm], 1);
            spriteSlots[(int)SpriteRenderSlots.ChestLeftHand].sprite = ItemEquipGraphics.RetrieveSprite("Left_Hand", item.SpriteCategoryLabel.Item2);
            ChangeAlpha(spriteSlots[(int)SpriteRenderSlots.ChestLeftHand], 1);
        }
        else if (item.Slot == EquipSlot.Legs)
        {
            spriteSlots[(int)SpriteRenderSlots.RightLeg].sprite = ItemEquipGraphics.RetrieveSprite("Right_Leg", item.SpriteCategoryLabel.Item2);
            ChangeAlpha(spriteSlots[(int)SpriteRenderSlots.RightLeg], 1);
            spriteSlots[(int)SpriteRenderSlots.RightFoot].sprite = ItemEquipGraphics.RetrieveSprite("Right_Foot", item.SpriteCategoryLabel.Item2);
            ChangeAlpha(spriteSlots[(int)SpriteRenderSlots.RightFoot], 1);
            spriteSlots[(int)SpriteRenderSlots.LeftLeg].sprite = ItemEquipGraphics.RetrieveSprite("Left_Leg", item.SpriteCategoryLabel.Item2);
            ChangeAlpha(spriteSlots[(int)SpriteRenderSlots.LeftLeg], 1);
            spriteSlots[(int)SpriteRenderSlots.LeftFoot].sprite = ItemEquipGraphics.RetrieveSprite("Left_Foot", item.SpriteCategoryLabel.Item2);
            ChangeAlpha(spriteSlots[(int)SpriteRenderSlots.LeftFoot], 1);
        }
    }
}
