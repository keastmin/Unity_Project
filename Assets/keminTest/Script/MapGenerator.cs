using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MApGenerator : MonoBehaviour
{
    public Transform gridPanel;
    private Vector2[,] gridPositions = new Vector2[7, 15];
    private List<List<Vector2>> paths = new List<List<Vector2>>();
    private List<Color> pathColors = new List<Color>();

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
        GeneratePaths();
    }

    void CreateGrid()
    {
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                Vector2 position = new Vector2(x * 200 + 400, y * 150 + 100);
                gridPositions[x, y] = position;
            }
        }
    }

    void GeneratePaths()
    {
        for (int pathNum = 0; pathNum < 6; pathNum++)
        {
            int currentX = Random.Range(0, 7); // ���� x ��ġ ���� ����
            List<Vector2> path = new List<Vector2>(); // �� ����� ��ġ�� ������ ����Ʈ

            for (int y = 0; y < 15; y++) // ��� ���� Ž��
            {
                path.Add(gridPositions[currentX, y]); // ���� ��ġ�� ��ο� �߰�

                // ���� ���� x ��ġ�� ����
                List<int> possibleX = new List<int>();
                if (currentX > 0) possibleX.Add(currentX - 1);
                possibleX.Add(currentX);
                if (currentX < 6) possibleX.Add(currentX + 1);

                currentX = possibleX[Random.Range(0, possibleX.Count)]; // ���� x ��ġ ���� ����
            }

            paths.Add(path); // ������ ��θ� ��� ����Ʈ�� �߰�
            pathColors.Add(Random.ColorHSV());
        }
    }

    private void OnDrawGizmos()
    {
        if (paths.Count > 0)
        {
            for(int pathIndex = 0; pathIndex < paths.Count; pathIndex++)
            {
                var path = paths[pathIndex];
                Gizmos.color = pathColors[pathIndex];
                for(int i = 0; i < path.Count - 1; i++)
                {
                    Gizmos.DrawLine(path[i], path[i + 1]);
                }
            }
        }
    }
}