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

        if (!stageEnable)
        {
            Debug.Log("아직 활성화 되지 않았습니다.");
        }
        else
        {
            stageSelect = true;
            stageClear = true;
            Debug.Log("해당 층 클리어");
        }
    }
}
