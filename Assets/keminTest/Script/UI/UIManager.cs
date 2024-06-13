using keastmin;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace keastmin
{
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


        #region private ����

        // ��ũ�Ѻ��� ������ ������Ʈ�� ���� ��ġ�� �����ϱ� ���� ����
        private RectTransform _scrollViewContent;

        // ��ũ���� ���� ��ġ�� ���� �ö� ������ ������ ��ġ ��
        private int _startHeight = 450;
        private int _floorHeight = 80;

        // Hour, Minute, Second
        private int[] _time = new int[3];

        private float miliSecond = 0f;

        [SerializeField] private TextMeshProUGUI timerText;

        #endregion


        // Start is called before the first frame update
        void Start()
        {
            uiManagerInstance = this;
            Canvas.ForceUpdateCanvases();

            foreach (RectTransform root in uiRoot)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(root);
            }
            _scrollViewContent = mapPanel.GetComponent<ScrollRect>().content;
        }

        // Update is called once per frame
        void Update()
        {
            SetTimer();

            if (Input.GetKeyDown(KeyCode.M))
            {
                OnClickMapButton();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                OnClickCardListPanel();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnClickSettingPanel();
            }
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

                // ���� ���� ���� �ִϸ��̼� Ȱ��ȭ
                List<StageNode> nodes = CreateMap.createMapInstance.GetStageNodeList();
                foreach (StageNode node in nodes)
                {
                    node.animator.SetBool("IsActive", true);
                }

                // ��ũ�� ���� ���� ��ġ ����
                int currFloor = CreateMap.createMapInstance.GetStageNodeFloor();
                Vector2 newPos = _scrollViewContent.anchoredPosition;
                newPos.y = _startHeight - (currFloor * _floorHeight);
                _scrollViewContent.anchoredPosition = newPos;
            }
        }

        public void OnClickCardListPanel()
        {
            if (cardsPanel.activeSelf)
            {
                AllPanelActiveFalse();
            }
            else
            {
                if (mapPanel.activeSelf)
                {
                    mapPanel.SetActive(false);
                }
                if (settingPanel.activeSelf)
                {
                    settingPanel.SetActive(false);
                }
                cardsPanel.SetActive(true);
            }
        }

        public void OnClickSettingPanel()
        {
            if (settingPanel.activeSelf)
            {
                AllPanelActiveFalse();
            }
            else
            {
                if (mapPanel.activeSelf)
                {
                    mapPanel.SetActive(false);
                }
                if (cardsPanel.activeSelf)
                {
                    cardsPanel.SetActive(false);
                }
                settingPanel.SetActive(true);
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

        // Ÿ�̸� �Լ�
        void SetTimer()
        {
            miliSecond += Time.deltaTime;
            if (miliSecond >= 1)
            {
                miliSecond = 0f;
                _time[2] += 1;
            }
            if (_time[2] >= 60)
            {
                _time[2] = 0;
                _time[1] += 1;
            }
            if (_time[1] >= 60)
            {
                _time[1] = 0;
                _time[0] += 1;
            }
            timerText.text = _time[0].ToString() + ":" + _time[1].ToString() + ":" + _time[2].ToString();
        }

        #endregion
    }
}