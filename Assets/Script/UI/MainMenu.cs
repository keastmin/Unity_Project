using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // 판넬
    public GameObject mainMenuPanel;        // 메인 메뉴
    public GameObject gameModePanel;        // 게임 모드
    public GameObject characterSelectPanel; // 캐릭터 선택

    // 캐릭터 배경
    public GameObject[] characterBackground;

    // 출정 버튼
    public GameObject embarkButton;

    // 선택 캐릭터 구분 변수 - 0: 아이언클래드
    private int selectCharacter;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        gameModePanel.SetActive(false);
        characterSelectPanel.SetActive(false);
        ResetCharacterSelectPanel();
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

    // 게임 모드 선택에서 뒤로가기 버튼 클릭 함수
    public void OnClickModeBack()
    {
        Debug.Log("뒤로가기");
        mainMenuPanel.SetActive(true);
        gameModePanel.SetActive(false);
    }

    // 일반 게임 버튼 클릭 함수
    public void OnClickNormal()
    {
        Debug.Log("일반 게임");
        gameModePanel.SetActive(false);
        characterSelectPanel.SetActive(true);
        ResetCharacterSelectPanel();
    }

    // 캐릭터 선택 화면 초기화
    private void ResetCharacterSelectPanel()
    {
        // 선택 캐릭터 없음
        selectCharacter = -1;

        // 모든 캐릭터 배경 off
        for (int i = 0; i < characterBackground.Length; i++)
        {
            characterBackground[i].SetActive(false);
        }

        // 출정 버튼 비활성화
        embarkButton.SetActive(false);
    }

    // 캐릭터 선택에서 뒤로가기 버튼 클릭 함수
    public void OnClickSelectBack()
    {
        Debug.Log("뒤로가기");
        gameModePanel.SetActive(true);
        characterSelectPanel.SetActive(false);
    }

    // 아이언 클래드 선택 버튼 클릭 함수
    public void OnClickSelectIronClad()
    {
        Debug.Log("아이언클래드 선택");

        // 아이언클래드 0번
        selectCharacter = 0;

        // 아이언 클래드 외의 캐릭터 배경 off
        for (int i = 0; i < characterBackground.Length; i++)
        {
            if (i == selectCharacter)
            {
                characterBackground[i].SetActive(true);
            }
            else
            {
                characterBackground[i].SetActive(false);
            }
        }

        // 출정 버튼 활성화
        embarkButton.SetActive(true);
    }

    // 출정 버튼 클릭 함수
    public void OnClickEmbark()
    {
        Debug.Log("출정");
        // 아이언클래드 0번
        if(selectCharacter == 0)
        {
            Debug.Log("아이언클래드로 출정");
            SceneManager.LoadScene("MapScene");
        }
    }
}
