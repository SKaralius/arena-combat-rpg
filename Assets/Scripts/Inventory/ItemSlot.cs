using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public abstract class ItemSlot : MonoBehaviour
{
    protected EquippableItem _item = null;
    public virtual EquippableItem item { set {
            _item = value;
        } }
    public TextMeshProUGUI itemName;
    public Image itemImage;
    public Button button;
    // Start is called before the first frame update
    public abstract void RenderUI();
    protected virtual void RenameButton(string name)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = name;
    }
}
