using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawWorm : MonoBehaviour
{
    
}

class B_JawWorm : Monster
{
    int shield;
    int plusForce = 3;

    public B_JawWorm()
    {
        base.name = "Jaw Worm";
        base.health = Random.Range(40, 45);
        base.attackForce = 11;
        base.SetMaxHealth();
    }

    // 방어도 증가 메소드
    private void UseShield()
    {
        this.health += shield;
    }

    // 공격력을 낮추고 방어도를 얻는 패턴
    public void LowAttackAndUseShield()
    {
        shield = 5;
        this.attackForce = 7;

        UseShield();
    }

    // '힘' 버프를 사용하고 방어도를 얻는 패턴
    public void UseForce()
    {
        shield = 6;
        this.attackForce += plusForce;

        UseShield();
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
