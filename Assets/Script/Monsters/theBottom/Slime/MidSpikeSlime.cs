using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidSpikeSlime : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_MidSpikeSlime midSpikeSlime;

    private void Awake()
    {
        midSpikeSlime = new B_MidSpikeSlime();
        midSpikeSlime.player = player;

        Debug.Log(midSpikeSlime.GetName() + " ����"); // �׽�Ʈ �ڵ�
    }
}

class B_MidSpikeSlime : Monster
{
    bool isGivingCard; // �� �Ͽ� '����������' ī�带 �ο��ϴ� ������ ����ߴ����� ���� bool�� ��ȯ

    public B_MidSpikeSlime()
    {
        InitMonster();
    }

    public override void StartTurn()
    {
        if (isAttackTurn)
        {
            AttackPattern();
            isAttackTurn = !isAttackTurn;
        }
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    private void InitMonster()
    {
        base.name = "Mid Spike Slime";
        base.health = Random.Range(28, 33);
        base.attackForce = 8;
        base.SetMaxHealth();
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

    private void AttackPattern()
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
}