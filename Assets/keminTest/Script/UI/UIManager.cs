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


        #region private 변수

        // 스크롤뷰의 컨텐츠 오브젝트의 시작 위치를 조절하기 위한 변수
        private RectTransform _scrollViewContent;

        // 스크롤의 시작 위치와 층이 올라갈 때마다 증가할 위치 값
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

                // 선택 가능 노드들 애니메이션 활성화
                List<StageNode> nodes = CreateMap.createMapInstance.GetStageNodeList();
                foreach (StageNode node in nodes)
                {
                    node.animator.SetBool("IsActive", true);
                }

                // 스크롤 뷰의 시작 위치 설정
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

        // 타이머 함수
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