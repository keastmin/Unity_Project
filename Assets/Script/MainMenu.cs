using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNewGame()
    {
        Debug.Log("�� ����");
    }

    public void OnClickEncyclopedia()
    {
        Debug.Log("�������");
    }

    public void OnClickStatistics()
    {
        Debug.Log("���");
    }

    public void OnClickSetting()
    {
        Debug.Log("����");
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
