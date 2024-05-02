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
    List<List<int>> paths = new List<List<int>>();
    Node[,] nodeGrid = new Node[7, 15]; 
    


    int col = 7;
    int row = 15;
    //List<GameObject> buttons = new List<GameObject>();
    
    void Start()
    {
        CreateGrid();
        GeneratePath();
        CreateButtons();
        
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
        for (int i = 0; i < col; i++)
        {
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
                }
            }
        }
    }



    void GeneratePath()
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

            //현재 버튼의 값을 넣는다
            nodeGrid[currentX, 0].isPathNode = true;
            tmppathX.Add(currentX);

            for (int y = 0; y < row -1; y++)
            {
                

                if(currentX > 0)
                {

                    if (nodeGrid[currentX - 1, y].isPathNode)
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

                //어떤 조건에도 걸리지 않음
                possibleX.Add(currentX);

                if(currentX > row -1)
                {
                    check = true;

                    for (int k = 0; k < nodeGrid[currentX, y-1].nextButton.Count; k++)
                    {
                        if (nodeGrid[currentX, y].nextButton[k] == nodeGrid[currentX, y -1])
                        {
                            check = false;
                            changeX.Add(currentX);
                            break;

                        }
                    }

                    if (check)
                    {
                        possibleX.Add(currentX);
                    }

                }

                else
                {
                    possibleX.Add(currentX);
                }
                
            }

        }

        //    if (currentX == firstX)
        //        currentX = Random.Range(0, 7);

        //    tmppathX.Add(currentX);

    }
       
        
    
}
