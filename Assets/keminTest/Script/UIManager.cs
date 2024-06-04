using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public RectTransform uiRoot1;
    public RectTransform uiRoot2;
    public RectTransform uiRoot3;
    public RectTransform uiRoot4;
    public RectTransform uiRoot5;
    public RectTransform uiRoot6;
    public RectTransform uiRoot7;



    // Start is called before the first frame update
    void Start()
    {
        Canvas.ForceUpdateCanvases();
        //LayoutRebuilder.ForceRebuildLayoutImmediate(uiRoot1);
        //LayoutRebuilder.ForceRebuildLayoutImmediate(uiRoot2);
        //LayoutRebuilder.ForceRebuildLayoutImmediate(uiRoot3);
        //LayoutRebuilder.ForceRebuildLayoutImmediate(uiRoot4);
        LayoutRebuilder.ForceRebuildLayoutImmediate(uiRoot5);
        LayoutRebuilder.ForceRebuildLayoutImmediate(uiRoot6);
        LayoutRebuilder.ForceRebuildLayoutImmediate(uiRoot7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
