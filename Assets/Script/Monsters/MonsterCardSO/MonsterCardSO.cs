using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MonsterCard SO", fileName = "new MonsterCardSO")]
public class MonsterCardSO : ScriptableObject
{
    [SerializeField] string cardName; // ī�� �̸�
    [TextArea(3, 6)]
    [SerializeField] string description; // ī�� ����   
    [SerializeField] string type; // ī�� Ÿ��
    [SerializeField] int cost; // ī�� ���  
    [SerializeField] Sprite artwork; // ī�� �̹���

    public string GetCardName()
    {
        return cardName;
    }

    public string GetCardDescription()
    {
        return description;
    }

    public string GetCardType()
    {
        return type;
    }

    public int GetCardCost()
    {
        return cost;
    }

    public Sprite GetCardSprite()
    {
        return artwork;
    }
}