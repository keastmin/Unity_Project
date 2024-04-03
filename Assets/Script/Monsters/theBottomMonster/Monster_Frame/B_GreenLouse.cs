using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_GreenLouse : Monster
{
    int shield;
    bool isUsingShieldOnce;
    bool isDamagedOnce;

    public B_GreenLouse()
    {
        InitMonster();
    }

    // 몬스터 초기화
    public override void InitMonster()
    {
        base.name = "Green Louse";
        base.health = Random.Range(10, 16);
        base.attackForce = Random.Range(5, 8);
        this.shield = Random.Range(3, 8);
        base.SetMaxHealth();
    }

    // 공격 or 약화 2 부여 패턴 임의로 사용
    public override void AttackPattern()
    {
        if (isDamagedOnce) // 플레이어에게 공격을 받았을 경우
        {
            UseShield(); // 몸 말기 버프 사용
        }

        int randomPattern = Random.Range(0, 5);

        Debug.Log("랜덤 패턴 값 : " + randomPattern); // 테스트 코드

        if (randomPattern < 3)
        {
            Debug.Log("플레이어 공격"); // 테스트 코드
            Attack(player);
        }
        else
        {
            Debug.Log("플레이어에게 약화 부여"); // 테스트 코드
            GiveWeakening(6); // 임의의 인수 부여. GiveWeakening(player.attackForce);
        }
    }

    // 몬스터가 죽었을 때
    public override void Die()
    {

    }

    public void SetIsDamageTrue()
    {
        isDamagedOnce = true;
    }

    // '몸 말기' 버프 사용
    private void UseShield()
    {
        if (!isUsingShieldOnce)
        {
            Debug.Log("현재 체력 " + health); // 테스트 코드

            isUsingShieldOnce = true;
            health += shield;

            Debug.Log("현재 체력 " + health); // 테스트 코드
        }
    }

    // '약화' 디버프 부여, 플레이어의 공격력을 매개변수로 받음.
    private void GiveWeakening(int playerAttackForce)
    {
        playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}
