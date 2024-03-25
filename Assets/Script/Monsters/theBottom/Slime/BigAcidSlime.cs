using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAcidSlime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

class B_BigAcidSlime : Monster
{
    public B_BigAcidSlime()
    {
        base.name = "Big Acid Slime";
        base.health = Random.Range(65, 70);
        base.attackForce = 11;
        base.SetMaxHealth();
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

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void StartTurn()
    {
        throw new System.NotImplementedException();
    }
}