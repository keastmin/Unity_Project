using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace keastmin
{
    public class MouseInfoUI : MonoBehaviour
    {
        private bool isMouseOver = false;

        [SerializeField] private RectTransform canvasRectTransform;
        [SerializeField] private RectTransform topPanelRectTransform;
        [SerializeField] private GameObject infoPanel;

        [SerializeField][TextArea] private string infoTitle;
        [SerializeField][TextArea] private string infoText;

        private GameObject[] panelChilds = new GameObject[2];
        private GameObject infoObject;
        private RectTransform panelRectTransform;

        #region MonoBehaviour 매서드

        private void Start()
        {
            canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
            topPanelRectTransform = GameObject.Find("Top UI Panel").GetComponent<RectTransform>();
            panelRectTransform = GetComponent<RectTransform>();
            if (canvasRectTransform == null || topPanelRectTransform == null)
            {
                Debug.LogError("캔버스 찾을 수 없음");
            }

            if (infoPanel == null)
            {
                Debug.LogError("infoPanel이 없습니다.");
            }
            else {
                infoObject = Instantiate(infoPanel, canvasRectTransform);
                panelChilds[0] = infoObject.transform.GetChild(0).gameObject;
                panelChilds[1] = infoObject.transform.GetChild(1).gameObject;

                TextMeshProUGUI currTitle = panelChilds[0].GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI currText = panelChilds[1].GetComponent<TextMeshProUGUI>();
                currTitle.text = infoTitle;
                currText.text = infoText;
                infoObject.SetActive(false);
            } 
        }

        private void Update()
        {
            Vector2 localMousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, Input.mousePosition, null, out localMousePosition);
            bool isInPanel = panelRectTransform.rect.Contains(localMousePosition);

            if (isInPanel)
            {
                if (!infoObject.activeSelf) infoObject.SetActive(true);
                MouseHover();
            }
            else
            {
                if (infoObject.activeSelf) infoObject.SetActive(false);
            }
        }

        #endregion

        private void MouseHover()
        {
            Vector2 mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, null,out mousePosition);
            Vector2 plusPosition = new Vector2(0f, -50f);
            RectTransform infoRectTransform = infoObject.GetComponent<RectTransform>();
            infoRectTransform.localPosition = mousePosition + plusPosition;
        }
    }
}