using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MApGenerator : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform gridPanel;
    private GameObject[,] grid = new GameObject[7, 15];

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
        GeneratePaths();
    }

    void CreateGrid()
    {
        for(int x = 0; x < 7; x++)
        {
            for(int y = 0; y < 15; y++)
            {
                GameObject buttonObj = Instantiate(buttonPrefab, gridPanel);
                buttonObj.name = $"Button_{x}_{y}";
                buttonObj.transform.localPosition = new Vector2(x * 100, y * -100);
                grid[x, y] = buttonObj;
            }
        }
    }

    void GeneratePaths()
    {
        for(int i = 0; i < 6; i++)
        {
            int currentX = Random.Range(0, 7);
            for(int y = 0; y < 15; y++)
            {
                Image buttonImage = grid[currentX, y].GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.color = Random.ColorHSV();
                }
                else
                {
                    Debug.LogError("No Image component found on the button prefab!");
                }
                List<int> possibleX = new List<int>();
                if (currentX > 0) possibleX.Add(currentX - 1);
                if (currentX < 6) possibleX.Add(currentX + 1);
                possibleX.Add(currentX);

                currentX = possibleX[Random.Range(0, possibleX.Count)];
            }
        }
    }
}
