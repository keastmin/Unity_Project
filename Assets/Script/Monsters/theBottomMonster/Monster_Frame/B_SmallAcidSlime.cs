using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_SmallAcidSlime : Monster
{
    bool isGivingWeak; // �� �Ͽ� '��ȭ' ������� �ο��ϴ� ������ ����ߴ����� ���� bool�� ��ȯ

    public B_SmallAcidSlime()
    {
        InitMonster();
    }

    public override void InitMonster()
    {
        base.name = "Small Acid Slime";
        base.health = Random.Range(8, 13);
        base.attackForce = 3;
        base.SetMaxHealth();
    }

    public override void AttackPattern()
    {
        if (isGivingWeak)
        {
            Attack(player);
        }
        else
        {
            GiveWeakening(10); // ������ �� ����
        }

        isGivingWeak = !isGivingWeak;
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    // '��ȭ' ����� �ο�, �÷��̾��� ���ݷ��� �Ű������� ����.
    private int GiveWeakening(int playerAttackForce)
    {
        return playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}