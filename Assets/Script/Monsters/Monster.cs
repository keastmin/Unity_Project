using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster
{
    protected string name;
    protected int health;
    protected int maxHealth;
    protected int attackForce;
    protected bool isAttackTurn;
    public PlayerSc player;

    public string GetName()
    {
        return name;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetAttackForce()
    {
        return attackForce;
    }

    public bool GetIsAttackTurn()
    {
        return isAttackTurn;
    }

    public void SetMaxHealth()
    {
        maxHealth = health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    // 몬스터 초기화
    public abstract void InitMonster();

    // 공격 턴일 때 행동 구현
    public void StartTurn()
    {
        if (isAttackTurn)
        {
            AttackPattern();
            isAttackTurn = false;
        }
    }

    // 몬스터의 공격 패턴 구현
    public abstract void AttackPattern();

    // 몬스터가 죽었을 때
    public abstract void Die();

    // 플레이어를 공격했을 때
    public void Attack(PlayerSc player)
    {
        // player.hp -= attackForce;
        Debug.Log("플레이어를 " + attackForce + "만큼 공격했습니다."); // 테스트 코드
    }

    // 몬스터의 체력을 깎는 메소드(플레이어에게 공격받았을 때)
    public void Damaged(int damage)
    {
        if (health - damage > 0)
        {
            health -= damage;
            Debug.Log("현재 체력 : " + health);
        }
        else
        {
            Die();
        }
    }
}
