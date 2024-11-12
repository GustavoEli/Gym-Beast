using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUp_UI : MonoBehaviour
{
    [SerializeField] GameObject levelUpcanvas;
    [SerializeField] TextMeshProUGUI valueTxt;
    [SerializeField] TextMeshProUGUI currentLimitTxt;
    [SerializeField] TextMeshProUGUI moneyTxt;
    [SerializeField] Player player;
    int cost = 10;

    void Update()
    {
        valueTxt.text = "R$ " + cost.ToString();
        moneyTxt.text = "Money: " + Player.money.ToString();
        currentLimitTxt.text = "Current Limit: " + player.GetStackLimit().ToString();
    }

    public void StacklLimit_Btn() {
        if (Player.money >= cost) {
            cost += 10;
            player.SetStackLimit();
        }
    }
}
