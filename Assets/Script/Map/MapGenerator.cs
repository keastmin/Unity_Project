using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonPrefab;

    private List<GameObject> buttons = new List<GameObject>(); // ������ ��ư���� ������ ����Ʈ

    [SerializeField]
    private RectTransform Panel;

    

    private void GenerateGrid()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                GameObject buttonObj = Instantiate(ButtonPrefab, Panel);
                buttons.Add(buttonObj); // ������ ��ư�� ����Ʈ�� �߰�
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
