using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_BigSpikeSlime : Monster
{
    bool isGivingCard; // �� �Ͽ� '����������' ī�带 �ο��ϴ� ������ ����ߴ����� ���� bool�� ��ȯ
    MonsterCardSO monsterCardSO;
    GameObject cardPrefab;
    GameObject cardCanvas;

    public B_BigSpikeSlime()
    {
        InitMonster();
    }

    public override void InitMonster()
    {
        base.name = "Big Spike Slime";
        base.health = Random.Range(64, 71);
        base.attackForce = 16;
        base.SetMaxHealth();
        cardCanvas = GameObject.Find("CardCanvas"); // ���� ����
    }

    public override void AttackPattern()
    {
        Attack(player);

        if (isGivingCard)
        {
            Debug.Log("��ȭ �ο�"); // �׽�Ʈ �ڵ�
            GiveLowDefence(20); // ������ �÷��̾� �� ����.
        }
        else
        {
            Debug.Log("���������� ī�� �ο�"); // �׽�Ʈ �ڵ�
            GiveSlimedCard();
        }

        isGivingCard = !isGivingCard;
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public void SetMonsterCardInfo(MonsterCardSO targetSO, GameObject targetPrefab)
    {
        monsterCardSO = targetSO;
        cardPrefab = targetPrefab;
    }

    // ���������� ī�� 1�� ����
    private void GiveSlimedCard()
    {
        cardPrefab.GetComponent<MonsterCard>().cardSO = monsterCardSO;
        Instantiate(cardPrefab, cardCanvas.transform);
    }

    // '�ջ�' ����� �ο�, �÷��̾��� ���� �Ű������� ����.
    private int GiveLowDefence(int playerShield)
    {
        return (int)(playerShield * 0.75);
    }
}