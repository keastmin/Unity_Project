using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardSc : MonoBehaviour
{
    public string cardName; // ī���� �̸�
    public string description; // ī���� ����   
    public int cost; // ī���� ���  
    public Sprite artwork; // ī���� ��Ʈ��ũ(�̹���)   
    public CardType type; // ī���� ����
    public PlayerSc playersc; // �÷��̾� ��ũ��Ʈ ����
    public List<GameObject> Target = new List<GameObject>(); // ��� ����Ʈ

    private void Start()
    {
        // PlayerSc ��ũ��Ʈ�� �ν��Ͻ��� ã��
        playersc = FindObjectOfType<PlayerSc>();
        this.gameObject.GetComponent<Image>().sprite = artwork;
    }

    // ī�带 ����ϴ� �޼���
    public void Play()
    {
        if (isUsable())
        {
            playersc.energy -= cost;
            Debug.Log("ī�� ���: " + cardName);
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

