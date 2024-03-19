using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public enum BuffType
{
    Strength, // 힘 0
    Poisoned, // 독 1
    Vulnerable, // 취약 2
    Dexterity //민첩 3
    // 추가적인 버프와 디버프는 여기에 추가가능
};


public class PlayerSc : MonoBehaviour
{
    //전투 변수들
    public int energy; // 에너지
    public int health; // 현재체력
    public int maxHealth = 100; // 최대체력
    public int block; // 방어

    private List<BuffType> buffs = new List<BuffType>();

    void Start()
    {
        InitializePlayer(); // 플레이어 초기화
    }

 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartTurn(); // 턴 시작
        }
    }


    // 버프 추가
    public void AddBuff(BuffType buff)
    {
        buffs.Add(buff);
        // 버프를 적용하는 추가적인 로직을 여기에 추가가능
    }

    // 버프 제거
    public void RemoveBuff(BuffType buff)
    {
        buffs.Remove(buff);
        // 버프를 제거하는 추가적인 로직을 여기에 추가 가능
    }

    // 특정 버프 확인
    public bool HasBuff(BuffType buff)
    {
        return buffs.Contains(buff);
    }

    void InitializePlayer() //플레이어 초기화
    {
        // 덱 초기화
        // 핸드 초기화
        energy = 3; // 에너지
        health = maxHealth; // 체력
        block = 0; // 방어
    }


    public void StartTurn() //턴 시작
    {
        energy = 3; // 에너지
        DrawCards(5); // 카드 드로우
    }

    public void DrawCards(int numCards) //카드 드로우
    {
        for (int i = 0; i < numCards; i++)
        {
            //카드 드로우 함수
        }
    }

    public void EndTurn() //턴 종료
    {
        // 핸드 버리기
    }
    public void TakeEnergy(int cost) //에너지 감소
    {
        if (energy <= 0)
        {
            energy -= cost;
        }
    }

    public void GainEnergy(int cost) //에너지 증가
    {
        energy += cost;
    }

    public void TakeDamage(int damage)
    {
        health -= damage; // 체력 감소
        if (health <= 0)
        {
            Debug.Log("Game Over!"); // 게임 종료!
        }
    }

    public void GainBlock(int amount)
    {
        block += amount; // 방어 증가
    }

    public void LoseBlock(int amount)
    {
        block -= amount; // 방어 감소
        if (block < 0)
        {
            block = 0; // 방어가 0보다 작으면 0으로 설정
        }
    }
}