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
        isPathNode = false; // 초기값 설정
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

                // 버튼의 위치 설정
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

            //현재 버튼의 값을 넣는다
            nodeGrid[currentX, 0].isPathNode = true; // 변경된 변수명 반영
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
