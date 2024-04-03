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
        Debug.Log(greenLouse.GetName() + " ����"); // �׽�Ʈ �ڵ�
    }

    // �׽�Ʈ �޼���
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

    // ���ݹ޴� �޼���
    public void Damaged()
    {
        greenLouse.SetIsDamageTrue();
        // ������ 3 �� �ο�. ���� ����.
        greenLouse.Damaged(3);
    }
}