using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAcidSlime : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_SmallAcidSlime smallAcidSlime;

    private void Awake()
    {
        smallAcidSlime = new B_SmallAcidSlime();
        player = smallAcidSlime.player;

        Debug.Log(smallAcidSlime.GetName() + " 생성"); // 테스트 코드
    }
}