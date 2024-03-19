using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawWorm : MonoBehaviour
{
    
}

public class JawWormFunction : Monster
{
    int shield;
    int plusForce = 3;

    public JawWormFunction()
    {
        this.name = "Jaw Worm";
        this.health = Random.Range(40, 45);
        this.attackForce = 11;
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
}
