using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSpikeSlime : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_SmallSpikeSlime smallSpikeSlime;

    private void Awake()
    {
        smallSpikeSlime = new B_SmallSpikeSlime();
        smallSpikeSlime.player = player;
    }

    private void Update()
    {
        // if (playerTurnEnd)
            // isAttackTurn = true;
            // smallSpikeSlime.StartTurn();
    }
}

class B_SmallSpikeSlime : Monster
{
    public B_SmallSpikeSlime()
    {
        InitMonster();
    }

    // ���� ���� ��
    public override void StartTurn()
    {
        if (isAttackTurn)
        {
            Attack(player);
            isAttackTurn = !isAttackTurn;
        }
    }

    //�׾��� ��
    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    // ���� �ʱ�ȭ
    private void InitMonster()
    {
        base.name = "Small Spike Slime";
        base.health = Random.Range(10, 15);
        base.attackForce = 5;
        base.SetMaxHealth();
    }
}