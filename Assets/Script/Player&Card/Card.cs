using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카드 유형을 나타내는 열거형
public enum CardType
{
    Attack, // 공격 카드
    Skill, // 스킬 카드
    Power, // 파워 카드
    Curse // 저주 카드
}

// 카드 클래스
public class Card : ScriptableObject
{
    public string cardName; // 카드의 이름
    public string description; // 카드의 설명   
    public int cost; // 카드의 비용  
    public Sprite artwork; // 카드의 아트워크(이미지)   
    public CardType type; // 카드의 유형
    public PlayerSc playersc;

    // 카드 속성을 초기화하는 생성자
    public Card(string name, string desc, int cost, Sprite art, CardType type)
    {
        this.cardName = name;
        this.description = desc;
        this.cost = cost;
        this.artwork = art;
        this.type = type;
    }

    // 카드를 사용하는 메서드
    public virtual void Play()
    {
        Debug.Log("카드 사용: " + cardName);
        // 여기서 카드 효과를 구현합니다.
    }
}

public class AttackCard : Card
{
    public int damage; // 공격력

    // AttackCard 생성자
    public AttackCard(string name, string desc, int cost, Sprite art, int damage)
        : base(name, desc, cost, art, CardType.Attack)
    {
        this.damage = damage;
    }

    // AttackCard의 Play 메서드를 재정의합니다.
    public override void Play()
    {
        base.Play(); // 부모 클래스의 Play 메서드 호출
        Debug.Log("공격력: " + damage);
        // 여기서 추가적인 공격 카드 효과를 구현합니다.
    }
}

// 스킬 카드 클래스
public class SkillCard : Card
{
    // 스킬 카드에 대한 특화된 속성들
    public int effectValue; // 스킬 효과 값

    // 스킬 카드 생성자
    public SkillCard(string name, string desc, int cost, Sprite art, int effectValue)
        : base(name, desc, cost, art, CardType.Skill)
    {
        this.effectValue = effectValue;
    }

    // 스킬 카드의 Play 메서드를 재정의합니다.
    public override void Play()
    {
        base.Play(); // 부모 클래스의 Play 메서드 호출
        Debug.Log("스킬 효과 값: " + effectValue);
        // 여기서 추가적인 스킬 카드 효과를 구현합니다.
    }
}

// 파워 카드 클래스
public class PowerCard : Card
{
    // 파워 카드에 대한 특화된 속성들
    public int buffValue; // 파워 효과 값

    // 파워 카드 생성자
    public PowerCard(string name, string desc, int cost, Sprite art, int buffValue)
        : base(name, desc, cost, art, CardType.Power)
    {
        this.buffValue = buffValue;
    }

    // 파워 카드의 Play 메서드를 재정의합니다.
    public override void Play()
    {
        base.Play(); // 부모 클래스의 Play 메서드 호출
        Debug.Log("파워 효과 값: " + buffValue);
        // 여기서 추가적인 파워 카드 효과를 구현합니다.
    }
}

// 저주 카드 클래스
public class CurseCard : Card
{
    // 저주 카드에 대한 특화된 속성들
    public int curseValue; // 저주 효과 값

    // 저주 카드 생성자
    public CurseCard(string name, string desc, int cost, Sprite art, int curseValue)
        : base(name, desc, cost, art, CardType.Curse)
    {
        this.curseValue = curseValue;
    }

    // 저주 카드의 Play 메서드를 재정의합니다.
    public override void Play()
    {
        base.Play(); // 부모 클래스의 Play 메서드 호출
        Debug.Log("저주 효과 값: " + curseValue);
        // 여기서 추가적인 저주 카드 효과를 구현합니다.
    }
}