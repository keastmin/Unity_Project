using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBaseButton : MonoBehaviour
{
    // �� ���� ��ũ��Ʈ ����
    public MApGenerator mapGenerator;

    // ���۽� ���� ������ ��� ��ư �ִϸ��̼� Ȱ��ȭ
    private void Start()
    {
        if (stageEnable)
        {
            StartCoroutine(Pulse());
        }
    }

    // ��ư ũ�� �ִϸ��̼�
    IEnumerator Pulse()
    {
        // ���� ������ ��ư�� ���Ͽ�
        while (stageEnable)
        {
            // 1�ʰ� ũ�� Ű��
            for(float i = 0; i <= 1f; i+= Time.deltaTime)
            {
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 1.6f, i);
                yield return null;
            }

            // 1�ʰ� ũ�� �۰�
            for(float i = 0; i <= 1f; i += Time.deltaTime)
            {
                transform.localScale = Vector3.Lerp(Vector3.one * 1.6f, Vector3.one, i);
                yield return null;
            }
        }
    }

    // ����� ���� ���� ���ΰ� ����� �� ȣ��
    public void SetStageEnable(bool enable)
    {
        // ���� ���� ���θ� �ʱ�ȭ
        stageEnable = enable;
        if (stageEnable)
        {
            // �ִϸ��̼� ����
            StopAllCoroutines();
            StartCoroutine(Pulse());
        }
        else {
            // �ִϸ��̼� �ߴ� �� ���� ũ��� �ǵ���
            StopAllCoroutines();
            transform.localScale = Vector3.one;
        }
    }

    // ��� ������ �޾ƿ�
    public void SetMapGenerator(int x, int y, MApGenerator generator)
    {
        mapGenerator = generator;
        this.x = x;
        this.y = y;
    }

    // 0: �Ϲ� ����, 1: �޽�, 2: �̺�Ʈ, 3: ����Ʈ, 4: ����, 5: ����, 6: ����
    [HideInInspector]
    public int stageType;       // �������� ����: 0 ~ 5

    [HideInInspector]
    public bool stageEnable;    // �������� ���� ���� ����

    [HideInInspector]
    public bool stageSelect;    // �������� ���� ����

    [HideInInspector]
    public bool stageClear;     // �������� Ŭ���� ����

    [HideInInspector]
    public int x, y;     // ����� �ε��� ����

    public virtual void OnStageButtonClick()
    {
        string[] str = { "�Ϲ� ����", "�޽�", "�̺�Ʈ", "����Ʈ", "����", "����", "����" };
        Debug.Log(str[stageType] + " ����");

        if (stageEnable)
        {
            stageSelect = true;
            stageClear = true;
            Debug.Log("�ش� �� Ŭ����");

            // MapGenerator�� Ŭ����� ��� ���� ����
            mapGenerator.OnNodeCleared(x, y);
        }
        else
        {
            Debug.Log("���� Ȱ��ȭ ���� �ʾҽ��ϴ�.");
        }
    }


}
