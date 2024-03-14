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
        Debug.Log("새 게임");
    }

    public void OnClickEncyclopedia()
    {
        Debug.Log("백과사전");
    }

    public void OnClickStatistics()
    {
        Debug.Log("통계");
    }

    public void OnClickSetting()
    {
        Debug.Log("설정");
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
