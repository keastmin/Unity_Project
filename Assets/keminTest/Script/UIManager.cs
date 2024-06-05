using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region public ����

    public List<RectTransform> uiRoot; // ��� UI ����Ʈ

    // ��ư�� ������ Ȱ��ȭ ���θ� ������ �гε�
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
