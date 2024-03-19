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

    // 점액투성이 카드 1장 지급

    // '손상' 디버프 부여, 플레이어의 방어도를 매개변수로 받음.
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

    // 점액투성이 카드 1장 지급

    // '손상' 디버프 부여, 플레이어의 방어도를 매개변수로 받음.
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

    // '약화' 디버프 부여, 플레이어의 공격력을 매개변수로 받음.
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

    // 점액투성이 카드 1장 지급

    // 공격력 10으로 상승
    public void RaiseAttackForce()
    {
        this.attackForce = 10;
    }

    // '약화' 디버프 부여, 플레이어의 공격력을 매개변수로 받음.
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

    // 점액투성이 카드 1장 지급

    // 공격력 16으로 상승
    public void RaiseAttackForce()
    {
        this.attackForce = 16;
    }

    // '약화' 디버프 부여, 플레이어의 공격력을 매개변수로 받음.
    public int GiveWeakening(int playerAttackForce)
    {
        return playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}
