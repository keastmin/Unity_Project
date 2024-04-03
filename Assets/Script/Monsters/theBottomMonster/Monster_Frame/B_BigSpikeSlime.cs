using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_BigSpikeSlime : Monster
{
    bool isGivingCard; // �� �Ͽ� '����������' ī�带 �ο��ϴ� ������ ����ߴ����� ���� bool�� ��ȯ

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
    }

    public override void AttackPattern()
    {
        Attack(player);

        if (isGivingCard)
        {
            GiveLowDefence(20); // ������ �÷��̾� �� ����.
        }
        else
        {
            GiveMucusCard();
        }

        isGivingCard = !isGivingCard;
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    // ���������� ī�� 1�� ����
    private void GiveMucusCard()
    {

    }

    // '�ջ�' ����� �ο�, �÷��̾��� ���� �Ű������� ����.
    private int GiveLowDefence(int playerShield)
    {
        return (int)(playerShield * 0.75);
    }
}