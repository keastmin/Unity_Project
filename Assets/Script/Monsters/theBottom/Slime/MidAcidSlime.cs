using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidAcidSlime : MonoBehaviour
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

class B_MidAcidSlime : Monster
{
    public B_MidAcidSlime()
    {
        base.name = "Mid Acid Slime";
        base.health = Random.Range(28, 33);
        base.attackForce = 7;
        base.SetMaxHealth();
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

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void StartTurn()
    {
        throw new System.NotImplementedException();
    }
}