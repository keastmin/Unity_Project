using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public bool isGame;

    public RectTransform cardPosition;

    public List<GameObject> allCards; // ��� ī�� ������
    public List<GameObject> playerCards; // �÷��̾ ������ ī�� ������
    public List<GameObject> beforeUseCards; // �÷��̾ ����ϱ� �� ī�� ������
    public List<GameObject> handCards; // �÷��̰� �տ� ��� �ִ� ī�� ������
    public List<GameObject> usedCards; // �÷��̾ ����� ī�� ������

    public int maxHand;
    private void Start()
    {
        Init(); // �ʱ�ȭ �Լ� ȣ��
    }

    private void Init()
    {
        maxHand = 5;
        for (int i = 0; i < 20; i++)
        {
            playerCards.Add(allCards[0]);
        }

        beforeUseCards = new List<GameObject>(playerCards); // playerCards�� beforeUseCards�� ����
        DrawStartingHand(); // ���� �� �� �̱�
    }

    public void DrawStartingHand()
    {
        for (int i = 0; i < maxHand; i++)
        {
            DrawCard(); // ī�� �̱�
        }
    }

    public void DrawCard()
    {
        if (beforeUseCards.Count == 0)
        {
            if (usedCards.Count > 0)
            {
                ShuffleCards(); // usedCards�� ��� beforeUseCards�� �̵�
            }
            else
            {
                Debug.LogWarning("�� �̻� ī�尡 �����ϴ�!"); // �� �̻� ī�尡 ������ ���
                return;
            }
        }

        GameObject drawnCard = beforeUseCards[0]; // ���� �� �� ī�� �̱�
        beforeUseCards.RemoveAt(0); // ���� ī�带 beforeUseCards���� ����
        handCards.Add(drawnCard); // ���� ī�带 �÷��̾� �տ� �߰�

        // ī�� ���� �� ���� �Լ� ȣ��
        PlaceCard(drawnCard);

        Debug.Log("�÷��̾ ī�带 �̾ҽ��ϴ�: " + drawnCard.name); // ī�带 �̾����� �����
    }

    private void PlaceCard(GameObject card)
    {
        // �Ÿ�(offset)�� �����մϴ�.
        float offset = 50f; // �ʿ信 ���� ���� �����ϼ���

        // �� ��ġ�� ����մϴ�. 
        // �� ī�� ������ �Ÿ��� �����ϱ� ���� �̹� �տ� �ִ� ī���� ���� offset�� ���մϴ�.
        Vector3 newPosition = cardPosition.position + new Vector3(offset * handCards.Count - 990, 0f, 0f);

        // �� ��ġ�� ī�带 �����մϴ�.
        GameObject newCard = Instantiate(card, newPosition, Quaternion.identity);

        // ī�带 cardPosition�� �ڽ����� �����մϴ�.
        newCard.transform.SetParent(cardPosition, false);
    }


    private void ShuffleCards()
    {
        int n = usedCards.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            GameObject temp = usedCards[k];
            usedCards[k] = usedCards[n];
            usedCards[n] = temp;
        }

        beforeUseCards.AddRange(usedCards); // usedCards�� beforeUseCards�� �߰�
        usedCards.Clear(); // usedCards ����

        Debug.Log("ī�带 ��� �ٽ� ���Ǳ� �� ī��� �̵��߽��ϴ�.");
    }

    public void EndTurn()
    {
        foreach (var card in handCards)
        {
            usedCards.Add(card); // �տ� �ִ� ī����� usedCards�� �߰�
        }
        handCards.Clear(); // handCards ����
    }
}
