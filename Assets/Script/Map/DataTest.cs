using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class DataTest : MonoBehaviour
{
	//public Image image;
	//public Vector2 imagePos;

	public new Text name;
	//public Text level;
	public Text coin;
	public Image image;
	public Vector2 imagePos;

	void Start()
	{
		//imagePos = image.rectTransform.anchoredPosition;	 
		name.text += dataManager.instance.nowPlayer.name;
		//level.text += dataManager.instance.nowPlayer.level.ToString();
		coin.text += dataManager.instance.nowPlayer.coin.ToString();
		imagePos += dataManager.instance.nowPlayer.imagePos;
		//ItemSetting(dataManager.instance.nowPlayer.item);
	}

	//public void LevelUp()
	//{
	//	dataManager.instance.nowPlayer.level++;
	//	level.text = "레벨 : " + dataManager.instance.nowPlayer.level.ToString();
	//}

	public void CoinUp()
	{
		dataManager.instance.nowPlayer.coin++;
		coin.text = "코인 : " + dataManager.instance.nowPlayer.coin.ToString(); //UI에 보이게 하기
	}

	public void Save()
	{
		dataManager.instance.SaveData();
	}

	//public void ItemSetting(int number)
	//{
	//	for (int i = 0; i < item.Length; i++)
	//	{
	//		if (number == i)
	//		{
	//			item[i].SetActive(true);
	//			DataManager.instance.nowPlayer.item = number;
	//		}
	//		else
	//		{
	//			item[i].SetActive(false);
	//		}
	//	}
	//}
}
