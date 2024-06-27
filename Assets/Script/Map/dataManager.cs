using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerData
{
	// 이름, 레벨, 코인, 착용중인 무기
	public string name;
	//public int level = 1;
	public int coin = 100;
	public Vector2 imagePos;
}

public class dataManager : MonoBehaviour
{
	public static dataManager instance; // 싱글톤패턴

	public PlayerData nowPlayer = new PlayerData(); // 플레이어 데이터 생성
	//public DataTest dataTest = new DataTest();

	public string path; // 경로
	public int nowSlot; // 현재 슬롯번호

	private void Awake()
	{
		#region 싱글톤
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

		path = "D:\\유니티 포트폴리오\\Unity_SlaytheSpire\\Unity_Project\\Assets\\Script\\Map\\MapResource\\" + "/save";    // 경로 지정
	}

	public void SaveData()
	{
		string data = JsonUtility.ToJson(nowPlayer); //ToJson으로 변형
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
		nowPlayer = new PlayerData(); //초기값 설정으로 바뀜
		//dataTest = new DataTest(); //초기값 설정으로 바뀜
	}
}