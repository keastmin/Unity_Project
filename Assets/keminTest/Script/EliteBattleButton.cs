using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EliteBattleButton : StageBaseButton
{
    // 변수 초기화
    void Start()
    {
        stageType = 3;
        stageEnable = false;
        stageSelect = false;
        stageClear = false;
    }

    // 버튼 클릭시 동작
    public override void OnStageButtonClick()
    {
        base.OnStageButtonClick();

        stageSelect = true;
        stageClear = true;
        Debug.Log("해당 층 클리어");
    }
}
