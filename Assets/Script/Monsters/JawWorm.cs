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

    // �� ���� �޼ҵ�
    private void UseShield()
    {
        this.health += shield;
    }

    // ���ݷ��� ���߰� ���� ��� ����
    public void LowAttackAndUseShield()
    {
        shield = 5;
        this.attackForce = 7;

        UseShield();
    }

    // '��' ������ ����ϰ� ���� ��� ����
    public void UseForce()
    {
        shield = 6;
        this.attackForce += plusForce;

        UseShield();
    }
}
