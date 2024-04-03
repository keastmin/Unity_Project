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
            GiveWeakening(10); // 임의의 인수 설정;
        }

        Attack(player);
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    // 점액투성이 카드 1장 지급
    private void GiveMucusCard()
    {

    }

    // 공격력 10으로 상승
    private void RaiseAttackForce()
    {
        this.attackForce = 10;
    }

    // '약화' 디버프 부여, 플레이어의 공격력을 매개변수로 받음.
    private int GiveWeakening(int playerAttackForce)
    {
        return playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}