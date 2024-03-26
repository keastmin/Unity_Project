using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MerchantStageButton : StageBaseButton
{
    // ���� �ʱ�ȭ
    void Start()
    {
        stageType = 4;
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
