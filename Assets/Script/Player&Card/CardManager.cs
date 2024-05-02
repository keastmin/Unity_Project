using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public bool isGame;

    public RectTransform cardPosition;

    public List<GameObject> allCards; // 모든 카드 모음집
    public List<GameObject> playerCards; // 플레이어가 소유한 카드 모음집
    public List<GameObject> beforeUseCards; // 플레이어가 사용하기 전 카드 모음집
    public List<GameObject> handCards; // 플레이가 손에 들고 있는 카드 모음집
    public List<GameObject> usedCards; // 플레이어가 사용한 카드 모음집

    public int maxHand;
    private void Start()
    {
        Init(); // 초기화 함수 호출
    }

    private void Init()
    {
        maxHand = 5;
        for (int i = 0; i < 20; i++)
        {
            playerCards.Add(allCards[0]);
        }

        beforeUseCards = new List<GameObject>(playerCards); // playerCards를 beforeUseCards에 복사
        DrawStartingHand(); // 시작 손 패 뽑기
    }

    public void DrawStartingHand()
    {
        for (int i = 0; i < maxHand; i++)
        {
            DrawCard(); // 카드 뽑기
        }
    }

    public void DrawCard()
    {
        if (beforeUseCards.Count == 0)
        {
            if (usedCards.Count > 0)
            {
                ShuffleCards(); // usedCards를 섞어서 beforeUseCards로 이동
            }
            else
            {
                Debug.LogWarning("더 이상 카드가 없습니다!"); // 더 이상 카드가 없음을 경고
                return;
            }
        }

        GameObject drawnCard = beforeUseCards[0]; // 덱의 맨 위 카드 뽑기
        beforeUseCards.RemoveAt(0); // 뽑은 카드를 beforeUseCards에서 제거
        handCards.Add(drawnCard); // 뽑은 카드를 플레이어 손에 추가

        // 카드 생성 및 정렬 함수 호출
        PlaceCard(drawnCard);

        Debug.Log("플레이어가 카드를 뽑았습니다: " + drawnCard.name); // 카드를 뽑았음을 디버그
    }

    private void PlaceCard(GameObject card)
    {
        // 거리(offset)를 정의합니다.
        float offset = 50f; // 필요에 따라 값을 조정하세요

        // 새 위치를 계산합니다. 
        // 각 카드 사이의 거리를 조절하기 위해 이미 손에 있는 카드의 수에 offset을 곱합니다.
        Vector3 newPosition = cardPosition.position + new Vector3(offset * handCards.Count - 990, 0f, 0f);

        // 새 위치에 카드를 생성합니다.
        GameObject newCard = Instantiate(card, newPosition, Quaternion.identity);

        // 카드를 cardPosition의 자식으로 설정합니다.
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

        beforeUseCards.AddRange(usedCards); // usedCards를 beforeUseCards에 추가
        usedCards.Clear(); // usedCards 비우기

        Debug.Log("카드를 섞어서 다시 사용되기 전 카드로 이동했습니다.");
    }

    public void EndTurn()
    {
        foreach (var card in handCards)
        {
            usedCards.Add(card); // 손에 있는 카드들을 usedCards에 추가
        }
        handCards.Clear(); // handCards 비우기
    }
}
