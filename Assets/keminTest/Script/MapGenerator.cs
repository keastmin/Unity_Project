using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MApGenerator : MonoBehaviour
{
    // �� ���� Ȱ��ȭ �� ���
    public class Node
    {
        // ��ġ
        public Vector2 position;
        // �ε���
        public int x;
        public int y;
        // �������� Ÿ��
        public int stageType;
        // ���� ����
        public bool isSelected;
        // ���� ���� ����
        public bool isEnable;
        // Ŭ���� ����
        public bool isClear;
        // ��ư ������
        public GameObject buttonPrefab;
        // ���� ��� ��� ����Ʈ
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

    // �� ���������� �ش��ϴ� ��ư ������
    // 0: �Ϲ� ����, 1: �޽�, 2: �̺�Ʈ, 3: ����Ʈ ����, 4: ����, 5: ���� ����
    public GameObject[] buttons = new GameObject[6];

    // �׸����� ��ġ�� ����̵� �ǳ�
    public Transform gridPanel;
    
    // ��ο� ���� ����
    private Vector2[,] gridPositions = new Vector2[7, 15];
    private Node[,] nodeGrid = new Node[7, 15];

    // �׸��� ����
    private int col = 7;  // ���� ��
    private int row = 15; // ���� ��

    // ����� ������� ���� ����
    List<List<int>> paths = new List<List<int>>();
    HashSet<Color> usedColors = new HashSet<Color>();
    private List<Color> pathColors = new List<Color>();
    public GameObject linePrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
        GeneratePaths();
        pathLine();
        CreateButton();
        FirstFloorEnable();
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
        float panelWidth = gridRectTransform.rect.width;    // �ǳ��� �ʺ�
        float panelHeight = gridRectTransform.rect.height;  // �ǳ��� ����

        float spacingX = panelWidth / (col + 1);  // �� ���� ����
        float spacingY = 150; // �� ���� ����

        // �׸����� ���� ��ġ�� ��� (���� �ϴ� �𼭸� ����)
        float startX = spacingX;
        float startY = panelHeight / (row + 1);

        for (int x = 0; x < col; x++)
        {
            for (int y = 0; y < row; y++)
            {
                // �� ���� ��ġ�� ���
                float posX = startX + x * spacingX;
                float posY = startY + y * spacingY;

                // �� ��ġ�� �迭�� ����
                Vector2 position = new Vector2(posX, posY);
                gridPositions[x, y] = position;
                nodeGrid[x, y] = new Node(position, x, y);
            }
        }
    }

    void GeneratePaths()
    {
        // 1��° x�� ����
        int firstX = 0;

        for (int pathNum = 0; pathNum < 6; pathNum++)
        {
            List<int> tmp = new List<int>();
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

            nodeGrid[currentX, 0].isSelected = true;
            tmp.Add(currentX);

            for (int y = 0; y < row - 1; y++) // ��� ���� Ž��
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
        
            // ����� ���� ��� �����, ��θ��� ������ �ٸ��� �ο�
            Color newColor;
            do
            {
                newColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
            } while (usedColors.Contains(newColor));
            usedColors.Add(newColor);
            pathColors.Add(newColor);
            paths.Add(tmp);
        }
    }

    // Ȯ���� ��ư ����
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

                    // float lineLength = linePrefab.transform.localScale.y;

                    for(int k = 0; k <= lineCount; k++)
                    {
                        Vector2 position = startPos + direction * (lineSpacing * k);
                        GameObject line = Instantiate(linePrefab, new Vector3(position.x, position.y, 0), rotation, gridPanel);
                    }
                }
            }
        }
    }

    // ����� ���� ��� �����
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