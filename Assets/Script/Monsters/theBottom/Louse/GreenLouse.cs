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



class B_GreenLouse : Monster
{
    int shield;
    bool isUsingShieldOnce;
    bool isDamagedOnce;

    public B_GreenLouse()
    {
        InitMonster();
    }

    // ���� ���� ��
    public override void StartTurn()
    {
        Debug.Log("StartTurn �޼��� ȣ��"); // �׽�Ʈ �ڵ�
        isAttackTurn = true; // �׽�Ʈ �ڵ�
        isDamagedOnce = true; // �׽�Ʈ �ڵ�

        if (isAttackTurn)
        {
            if (isDamagedOnce) // �÷��̾�� ������ �޾��� ���
            {
                UseShield(); // �� ���� ���� ���
            }

            AttackPattern(); // ���� ���� �������� �÷��̾� ����
            isAttackTurn = !isAttackTurn;
        }
    }

    // �׾��� ��
    public override void Die()
    {

    }

    public void SetIsDamageTrue()
    {
        isDamagedOnce = true;
    }

    // ���� �ʱ�ȭ
    private void InitMonster()
    {
        base.name = "Green Louse";
        base.health = Random.Range(10, 16);
        base.attackForce = Random.Range(5, 8);
        this.shield = Random.Range(3, 8);
        base.SetMaxHealth();
    }

    // '�� ����' ���� ���
    private void UseShield()
    {
        if (!isUsingShieldOnce)
        {
            Debug.Log("���� ü�� " + health); // �׽�Ʈ �ڵ�

            isUsingShieldOnce = true;
            health += shield;

            Debug.Log("���� ü�� " + health); // �׽�Ʈ �ڵ�
        }
    }

    // '��ȭ' ����� �ο�, �÷��̾��� ���ݷ��� �Ű������� ����.
    private void GiveWeakening(int playerAttackForce)
    {
        playerAttackForce = (int)(playerAttackForce * 0.75);
    }

    // ���� or ��ȭ 2 �ο� ���� ���Ƿ� ���
    private void AttackPattern()
    {
        int randomPattern = Random.Range(0, 5);

        Debug.Log("���� ���� �� : " + randomPattern); // �׽�Ʈ �ڵ�

        if (randomPattern < 3)
        {
            Debug.Log("�÷��̾� ����"); // �׽�Ʈ �ڵ�
            Attack(player);
        }
        else
        {
            Debug.Log("�÷��̾�� ��ȭ �ο�"); // �׽�Ʈ �ڵ�
            GiveWeakening(6); // GiveWeakening(player.attackForce);
        }
    }
}