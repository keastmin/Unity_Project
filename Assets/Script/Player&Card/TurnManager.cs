using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    [SerializeField][Tooltip("ī�� ����� �ſ� �������ϴ�.")] bool fastMode;

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

    #region �ϰ���
    public void EndTurn() // �� �����
    {
        myTurn = false;
        // �ڵ忡 �ִ� ��� ī�带 usedCards ����Ʈ�� �߰��մϴ�.
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
    public void StartTurn() // �� ���۽�
    {
            myTurn = true;
            StartCoroutine(CardManager.instance.DrawCardsWithDelay(delay05));
    }

    #endregion
}
