using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonPrefab;

    private List<GameObject> buttons = new List<GameObject>(); // 생성된 버튼들을 저장할 리스트

    [SerializeField]
    private RectTransform Panel;

    

    private void GenerateGrid()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                GameObject buttonObj = Instantiate(ButtonPrefab, Panel);
                buttons.Add(buttonObj); // 생성된 버튼을 리스트에 추가
            }
        }

    }

    private void GeneratePath()
    {
        foreach (GameObject buttonObj in buttons)
        {
            
        }
    }


    private void Start()
    {
        GenerateGrid();
    }
}
