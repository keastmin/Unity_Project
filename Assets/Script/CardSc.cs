using UnityEngine;

public class CardSc : MonoBehaviour
{
    PlayerSc playerSc;

    // ī�� ������
    public string cardName; // ī�� �̸�
    public string cardType; // ī�� Ÿ��
    public int cost; // ���
    public int damage; // ������
    public int block; // ��


    public void Play() // ����� �ִ� ī�带 ����Ҷ� �ҷ��� �Լ�
    {
        if (playerSc.energy > cost) // �÷��̾��� energy�� cost ���� ũ��
        {
            playerSc.TakeEnergy(cost); // �÷��̾��� energy�� cost��ŭ ���̱�
        }
        if(block > 0)
        {
            playerSc.GainBlock(block);
        }
    }

    public void Play(GameObject obj) // ����� �ִ� ī�带 ����Ҷ� �ҷ��� �Լ�
    {
        if (damage > 0) // �������� 0���� ũ�� ����� ü���� 0 �̻��̸�
        {
            //������ TakeDamage�Լ� �ҷ�����
        }
        if (playerSc.energy > cost) // �÷��̾��� energy�� cost ���� ũ��
        {
            playerSc.TakeEnergy(cost); // �÷��̾��� energy�� cost��ŭ ���̱�
        }
        if (block > 0)
        {
            playerSc.GainBlock(block);
        }
    }
}