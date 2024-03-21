using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSpikeSlime : MonoBehaviour
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

class B_SmallSpikeSlime : Monster
{
    public B_SmallSpikeSlime()
    {
        base.name = "Small Spike Slime";
        base.health = Random.Range(10, 15);
        base.attackForce = 5;
        base.SetMaxHealth();
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