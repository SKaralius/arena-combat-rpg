using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gold : MonoBehaviour
{
    private TextMeshProUGUI wealthText;
    private int _wealth = 0;
    public int Wealth { 
        get => _wealth;
        set
        {
            ChangeGold(value);
        }
    }
    public static Gold instance;
    // Start is called before the first frame update
    #region Singleton logic
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion
    void Start()
    {
        wealthText = GetComponent<TextMeshProUGUI>();
        EventManager.OnGoldChange += ChangeGold;
    }
    private void ChangeGold(int amount)
    {
        _wealth += amount;
        wealthText.text = Wealth.ToString();
    }
}
