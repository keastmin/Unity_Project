using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_JawWorm : Monster
{
    int shield;
    int plusForce = 3;

    public B_JawWorm()
    {
        InitMonster();
    }

    public override void InitMonster()
    {
        base.name = "Jaw Worm";
        base.health = Random.Range(40, 45);
        base.attackForce = 11;
        base.SetMaxHealth();
    }

    public override void AttackPattern()
    {
        int randomAttackPattern = Random.Range(0, 3);

        if (randomAttackPattern == 0)
        {
            Attack(player);
        }
        else if (randomAttackPattern == 1)
        {
            LowAttackAndUseShield();
        }
        else
        {
            UseForceAndShield();
        }
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    // �� ���� �޼ҵ�
    private void UseShield()
    {
        this.health += shield;
    }

    // ���ݷ��� ���߰� ���� ��� ����
    private void LowAttackAndUseShield()
    {
        shield = 5;
        this.attackForce = 7;
        Attack(player);

        UseShield();
    }

    // '��' ������ ����ϰ� ���� ��� ����
    private void UseForceAndShield()
    {
        shield = 6;
        this.attackForce += plusForce;

        UseShield();
    }
}