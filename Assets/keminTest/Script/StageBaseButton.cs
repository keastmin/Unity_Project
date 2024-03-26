using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBaseButton : MonoBehaviour
{
    // 0: 일반 전투, 1: 휴식, 2: 이벤트, 3: 엘리트, 4: 상점, 5: 보물, 6: 보스
    [HideInInspector]
    public int stageType;       // 스테이지 유형: 0 ~ 5

    [HideInInspector]
    public bool stageEnable;    // 스테이지 선택 가능 여부

    [HideInInspector]
    public bool stageSelect;    // 스테이지 선택 여부

    [HideInInspector]
    public bool stageClear;     // 스테이지 클리어 여부

    public virtual void OnStageButtonClick()
    {
        string[] str = { "일반 전투", "휴식", "이벤트", "엘리트", "상점", "보물", "보스" };
        Debug.Log(str[stageType] + " 선택");
    }
}
