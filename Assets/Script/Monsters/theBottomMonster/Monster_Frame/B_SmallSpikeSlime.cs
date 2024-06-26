using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_SmallSpikeSlime : Monster
{
    public B_SmallSpikeSlime()
    {
        InitMonster();
    }

    // 몬스터 초기화
    public override void InitMonster()
    {
        base.name = "Small Spike Slime";
        base.health = Random.Range(10, 15);
        base.attackForce = 5;
        base.SetMaxHealth();
    }

    public override void AttackPattern()
    {
        Attack(player);
    }

    //죽었을 때
    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}