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
        public bool isEnable;
        public bool isClear;
        public GameObject buttonPrefab;
        public List<Node> nextButton;

        public Node(Vector2 pos)
        {
            position = pos;
            isSelected = false;
            isEnable = false;
            isClear = false;
            nextButton = new List<Node>();
        }
    }

    // 각 스테이지에 해당하는 버튼 프리팹
    public GameObject normalStage;
    public GameObject eliteStage;
    public GameObject restStage;
    public GameObject eventStage;
    public GameObject merchantStage;
    public GameObject treasureStage;

    // 그리드의 위치의 기반이될 판넬
    public Transform gridPanel;
    
    // 경로에 대한 변수
    private Vector2[,] gridPositions = new Vector2[7, 15];
    private Node[,] nodeGrid = new Node[7, 15];

    // 그리드 정보
    private int col = 7;  // 열의 수
    private int row = 15; // 행의 수

    // 기즈모 디버깅을 위한 변수
    List<List<int>> paths = new List<List<int>>();
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
        RectTransform gridRectTransform = gridPanel.GetComponent<RectTransform>();
        float panelWidth = gridRectTransform.rect.width;    // 판넬의 너비
        float panelHeight = gridRectTransform.rect.height;  // 판넬의 높이

        float spacingX = panelWidth / (col + 1);  // 열 간의 간격
        float spacingY = 120; // 행 간의 간격

        // 그리드의 시작 위치를 계산 (왼쪽 하단 모서리 기준)
        float startX = spacingX;
        float startY = panelHeight / (row + 1);

        for (int x = 0; x < col; x++)
        {
            for (int y = 0; y < row; y++)
            {
                // 각 셀의 위치를 계산
                float posX = startX + x * spacingX;
                float posY = startY + y * spacingY;

                // 셀 위치를 배열에 저장
                Vector2 position = new Vector2(posX, posY);
                gridPositions[x, y] = position;
                nodeGrid[x, y] = new Node(position);
            }
        }
    }

    void GeneratePaths()
    {
        // 1번째 x값 저장
        int firstX = 0;

        for (int pathNum = 0; pathNum < 6; pathNum++)
        {
            List<int> tmp = new List<int>();
            int currentX = Random.Range(0, 7); // 시작 x 위치 랜덤 선택

            // 최소한 2개의 서로 다른 시작 지점 보장
            if (pathNum == 0) firstX = currentX;
            else if (pathNum == 1)
            {
                while (currentX == firstX)
                {
                    currentX = Random.Range(0, 7);
                }
            }

            nodeGrid[currentX, 0].isSelected = true;
            tmp.Add(currentX);

            for (int y = 0; y < row - 1; y++) // 모든 층을 탐색
            {
                List<int> possibleX = new List<int>();
                if (currentX > 0)
                {
                    if (nodeGrid[currentX - 1, y].isSelected)
                    {
                        bool check = true;

                        for (int k = 0; k < nodeGrid[currentX - 1, y].nextButton.Count; k++)
                        {
                            if (nodeGrid[currentX - 1, y].nextButton[k].position == nodeGrid[currentX, y + 1].position)
                            {
                                check = false;
                                break;
                            }
                        }

                        if (check)
                        {
                            possibleX.Add(currentX - 1);
                        }
                    }
                    else
                    {
                        possibleX.Add(currentX - 1);
                    }
                }
                possibleX.Add(currentX);
                if (currentX < 6)
                {
                    if (nodeGrid[currentX + 1, y].isSelected)
                    {
                        bool check = true;

                        for (int k = 0; k < nodeGrid[currentX + 1, y].nextButton.Count; k++)
                        {
                            if (nodeGrid[currentX + 1, y].nextButton[k].position == nodeGrid[currentX, y + 1].position)
                            {
                                check = false;
                                break;
                            }
                        }

                        if (check)
                        {
                            possibleX.Add(currentX + 1);
                        }
                    }
                    else
                    {
                        possibleX.Add(currentX + 1);
                    }
                }

                int prevX = currentX;
                currentX = possibleX[Random.Range(0, possibleX.Count)];
                nodeGrid[prevX, y].nextButton.Add(nodeGrid[currentX, y + 1]);
                nodeGrid[currentX, y + 1].isSelected = true;
                tmp.Add(currentX);
            }
        
            // 기즈모를 통한 경로 디버깅, 경로마다 색상을 다르게 부여
            Color newColor;
            do
            {
                newColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
            } while (usedColors.Contains(newColor));
            usedColors.Add(newColor);
            pathColors.Add(newColor);
            paths.Add(tmp);
        }

        // 버튼 생성
        CreateButton();
    }

    // 확률적 버튼 생성
    void CreateButton()
    {
        for(int i = 0; i < col; i++)
        {
            for(int j = 0; j < row; j++)
            {
                if (nodeGrid[i, j].isSelected)
                {
                    Vector2 buttonPos = nodeGrid[i, j].position;
                    GameObject buttonObj = Instantiate(normalStage, gridPanel);
                    buttonObj.transform.position = buttonPos;
                }
            }
        }

        //for(int i = 0; i < 15; i++)
        //{
        //    for (int j = 0; j < posX[i].Count; j++) {
        //        Vector2 buttonPos = gridPositions[posX[i][j], i];
        //        GameObject randomButton;

        //        if (i == 0)
        //        {
        //            randomButton = normalStage;
        //        }
        //        else if (i == 8)
        //        {
        //            randomButton = treasureStage;
        //        }
        //        else if (i == 14)
        //        {
        //            randomButton = restStage;
        //        }
        //        else
        //        {
        //            int rand = Random.Range(0, 100);

        //            if (rand < 45)
        //            {
        //                randomButton = normalStage;
        //            }
        //            else if (rand < 67)
        //            {
        //                randomButton = eventStage;
        //            }
        //            else if (rand < 83)
        //            {
        //                randomButton = eliteStage;
        //            }
        //            else if (rand < 95)
        //            {
        //                randomButton = restStage;
        //            }
        //            else
        //            {
        //                randomButton = merchantStage;
        //            }
        //        }

        //        GameObject buttonObj = Instantiate(randomButton, gridPanel);
        //        buttonObj.transform.position = buttonPos;
        //    }
        //}
    }

    // 기즈모를 통한 경로 디버그
    private void OnDrawGizmos()
    {
        if (paths.Count > 0)
        {
            for (int i = 0; i < paths.Count; i++)
            {
                Gizmos.color = pathColors[i];

                for (int j = 0; j < 14; j++)
                {
                    int fX = paths[i][j];
                    int sX = paths[i][j + 1];

                    Gizmos.DrawLine(gridPositions[fX, j], gridPositions[sX, j + 1]);
                }
            }
        }
    }
}