using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "cardData", menuName = "Scriptable object/CardData")]
// 유니티 에디터에서 Assets 메뉴에 새로운 메뉴 항목을 추가할 수 있습니다.
// 이 항목을 선택하면 지정된 파일 이름과 경로에 새로운 ScriptableObject 인스턴스를 생성할 수 있습니다.
[System.Serializable] // 유니티 에디터에서 복잡한 데이터 구조를 설정하고 관리할 수 있도록 합니다.
public class CardData : ScriptableObject
{
    public enum CardType { Attack, Skill, Power }
    public enum CardRank { Basic, Special, Rarity }
    public enum Color { Red, Gray, Blue }
    public enum TargetType { None, One, Random, All }

    [Header("# 메인 정보")]
    public CardType Type; // 카드의 타입을 정합니다.
    public Color color; // 카드의 컬러(희귀도) 
    public TargetType targetType; // 카드의 타겟타입
    public bool isUpgraded; // 카드가 업그레이드 되었는지 불변수
    public int cost; // 카드의 코스트
    public int upgradeCost; // 업그레이드 시 코스트
    public string cardName; // 카드 이름
    [Header("# 카드 설명")]
    [TextArea]
    public string baseDesc; // 카드 기본 설명
    [TextArea] 
    public string upgradeDesc; // 업그레이드 시 설명
    [Header("# 카드이미지")]
    public Sprite Icon; // 카드 이미지
    [Header("# 효과들")]
    public Effect[] baseEffects; // 카드 효과
    public Effect[] upgradeEffects; // 업그레이드 시 효과
}

[System.Serializable]
public class Effect // 효과 정의
{
    public enum EffectType { Attack, Defence, Heal, Draw, StrengthBuff, Dexterity, RegenBuff, VulnerableBuff
            ,WeakBuff, FrailBuff, ThornsBuff, BarricadeBuff, EnergizedBuff, Extinction, Condition }
    /*
    Attack / 공격: 공격력만큼 공격합니다.
    Defence / 방어력: 방어력만큼 방어력을 채워줍니다.
    Heal / 힐: 체력을 회복합니다(음수도 가능)
    Draw / 드로우: 카드를 드로우 합니다.
    Strength / 힘: 플레이어의 공격 카드가 추가 피해를 입힙니다.
    Dexterity / 민첩성: 플레이어의 방어 카드가 추가 방어력을 제공합니다.
    Regen / 재생: 매 턴마다 플레이어의 체력이 회복됩니다.
    Vulnerable / 취약: 플레이어에게 입는 피해가 증가합니다.
    Weak / 약화: 플레이어의 공격 피해가 감소합니다.
    Frail / 연약: 플레이어의 방어력이 감소합니다.
    Thorns / 가시: 플레이어에게 공격한 적에게 피해를 반사합니다.
    Barricade / 바리케이드: 플레이어의 방어력이 턴이 끝날 때 사라지지 않습니다.
    Energized / 활력: 다음 턴에 플레이어의 에너지가 증가합니다.
    Extinction / 소멸: 카드 사용시 소멸된다.
    Condition / 조건: 특정한 조건이 있을 때만 발동.
    */
    public EffectType effectType; // 효과 타입
    public int amount; // 효과 크기
}
