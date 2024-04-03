using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpikeSlime : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_BigSpikeSlime bigSpikeSlime;

    private void Awake()
    {
        bigSpikeSlime = new B_BigSpikeSlime();
        bigSpikeSlime.player = player;

        Debug.Log(bigSpikeSlime.GetName() + " 생성"); // 테스트 코드
    }
}