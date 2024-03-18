using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Louse : MonoBehaviour
{
    void Start()
    {
        RedLouse red = new RedLouse();
        GreenLouse green = new GreenLouse();

        Debug.Log(red.GetName() + red.GetHealth() + red.GetAttackForce());
        Debug.Log(green.GetName() + green.GetHealth() + green.GetAttackForce());
    }

    void Update()
    {
        
    }
}

class RedLouse : Monster
{
    int shield;
    int plusForce = 5;
    bool isUsingShieldOnce = false;

    public RedLouse()
    {
        base.name = "Red Louse";
        base.health = Random.Range(10, 16);
        base.attackForce = Random.Range(5, 8);
        this.shield = Random.Range(3, 8);
    }

    // '몸 말기' 버프 사용
    public void UseShield()
    {
        if (!isUsingShieldOnce)
        {
            isUsingShieldOnce = true;
            health += shield;
        }
    }

    // '힘' 버프 사용
    public void UseForce()
    {
        attackForce += plusForce;
    }
}

class GreenLouse : Monster
{
    int shield;
    bool isUsingShieldOnce = false;

    public GreenLouse()
    {
        base.name = "Green Louse";
        base.health = Random.Range(10, 16);
        base.attackForce = Random.Range(5, 8);
        this.shield = Random.Range(3, 8);
    }

    // '몸 말기' 버프 사용
    public void UseShield()
    {
        if (!isUsingShieldOnce)
        {
            isUsingShieldOnce = true;
            health += shield;
        }
    }

    // '약화' 디버프 부여, 플레이어의 공격력을 매개변수로 받음.
    public int GiveWeakening(int playerAttackForce)
    {
        return playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}
