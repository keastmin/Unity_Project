using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looter : MonoBehaviour
{

}

class LooterFunction : Monster
{
    int thiefPoint = 15;
    int shield = 6;
    int stealGoldBank = 0;

    public LooterFunction()
    {
        this.name = "Looter";
        this.health = Random.Range(44, 49);
    }

    // 금화 뺏기, 플레이어의 현재 금화 개수를 매개변수로 받음.
    public int GetGold(int playerGold)
    {
        if (playerGold - thiefPoint > 0)
        {
            stealGoldBank += thiefPoint;
            return playerGold - thiefPoint;
        }
        else
        {
            stealGoldBank += playerGold;
            return 0;
        }
    }

    // '훔치기' 기술 사용
    public void Steal()
    {
        this.attackForce = 10;
    }

    // '찌르기' 기술 사용
    public void Sting()
    {
        this.attackForce = 12;
    }

    // 방어도 6 얻기 기술 사용
    public void UseShield()
    {
        this.health += shield;
    }

    // 도망 기술 사용
    public void RunAway()
    {
        Debug.Log("털렸다...");
    }

    // 금화를 돌려줄 경우
    public int ReturnGold()
    {
        return stealGoldBank;
    }
}