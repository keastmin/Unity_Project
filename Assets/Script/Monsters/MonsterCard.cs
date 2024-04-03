using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCard : MonoBehaviour
{
    [Header("Card Info")]
    [SerializeField] TextMeshProUGUI cardName;
    [SerializeField] TextMeshProUGUI cardDesc;
    [SerializeField] TextMeshProUGUI cardType;
    [SerializeField] TextMeshProUGUI cardCost;
    [SerializeField] Image cardImage;

    [Header("Monster Info")]
    [SerializeField] MonsterCardSO cardSO;

    private void Awake()
    {
        cardName.text = cardSO.GetCardName();
        cardDesc.text = cardSO.GetCardDescription();
        cardType.text = cardSO.GetCardType();
        cardCost.text = cardSO.GetCardCost().ToString("D1");
        cardImage.sprite = cardSO.GetCardSprite();
    }
}
