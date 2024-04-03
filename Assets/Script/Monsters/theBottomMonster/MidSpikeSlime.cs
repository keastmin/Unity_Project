using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidSpikeSlime : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_MidSpikeSlime midSpikeSlime;

    private void Awake()
    {
        midSpikeSlime = new B_MidSpikeSlime();
        midSpikeSlime.player = player;

        Debug.Log(midSpikeSlime.GetName() + " 생성"); // 테스트 코드
    }
}