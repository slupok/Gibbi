using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleLevel : MonoBehaviour
{
    [HideInInspector]public SelectLevelView SelectLevelView;
    private int _lvl;
    public Text LvlText;

    public void ClickLevel()
    {
        SelectLevelView?.SetScene(_lvl);
    }

    public void SetLevel(int value)
    {
        _lvl = value;
        if (LvlText == null)
            return;
        LvlText.text = value.ToString();
    }

    public void SetInteactable(bool flag)
    {
        GetComponent<Toggle>().interactable = flag;
    }

    public void SetToggleGroup(ToggleGroup group)
    {
        GetComponent<Toggle>().group = group;
    }
    
    
    
}
