using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAcidSlime : MonoBehaviour
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

class B_SmallAcidSlime : Monster
{
    public B_SmallAcidSlime()
    {
        base.name = "Small Acid Slime";
        base.health = Random.Range(8, 13);
        base.attackForce = 3;
        base.SetMaxHealth();
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    // '��ȭ' ����� �ο�, �÷��̾��� ���ݷ��� �Ű������� ����.
    public int GiveWeakening(int playerAttackForce)
    {
        return playerAttackForce = (int)(playerAttackForce * 0.75);
    }

    public override void StartTurn()
    {
        throw new System.NotImplementedException();
    }
}