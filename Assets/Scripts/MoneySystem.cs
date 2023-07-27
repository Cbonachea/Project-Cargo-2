using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    private int currentMoney;
    private int payOutAmount;

    private Text moneyUI;

    void Start()
    {
        GameEvents.current.onDeliverCargo += PayOut;
        moneyUI = GetComponentInChildren<Text>();
    }

    private void PayOut()
    {
        payOutAmount = Random.Range(6, 12);
        currentMoney = currentMoney + payOutAmount;
        moneyUI.text = currentMoney.ToString();
        Debug.Log("current money = " + currentMoney);
    }
}
