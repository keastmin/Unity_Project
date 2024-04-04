using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MApGenerator : MonoBehaviour
{
    // 각 층에 활성화 될 노드
    public class Node
    {
        public Vector2 position;
        public int x;
        public int y;
        public int stageType;
        public bool isSelected;
        public bool isEnable;
        public bool isClear;
        public GameObject buttonPrefab;
        public List<Node> nextButton;

        public Node(Vector2 pos, int width, int height)
        {
            position = pos;
            x = width;
            y = height;
            isSelected = false;
            isEnable = false;
            isClear = false;
            nextButton = new List<Node>();
        }
    }

    // 각 스테이지에 해당하는 버튼 프리팹
    // 0: 일반 전투, 1: 휴식, 2: 이벤트, 3: 엘리트 전투, 4: 상점, 5: 보물 상자
    public GameObject[] buttons = new GameObject[6];

    // 그리드의 위치의 기반이될 판넬
    public Transform gridPanel;
    public Transform viewContent;
    
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
        FirstFloorEnable();
        //ScrollTest();
    }

    void ScrollTest()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject newButton = Instantiate(buttons[0], viewContent);
            Vector2 position = new Vector2(i, 100 * i);
            newButton.transform.position = position;
        }
    }

    void FirstFloorEnable()
    {
        for(int x = 0; x < col; x++)
        {
            for (int y = 0; y < row; y++)
            {
                if (nodeGrid[x, y].isSelected)
                {
                    StageBaseButton buttonComponent = nodeGrid[x, y].buttonPrefab.GetComponent<StageBaseButton>();

                    if (buttonComponent != null)
                    {
                        if (y == 0)
                        {
                            buttonComponent.stageEnable = true;
                            buttonComponent.SetStageEnable(true);
                        }
                        else
                        {
                            buttonComponent.stageEnable = false;
                            buttonComponent.SetStageEnable(false);
                        }
                    }
                }
            }
        }
    }

    public void OnNodeCleared(int x, int y)
    {
        foreach(Node node in nodeGrid)
        {
            if(node.buttonPrefab != null)
            {
                StageBaseButton buttonComponent = node.buttonPrefab.GetComponent<StageBaseButton>();
                if(buttonComponent != null)
                {
                    buttonComponent.stageEnable = false;
                    buttonComponent.SetStageEnable(false);
                }
            }
        }

        Node clearedNode = nodeGrid[x, y];
        foreach(Node nextNode in clearedNode.nextButton)
        {
            if(nextNode.buttonPrefab != null)
            {
                StageBaseButton nextButtonComponent = nextNode.buttonPrefab.GetComponent<StageBaseButton>();
                if(nextButtonComponent != null)
                {
                    nextButtonComponent.stageEnable = true;
                    nextButtonComponent.SetStageEnable(true);
                }
            }
        }
    }

    void CreateGrid()
    {
        RectTransform gridRectTransform = gridPanel.GetComponent<RectTransform>();
        // RectTransform gridRectTransform = viewContent.GetComponent<RectTransform>();
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
                nodeGrid[x, y] = new Node(position, x, y);
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
                if (!nodeGrid[prevX, y].nextButton.Contains(nodeGrid[currentX, y + 1]))
                {
                    nodeGrid[prevX, y].nextButton.Add(nodeGrid[currentX, y + 1]);
                    nodeGrid[currentX, y + 1].isSelected = true;
                }
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
        for (int i = 0; i < col; i++)
        {
            if (nodeGrid[i, 0].isSelected)
            {
                Queue<Node> nodeQ = new Queue<Node>();
                nodeQ.Enqueue(nodeGrid[i, 0]);

                while (nodeQ.Count > 0)
                {
                    Node node = nodeQ.Dequeue();
                    int x = node.x;
                    int y = node.y;

                    if (nodeGrid[x, y].buttonPrefab != null) continue;
                    int type = chooseType(y);

                    nodeGrid[x, y].stageType = type;
                    nodeGrid[x, y].buttonPrefab = Instantiate(buttons[type], gridPanel);
                    //nodeGrid[x, y].buttonPrefab = Instantiate(buttons[type], viewContent);
                    nodeGrid[x, y].buttonPrefab.transform.position = nodeGrid[x, y].position;
                    StageBaseButton buttonComponent = nodeGrid[x, y].buttonPrefab.GetComponent<StageBaseButton>();
                    if(buttonComponent != null)
                    {
                        buttonComponent.SetMapGenerator(x, y, this);
                    }

                    foreach (var nextNode in node.nextButton)
                    {
                        nodeQ.Enqueue(nextNode);
                    }
                }
            }
        }
    }

    private int chooseType(int floor)
    {
        if(floor == 8)
        {
            return 5;
        }
        else if( floor == 14)
        {
            return 1;
        }
        else if(floor > 0)
        {
            int rand = Random.Range(0, 100);

            if(rand < 5)
            {
                return 4;
            }
            else if(rand < 17)
            {
                return 1;
            }
            else if(rand < 33)
            {
                return 3;
            }
            else if(rand < 55)
            {
                return 2;
            }
        }
        
        return 0;
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