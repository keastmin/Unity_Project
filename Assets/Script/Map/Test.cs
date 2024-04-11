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

    Vector2[,] grid = new Vector2[7, 15];
    GameObject[,] buttons = new GameObject[7, 15];
    GameObject[,] paths = new GameObject[7, 15];
    Node[,] nodeGrid = new Node[7, 15]; 
    


    int col = 7;
    int row = 15;
    //List<GameObject> buttons = new List<GameObject>();
    
    void Start()
    {
        CreateGrid();
        CreateButtons();
        GeneratePath();
    }

    void CreateGrid()
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
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 15; y++)
            {
                Vector2 buttonPos = grid[x, y];
                GameObject buttonObj = Instantiate(buttonPrefab, panel);
                RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();

                // ��ư�� ��ġ ����
                buttonRect.anchoredPosition = buttonPos;
                buttons[x, y] = buttonObj;
                
            }
        }
    }

    void GeneratePath()
    {
        List<int> tmppathX = new List<int>();
        for (int num = 0; num < 7; num++)
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
            nodeGrid[currentX, 0].isPathNode = true; // ����� ������ �ݿ�
            tmppathX.Add(currentX);

            for (int y = 0; y < row; y++)
            {
                List<int> PossibleX = new List<int>();

                if(currentX > 0)
                {
                    if (nodeGrid[currentX - 1, y].isPathNode)
                    {
                        bool check = true;

                        
                    }
                }
            }


        }

        //    if (currentX == firstX)
        //        currentX = Random.Range(0, 7);

        //    tmppathX.Add(currentX);

    }
       
        
    
}
