using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looter : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_Looter looter;

    private void Awake()
    {
        looter = new B_Looter();
        looter.player = player;

        Debug.Log(looter.GetName() + " 생성"); // 테스트 코드
    }
}

class B_Looter : Monster
{
    int thiefPoint = 15;
    int shield = 6;
    int stealGoldBank = 0;

    public B_Looter()
    {
        InitMonster();
    }

    public override void InitMonster()
    {
        base.name = "Looter";
        base.health = Random.Range(44, 49);
        base.SetMaxHealth();
    }

    public override void AttackPattern()
    {
        GetGold(100); // 임의의 인수 사용.

        int randomAttackPattern = Random.Range(0, 4);

        if (randomAttackPattern == 0)
        {
            Steal();
        }
        else if (randomAttackPattern == 1)
        {
            Sting();
        }
        else if (randomAttackPattern == 2)
        {
            UseShield();
        }
        else
        {
            RunAway();
        }
    }

    public override void Die()
    {
        ReturnGold();
    }

    // 금화 뺏기, 플레이어의 현재 금화 개수를 매개변수로 받음.
    private int GetGold(int playerGold)
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