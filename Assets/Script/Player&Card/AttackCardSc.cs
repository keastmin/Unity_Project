using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCardSc : MonoBehaviour
{
    public string cardName; // ī���� �̸�
    public string description; // ī���� ����   
    public int cost; // ī���� ���  
    public Sprite artwork; // ī���� ��Ʈ��ũ(�̹���)   
    public CardType type; // ī���� ����
    public PlayerSc playersc; // �÷��̾� ��ũ��Ʈ ����
    public CardManager cardManager; // �÷��̾� ��ũ��Ʈ ����
    public List<GameObject> Target = new List<GameObject>(); // ��� ����Ʈ
    public int damage; // ī���� ������

    private void Start()
    {
        // PlayerSc ��ũ��Ʈ�� �ν��Ͻ��� ã��
        playersc = FindObjectOfType<PlayerSc>();
        cardManager = FindObjectOfType<CardManager>();
        this.gameObject.GetComponent<Image>().sprite = artwork;
    }

    // ī�带 ����ϴ� �޼���
    public void Play()
    {
        if (isUsable())
        {
            playersc.energy -= cost;
            Debug.Log("ī�� ���: " + cardName);
            cardManager.usedCards.Add(gameObject); // Add the card to the usedCards list
            cardManager.handCards.Remove(gameObject); // Remove the card from the handCards list
        }
    }

    public void Play(List<GameObject> target)
    {
        if (isUsable())
        {
            playersc.energy -= cost;
            Debug.Log("ī�� ���: " + cardName);
            Debug.Log("�����: " + target);
            Target.Clear();
            cardManager.usedCards.Add(gameObject); // Add the card to the usedCards list
            cardManager.handCards.Remove(gameObject); // Remove the card from the handCards list
        }
    }

    public bool isUsable()
    {
        Debug.Log("isUsable����");
        if (playersc.energy >= cost)
        {
            Debug.Log("������ ���� -> ����մϴ�.");
            return true;
        }
        Debug.Log("������ ������ -> �����մϴ�.");
        return false;
    }
}