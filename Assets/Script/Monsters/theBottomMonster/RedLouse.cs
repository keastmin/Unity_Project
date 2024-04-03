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
        Debug.Log(redLouse.GetName() + " ����"); // �׽�Ʈ �ڵ�
    }

    private void Update()
    {
        // if (playerTurnEnd)
            // isAttackTurn = true;
            // redLouse.StartTurn();
    }

    // ���ݹ޴� �޼���
    public void Damaged()
    {
        redLouse.SetIsDamageTrue();
        // ������ 3 �� �ο�. ���� ����.
        redLouse.Damaged(3);
    }
}