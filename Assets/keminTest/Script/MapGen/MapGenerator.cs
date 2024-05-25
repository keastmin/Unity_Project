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

    // ���� ��带 �ȳ��ϴ� �Ǽ��� ǥ���ϱ� ���� ����
    List<List<int>> paths = new List<List<int>>();
    public GameObject linePrefab;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();           // �׸��� ����
        GeneratePaths();        // ��� ����
        pathLine();             // �Ǽ� �׸���
        CreateButton();         // ��ư ����
        consecutiveEdit();      // ��ư ����
        FirstFloorEnable();     // ù��° �� Ȱ��ȭ
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

    // ���� ��� Ȱ��ȭ �� �� ���� ��� ��Ȱ��ȭ
    public void OnNodeCleared(int x, int y)
    {
        Debug.Log("���� ����� ���� = " + nodeGrid[x, y].nextButton.Count);

        // ��� ��� ��Ȱ��ȭ
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

        // ���� ��ġ�ϴ� �ε����� ��带 Ŭ���� �� ���� �ΰ� �� ����� ���� ��ο� �����ϴ� ���� Ȱ��ȭ
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
                    Debug.Log((nextNode.x) + " " + (nextNode.y) + "���� ��� Ȱ��ȭ �۵� �Ϸ�");
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
                // ���� ��η� ������ ���� ���� X �ε����� ������ ����Ʈ
                List<int> possibleX = new List<int>();

                // �ر�Ģ: ��γ����� X�ڷ� �����Ǿ�� �ȵȴ�. �װ��� ���� if��

                // ���� �ε��� X�� 0���� Ŭ ��, ���� �ε������� ���� ���� ��θ� ������ �� �ִ��� �˻��ϴ� ���ǹ�
                if (currentX > 0)
                {
                    // ���纸�� ������ ��尡 ��ημ� ���õ� ���� �ִ� ����� ���
                    if (nodeGrid[currentX - 1, y].isSelected)
                    {
                        bool check = true;

                        // ������ ���� ���� ���õ� ����� ���� ��ε��� X�ڷ� ������ �� �ִ��� �˻�
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

                // ���� �ε������� �ٷ� ���� X�ڷ� ������ ���� �����Ƿ� ������ ����
                possibleX.Add(currentX);

                // ���� �ε��� X�� 6���� ���� ��, ���� �ε������� ������ ���� ��θ� ������ �� �ִ��� �˻��ϴ� ���ǹ�
                if (currentX < 6)
                {
                    // ���纸�� �������� ��尡 ��ημ� ���õ� ���� �ִ� ����� ���
                    if (nodeGrid[currentX + 1, y].isSelected)
                    {
                        bool check = true;

                        // �������� ���� ���� ���õ� ����� ���� ��ε��� X�ڷ� ������ �� �ִ��� �˻�
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
        // ���������� ���������� �ٷ� �� ������ ��ȸ
        for(int y = 0; y < row - 1; y++)
        {
            // ���� ������ �� �ܰ� ���� ������ ��Ģ�� �����ϴ� ���� �����Ѵٸ� �ش� ������ ����ִ� Ÿ���� �����ϰ� �����
            List<int>[] changeNode = new List<int>[7];
            
            
            // �� �ܰ� ���� ���� ���� �������� ���̱� ������ ��Ģ�� �����ִ� ������ �� �ܰ� ���� ���� �������� ���ؾ� �� Ÿ�� ����
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

            // ���������� �� ���� ���� ��ȸ
            for (int x = 0; x < col; x++)
            {
                // ���� �ε����� ��尡 ���õ� �� �ִ� ���̶�� ����
                if (nodeGrid[x, y].isSelected)
                {
                    // ���� ���õ� ���� Ÿ��
                    Node curr = nodeGrid[x, y];
                    int currType = curr.stageType;
                    
                    // ��Ģ���� �ߺ��Ǹ� �� �Ǵ� ���
                    if(currType == 1 || currType == 3 || currType == 4)
                    {
                        // ���� ��忡�� ���� ��ο� �����ϴ� ���� ��ȸ
                        foreach(var nextNode in curr.nextButton)
                        {
                            // ���� ���� ��� ���� ��ο� �����ϴ� ���� Ÿ���� ���� ��� ����
                            if(nextNode.stageType == currType)
                            {
                                int nx = nextNode.x;
                                int nt = nextNode.stageType;

                                // �̹� �ش� �ε����� ���ؾ��� ���� ����ִٸ� ����
                                if (!changeNode[nx].Contains(nt))
                                {
                                    changeNode[nx].Add(nt);
                                }
                            }
                        }
                    }
                }
            }

            // ���� ���� �ߺ��Ǵ� ���� ��� ���� �����ϸ� ���� ����
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
                            Debug.Log("�����");
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