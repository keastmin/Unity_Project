using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureStageButton : StageBaseButton
{
    // ���� �ʱ�ȭ
    void Start()
    {
        stageType = 5;
        stageSelect = false;
        stageClear = false;
    }

    // ��ư Ŭ���� ����
    public override void OnStageButtonClick()
    {
        base.OnStageButtonClick();

        if (!stageEnable)
        {
            Debug.Log("���� Ȱ��ȭ ���� �ʾҽ��ϴ�.");
        }
        else
        {
            stageSelect = true;
            stageClear = true;
            Debug.Log("�ش� �� Ŭ����");
        }
    }
}
