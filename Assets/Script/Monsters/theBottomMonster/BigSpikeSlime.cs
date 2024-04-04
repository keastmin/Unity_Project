using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpikeSlime : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_BigSpikeSlime bigSpikeSlime;

    [Header("Monster Card")]
    [SerializeField] GameObject cardPrefab;
    [SerializeField] MonsterCardSO cardSO;

    private void Awake()
    {
        bigSpikeSlime = new B_BigSpikeSlime();
        bigSpikeSlime.player = player;
        bigSpikeSlime.SetMonsterCardInfo(cardSO, cardPrefab);

        Debug.Log(bigSpikeSlime.GetName() + " ����"); // �׽�Ʈ �ڵ�
    }

    private void Start()
    {
        bigSpikeSlime.AttackPattern();
    }
}