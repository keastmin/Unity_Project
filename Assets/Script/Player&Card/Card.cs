using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public CardData cardData; // 카드 데이터를 가지고 있기위한 변수
    public SpriteRenderer spriteRenderer; // 카드 이미지저장하기 위한 변수
    public TextMeshPro textName; // 카드 이름을 담는 TextMeshPro
    public TextMeshPro textType; // 카드 타입을 담는 TextMeshPro
    public TextMeshPro textDesc; // 카드 설명을 담는 TextMeshPro
    public TextMeshPro textCost; // 카드 코스트을 담는 TextMeshPro
    public PRS originPRS; // 카드 정렬위치를 담는 PRS클래스 변수

    public void SetUp()
    {
        spriteRenderer.sprite = cardData.Icon;
        spriteRenderer.transform.localScale = new Vector3(1.4f, 1.26f, 1);

        textName.text = cardData.cardName;
        switch (cardData.Type)
        {
            case CardData.CardType.Attack:
                textType.text = "공격";
                break;
            case CardData.CardType.Skill:
                textType.text = "스킬";
                break;
            case CardData.CardType.Power:
                textType.text = "파워";
                break;
        }
        textDesc.text = cardData.baseDesc;
        textCost.text = cardData.cost.ToString();
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if(useDotween) {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }

    private void OnMouseOver()
    {
        CardManager.instance.CardMouseOver(this);
    }
    private void OnMouseExit()
    {
        CardManager.instance.CardMouseExit(this);
    }
}
