using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLouse : MonoBehaviour
{
    [SerializeField] PlayerSc player;
    B_GreenLouse greenLouse;

    private void Awake()
    {
        greenLouse = new B_GreenLouse();
        greenLouse.player = player;
        Debug.Log(greenLouse.GetName() + " 생성"); // 테스트 코드
    }

    // 테스트 메서드
    private void Start()
    {
        greenLouse.StartTurn();
    }

    private void Update()
    {
        // if (playerTurnEnd)
            // isAttackTurn = true;
            // greenLouse.StartTurn();
    }

    // 공격받는 메서드
    public void Damaged()
    {
        greenLouse.SetIsDamageTrue();
        // 임의의 3 값 부여. 수정 예정.
        greenLouse.Damaged(3);
    }
}