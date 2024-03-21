using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpikeSlime : MonoBehaviour
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

class B_BigSpikeSlime : Monster
{
    public B_BigSpikeSlime()
    {
        base.name = "Big Spike Slime";
        base.health = Random.Range(64, 71);
        base.attackForce = 16;
        base.SetMaxHealth();
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    // 점액투성이 카드 1장 지급

    // '손상' 디버프 부여, 플레이어의 방어도를 매개변수로 받음.
    public int GiveLowDefence(int playerShield)
    {
        return (int)(playerShield * 0.75);
    }

    public override void StartTurn()
    {
        throw new System.NotImplementedException();
    }
}