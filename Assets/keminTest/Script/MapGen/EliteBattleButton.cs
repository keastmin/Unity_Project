using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EliteBattleButton : StageBaseButton
{
    // ���� �ʱ�ȭ
    void Start()
    {
        stageType = 3;
        stageSelect = false;
        stageClear = false;
    }

    // ��ư Ŭ���� ����
    public override void OnStageButtonClick()
    {
        base.OnStageButtonClick();
    }
}
