using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardData cardData;
    public SpriteRenderer spriteRenderer;
    public TextMeshPro textName;
    public TextMeshPro textType;
    public TextMeshPro textDesc;
    public TextMeshPro textCost;

    private void Awake()
    {
        spriteRenderer.sprite = cardData.Icon;
        spriteRenderer.transform.localScale = new Vector3(1.4f, 1.26f, 1);

        textName.text = cardData.name;
        switch (cardData.Type)
        {
            case CardData.CardType.Attack:
                textType.text = "����";
                break;
            case CardData.CardType.Skill:
                textType.text = "��ų";
                break;
            case CardData.CardType.Power:
                textType.text = "�Ŀ�";
                break;
        }
        textDesc.text = cardData.baseDesc;
        textCost.text = cardData.cost.ToString();
    }
}
