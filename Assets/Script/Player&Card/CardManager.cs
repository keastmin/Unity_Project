using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
   

    // ī�� ��
    private List<Card> deck = new List<Card>();

    // ���� ī�� ��
    private List<Card> usedDeck = new List<Card>();

    // ī�� �Ŵ��� �ν��Ͻ�
    private static CardManager instance;
    public static CardManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CardManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("CardManager");
                    instance = obj.AddComponent<CardManager>();
                }
            }
            return instance;
        }
    }

    // ī�� �� �ʱ�ȭ
    public void InitializeDeck(List<Card> cards)
    {
        deck.Clear();
        usedDeck.Clear();
        deck.AddRange(cards);
        ShuffleDeck();
    }

    // ī�� ���� ���� �޼���
    private void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    // ī�带 �̴� �޼���
    public Card DrawCard()
    {
        if (deck.Count == 0)
        {
            // ī�� ���� ������� �� ���� ��� ���� ī�� ���� ī�� ������ �ű�
            deck.AddRange(usedDeck);
            usedDeck.Clear();
            ShuffleDeck();
        }

        Card drawnCard = deck[0];
        deck.RemoveAt(0);
        usedDeck.Add(drawnCard);
        return drawnCard;
    }

    // ���� ī�� ������ ī�带 �����ϴ� �޼���
    public void RemoveCardFromUsedDeck(Card card)
    {
        usedDeck.Remove(card);
    }

    // ���� �� ī�� ��뿡 �ʿ��� �߰����� ����� ������ �� �ֽ��ϴ�.
}

