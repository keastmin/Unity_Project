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
    PlayerSc player;

    public B_GreenLouse()
    {
        base.name = "Green Louse";
        base.health = Random.Range(10, 16);
        base.attackForce = Random.Range(5, 8);
        this.shield = Random.Range(3, 8);
        base.SetMaxHealth();
    }

    public void SetIsDamageTrue()
    {
        isDamagedOnce = true;
    }

    // '�� ����' ���� ���
    public void UseShield()
    {
        if (!isUsingShieldOnce)
        {
            Debug.Log("���� ü�� " + health); // �׽�Ʈ �ڵ�

            isUsingShieldOnce = true;
            health += shield;

            Debug.Log("���� ü�� " + health); // �׽�Ʈ �ڵ�
        }
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

    // '��ȭ' ����� �ο�, �÷��̾��� ���ݷ��� �Ű������� ����.
    public void GiveWeakening(int playerAttackForce)
    {
        playerAttackForce = (int)(playerAttackForce * 0.75);
    }

    // ���� or ��ȭ 2 �ο� ���� ���Ƿ� ���
    private void AttackPattern()
    {
        int randomPattern = Random.Range(0, 2);

        Debug.Log("���� ���� �� : " + randomPattern); // �׽�Ʈ �ڵ�

        if (randomPattern == 0)
        {
            Attack(player);
        }
        else if (randomPattern == 1)
        {
            // ���Ƿ� 6 �ο�. ���� ����.
            GiveWeakening(6);
        }
    }
}