using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestButton : StageBaseButton
{
    // 변수 초기화
    void Start()
    {
        stageType = 1;
        stageSelect = false;
        stageClear = false;
    }

    // 버튼 클릭시 동작
    public override void OnStageButtonClick()
    {
        base.OnStageButtonClick();
    }
}
