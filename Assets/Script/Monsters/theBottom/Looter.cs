using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looter : MonoBehaviour
{

}

class B_Looter : Monster
{
    int thiefPoint = 15;
    int shield = 6;
    int stealGoldBank = 0;

    public B_Looter()
    {
        base.name = "Looter";
        base.health = Random.Range(44, 49);
        base.SetMaxHealth();
    }

    // ��ȭ ����, �÷��̾��� ���� ��ȭ ������ �Ű������� ����.
    public int GetGold(int playerGold)
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

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void StartTurn()
    {
        throw new System.NotImplementedException();
    }
}