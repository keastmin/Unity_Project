using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looter : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_Looter looter;

    private void Awake()
    {
        looter = new B_Looter();
        looter.player = player;

        Debug.Log(looter.GetName() + " 생성"); // 테스트 코드
    }
}