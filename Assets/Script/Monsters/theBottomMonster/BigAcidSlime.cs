using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigAcidSlime : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_BigAcidSlime bigAcidSlime;

    private void Awake()
    {
        bigAcidSlime = new B_BigAcidSlime();
        player = bigAcidSlime.player;

        Debug.Log(bigAcidSlime.GetName() + " 생성"); // 테스트 코드
    }
}