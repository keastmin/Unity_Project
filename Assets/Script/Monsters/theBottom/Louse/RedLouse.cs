using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLouse : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_RedLouse redLouse;

    private void Awake()
    {
        redLouse = new B_RedLouse();
        redLouse.player = player;
        Debug.Log(redLouse.GetName() + " 생성"); // 테스트 코드
    }

    private void Update()
    {
        // if (playerTurnEnd)
            // isAttackTurn = true;
            // redLouse.StartTurn();
    }

    // 공격받는 메서드
    public void Damaged()
    {
        redLouse.SetIsDamageTrue();
        // 임의의 3 값 부여. 수정 예정.
        redLouse.Damaged(3);
    }
}



class B_RedLouse : Monster
{
    int shield;
    int plusForce = 5;
    bool isDamagedOnce;
    bool isUsingShieldOnce;

    public B_RedLouse()
    {
        InitMonster();
    }

    // 공격 턴일 때
    public override void StartTurn()
    {
        Debug.Log("StartTurn 메서드 호출"); // 테스트 코드
        isAttackTurn = true; // 테스트 코드
        isDamagedOnce = true; // 테스트 코드

        if (isAttackTurn)
        {
            if (isDamagedOnce) // 플레이어에게 공격을 받았을 경우
            {
                UseShield(); // 몸 말기 버프 사용
            }

            AttackPattern(); // 랜덤 공격 패턴으로 플레이어 공격
            isAttackTurn = !isAttackTurn;
        }
    }

    // 죽었을 때
    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public void SetIsDamageTrue()
    {
        isDamagedOnce = true;
    }

    // 몬스터 초기화
    private void InitMonster()
    {
        base.name = "Red Louse";
        base.health = Random.Range(10, 16);
        base.attackForce = Random.Range(5, 8);
        this.shield = Random.Range(3, 8);
        base.SetMaxHealth();
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

    // '힘' 버프 사용
    private void UseForce()
    {
        attackForce += plusForce;
    }

    // 공격 or 힘 버프 패턴 임의로 사용
    private void AttackPattern()
    {
        int randomPattern = Random.Range(0, 5);

        Debug.Log("랜덤 패턴 값 : " + randomPattern); // 테스트 코드

        if (randomPattern < 3)
        {
            Debug.Log("플레이어 공격"); // 테스트 코드
            Attack(player);
        }
        else
        {
            Debug.Log("힘 버프 얻기"); // 테스트 코드
            UseForce();
        }
    }
}