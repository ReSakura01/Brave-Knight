using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CurrencyTextUI : MonoBehaviour
{
    public TextMeshProUGUI currencyText;

    public void Start()
    {
        currencyText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        currencyText.text = PlayerManager.instance.GetCurrency().ToString();
    }
}
