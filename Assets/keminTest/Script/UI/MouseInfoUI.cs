using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace keastmin
{
    public class MouseInfoUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private bool isMouseOver = false;

        [SerializeField] private RectTransform canvasRectTransform;
        [SerializeField] private RectTransform topPanelRectTransform;
        [SerializeField] private GameObject infoPanel;

        [SerializeField] private TextMeshPro title;
        [SerializeField] private TextMeshPro text;

        #region MonoBehaviour 매서드

        private void Start()
        {
            canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
            topPanelRectTransform = GameObject.Find("Top UI Panel").GetComponent<RectTransform>();
            if (canvasRectTransform == null || topPanelRectTransform == null)
            {
                Debug.LogError("캔버스 찾을 수 없음");
            }
            
            
        }

        private void Update()
        {
            if (isMouseOver)
            {
                Debug.Log("True");
            }
            else
            {
                Debug.Log("False");
            }
        }

        #endregion

        public void OnPointerEnter(PointerEventData evnetData)
        {
            isMouseOver = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isMouseOver = false;
        }
    }
}