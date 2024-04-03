using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_MidAcidSlime : Monster
{
    public B_MidAcidSlime()
    {
        InitMonster();
    }

    public override void InitMonster()
    {
        base.name = "Mid Acid Slime";
        base.health = Random.Range(28, 33);
        base.attackForce = 7;
        base.SetMaxHealth();
    }

    public override void AttackPattern()
    {
        int randomAttackPattern = Random.Range(0, 3);

        if (randomAttackPattern == 0)
        {
            GiveMucusCard();
        }
        else if (randomAttackPattern == 1)
        {
            RaiseAttackForce();
        }
        else
        {
            GiveWeakening(10); // ������ �μ� ����;
        }

        Attack(player);
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    // ���������� ī�� 1�� ����
    private void GiveMucusCard()
    {

    }

    // ���ݷ� 10���� ���
    private void RaiseAttackForce()
    {
        this.attackForce = 10;
    }

    // '��ȭ' ����� �ο�, �÷��̾��� ���ݷ��� �Ű������� ����.
    private int GiveWeakening(int playerAttackForce)
    {
        return playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}