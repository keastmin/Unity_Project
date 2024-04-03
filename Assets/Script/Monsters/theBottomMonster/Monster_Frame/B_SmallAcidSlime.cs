using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_SmallAcidSlime : Monster
{
    bool isGivingWeak; // 전 턴에 '약화' 디버프를 부여하는 패턴을 사용했는지에 대한 bool형 반환

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
            GiveWeakening(10); // 임의의 값 설정
        }

        isGivingWeak = !isGivingWeak;
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    // '약화' 디버프 부여, 플레이어의 공격력을 매개변수로 받음.
    private int GiveWeakening(int playerAttackForce)
    {
        return playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}