using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MApGenerator : MonoBehaviour
{
    // 각 층에 활성화 될 노드
    public class Node
    {
        public Vector2 position;
        public int stageType;
        public bool isSelected;
        public GameObject buttonPrefab;
        public List<Node> nextButton;
    }

    // 각 스테이지에 해당하는 프리팹
    public GameObject normalStage;
    public GameObject eliteStage;
    public GameObject restStage;
    public GameObject eventStage;
    public GameObject merchantStage;
    public GameObject treasureStage;

    // 그리드의 위치의 기반이될 판넬
    public Transform gridPanel;
    private Vector2[,] gridPositions = new Vector2[7, 15];
    private List<List<Vector2>> paths = new List<List<Vector2>>();

    // 그리드 정보
    private int col = 7;  // 열의 수
    private int row = 15; // 행의 수

    // 경로와 각 방에 해당하는 노드 연결 변수
    List<int>[] posX = new List<int>[15];
    HashSet<int>[] usedPosX = new HashSet<int>[15];

    // 기즈모 디버깅을 위한 변수
    HashSet<Color> usedColors = new HashSet<Color>();
    private List<Color> pathColors = new List<Color>();

    // Test
    private GameObject[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
        GeneratePaths();
    }

    void CreateGrid()
    {
        RectTransform gridRectTransform = gridPanel.GetComponent<RectTransform>();
        float panelWidth = gridRectTransform.rect.width;    // 판넬의 너비
        float panelHeight = gridRectTransform.rect.height;  // 판넬의 높이

        float spacingX = panelWidth / col;  // 열 간의 간격
        float spacingY = 120; // 행 간의 간격

        // 그리드의 시작 위치를 계산 (왼쪽 하단 모서리 기준)
        float startX = spacingX;
        float startY = 20;

        for (int x = 0; x < col; x++)
        {
            for (int y = 0; y < row; y++)
            {
                // 각 셀의 위치를 계산
                float posX = startX + x * spacingX;
                float posY = startY + y * spacingY;

                // 셀 위치를 배열에 저장
                gridPositions[x, y] = new Vector2(posX, posY);
            }
        }
    }

    void InitPosXList()
    {
        for(int i = 0; i < 15; i++)
        {
            usedPosX[i] = new HashSet<int>();
            posX[i] = new List<int>();
        }
    }

    void GeneratePaths()
    {
        InitPosXList();

        // 1번째 x값 저장
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

                if (!usedPosX[y].Contains(currentX))
                {
                    usedPosX[y].Add(currentX);
                    posX[y].Add(currentX);
                }

                // 다음 층의 x 위치를 결정
                List<int> possibleX = new List<int>();
                if (currentX > 0) possibleX.Add(currentX - 1);
                possibleX.Add(currentX);
                if (currentX < 6) possibleX.Add(currentX + 1);

                currentX = possibleX[Random.Range(0, possibleX.Count)]; // 다음 x 위치 랜덤 선택
            }

            paths.Add(path); // 생성된 경로를 경로 리스트에 추가

            // 기즈모를 통한 경로 디버깅, 경로마다 색상을 다르게 부여
            Color newColor;
            do
            {
                newColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
            } while (usedColors.Contains(newColor));
            usedColors.Add(newColor);
            pathColors.Add(newColor);
        }

        // 버튼 생성
        // CreateButton();
    }

    // 확률적 버튼 생성
    void CreateButton()
    {
        for(int i = 0; i < 15; i++)
        {
            for (int j = 0; j < posX[i].Count; j++) {
                Vector2 buttonPos = gridPositions[posX[i][j], i];
                GameObject randomButton;

                if (i == 0)
                {
                    randomButton = normalStage;
                }
                else if (i == 8)
                {
                    randomButton = treasureStage;
                }
                else if (i == 14)
                {
                    randomButton = restStage;
                }
                else
                {
                    int rand = Random.Range(0, 100);

                    if (rand < 45)
                    {
                        randomButton = normalStage;
                    }
                    else if (rand < 67)
                    {
                        randomButton = eventStage;
                    }
                    else if (rand < 83)
                    {
                        randomButton = eliteStage;
                    }
                    else if (rand < 95)
                    {
                        randomButton = restStage;
                    }
                    else
                    {
                        randomButton = merchantStage;
                    }
                }

                GameObject buttonObj = Instantiate(randomButton, gridPanel);
                buttonObj.transform.position = buttonPos;
            }
        }
    }

    // 기즈모를 통한 경로 디버그
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