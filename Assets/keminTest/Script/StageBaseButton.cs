using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBaseButton : MonoBehaviour
{
    // 0: �Ϲ� ����, 1: �޽�, 2: �̺�Ʈ, 3: ����Ʈ, 4: ����, 5: ����, 6: ����
    [HideInInspector]
    public int stageType;       // �������� ����: 0 ~ 5

    [HideInInspector]
    public bool stageEnable;    // �������� ���� ���� ����

    [HideInInspector]
    public bool stageSelect;    // �������� ���� ����

    [HideInInspector]
    public bool stageClear;     // �������� Ŭ���� ����

    public virtual void OnStageButtonClick()
    {
        string[] str = { "�Ϲ� ����", "�޽�", "�̺�Ʈ", "����Ʈ", "����", "����", "����" };
        Debug.Log(str[stageType] + " ����");
    }
}
