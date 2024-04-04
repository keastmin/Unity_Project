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
        cardCanvas = GameObject.Find("CardCanvas"); // ���� ����
    }

    public override void AttackPattern()
    {
        int randomAttackPattern = Random.Range(0, 3);

        if (randomAttackPattern == 0)
        {
            Debug.Log("���������� ī�� �ο�"); // �׽�Ʈ �ڵ�
            GiveSlimedCard();
        }
        else if (randomAttackPattern == 1)
        {
            Debug.Log("���ݷ� ����"); // �׽�Ʈ �ڵ�
            RaiseAttackForce();
        }
        else
        {
            Debug.Log("��ȭ �ο�"); // �׽�Ʈ �ڵ�
            GiveWeakening(10); // ������ �μ� ����;
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

    // ���������� ī�� 1�� ����
    private void GiveSlimedCard()
    {
        cardPrefab.GetComponent<MonsterCard>().cardSO = monsterCardSO;
        Instantiate(cardPrefab, cardCanvas.transform);
    }

    // ���ݷ� 16���� ���
    private void RaiseAttackForce()
    {
        this.attackForce = 16;
    }

    // '��ȭ' ����� �ο�, �÷��̾��� ���ݷ��� �Ű������� ����.
    private int GiveWeakening(int playerAttackForce)
    {
        return playerAttackForce = (int)(playerAttackForce * 0.75);
    }
}