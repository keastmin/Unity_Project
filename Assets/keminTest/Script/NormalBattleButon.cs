using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NormalBattleButon : StageBaseButton
{
    // ���� �ʱ�ȭ
    void Start()
    {
        stageType = 0;
        stageEnable = false;
        stageSelect = false;
        stageClear = false;
    }

    // ��ư Ŭ���� ����
    public override void OnStageButtonClick()
    { 
        base.OnStageButtonClick();

        stageSelect = true;
        stageClear = true;
        Debug.Log("�ش� �� Ŭ����");
    }
}
