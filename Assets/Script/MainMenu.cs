using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject gameModePanel;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        gameModePanel.SetActive(false);
    }

    // 새 게임 버튼 클릭 함수
    public void OnClickNewGame()
    {
        Debug.Log("새 게임");
        mainMenuPanel.SetActive(false);
        gameModePanel.SetActive(true);
    }

    // 백과사전 버튼 클릭 함수
    public void OnClickEncyclopedia()
    {
        Debug.Log("백과사전");
    }

    // 통계 버튼 클릭 함수
    public void OnClickStatistics()
    {
        Debug.Log("통계");
    }

    // 설정 버튼 클릭 함수
    public void OnClickSetting()
    {
        Debug.Log("설정");
    }

    // 종료 버튼 클릭 함수
    public void OnClickExit()
    {
#if UNITY_EDITOR                                         // 에디터에서 플레이할 시
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 종료
#else
        Application.Quit();
#endif
    }

    // 뒤로가기 버튼 클릭 함수
    public void OnClickBack()
    {
        Debug.Log("뒤로가기");
        mainMenuPanel.SetActive(true);
        gameModePanel.SetActive(false);
    }

    // 일반 게임 버튼 클릭 함수
    public void OnClickNormal()
    {
        Debug.Log("일반 게임");
    }
}
