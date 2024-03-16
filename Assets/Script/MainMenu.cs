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

    // �ڷΰ��� ��ư Ŭ�� �Լ�
    public void OnClickBack()
    {
        Debug.Log("�ڷΰ���");
        mainMenuPanel.SetActive(true);
        gameModePanel.SetActive(false);
    }

    // �Ϲ� ���� ��ư Ŭ�� �Լ�
    public void OnClickNormal()
    {
        Debug.Log("�Ϲ� ����");
    }
}
