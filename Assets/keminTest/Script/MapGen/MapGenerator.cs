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
        // 위치
        public Vector2 position; 
        // 인덱스
        public int x;
        public int y;
        // 스테이지 타입
        public int stageType;
        // 선택 여부
        public bool isSelected;
        // 선택 가능 여부
        public bool isEnable;
        // 클리어 여부
        public bool isClear;
        // 버튼 프리팹
        public GameObject buttonPrefab;
        // 다음 경로 노드 리스트
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
    
    // 경로에 대한 변수
    private Vector2[,] gridPositions = new Vector2[7, 15];
    private Node[,] nodeGrid = new Node[7, 15];

    // 그리드 정보
    private int col = 7;  // 열의 수
    private int row = 15; // 행의 수

    // 다음 노드를 안내하는 실선을 표현하기 위한 변수
    List<List<int>> paths = new List<List<int>>();
    public GameObject linePrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();           // 그리드 생성
        GeneratePaths();        // 경로 생성
        pathLine();             // 실선 그리기
        CreateButton();         // 버튼 생성
        consecutiveEdit();      // 버튼 수정
        FirstFloorEnable();     // 첫번째 층 활성화
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

    // 다음 노드 활성화 및 그 외의 노드 비활성화
    public void OnNodeCleared(int x, int y)
    {
        Debug.Log("다음 경로의 수는 = " + nodeGrid[x, y].nextButton.Count);

        // 모든 노드 비활성화
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

        // 현재 위치하는 인덱스의 노드를 클리어 한 노드로 두고 그 노드의 다음 경로에 존재하는 노드들 활성화
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
                    Debug.Log((nextNode.x) + " " + (nextNode.y) + "현재 노드 활성화 작동 완료");
                }
            }
        } 
    }

    void CreateGrid()
    {
        RectTransform gridRectTransform = gridPanel.GetComponent<RectTransform>();
        float panelWidth = gridRectTransform.rect.width;    // 판넬의 너비
        float panelHeight = gridRectTransform.rect.height;  // 판넬의 높이

        float spacingX = panelWidth / (col + 1);  // 열 간의 간격
        float spacingY = 150; // 행 간의 간격

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
                // 다음 경로로 가능한 다음 층의 X 인덱스를 저장할 리스트
                List<int> possibleX = new List<int>();

                // ※규칙: 경로끼리는 X자로 교차되어서는 안된다. 그것을 위한 if문

                // 현재 인덱스 X가 0보다 클 때, 현재 인덱스에서 왼쪽 위의 경로를 선택할 수 있는지 검사하는 조건문
                if (currentX > 0)
                {
                    // 현재보다 왼쪽의 노드가 경로로서 선택된 적이 있는 노드일 경우
                    if (nodeGrid[currentX - 1, y].isSelected)
                    {
                        bool check = true;

                        // 왼쪽의 노드와 현재 선택된 노드의 다음 경로들이 X자로 교차될 수 있는지 검사
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

                // 현재 인덱스에서 바로 위는 X자로 교차될 일이 없으므로 무조건 선택
                possibleX.Add(currentX);

                // 현재 인덱스 X가 6보다 작을 때, 현재 인덱스에서 오른쪽 위의 경로를 선택할 수 있는지 검사하는 조건문
                if (currentX < 6)
                {
                    // 현재보다 오른쪽의 노드가 경로로서 선택된 적이 있는 노드일 경우
                    if (nodeGrid[currentX + 1, y].isSelected)
                    {
                        bool check = true;

                        // 오른쪽의 노드와 현재 선택된 노드의 다음 경로들이 X자로 교차될 수 있는지 검사
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

                // 
                int prevX = currentX;
                currentX = possibleX[Random.Range(0, possibleX.Count)];
                if (!nodeGrid[prevX, y].nextButton.Contains(nodeGrid[currentX, y + 1]))
                {
                    nodeGrid[prevX, y].nextButton.Add(nodeGrid[currentX, y + 1]);
                    nodeGrid[currentX, y + 1].isSelected = true;
                }
                tmp.Add(currentX);
            }
        
            paths.Add(tmp);
        }
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
                    nodeGrid[x, y].buttonPrefab.transform.position = nodeGrid[x, y].position;
                    StageBaseButton buttonComponent = nodeGrid[x, y].buttonPrefab.GetComponent<StageBaseButton>();
                    if (buttonComponent != null)
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

    private void consecutiveEdit()
    {
        // 최하층부터 마지막에서 바로 전 층까지 순회
        for(int y = 0; y < row - 1; y++)
        {
            // 현재 층보다 한 단계 높은 층에서 규칙을 위반하는 방이 존재한다면 해당 변수에 들어있는 타입을 제외하고 재생성
            List<int>[] changeNode = new List<int>[7];
            
            
            // 한 단계 높은 층에 대해 변경해줄 것이기 때문에 규칙에 쓰여있는 층보다 한 단계 낮은 층을 기준으로 피해야 할 타입 저장
            for(int x = 0; x < col; x++)
            {
                changeNode[x] = new List<int>();
                if(y < 4 || y == 12)
                {
                    changeNode[x].Add(1);
                }
                if(y < 4)
                {
                    changeNode[x].Add(3);
                }
            }

            // 본격적으로 각 층의 방을 순회
            for (int x = 0; x < col; x++)
            {
                // 현재 인덱스의 노드가 선택된 적 있는 방이라면 진입
                if (nodeGrid[x, y].isSelected)
                {
                    // 현재 선택된 노드와 타입
                    Node curr = nodeGrid[x, y];
                    int currType = curr.stageType;
                    
                    // 규칙에서 중복되면 안 되는 방들
                    if(currType == 1 || currType == 3 || currType == 4)
                    {
                        // 현재 노드에서 다음 경로에 존재하는 방을 순회
                        foreach(var nextNode in curr.nextButton)
                        {
                            // 만약 현재 방과 다음 경로에 존재하는 방의 타입이 같을 경우 진입
                            if(nextNode.stageType == currType)
                            {
                                int nx = nextNode.x;
                                int nt = nextNode.stageType;

                                // 이미 해당 인덱스에 피해야할 값이 들어있다면 무시
                                if (!changeNode[nx].Contains(nt))
                                {
                                    changeNode[nx].Add(nt);
                                }
                            }
                        }
                    }
                }
            }

            // 현재 방들과 중복되는 다음 경로 방이 존재하면 변경 시작
            for(int x = 0; x < col; x++)
            {
                if (nodeGrid[x, y + 1].isSelected)
                {
                    Node nextNode = nodeGrid[x, y + 1];
                    int newType = nextNode.stageType;
                    if (changeNode[x].Contains(newType))
                    {
                        Destroy(nextNode.buttonPrefab);
                        nextNode.buttonPrefab = null;

                        while (changeNode[x].Contains(newType))
                        {
                            Debug.Log("변경됨");
                            newType = chooseType(y + 1);
                        }

                        nextNode.stageType = newType;
                        nextNode.buttonPrefab = Instantiate(buttons[newType], gridPanel);
                        nextNode.buttonPrefab.transform.position = nextNode.position;
                        StageBaseButton buttonComponent = nextNode.buttonPrefab.GetComponent<StageBaseButton>();
                        if (buttonComponent != null)
                        {
                            buttonComponent.SetMapGenerator(x, y + 1, this);
                        }
                    }
                }
            }
        }
    }

    private void pathLine()
    {
        float lineSpacing = 25.0f;
        if(paths.Count > 0)
        {
            for(int i = 0; i < paths.Count; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    int sX = paths[i][j];
                    int fX = paths[i][j + 1];

                    Vector2 startPos = new Vector2(gridPositions[sX, j].x, gridPositions[sX, j].y);
                    Vector2 finishPos = new Vector2(gridPositions[fX, j + 1].x, gridPositions[fX, j + 1].y);

                    float distance = Vector2.Distance(startPos, finishPos);
                    Vector2 direction = (finishPos - startPos).normalized;
                    float angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

                    int lineCount = Mathf.FloorToInt(distance / lineSpacing);

                    for(int k = 0; k <= lineCount; k++)
                    {
                        Vector2 position = startPos + direction * (lineSpacing * k);
                        Instantiate(linePrefab, new Vector3(position.x, position.y, 0), rotation, gridPanel);
                    }
                }
            }
        }
    }
}