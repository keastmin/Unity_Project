using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLouse : MonoBehaviour
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

class B_RedLouse : Monster
{
    int shield;
    int plusForce = 5;
    bool isUsingShieldOnce = false;

    public B_RedLouse()
    {
        base.name = "Red Louse";
        base.health = Random.Range(10, 16);
        base.attackForce = Random.Range(5, 8);
        this.shield = Random.Range(3, 8);
        base.SetMaxHealth();
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

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void StartTurn()
    {
        throw new System.NotImplementedException();
    }
}