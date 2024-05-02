using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardSc : MonoBehaviour
{
    public string cardName; // 카드의 이름
    public string description; // 카드의 설명   
    public int cost; // 카드의 비용  
    public Sprite artwork; // 카드의 아트워크(이미지)   
    public CardType type; // 카드의 유형
    public PlayerSc playersc; // 플레이어 스크립트 접근
    public List<GameObject> Target = new List<GameObject>(); // 대상 리스트

    private void Start()
    {
        // PlayerSc 스크립트의 인스턴스를 찾음
        playersc = FindObjectOfType<PlayerSc>();
        this.gameObject.GetComponent<Image>().sprite = artwork;
    }

    // 카드를 사용하는 메서드
    public void Play()
    {
        if (isUsable())
        {
            playersc.energy -= cost;
            Debug.Log("카드 사용: " + cardName);
        }
    }

    public bool isUsable()
    {
        Debug.Log("isUsable들어옴");
        if (playersc.energy >= cost)
        {
            Debug.Log("에너지 충족 -> 사용합니다.");
            return true;
        }
        Debug.Log("에너지 불충족 -> 사용안합니다.");
        return false;
    }
}

