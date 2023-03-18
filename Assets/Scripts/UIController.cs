using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public TMP_Text CoinText;
    public TMP_Text ScoreText;
    public TMP_Text MultiplierText;
    public int CurrentCoins = 0;
    public int CurrentScore = 0;
    public int CurrentMultiplier = 1;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        ScoreText.text = CurrentScore.ToString();
        MultiplierText.text = "X"+CurrentMultiplier.ToString();
        CoinText.text = CurrentCoins.ToString();
    }

    public void IncreaseScore(int distance, int multiplier)
    {
        CurrentScore += (distance / distance) * multiplier;
        ScoreText.text = CurrentScore.ToString();
        MultiplierText.text = "X"+multiplier.ToString();
    }
    public void IncreaseCoins(int amount)
    {
        CurrentCoins += amount;
        CoinText.text = CurrentCoins.ToString();
    }
}
