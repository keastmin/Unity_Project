using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    
}

class SmallSpikeSlime : Monster
{
    public SmallSpikeSlime()
    {
        this.name = "Small Spike Slime";
        this.health = Random.Range(10, 15);
        this.attackForce = 5;
    }
}

class MidSpikeSlime : Monster
{
    public MidSpikeSlime()
    {
        this.name = "Mid Spike Slime";
        this.health = Random.Range(28, 33);
        this.attackForce = 8;
    }

    // ���������� ī�� 1�� ����

    // '�ջ�' ����� �ο�, �÷��̾��� ���� �Ű������� ����.
    public int GiveLowDefence(int playerShield)
    {
        return (int)(playerShield * 0.75);
    }
}

class BigSpikeSlime : Monster
{
    public BigSpikeSlime()
    {
        this.name = "Big Spike Slime";
        this.health = Random.Range(64, 71);
        this.attackForce = 16;
    }

    // ���������� ī�� 1�� ����

    // '�ջ�' ����� �ο�, �÷��̾��� ���� �Ű������� ����.
    public int GiveLowDefence(int playerShield)
    {
        return (int)(playerShield * 0.75);
    }
}

class SmallAcidSlime : Monster
{
    public SmallAcidSlime()
    {
        this.name = "Small Acid Slime";
        this.health = Random.Range(8, 13);
        this.attackForce = 3;
    }

    // '��ȭ' ����� �ο�, �÷��̾��� ���ݷ��� �Ű������� ����.
    public int GiveWeakening(int playerAttackForce)
    {
        return playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}

class MidAcidSlime : Monster
{
    public MidAcidSlime()
    {
        this.name = "Mid Acid Slime";
        this.health = Random.Range(28, 33);
        this.attackForce = 7;
    }

    // ���������� ī�� 1�� ����

    // ���ݷ� 10���� ���
    public void RaiseAttackForce()
    {
        this.attackForce = 10;
    }

    // '��ȭ' ����� �ο�, �÷��̾��� ���ݷ��� �Ű������� ����.
    public int GiveWeakening(int playerAttackForce)
    {
        return playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}

class BigAcidSlime : Monster
{
    public BigAcidSlime()
    {
        this.name = "Big Acid Slime";
        this.health = Random.Range(65, 70);
        this.attackForce = 11;
    }

    // ���������� ī�� 1�� ����

    // ���ݷ� 16���� ���
    public void RaiseAttackForce()
    {
        this.attackForce = 16;
    }

    // '��ȭ' ����� �ο�, �÷��̾��� ���ݷ��� �Ű������� ����.
    public int GiveWeakening(int playerAttackForce)
    {
        return playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}
