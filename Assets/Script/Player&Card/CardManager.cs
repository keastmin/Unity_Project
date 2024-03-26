using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
   

    // 카드 덱
    private List<Card> deck = new List<Card>();

    // 사용된 카드 덱
    private List<Card> usedDeck = new List<Card>();

    // 카드 매니저 인스턴스
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

    // 카드 덱 초기화
    public void InitializeDeck(List<Card> cards)
    {
        deck.Clear();
        usedDeck.Clear();
        deck.AddRange(cards);
        ShuffleDeck();
    }

    // 카드 덱을 섞는 메서드
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

    // 카드를 뽑는 메서드
    public Card DrawCard()
    {
        if (deck.Count == 0)
        {
            // 카드 덱이 비어있을 때 새로 섞어서 사용된 카드 덱을 카드 덱으로 옮김
            deck.AddRange(usedDeck);
            usedDeck.Clear();
            ShuffleDeck();
        }

        Card drawnCard = deck[0];
        deck.RemoveAt(0);
        usedDeck.Add(drawnCard);
        return drawnCard;
    }

    // 사용된 카드 덱에서 카드를 제거하는 메서드
    public void RemoveCardFromUsedDeck(Card card)
    {
        usedDeck.Remove(card);
    }

    // 게임 중 카드 사용에 필요한 추가적인 기능을 구현할 수 있습니다.
}

