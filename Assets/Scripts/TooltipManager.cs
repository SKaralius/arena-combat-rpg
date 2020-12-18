﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public GameObject tooltip;
    public TextMeshProUGUI textComp;
    void Awake()
    {
        GameObject textGO = tooltip.transform.Find("TooltipText").gameObject;
        textComp = textGO.GetComponent<TextMeshProUGUI>();
    }
}
