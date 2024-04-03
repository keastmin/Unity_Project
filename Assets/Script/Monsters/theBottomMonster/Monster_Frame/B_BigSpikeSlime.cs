using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_BigSpikeSlime : Monster
{
    bool isGivingCard; // 전 턴에 '점액투성이' 카드를 부여하는 패턴을 사용했는지에 대한 bool형 반환

    public B_BigSpikeSlime()
    {
        InitMonster();
    }

    public override void InitMonster()
    {
        base.name = "Big Spike Slime";
        base.health = Random.Range(64, 71);
        base.attackForce = 16;
        base.SetMaxHealth();
    }

    public override void AttackPattern()
    {
        Attack(player);

        if (isGivingCard)
        {
            GiveLowDefence(20); // 임의의 플레이어 방어도 설정.
        }
        else
        {
            GiveMucusCard();
        }

        isGivingCard = !isGivingCard;
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    // 점액투성이 카드 1장 지급
    private void GiveMucusCard()
    {

    }

    // '손상' 디버프 부여, 플레이어의 방어도를 매개변수로 받음.
    private int GiveLowDefence(int playerShield)
    {
        return (int)(playerShield * 0.75);
    }
}