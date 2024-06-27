using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class Select : MonoBehaviour
{
	public GameObject creat;    // �÷��̾� �г��� �Է�UI
	public Text[] slotText;     // Text��
	public Text newPlayerName;  // ���� �Էµ� �÷��̾��� �г���

	bool[] savefile = new bool[3];  // ���̺����� �������� ����

	void Start()
	{
		// ���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�.
		for (int i = 0; i < 3; i++)
		{
			if (File.Exists(dataManager.instance.path + $"{i}"))    // �����Ͱ� �ִ� ���
			{
				savefile[i] = true;         // �ش� ���� ��ȣ�� bool�迭 true�� ��ȯ
				dataManager.instance.nowSlot = i;   // ������ ���� ��ȣ ����
				dataManager.instance.LoadData();    // �ش� ���� ������ �ҷ���
				slotText[i].text = dataManager.instance.nowPlayer.name; // ��ư�� �г��� ǥ��
			}
			else    // �����Ͱ� ���� ���
			{
				slotText[i].text = "�������";
			}
		}
		// �ҷ��� �����͸� �ʱ�ȭ��Ŵ.(��ư�� �г����� ǥ���ϱ������̾��� ����)
		dataManager.instance.DataClear();
	}

	public void Slot(int number)    // ������ ��� ���� (��ư�� ����)
	{
		dataManager.instance.nowSlot = number;  // ������ ��ȣ�� ���Թ�ȣ�� �Է���.

		if (savefile[number])   // bool �迭���� ���� ���Թ�ȣ�� true��� = ������ �����Ѵٴ� ��
		{
			dataManager.instance.LoadData();    // �����͸� �ε��ϰ�
			GoGame();   // ���Ӿ����� �̵�
		}
		else    // bool �迭���� ���� ���Թ�ȣ�� false��� �����Ͱ� ���ٴ� ��
		{
			Creat();    // �÷��̾� �г��� �Է� UI Ȱ��ȭ
		}
	}

	public void Creat() // �÷��̾� �г��� �Է� UI�� Ȱ��ȭ�ϴ� �޼ҵ�
	{
		creat.gameObject.SetActive(true);
	}

	public void GoGame()    // ���Ӿ����� �̵�
	{
		if (!savefile[dataManager.instance.nowSlot])    // ���� ���Թ�ȣ�� �����Ͱ� ���ٸ�
		{
			dataManager.instance.nowPlayer.name = newPlayerName.text; // �Է��� �̸��� �����ؿ�
			dataManager.instance.SaveData(); // ���� ������ ������.
		}
		SceneManager.LoadScene(2); // ���Ӿ����� �̵�
	}
}