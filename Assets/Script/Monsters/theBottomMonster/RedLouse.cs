using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLouse : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_RedLouse redLouse;

    private void Awake()
    {
        redLouse = new B_RedLouse();
        redLouse.player = player;
        Debug.Log(redLouse.GetName() + " 생성"); // 테스트 코드
    }

    private void Update()
    {
        // if (playerTurnEnd)
            // isAttackTurn = true;
            // redLouse.StartTurn();
    }

    // 공격받는 메서드
    public void Damaged()
    {
        redLouse.SetIsDamageTrue();
        // 임의의 3 값 부여. 수정 예정.
        redLouse.Damaged(3);
    }
}