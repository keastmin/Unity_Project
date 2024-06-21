using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace keastmin
{
    public class MouseInfoUI : MonoBehaviour
    {
        [SerializeField] private RectTransform canvasRectTransform;
        [SerializeField] private RectTransform topPanelRectTransform;
        [SerializeField] private GameObject infoPanel;

        [SerializeField][TextArea] private string infoTitle;
        [SerializeField][TextArea] private string infoText;

        private GameObject[] panelChilds = new GameObject[2];
        private GameObject infoObject;
        private RectTransform panelRectTransform;

        [Header("Info Position")]
        [SerializeField] private float plusX = 0f;
        [SerializeField] private float plusY = -50f;
        [SerializeField] private bool anchorX = false;
        [SerializeField] private float positionX = 0f;
        [SerializeField] private bool anchorY = false;
        [SerializeField] private float positionY = 0f;

        private float canvasWidth;
        private float canvasHeight;
        private float topPanelHeight;

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

                RectTransform rectTransform = infoObject.GetComponent<RectTransform>();
                LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);

                infoObject.SetActive(false);

                canvasWidth = canvasRectTransform.rect.width / 2;
                canvasHeight = canvasRectTransform.rect.height / 2;
                topPanelHeight = topPanelRectTransform.rect.height;

                //Debug.Log(name + " " + rectTransform.rect.width + " " + rectTransform.rect.height);
            } 
        }

        private void Update()
        {
            Vector2 localMousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, Input.mousePosition, null, out localMousePosition);
            bool isInPanel = panelRectTransform.rect.Contains(localMousePosition);

            if (isInPanel)
            {
                MouseHover();
                if (!infoObject.activeSelf) infoObject.SetActive(true);
            }
            else
            {
                if (infoObject.activeSelf) infoObject.SetActive(false);
            }
        }

        #endregion

        private void SetCanvasSize()
        {

        }

        private void MouseHover()
        {
            Vector2 mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, null,out mousePosition);
            Vector2 plusPosition = new Vector2(plusX, plusY);
            RectTransform infoRectTransform = infoObject.GetComponent<RectTransform>();
            Vector2 proposedPosition = mousePosition;// + plusPosition;

            float infoWidth = infoRectTransform.rect.width / 2;
            float infoHeight = infoRectTransform.rect.height / 2;

            if (proposedPosition.y < -canvasHeight + 10 + infoHeight) proposedPosition.y = -canvasHeight + 10 + infoHeight;
            if (proposedPosition.y > canvasHeight - topPanelHeight - 10 - infoHeight) proposedPosition.y = canvasHeight - topPanelHeight - 10 - infoHeight;
            if (proposedPosition.x < -canvasWidth + 10 + infoWidth) proposedPosition.x = -canvasWidth + 10 + infoWidth;
            if (proposedPosition.x > canvasWidth - 10 - infoWidth) proposedPosition.x = canvasWidth - 10 - infoWidth;

            // 최종 위치 설정
            infoRectTransform.localPosition = proposedPosition;
        }
    }
}