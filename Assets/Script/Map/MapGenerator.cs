using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // ��ư �����յ��� ������ ����Ʈ
    public List<GameObject> buttonPrefabs = new List<GameObject>();

    public RectTransform gridPanel;
    private Vector2[,] gridPositions = new Vector2[7, 15];
    private List<List<Vector2>> paths = new List<List<Vector2>>(); // 2���� ����Ʈ

    private List<GameObject> randomImgList = new List<GameObject>(); //���� �̹���

    // ��ο� �� �濡 �ش��ϴ� ��� ���� ����
    private List<int>[] posX = new List<int>[15];
    private HashSet<int>[] usedPosX = new HashSet<int>[15]; // �ߺ� �� ��� x

    // ����� ������� ���� ����
    private HashSet<Color> usedColors = new HashSet<Color>();
    private List<Color> pathColors = new List<Color>();

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
        GeneratePaths();
    }

    void CreateGrid()
    {
        RectTransform panelRect = gridPanel.GetComponent<RectTransform>();

        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 15; y++)
            {

                Vector2 position = new Vector2(x * 120, y * 120);
                gridPositions[x, y] = position;
            }
        }
    }

    void InitPosXList()
    {
        for (int i = 0; i < 15; i++)
        {
            usedPosX[i] = new HashSet<int>();
            posX[i] = new List<int>();
        }
    }

    void GeneratePaths()
    {
        InitPosXList();

        // 1��° x�� ����
        int firstX = 0;

        for (int pathNum = 0; pathNum < 6; pathNum++)
        {
            int currentX = Random.Range(0, 7); // ���� x ��ġ ���� ����

            // �ּ��� 2���� ���� �ٸ� ���� ���� ����
            if (pathNum == 0) firstX = currentX;
            else if (pathNum == 1)
            {
                while (currentX == firstX)
                {
                    currentX = Random.Range(0, 7);
                }
            }

            List<Vector2> path = new List<Vector2>(); // �� ����� ��ġ�� ������ ����Ʈ

            for (int y = 0; y < 15; y++) // ��� ���� Ž��
            {
                path.Add(gridPositions[currentX, y]); // ���� ��ġ�� ��ο� �߰�

                if (!usedPosX[y].Contains(currentX))
                {
                    usedPosX[y].Add(currentX);
                    posX[y].Add(currentX);
                }

                // ���� ���� x ��ġ�� ����
                List<int> possibleX = new List<int>();
                if (currentX > 0) possibleX.Add(currentX - 1);
                possibleX.Add(currentX);
                if (currentX < 6) possibleX.Add(currentX + 1);

                currentX = possibleX[Random.Range(0, possibleX.Count)]; // ���� x ��ġ ���� ����
            }

            paths.Add(path); // ������ ��θ� ��� ����Ʈ�� �߰�

            Color newColor;
            do
            {
                newColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
            } while (usedColors.Contains(newColor));
            usedColors.Add(newColor);
            pathColors.Add(newColor);
        }

        CreateButton();
    }

    void CreateButton()
    {
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < posX[i].Count; j++)
            {
                Vector2 buttonPos = gridPositions[posX[i][j], i];
                GameObject buttonPrefab = buttonPrefabs[Random.Range(0, buttonPrefabs.Count)]; // ������ ��ư ������ ����
                GameObject buttonObj = Instantiate(buttonPrefab, gridPanel);
                buttonObj.transform.position = buttonPos;
                randomImgList.Add(buttonObj);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (paths.Count > 0)
        {
            for (int pathIndex = 0; pathIndex < paths.Count; pathIndex++)
            {
                var path = paths[pathIndex];
                Gizmos.color = pathColors[pathIndex];
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Gizmos.DrawLine(path[i], path[i + 1]);
                }
            }
        }
    }
}
