using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_RedLouse : Monster
{
    int shield;
    int plusForce = 5;
    bool isDamagedOnce;
    bool isUsingShieldOnce;

    public B_RedLouse()
    {
        InitMonster();
    }

    // ���� �ʱ�ȭ
    public override void InitMonster()
    {
        base.name = "Red Louse";
        base.health = Random.Range(10, 16);
        base.attackForce = Random.Range(5, 8);
        this.shield = Random.Range(3, 8);
        base.SetMaxHealth();
    }

    // ���� or �� ���� ���� ���Ƿ� ���
    public override void AttackPattern()
    {
        if (isDamagedOnce) // �÷��̾�� ������ �޾��� ���
        {
            UseShield(); // �� ���� ���� ���
        }

        int randomPattern = Random.Range(0, 5);

        Debug.Log("���� ���� �� : " + randomPattern); // �׽�Ʈ �ڵ�

        if (randomPattern < 3)
        {
            Debug.Log("�÷��̾� ����"); // �׽�Ʈ �ڵ�
            Attack(player);
        }
        else
        {
            Debug.Log("�� ���� ���"); // �׽�Ʈ �ڵ�
            UseForce();
        }
    }

    // �׾��� ��
    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public void SetIsDamageTrue()
    {
        isDamagedOnce = true;
    }

    // '�� ����' ���� ���
    private void UseShield()
    {
        if (!isUsingShieldOnce)
        {
            Debug.Log("���� ü�� " + health); // �׽�Ʈ �ڵ�

            isUsingShieldOnce = true;
            health += shield;

            Debug.Log("���� ü�� " + health); // �׽�Ʈ �ڵ�
        }
    }

    // '��' ���� ���
    private void UseForce()
    {
        attackForce += plusForce;
    }
}