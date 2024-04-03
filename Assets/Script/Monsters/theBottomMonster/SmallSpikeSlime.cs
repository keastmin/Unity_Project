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