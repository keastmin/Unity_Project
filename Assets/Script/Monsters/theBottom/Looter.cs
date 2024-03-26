using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looter : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_Looter looter;

    private void Awake()
    {
        looter = new B_Looter();
        looter.player = player;

        Debug.Log(looter.GetName() + " ����"); // �׽�Ʈ �ڵ�
    }
}

class B_Looter : Monster
{
    int thiefPoint = 15;
    int shield = 6;
    int stealGoldBank = 0;

    public B_Looter()
    {
        InitMonster();
    }

    public override void InitMonster()
    {
        base.name = "Looter";
        base.health = Random.Range(44, 49);
        base.SetMaxHealth();
    }

    public override void AttackPattern()
    {
        GetGold(100); // ������ �μ� ���.

        int randomAttackPattern = Random.Range(0, 4);

        if (randomAttackPattern == 0)
        {
            Steal();
        }
        else if (randomAttackPattern == 1)
        {
            Sting();
        }
        else if (randomAttackPattern == 2)
        {
            UseShield();
        }
        else
        {
            RunAway();
        }
    }

    public override void Die()
    {
        ReturnGold();
    }

    // ��ȭ ����, �÷��̾��� ���� ��ȭ ������ �Ű������� ����.
    private int GetGold(int playerGold)
    {
        if (playerGold - thiefPoint > 0)
        {
            stealGoldBank += thiefPoint;
            return playerGold - thiefPoint;
        }
        else
        {
            stealGoldBank += playerGold;
            return 0;
        }
    }

    // '��ġ��' ��� ���
    public void Steal()
    {
        this.attackForce = 10;
    }

    // '���' ��� ���
    public void Sting()
    {
        this.attackForce = 12;
    }

    // �� 6 ��� ��� ���
    public void UseShield()
    {
        this.health += shield;
    }

    // ���� ��� ���
    public void RunAway()
    {
        Debug.Log("�зȴ�...");
    }

    // ��ȭ�� ������ ���
    public int ReturnGold()
    {
        return stealGoldBank;
    }
}