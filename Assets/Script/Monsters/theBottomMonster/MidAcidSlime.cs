using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidAcidSlime : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_MidAcidSlime midAcidSlime;

    private void Awake()
    {
        midAcidSlime = new B_MidAcidSlime();
        player = midAcidSlime.player;

        Debug.Log(midAcidSlime.GetName() + " 생성"); // 테스트 코드
    }
}