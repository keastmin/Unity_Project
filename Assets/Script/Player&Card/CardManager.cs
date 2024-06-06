using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public TurnManager turnManager;
    public GameManager gameManager;

    public enum Cards { Strike/*Ÿ��*/, Defend/*����*/, Bash/*���*/ }

    [Header("# ī�� ���� ������")]
    [SerializeField] GameObject cardPrefab; // ī�� ���� ������
    [Header("# ��� ī�� ������")]
    [SerializeField] CardData[] cardData; // ��� ī�� ������
    [Header("# ī�� ���� ��ġ")]
    [SerializeField] Transform cardSpawnPoint; // �տ� �� ī���� ��ġ 
    [SerializeField] Transform cardLeft; // ī�� ���Ľ� ���� �ִ� ��ġ
    [SerializeField] Transform cardRight; // ī�� ���Ľ� ������ �ִ� ��ġ
    [Header("# ī�� ����Ʈ")]
    [SerializeField] List<CardData> playerCardList = new List<CardData>(); // �÷��̾ ������ ī�� ����Ʈ

    public List<CardData> beforeDraw = new List<CardData>(); // ���� ������ �̱� �� ī�� ����Ʈ  
    public List<CardData> handCards = new List<CardData>(); // �տ� �ִ� ī�� ����Ʈ
    public List<GameObject> handCardGameObjects = new List<GameObject>(); // �տ� �ִ� ī�� ���� ������Ʈ ����Ʈ
    public List<CardData> usedCards = new List<CardData>(); // ����� ī�� ����Ʈ
    [Header("# ī�� ����Ʈ")]
    public int drawNumber; // ��ο��� ī�� ��

    Card selectCard;

    #region �ν��Ͻ�, Awake
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion

    #region Start
    private void Start()
    {
        InitializePlayerCardList();
        InitializeDrawPile();
    }
    #endregion

    #region ���ӽ��� �� ���� ī�� ����
    private void InitializePlayerCardList()
    {
        // ���� ī�� ���� Ÿ��5, ����4, ���1
        for (int i = 0; i < 5; i++)
        {
            playerCardList.Add(cardData[(int)Cards.Strike]);
        }
        for (int i = 0; i < 4; i++)
        {
            playerCardList.Add(cardData[(int)Cards.Defend]);
        }
        for (int i = 0; i < 1; i++)
        {
            playerCardList.Add(cardData[(int)Cards.Bash]);
        }
    }
    #endregion

    #region ���� �� �� �ʱ�ȭ
    // ���� ������ �̱� �� ī�� �ʱ�ȭ
    private void InitializeDrawPile()
    {
        beforeDraw.AddRange(playerCardList);
        ShuffleCard(beforeDraw);
    }
    #endregion

    #region ī�� ���
    public void UseCard(Card card)
    {
        if (handCards.Contains(card.cardData))
        {
            usedCards.Add(card.cardData);
            handCards.Remove(card.cardData);

            GameObject cardObject = handCardGameObjects.Find(obj => obj.GetComponent<Card>().cardData == card.cardData);
            if (cardObject != null)
            {
                handCardGameObjects.Remove(cardObject);
                Destroy(cardObject);
            }

            // ī�带 ����� �� ���� ������Ʈ
            UpdateCardPositions();
        }
    }
    #endregion

    #region ī�� �̱�
    public void DrawCard()
    {
        if (beforeDraw.Count == 0)
        {
            beforeDraw.AddRange(usedCards);
            usedCards.Clear();
            ShuffleCard(beforeDraw);
        }

        if (beforeDraw.Count > 0)
        {
            CardData drawnCardData = beforeDraw[0];
            beforeDraw.RemoveAt(0);

            GameObject cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Quaternion.identity);
            handCardGameObjects.Add(cardObject);
            Card card = cardObject.GetComponent<Card>();
            card.cardData = drawnCardData;
            card.SetUp();

            handCards.Add(drawnCardData);
            cardObject.GetComponent<Order>().SetOriginOrder(handCards.Count);

            // ī�� �߰� �� ���� ������Ʈ
            UpdateCardPositions();
        }
    }

    public IEnumerator DrawCardsWithDelay(WaitForSeconds delay)
    {
        turnManager.isLoading = true;
        for (int i = 0; i < drawNumber; i++)
        {
            DrawCard();
            yield return delay;
        }
        turnManager.isLoading = false;
    }
    #endregion

    #region ī�� ��ġ ����
    private void UpdateCardPositions()
    {
        List<PRS> originCardPRSs = RoundAlignment(cardLeft, cardRight, handCardGameObjects.Count, 0.5f, Vector3.one * 0.65f);
        for (int i = 0; i < handCardGameObjects.Count; i++)
        {
            handCardGameObjects[i].GetComponent<Card>().originPRS = originCardPRSs[i];
            handCardGameObjects[i].GetComponent<Card>().MoveTransform(handCardGameObjects[i].GetComponent<Card>().originPRS, true, 0.7f);
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);
        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (objCount -1);
                for(int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        for(int i = 0;i < objCount;i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Quaternion.identity;
            if(objCount >= 4)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }
            results.Add(new PRS(targetPos, targetRot, scale));
        }

        return results;
    }
    #endregion

    #region ī�� ����
    public void ShuffleCard(List<CardData> targetCardData)
    {
        for (int i = 0; i < targetCardData.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, targetCardData.Count);
            CardData temp = targetCardData[i];
            targetCardData[i] = targetCardData[randomIndex];
            targetCardData[randomIndex] = temp;
        }
    }
    #endregion

    public void CardMouseOver(Card card)
    {
        selectCard = card;
        EnlargeCard(true, card);
    }

    public void CardMouseExit(Card card)
    {
        EnlargeCard(false, card);
    }

    void EnlargeCard(bool isEnlarge, Card card)
    {
        if (isEnlarge)
        {
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x, -2.5f, -10f);
            card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * 0.8f), false);
        }
        else
            card.MoveTransform(card.originPRS, false);

        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge);
    }
}
