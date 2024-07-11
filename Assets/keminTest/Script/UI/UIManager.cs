using keastmin;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace keastmin
{
    public class UIManager : MonoBehaviour
    {
        #region public 변수

        // Map을 생성한 후 UI의 상태를 초기화 하기 위한 인스턴스
        public static UIManager instance;

        // 씬 이동 시 상단 패널을 초기화 할 변수 리스트
        public List<RectTransform> uiRoot; // 상단 UI 리스트

        // 버튼을 누르면 활성화 여부를 결정할 패널들
        public GameObject basePanel;
        public GameObject mapPanel;
        public GameObject buttonPanel;
        public GameObject cardsPanel;
        public GameObject settingPanel;

        #endregion


        #region private 변수

        // UI의 FadeIn, FadeOut을 담당할 캔버스 그룹
        private CanvasGroup _basePanelCanvasGroup;
        private CanvasGroup _mapPanelCanvasGroup;

        // 정보에 관한 패널의 활성화 여부
        private bool _isBasePanelActive = false;
        private bool _isMapPanelActive = false;
        private bool _isCardsPanelActive = false;
        private bool _isSettingPanelActive = false;

        // 코루틴 변수
        private Coroutine _basePanelCoroutine;
        private Coroutine _mapPanelCoroutine;

        #endregion


        #region MonoBehaviou 메서드

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            _basePanelCanvasGroup = basePanel.GetComponent<CanvasGroup>();
            _mapPanelCanvasGroup = mapPanel.GetComponent<CanvasGroup>();
            UpdateCanvasRayout(); // 캔버스의 UI 요소 중 실행 시 정상적이지 않은 UI 강제 업데이트
        }

        void Update()
        {
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

        #endregion


        #region 버튼 제어 함수

        public void OnClickMapButton()
        {
            if (_isMapPanelActive)
            {
                MapPanelOff();
                BasePanelOff();
            }
            else
            {
                BasePanelOn();
                MapPanelOn();
                CardListPanelOff();
                SettingPanelOff();            
            }
        }

        public void OnClickCardListPanel()
        {
            if (cardsPanel.activeSelf)
            {
                CardListPanelOff();
                BasePanelOff();
            }
            else
            {
                BasePanelOn();
                MapPanelOff();
                CardListPanelOn();
                SettingPanelOff();
            }
        }

        public void OnClickSettingPanel()
        {
            if (settingPanel.activeSelf)
            {
                SettingPanelOff();
                BasePanelOff();
            }
            else
            {
                BasePanelOn();
                MapPanelOff();
                CardListPanelOff();
                SettingPanelOn();
            }
        }

        #endregion


        #region private 매서드

        private void BasePanelOn()
        {
            if (!_isMapPanelActive && !_isCardsPanelActive && !_isSettingPanelActive)
            {
                _isBasePanelActive = true;
                basePanel.SetActive(true);
                if (_basePanelCoroutine != null)
                {
                    StopCoroutine(_basePanelCoroutine);
                }
                _basePanelCoroutine = StartCoroutine(FadeIn(_basePanelCanvasGroup, 0.5f));
            }
        }

        private void BasePanelOff()
        {
            _isBasePanelActive = false;
            if(_basePanelCoroutine != null)
            {
                StopCoroutine(_basePanelCoroutine);
            }
            _basePanelCoroutine = StartCoroutine(FadeOut(_basePanelCanvasGroup, 0.3f, () => basePanel.SetActive(false)));
        }

        private void MapPanelOn()
        {
            _isMapPanelActive = true;
            mapPanel.SetActive(true);
            buttonPanel.SetActive(true);
            if(_mapPanelCoroutine != null)
            {
                StopCoroutine(_mapPanelCoroutine);
            }
            _mapPanelCoroutine = StartCoroutine(FadeIn(_mapPanelCanvasGroup, 0.2f));
        }

        private void MapPanelOff()
        {
            _isMapPanelActive = false;
            buttonPanel.SetActive(false);
            if(_mapPanelCoroutine != null)
            {
                StopCoroutine(_mapPanelCoroutine);
            }
            _mapPanelCoroutine = StartCoroutine(FadeOut(_mapPanelCanvasGroup, 0.2f, () => mapPanel.SetActive(false)));           
        }

        private IEnumerator FadeOut(CanvasGroup canvasGroup, float duration, System.Action onComplete = null)
        {
            float startAlpha = canvasGroup.alpha;
            float time = 0;

            while(time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, time / duration);
                yield return null;
            }

            canvasGroup.alpha = 0;
            onComplete?.Invoke();
        }

        private IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
        {
            float startAlpha = canvasGroup.alpha;
            float time = 0;

            while(time < duration)
            {
                time += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, time / duration);
                yield return null; 
            }

            canvasGroup.alpha = 1;
        }

        private void CardListPanelOn()
        {
            _isCardsPanelActive = true;
            cardsPanel.SetActive(true);
        }

        private void CardListPanelOff()
        {
            _isCardsPanelActive = false;
            cardsPanel.SetActive(false);
        }

        private void SettingPanelOn()
        {
            _isSettingPanelActive = true;
            settingPanel.SetActive(true);
        }

        private void SettingPanelOff()
        {
            _isSettingPanelActive = false;
            settingPanel.SetActive(false);
        }

        public void AllPanelActiveFalse()
        {
            _mapPanelCanvasGroup.alpha = 0;
            _basePanelCanvasGroup.alpha = 0;
            basePanel.SetActive(false);
            mapPanel.SetActive(false);
            cardsPanel.SetActive(false);
            settingPanel.SetActive(false);
        }

        private void UpdateCanvasRayout()
        {
            Canvas.ForceUpdateCanvases();
            foreach (RectTransform root in uiRoot)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(root);
            }
        }

        #endregion
    }
}