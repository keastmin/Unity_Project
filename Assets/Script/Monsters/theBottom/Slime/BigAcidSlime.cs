using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAcidSlime : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_BigAcidSlime bigAcidSlime;

    private void Awake()
    {
        bigAcidSlime = new B_BigAcidSlime();
        player = bigAcidSlime.player;

        Debug.Log(bigAcidSlime.GetName() + " ����"); // �׽�Ʈ �ڵ�
    }
}

class B_BigAcidSlime : Monster
{
    public B_BigAcidSlime()
    {
        InitMonster();
    }

    public override void InitMonster()
    {
        base.name = "Big Acid Slime";
        base.health = Random.Range(65, 70);
        base.attackForce = 11;
        base.SetMaxHealth();
    }

    public override void AttackPattern()
    {
        int randomAttackPattern = Random.Range(0, 3);

        if (randomAttackPattern == 0)
        {
            GiveMucusCard();
        }
        else if (randomAttackPattern == 1)
        {
            RaiseAttackForce();
        }
        else
        {
            GiveWeakening(10); // ������ �μ� ����;
        }

        Attack(player);
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    // ���������� ī�� 1�� ����
    private void GiveMucusCard()
    {

    }

    // ���ݷ� 16���� ���
    private void RaiseAttackForce()
    {
        this.attackForce = 16;
    }

    // '��ȭ' ����� �ο�, �÷��̾��� ���ݷ��� �Ű������� ����.
    private int GiveWeakening(int playerAttackForce)
    {
        return playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}