using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboDisplay : Singleton<ComboDisplay>
{
    Text comboText;
    
    protected override void Awake()
    {
        base.Awake();
        comboText = GetComponent<Text>();
    }

    void Start()
    {
        ScoreManager.Instance.Reset();
    }

    public void UpdateCombo(int combo)
    {
        comboText.text = "Combo"+combo.ToString();
    }
}
