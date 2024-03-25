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

    // �÷��̾ �������� ��
    public void Attack(PlayerSc player)
    {
        // player.hp -= attackForce;
        Debug.Log("�÷��̾ " + attackForce + "��ŭ �����߽��ϴ�."); // �׽�Ʈ �ڵ�
    }

    // ������ ü���� ��� �޼ҵ�(�÷��̾�� ���ݹ޾��� ��)
    public void Damaged(int damage)
    {
        if (health - damage > 0)
        {
            health -= damage;
            Debug.Log("���� ü�� : " + health);
        }
        else
        {
            Die();
        }
    }

    // ���� ���� �� �ൿ ����
    public abstract void StartTurn();

    // ���Ͱ� �׾��� �� �����ϴ� �޼ҵ�
    public abstract void Die();
}
