using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // �ǳ�
    public GameObject mainMenuPanel;        // ���� �޴�
    public GameObject gameModePanel;        // ���� ���
    public GameObject characterSelectPanel; // ĳ���� ����

    // ĳ���� ���
    public GameObject[] characterBackground;

    // ���� ��ư
    public GameObject embarkButton;

    // ���� ĳ���� ���� ���� - 0: ���̾�Ŭ����
    private int selectCharacter;

    void Start()
    {
        mainMenuPanel.SetActive(true);
        gameModePanel.SetActive(false);
        characterSelectPanel.SetActive(false);
        ResetCharacterSelectPanel();
    }

    // �� ���� ��ư Ŭ�� �Լ�
    public void OnClickNewGame()
    {
        Debug.Log("�� ����");
        mainMenuPanel.SetActive(false);
        gameModePanel.SetActive(true);
    }

    // ������� ��ư Ŭ�� �Լ�
    public void OnClickEncyclopedia()
    {
        Debug.Log("�������");
    }

    // ��� ��ư Ŭ�� �Լ�
    public void OnClickStatistics()
    {
        Debug.Log("���");
    }

    // ���� ��ư Ŭ�� �Լ�
    public void OnClickSetting()
    {
        Debug.Log("����");
    }

    // ���� ��ư Ŭ�� �Լ�
    public void OnClickExit()
    {
#if UNITY_EDITOR                                         // �����Ϳ��� �÷����� ��
        UnityEditor.EditorApplication.isPlaying = false; // ������ ����
#else
        Application.Quit();
#endif
    }

    // ���� ��� ���ÿ��� �ڷΰ��� ��ư Ŭ�� �Լ�
    public void OnClickModeBack()
    {
        Debug.Log("�ڷΰ���");
        mainMenuPanel.SetActive(true);
        gameModePanel.SetActive(false);
    }

    // �Ϲ� ���� ��ư Ŭ�� �Լ�
    public void OnClickNormal()
    {
        Debug.Log("�Ϲ� ����");
        gameModePanel.SetActive(false);
        characterSelectPanel.SetActive(true);
        ResetCharacterSelectPanel();
    }

    // ĳ���� ���� ȭ�� �ʱ�ȭ
    private void ResetCharacterSelectPanel()
    {
        // ���� ĳ���� ����
        selectCharacter = -1;

        // ��� ĳ���� ��� off
        for (int i = 0; i < characterBackground.Length; i++)
        {
            characterBackground[i].SetActive(false);
        }

        // ���� ��ư ��Ȱ��ȭ
        embarkButton.SetActive(false);
    }

    // ĳ���� ���ÿ��� �ڷΰ��� ��ư Ŭ�� �Լ�
    public void OnClickSelectBack()
    {
        Debug.Log("�ڷΰ���");
        gameModePanel.SetActive(true);
        characterSelectPanel.SetActive(false);
    }

    // ���̾� Ŭ���� ���� ��ư Ŭ�� �Լ�
    public void OnClickSelectIronClad()
    {
        Debug.Log("���̾�Ŭ���� ����");

        // ���̾�Ŭ���� 0��
        selectCharacter = 0;

        // ���̾� Ŭ���� ���� ĳ���� ��� off
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

        // ���� ��ư Ȱ��ȭ
        embarkButton.SetActive(true);
    }

    // ���� ��ư Ŭ�� �Լ�
    public void OnClickEmbark()
    {
        Debug.Log("����");
        // ���̾�Ŭ���� 0��
        if(selectCharacter == 0)
        {
            Debug.Log("���̾�Ŭ����� ����");
            SceneManager.LoadScene("MapScene");
        }
    }
}
