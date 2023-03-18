using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public TMP_Text coinText;
    public int currentCoins = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        coinText.text = currentCoins.ToString();
    }

    public void IncreaseCoins(int amount)
    {
        currentCoins += amount;
        coinText.text = currentCoins.ToString();
    }
}
