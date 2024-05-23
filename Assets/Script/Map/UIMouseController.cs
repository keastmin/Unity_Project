using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEditor.VersionControl;

public class UIMouseController : MonoBehaviour ,IPointerEnterHandler ,IPointerExitHandler
{
    public GameObject targetGameObject;

    [SerializeField]
    private string uiText;
    private TextMeshProUGUI panalText;
    private Image panalImage;
    bool bCheck;

    private void Awake()
    {
        panalImage = targetGameObject.GetComponentInChildren<Image>();
        panalText = targetGameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        // ���콺 ��ġ�� ���� ���� ������Ʈ ��ġ ������Ʈ
        Vector3 mousePosition = Input.mousePosition;
        Vector3 plusPosition = new Vector3(0, -50, 0);
        panalImage.transform.position = mousePosition + plusPosition;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
            bCheck = true;
            Debug.Log("����");
            
            panalText.text = uiText;
            targetGameObject.SetActive(true);
        

        
    }

    //public void OnPointerMove(PointerEventData eventData)
    //{
    //    panalText.text = uiText;
    //    panalImage.gameObject.SetActive(true);
    //    //Vector3 mousePosition = Input.mousePosition;
    //    //panalImage.transform.position = mousePosition;
    //}

    public void OnPointerExit(PointerEventData eventData)
    {
        bCheck = false;
        Debug.Log("����");
        
        targetGameObject.SetActive(false);
    }

    
}
