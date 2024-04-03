using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MonsterCard SO", fileName = "new MonsterCardSO")]
public class MonsterCardSO : ScriptableObject
{
    [SerializeField] string cardName; // 카드 이름
    [TextArea(3, 6)]
    [SerializeField] string description; // 카드 설명   
    [SerializeField] string type; // 카드 타입
    [SerializeField] int cost; // 카드 비용  
    [SerializeField] Sprite artwork; // 카드 이미지

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