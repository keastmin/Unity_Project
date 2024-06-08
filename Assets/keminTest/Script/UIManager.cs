using keastmin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region public 변수

    // Map을 생성한 후 UI의 상태를 초기화 하기 위한 인스턴스
    public static UIManager uiManagerInstance;

    // 씬 이동 시 상단 패널을 초기화 할 변수 리스트
    public List<RectTransform> uiRoot; // 상단 UI 리스트

    // 지도 이미지의 애니메이터
    public Animator mapTopAnimator;
    public Animator mapMidAnimator;
    public Animator mapBotAnimator;

    // 버튼을 누르면 활성화 여부를 결정할 패널들
    public GameObject mapPanel;
    public GameObject buttonPanel;
    public GameObject cardsPanel;
    public GameObject settingPanel;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        uiManagerInstance = this;
        Canvas.ForceUpdateCanvases();

        foreach(RectTransform root in uiRoot)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(root);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region 버튼 제어 함수

    public void OnClickMapButton()
    {
        if (mapPanel.activeSelf)
        {
            AllPanelActiveFalse();
        }
        else
        {
            if (cardsPanel.activeSelf)
            {
                cardsPanel.SetActive(false);
            }
            if (settingPanel.activeSelf)
            {
                settingPanel.SetActive(false);
            }
            mapPanel.SetActive(true);
            buttonPanel.SetActive(true);
            mapTopAnimator.SetBool("MapActive", true);
            mapMidAnimator.SetBool("MapActive", true);
            mapBotAnimator.SetBool("MapActive", true);
            List<StageNode> nodes = CreateMap.createMapInstance.GetStageNodeList();
            foreach(StageNode node in nodes)
            {
                node.animator.SetBool("IsActive", true);
            }
        }
    }

    #endregion


    #region private 매서드

    public void AllPanelActiveFalse()
    {
        if (!mapPanel.activeSelf)
        {
            mapPanel.SetActive(true);
        }
        if (mapPanel.activeSelf)
        {
            buttonPanel.SetActive(false);
            mapTopAnimator.SetBool("MapActive", false);
            mapMidAnimator.SetBool("MapActive", false);
            mapBotAnimator.SetBool("MapActive", false);
            mapPanel.SetActive(false);
        }

        if (!cardsPanel.activeSelf)
        {
            cardsPanel.SetActive(true);
        }
        if (cardsPanel.activeSelf)
        {
            cardsPanel.SetActive(false);
        }

        if (!settingPanel.activeSelf)
        {
            settingPanel.SetActive(true);
        }
        if (settingPanel.activeSelf)
        {
            settingPanel.SetActive(false);
        }
    }

    #endregion
}
