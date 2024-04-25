using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public float scrollSpeed = 10f;
    public Transform panel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        RectTransform rectTransform = panel.GetComponent<RectTransform>();

        rectTransform.anchoredPosition += new Vector2(0, -scroll * scrollSpeed);
    }
}
