using keastmin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region public ����

    // Map�� ������ �� UI�� ���¸� �ʱ�ȭ �ϱ� ���� �ν��Ͻ�
    public static UIManager uiManagerInstance;

    // �� �̵� �� ��� �г��� �ʱ�ȭ �� ���� ����Ʈ
    public List<RectTransform> uiRoot; // ��� UI ����Ʈ

    // ���� �̹����� �ִϸ�����
    public Animator mapTopAnimator;
    public Animator mapMidAnimator;
    public Animator mapBotAnimator;

    // ��ư�� ������ Ȱ��ȭ ���θ� ������ �гε�
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

    #region ��ư ���� �Լ�

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


    #region private �ż���

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
