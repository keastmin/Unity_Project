using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region public 변수

    public List<RectTransform> uiRoot; // 상단 UI 리스트

    // 버튼을 누르면 활성화 여부를 결정할 패널들
    public GameObject mapPanel;
    public GameObject buttonPanel;
    public GameObject cardsPanel;
    public GameObject settingPanel;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
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
}
