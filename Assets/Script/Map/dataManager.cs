using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerData
{
	// �̸�, ����, ����, �������� ����
	public string name;
	//public int level = 1;
	public int coin = 100;
	public Vector2 imagePos;
}

public class dataManager : MonoBehaviour
{
	public static dataManager instance; // �̱�������

	public PlayerData nowPlayer = new PlayerData(); // �÷��̾� ������ ����
	//public DataTest dataTest = new DataTest();

	public string path; // ���
	public int nowSlot; // ���� ���Թ�ȣ

	private void Awake()
	{
		#region �̱���
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(instance.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
		#endregion

		path = "D:\\����Ƽ ��Ʈ������\\Unity_SlaytheSpire\\Unity_Project\\Assets\\Script\\Map\\MapResource\\" + "/save";    // ��� ����
	}

	public void SaveData()
	{
		string data = JsonUtility.ToJson(nowPlayer); //ToJson���� ����
		//string data = JsonUtility.ToJson(dataTest);
		File.WriteAllText(path + nowSlot.ToString(), data);
	}

	public void LoadData()
	{
		string data = File.ReadAllText(path + nowSlot.ToString());
		nowPlayer = JsonUtility.FromJson<PlayerData>(data);
		//dataTest = JsonUtility.FromJson<DataTest>(data);
	}

	public void DataClear()
	{
		nowSlot = -1;
		nowPlayer = new PlayerData(); //�ʱⰪ �������� �ٲ�
		//dataTest = new DataTest(); //�ʱⰪ �������� �ٲ�
	}
}