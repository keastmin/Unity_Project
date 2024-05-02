using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 카드 유형을 나타내는 열거형


// 카드 클래스
public class NoUseCard : MonoBehaviour
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
        if (playersc == null)
        {
            Debug.Log("못찾음");
        }
        else
            Debug.Log("찾음" + playersc.gameObject.name + "/" + playersc.energy + "/" + cost);

        this.gameObject.GetComponent<Image>().sprite = artwork;
    }


    // 카드 속성을 초기화하는 생성자
    public NoUseCard(string name, string desc, int cost, CardType type)
    {
        this.cardName = name;
        this.description = desc;
        this.cost = cost;
        this.type = type;
    }

    // 카드를 사용하는 메서드
    public virtual void Play()
    {
        playersc.energy -= cost;
        Debug.Log("카드 사용: " + cardName);
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

public class AttackCard : NoUseCard
{
    public int damage; // 공격력

    // AttackCard 생성자
    public AttackCard(string name, string desc, int cost, int damage)
        : base(name, desc, cost, CardType.Attack)
    {
        this.damage = damage;
    }

    // AttackCard의 Play 메서드를 재정의합니다.
    public override void Play() 
    {
        Debug.Log("Attack Play 들어옴" + isUsable());
        bool check = isUsable();
        Debug.Log("check됌");
        if (check)
        {
            base.Play(); // 부모 클래스의 Play 메서드 호출
        }
    }
}

// 스킬 카드 클래스
public class SkillCard : NoUseCard
{
    // 스킬 카드에 대한 특화된 속성들
    public int effectValue; // 스킬 효과 값

    // 스킬 카드 생성자
    public SkillCard(string name, string desc, int cost, int effectValue)
        : base(name, desc, cost, CardType.Skill)
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
public class PowerCard : NoUseCard
{
    // 파워 카드에 대한 특화된 속성들
    public int buffValue; // 파워 효과 값

    // 파워 카드 생성자
    public PowerCard(string name, string desc, int cost, int buffValue)
        : base(name, desc, cost, CardType.Power)
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
public class CurseCard : NoUseCard
{
    // 저주 카드에 대한 특화된 속성들
    public int curseValue; // 저주 효과 값

    // 저주 카드 생성자
    public CurseCard(string name, string desc, int cost, int curseValue)
        : base(name, desc, cost, CardType.Curse)
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

//타격카드
public class StrikeCard : AttackCard
{
    // Strike 카드 생성자
    public StrikeCard(string name, string desc, int cost, int damage)
        : base(name, desc, cost, damage)
    {
        
    }

    // Strike 카드의 Play 메서드를 재정의합니다.
    public override void Play()
    {
        base.Play(); // 부모 클래스의 Play 메서드 호출
        Debug.Log("Strike를 사용하여 " + damage + "의 피해를 입힙니다!");
        // 여기서 추가적인 효과를 구현할 수 있습니다.
    }
}
//수비카드
public class DefendCard : SkillCard
{
    // Defend 카드 생성자
    public DefendCard(string name, string desc, int cost, int defenseAmount)
        : base(name, desc, cost, defenseAmount)
    {
    }
    // Defend 카드의 Play 메서드를 재정의합니다.
    public override void Play()
    {
        base.Play(); // 부모 클래스의 Play 메서드 호출
        Debug.Log("Defend를 사용하여 방어력을 " + effectValue + "만큼 증가시킵니다!");
        PlayerSc.instance.GainBlock(effectValue); // 플레이어의 방어력을 지정된 값만큼 증가시킵니다.
    }
}

