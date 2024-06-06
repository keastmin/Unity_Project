using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public CardData cardData; // ī�� �����͸� ������ �ֱ����� ����
    public SpriteRenderer spriteRenderer; // ī�� �̹��������ϱ� ���� ����
    public TextMeshPro textName; // ī�� �̸��� ��� TextMeshPro
    public TextMeshPro textType; // ī�� Ÿ���� ��� TextMeshPro
    public TextMeshPro textDesc; // ī�� ������ ��� TextMeshPro
    public TextMeshPro textCost; // ī�� �ڽ�Ʈ�� ��� TextMeshPro
    public PRS originPRS; // ī�� ������ġ�� ��� PRSŬ���� ����

    public void SetUp()
    {
        spriteRenderer.sprite = cardData.Icon;
        spriteRenderer.transform.localScale = new Vector3(1.4f, 1.26f, 1);

        textName.text = cardData.cardName;
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
