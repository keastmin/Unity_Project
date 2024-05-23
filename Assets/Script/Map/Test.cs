using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Node
{
    public Vector2 position;
    public int x;
    public int y;
    public int stageType;
    public bool isPathNode;
    public GameObject buttonPrefab;
    public List<Node> nextButton;

    public Node(Vector2 pos, int width, int height)
    {
        position = pos;
        x = width;
        y = height;
        isPathNode = false; // �ʱⰪ ����
        nextButton = new List<Node>();
    }
}



public class Test : MonoBehaviour
{
    public GameObject buttonPrefab;
    public RectTransform panel;
    //private List<List<Vector2>> paths = new List<List<Vector2>>();
    //MapGenerator mapGenerator;

    Vector2[,] grid = new Vector2[7, 15];
    GameObject[,] buttons = new GameObject[7, 15];
    List<List<int>> paths = new List<List<int>>();
    Node[,] nodeGrid = new Node[7, 15];



    int col = 7;
    int row = 15;
    //List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
        GenerateGrid();
        CreatePath();
        CreateButtons();

    }

    void GenerateGrid()
    {
        RectTransform gridRectTransform = panel.GetComponent<RectTransform>();
        float panelWidth = gridRectTransform.rect.width;    // �ǳ��� �ʺ�
        float panelHeight = gridRectTransform.rect.height;  // �ǳ��� ����

        float spacingX = panelWidth / (col + 1);  // �� ���� ����
        float spacingY = 120; // �� ���� ����

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
                grid[x, y] = position;
                nodeGrid[x, y] = new Node(position, x, y);
            }
        }
    }

    void CreateButtons()
    {
        for (int i = 0; i < col; i++)
        {
            //ù ��° ��
            if (nodeGrid[i, 0].isPathNode)
            {
                Queue<Node> nodeQ = new Queue<Node>();
                nodeQ.Enqueue(nodeGrid[i, 0]);

                while (nodeQ.Count > 0)
                {
                    Node node = nodeQ.Dequeue();
                    int x = node.x;
                    int y = node.y;

                    if (nodeGrid[x, y].buttonPrefab != null) continue;

                    nodeGrid[x, y].buttonPrefab = Instantiate(buttonPrefab, panel);
                    nodeGrid[x, y].buttonPrefab.transform.position = nodeGrid[x, y].position;
                    StageBaseButton buttonComponent = nodeGrid[x, y].buttonPrefab.GetComponent<StageBaseButton>();

                    if (buttonComponent != null)
                    {
                        //buttonComponent.SetMapGenerator(x, y, mapGenerator);
                    }

                    foreach (var nextNode in node.nextButton)
                    {
                        nodeQ.Enqueue(nextNode);
                    }
                }
            }
        }
    }



    void CreatePath()
    {
        List<int> tmppathX = new List<int>();
        List<int> changeX = new List<int>();
        List<int> possibleX = new List<int>();
        bool check;
        for (int num = 0; num < 6; num++)
        {
            int firstX = 0;
            int currentX = Random.Range(0, 7);

            if (num == 0)
                firstX = currentX;
            if (num == 1)
            {
                while (firstX == currentX)
                {
                    currentX = Random.Range(0, 7);
                }
            }

            //���� ��ư�� ���� �ִ´�
            nodeGrid[currentX, 0].isPathNode = true;
            tmppathX.Add(currentX);

            for (int y = 0; y < row - 1; y++)
            {

                if (currentX > 0)
                {

                    if (nodeGrid[currentX - 1, y].isPathNode)//�н��� �ִٸ�
                    {
                        check = true;

                        for (int k = 0; k < nodeGrid[currentX - 1, y].nextButton.Count; k++)
                        {
                            if (nodeGrid[currentX - 1, y].nextButton[k] == nodeGrid[currentX, y + 1])
                            {
                                check = false;
                                changeX.Add(currentX - 1);
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

                //� ���ǿ��� �ɸ��� ����
                possibleX.Add(currentX);

                if (currentX < 6)
                {
                    if (nodeGrid[currentX + 1, y].isPathNode)
                    {
                        check = true;

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
                int prevX = currentX;
                currentX = possibleX[Random.Range(0, possibleX.Count)];
                //���� ��忡�� ���� ������ ��ΰ� �̹� �����ϴ��� ����
                if (!nodeGrid[prevX, y].nextButton.Contains(nodeGrid[currentX, y + 1]))
                {
                    nodeGrid[prevX, y].nextButton.Add(nodeGrid[currentX, y + 1]);
                    nodeGrid[currentX, y + 1].isPathNode = true;
                }
                tmppathX.Add(currentX);
            }
            paths.Add(tmppathX);
        }

    }
}
