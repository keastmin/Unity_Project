using UnityEngine;

public class PlayerSc : MonoBehaviour
{
    //���� ������
    public int energy; // ������
    public int health; // ����ü��
    public int maxHealth; // �ִ�ü��
    public int block; // ���

    void Start()
    {
        InitializePlayer(); // �÷��̾� �ʱ�ȭ
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartTurn(); // �� ����
        }
    }
    void InitializePlayer() //�÷��̾� �ʱ�ȭ
    {
        // �� �ʱ�ȭ
        // �ڵ� �ʱ�ȭ
        energy = 3; // ������
        health = 100; // ü��
        block = 0; // ���
    }


    public void StartTurn() //�� ����
    {
        energy = 3; // ������
        DrawCards(5); // ī�� ��ο�
    }

    public void DrawCards(int numCards) //ī�� ��ο�
    {
        for (int i = 0; i < numCards; i++)
        {
            //ī�� ��ο� �Լ�
        }
    }

    public void EndTurn() //�� ����
    {
        // �ڵ� ������
    }
    public void TakeEnergy(int cost) //������ ����
    {
        if (energy <= 0)
        {
            energy -= cost;
        }
    }

    public void GainEnergy(int cost) //������ ����
    {
        energy += cost;
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // ü�� ����
        if (health <= 0)
        {
            Debug.Log("Game Over!"); // ���� ����!
        }
    }

    public void GainBlock(int amount)
    {
        block += amount; // ��� ����
    }

    public void LoseBlock(int amount)
    {
        block -= amount; // ��� ����
        if (block < 0)
        {
            block = 0; // �� 0���� ������ 0���� ����
        }
    }
}