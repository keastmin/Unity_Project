using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected string name;
    protected int health;
    protected int attackForce;

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

    // 몬스터의 공격력을 반환하는 메소드(플레이어를 공격했을 때)
    public int Attack()
    {
        return attackForce;
    }

    // 몬스터의 체력을 깎는 메소드(플레이어에게 공격받았을 때)
    public void Damaged(int damage)
    {
        if (health - damage > 0)
        {
            health -= damage;
        }
        else
        {
            Die();
        }
    }

    // 몬스터가 죽었을 시 동작하는 메소드
    public void Die()
    {
        Destroy(gameObject);
    }
}
