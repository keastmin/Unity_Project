using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestButton : StageBaseButton
{
    // ���� �ʱ�ȭ
    void Start()
    {
        stageType = 1;
        stageSelect = false;
        stageClear = false;
    }

    // ��ư Ŭ���� ����
    public override void OnStageButtonClick()
    {
        base.OnStageButtonClick();
    }
}
