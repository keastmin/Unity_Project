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

    // ���� �ʱ�ȭ
    public override void InitMonster()
    {
        base.name = "Green Louse";
        base.health = Random.Range(10, 16);
        base.attackForce = Random.Range(5, 8);
        this.shield = Random.Range(3, 8);
        base.SetMaxHealth();
    }

    // ���� or ��ȭ 2 �ο� ���� ���Ƿ� ���
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
            Debug.Log("�÷��̾�� ��ȭ �ο�"); // �׽�Ʈ �ڵ�
            GiveWeakening(6); // ������ �μ� �ο�. GiveWeakening(player.attackForce);
        }
    }

    // ���Ͱ� �׾��� ��
    public override void Die()
    {

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

    // '��ȭ' ����� �ο�, �÷��̾��� ���ݷ��� �Ű������� ����.
    private void GiveWeakening(int playerAttackForce)
    {
        playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}
