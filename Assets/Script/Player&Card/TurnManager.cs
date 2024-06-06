using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    [SerializeField][Tooltip("카드 배분이 매우 빨라집니다.")] bool fastMode;

    [Header("Properties")]
    public bool isLoading;
    public bool myTurn;

    WaitForSeconds delay05 = new WaitForSeconds(0.5f);
    WaitForSeconds delay07 = new WaitForSeconds(0.7f);
    //wa

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
        if (fastMode)
            delay05 = new WaitForSeconds(0.05f);
    }

    #region 턴관련
    public void EndTurn() // 턴 종료시
    {
        myTurn = false;
        // 핸드에 있는 모든 카드를 usedCards 리스트에 추가합니다.
        foreach (CardData card in CardManager.instance.handCards)
        {
            CardManager.instance.usedCards.Add(card);
        }
        foreach (GameObject cardObject in CardManager.instance.handCardGameObjects)
        {
            Destroy(cardObject);
        }
        CardManager.instance.handCardGameObjects.Clear();
        CardManager.instance.handCards.Clear();
    }
    public void StartTurn() // 턴 시작시
    {
            myTurn = true;
            StartCoroutine(CardManager.instance.DrawCardsWithDelay(delay05));
    }

    #endregion
}
