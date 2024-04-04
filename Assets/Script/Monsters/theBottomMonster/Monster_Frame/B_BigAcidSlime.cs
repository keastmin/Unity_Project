using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_BigAcidSlime : Monster
{
    MonsterCardSO monsterCardSO;
    GameObject cardPrefab;
    GameObject cardCanvas;

    public B_BigAcidSlime()
    {
        InitMonster();
    }

    public override void InitMonster()
    {
        base.name = "Big Acid Slime";
        base.health = Random.Range(65, 70);
        base.attackForce = 11;
        base.SetMaxHealth();
        cardCanvas = GameObject.Find("CardCanvas"); // 임의 설정
    }

    public override void AttackPattern()
    {
        int randomAttackPattern = Random.Range(0, 3);

        if (randomAttackPattern == 0)
        {
            Debug.Log("점액투성이 카드 부여"); // 테스트 코드
            GiveSlimedCard();
        }
        else if (randomAttackPattern == 1)
        {
            Debug.Log("공격력 증가"); // 테스트 코드
            RaiseAttackForce();
        }
        else
        {
            Debug.Log("약화 부여"); // 테스트 코드
            GiveWeakening(10); // 임의의 인수 설정;
        }

        Attack(player);
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public void SetMonsterCardInfo(MonsterCardSO targetSO, GameObject targetPrefab)
    {
        monsterCardSO = targetSO;
        cardPrefab = targetPrefab;
    }

    // 점액투성이 카드 1장 지급
    private void GiveSlimedCard()
    {
        cardPrefab.GetComponent<MonsterCard>().cardSO = monsterCardSO;
        Instantiate(cardPrefab, cardCanvas.transform);
    }

    // 공격력 16으로 상승
    private void RaiseAttackForce()
    {
        this.attackForce = 16;
    }

    // '약화' 디버프 부여, 플레이어의 공격력을 매개변수로 받음.
    private int GiveWeakening(int playerAttackForce)
    {
        return playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}