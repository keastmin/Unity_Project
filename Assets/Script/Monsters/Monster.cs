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

    // ������ ���ݷ��� ��ȯ�ϴ� �޼ҵ�(�÷��̾ �������� ��)
    public int Attack()
    {
        return attackForce;
    }

    // ������ ü���� ��� �޼ҵ�(�÷��̾�� ���ݹ޾��� ��)
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

    // ���Ͱ� �׾��� �� �����ϴ� �޼ҵ�
    public void Die()
    {
        Destroy(gameObject);
    }
}
