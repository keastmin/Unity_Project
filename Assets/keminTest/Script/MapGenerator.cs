using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MApGenerator : MonoBehaviour
{
    // 버튼 프리팹
    public GameObject buttonPrefab;

    public Transform gridPanel;
    private Vector2[,] gridPositions = new Vector2[7, 15];
    private List<List<Vector2>> paths = new List<List<Vector2>>();

    // 경로와 각 방에 해당하는 노드 연결 변수
    List<int>[] posX = new List<int>[15];
    HashSet<int>[] usedPosX = new HashSet<int>[15];

    // 기즈모 디버깅을 위한 변수
    HashSet<Color> usedColors = new HashSet<Color>();
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

    void InitPosXList()
    {
        Debug.Log(usedPosX.Length);
        for(int i = 0; i < 15; i++)
        {
            usedPosX[i] = new HashSet<int>();
            posX[i] = new List<int>();
        }
    }

    void GeneratePaths()
    {
        InitPosXList();

        int firstX = 0;

        for (int pathNum = 0; pathNum < 6; pathNum++)
        {
            int currentX = Random.Range(0, 7); // 시작 x 위치 랜덤 선택
            
            // 최소한 2개의 서로 다른 시작 지점 보장
            if (pathNum == 0) firstX = currentX;
            else if(pathNum == 1)
            {
                while(currentX == firstX)
                {
                    currentX = Random.Range(0, 7);
                }
            }

            List<Vector2> path = new List<Vector2>(); // 이 경로의 위치를 저장할 리스트

            for (int y = 0; y < 15; y++) // 모든 층을 탐색
            {
                path.Add(gridPositions[currentX, y]); // 현재 위치를 경로에 추가

                // 다음 층의 x 위치를 결정
                List<int> possibleX = new List<int>();
                if (currentX > 0) possibleX.Add(currentX - 1);
                possibleX.Add(currentX);
                if (currentX < 6) possibleX.Add(currentX + 1);

                currentX = possibleX[Random.Range(0, possibleX.Count)]; // 다음 x 위치 랜덤 선택

                if (!usedPosX[y].Contains(currentX))
                {
                    usedPosX[y].Add(currentX);
                    posX[y].Add(currentX);
                }
            }

            paths.Add(path); // 생성된 경로를 경로 리스트에 추가

            Color newColor;
            do
            {
                newColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
            } while (usedColors.Contains(newColor));
            usedColors.Add(newColor);
            pathColors.Add(newColor);
        }

        for(int i = 0; i < 15; i++)
        {
            for(int j = 0; j < posX[i].Count; j++)
            {
                Debug.Log((i) + " " + (posX[i][j]));
            }
        }

        CreateButton();
    }

    void CreateButton()
    {
        for(int i = 0; i < 15; i++)
        {
            for (int j = 0; j < posX[i].Count; j++) {
                Vector2 buttonPos = gridPositions[posX[i][j], i];
                GameObject buttonObj = Instantiate(buttonPrefab, gridPanel);
                buttonObj.transform.localPosition = buttonPos;
            }
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