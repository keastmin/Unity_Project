using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAcidSlime : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_BigAcidSlime bigAcidSlime;

    [Header("Monster Card")]
    [SerializeField] GameObject cardPrefab;
    [SerializeField] MonsterCardSO cardSO;

    private void Awake()
    {
        bigAcidSlime = new B_BigAcidSlime();
        player = bigAcidSlime.player;
        bigAcidSlime.SetMonsterCardInfo(cardSO, cardPrefab);

        Debug.Log(bigAcidSlime.GetName() + " ����"); // �׽�Ʈ �ڵ�
    }

    private void Start()
    {
        bigAcidSlime.AttackPattern();
    }
}