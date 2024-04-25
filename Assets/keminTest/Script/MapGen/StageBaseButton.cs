using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBaseButton : MonoBehaviour
{
    // 맵 생성 스크립트 참조
    public MApGenerator mapGenerator;

    // 시작시 선택 가능한 노드 버튼 애니메이션 활성화
    private void Start()
    {
        if (stageEnable)
        {
            StartCoroutine(Pulse());
        }
    }

    // 버튼 크기 애니메이션
    IEnumerator Pulse()
    {
        // 선택 가능한 버튼에 한하여
        while (stageEnable)
        {
            // 1초간 크기 키움
            for(float i = 0; i <= 1f; i+= Time.deltaTime)
            {
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.6f, i);
                yield return null;
            }

            // 1초간 크기 작게
            for(float i = 0; i <= 1f; i += Time.deltaTime)
            {
                transform.localScale = Vector3.Lerp(Vector3.one * 1.6f, Vector3.one, i);
                yield return null;
            }
        }
    }

    // 노드의 선택 가능 여부가 변경될 시 호출
    public void SetStageEnable(bool enable)
    {
        // 선택 가능 여부를 초기화
        stageEnable = enable;
        if (stageEnable)
        {
            // 애니메이션 실행
            StopAllCoroutines();
            StartCoroutine(Pulse());
        }
        else {
            // 애니메이션 중단 후 원래 크기로 되돌림
            StopAllCoroutines();
            transform.localScale = Vector3.one;
        }
    }

    // 노드 정보를 받아옴
    public void SetMapGenerator(int x, int y, MApGenerator generator)
    {
        mapGenerator = generator;
        this.x = x;
        this.y = y;
    }

    // 0: 일반 전투, 1: 휴식, 2: 이벤트, 3: 엘리트, 4: 상점, 5: 보물, 6: 보스
    [HideInInspector]
    public int stageType;       // 스테이지 유형: 0 ~ 5

    [HideInInspector]
    public bool stageEnable;    // 스테이지 선택 가능 여부

    [HideInInspector]
    public bool stageSelect;    // 스테이지 선택 여부

    [HideInInspector]
    public bool stageClear;     // 스테이지 클리어 여부

    [HideInInspector]
    public int x, y;     // 노드의 인덱스 정보

    public virtual void OnStageButtonClick()
    {
        string[] str = { "일반 전투", "휴식", "이벤트", "엘리트", "상점", "보물", "보스" };
        Debug.Log(str[stageType] + " 선택");

        if (stageEnable)
        {
            stageSelect = true;
            stageClear = true;
            Debug.Log("해당 층 클리어");

            // MapGenerator에 클리어된 노드 정보 전달
            mapGenerator.OnNodeCleared(x, y);
        }
        else
        {
            Debug.Log("아직 활성화 되지 않았습니다.");
        }
    }


}
