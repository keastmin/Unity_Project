using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace keastmin
{
    public class CreateMap : MonoBehaviour
    {
        #region public ����

        // �׸��� ������
        public int row = 15; // ���� ��
        public int col = 7;  // ���� ��

        // ��ư�� ������ �г�
        public Transform stagePanel;

        // ��ư ������
        public Button stageButton;

        #endregion


        #region private ����

        // ��ġ ������ ��� �ִ� �׸���
        Vector2[,] positionGrid;
        StageNode[,] stageNodeGrid;

        #endregion


        #region MonoBehaviour �ż���

        void Start()
        {
            // public ���� �Ҵ� �˻�
            if(stagePanel == null || stageButton == null)
            {
                Debug.LogError("�� ������ �ʿ��� ������Ʈ�� �����ϴ�. " + this.name, this);
            }

            InitPositionGrid();
            GeneratePath();
            NodeEdit();
        }

        #endregion


        #region �׸����� ��ġ ����

        // �׸����� �� �������� ���ϴ� �Լ�
        void InitPositionGrid()
        {
            // �г��� ũ�⸦ ������� �׸����� �� ���� ��ġ ���
            RectTransform rectTransform = stagePanel.GetComponent<RectTransform>();
            float panelWidth = rectTransform.rect.width;
            float panelHeight = rectTransform.rect.height;

            Debug.Log(panelWidth + " " + panelHeight);

            float spacingX = panelWidth / (col + 1);
            float spacingY = panelHeight / (row + 1);

            float startX = spacingX - rectTransform.rect.width / 2;
            float startY = spacingY - rectTransform.rect.height / 2;

            positionGrid = new Vector2[col, row];
            stageNodeGrid = new StageNode[col, row];
            for(int x = 0; x < col; x++)
            {
                for(int y = 0; y < row; y++)
                {
                    float posX = startX + x * spacingX;
                    float posY = startY + y * spacingY;
                    positionGrid[x, y] = new Vector2(posX, posY);
                }
            }
        }

        #endregion


        #region ��� ���� �� ��ư ����

        // ��� ���� �Լ�
        void GeneratePath()
        {
            // ù ��° ���� �ε����� �� ��° ���� �ε����� �ߺ��� ���� ���� ����
            int firstStart = 0;

            for(int paths = 0; paths < 6; paths++)
            {
                // ���� ��ġ �ε��� ���� ����
                int x = Random.Range(0, col);
                int y = 0;

                // �ּ��� �� ���� ���� ��ġ�� ����
                if (paths == 0) firstStart = x;
                else if(paths == 1)
                {
                    while(x == firstStart)
                    {
                        x = Random.Range(0, col);
                    }
                }

                // ���� ��ġ �ε��� �߰�
                if (stageNodeGrid[x, y] == null)
                {
                    CreateButton(x, y);
                }

                // ��� ���� ����
                for(y = 1; y < row; y++)
                {
                    // ���� �ε��� ����
                    int prevX = x;
                    int prevY = y - 1;

                    List<int> possibleIndex = new List<int>() { x };

                    // X�ڷ� ��ġ�� ��θ� �����ϰ� ����Ʈ�� �ε��� ����
                    if(x < col - 1)
                    {
                        if(CheckPathCross(x, x + 1, prevY))
                        {
                            possibleIndex.Add(x + 1);
                        }
                    }
                    if(x > 0)
                    {
                        if(CheckPathCross(x, x - 1, prevY))
                        {
                            possibleIndex.Add(x - 1);
                        }
                    }

                    // ���� ��� ���� ����
                    int listIdx = Random.Range(0, possibleIndex.Count);
                    x = possibleIndex[listIdx];

                    // ���� ��ΰ� �̹� �����ϴ� ������� Ȯ���� �� �ƴ϶�� ����
                    if (stageNodeGrid[x, y] == null)
                    {
                        StageNode nextPath = CreateButton(x, y);
                        stageNodeGrid[prevX, prevY].nextNode.Add(nextPath);
                    }
                    else
                    {
                        if (CheckPathAlready(prevX, prevY, x, y))
                        {
                            StageNode nextPath = stageNodeGrid[x, y];
                            stageNodeGrid[prevX, prevY].nextNode.Add(nextPath);
                        }
                    }
                }
            }
        }

        // X�ڷ� ��ġ�� ��ΰ� �����ϴ��� �˻�
        bool CheckPathCross(int checkX, int targetX, int targetY)
        {
            if (stageNodeGrid[targetX, targetY] != null)
            {
                List<StageNode> targetNextNode = stageNodeGrid[targetX, targetY].nextNode;
                foreach (StageNode targetNode in targetNextNode)
                {
                    if (targetNode.x == checkX)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // ���õ� ���� ��ΰ� �̹� �����ϴ� ������� �˻�
        bool CheckPathAlready(int prevX, int prevY, int selectX, int selectY)
        {
            List<StageNode> nextNodes = stageNodeGrid[prevX, prevY].nextNode;
            foreach (StageNode node in nextNodes)
            {
                if (node.x == selectX && node.floor == selectY)
                {
                    return false;
                }
            }
            return true;
        }

        // ��ư ���� �Լ�
        StageNode CreateButton(int x, int y)
        {
            Button _button = Instantiate(stageButton, stagePanel);
            RectTransform _buttonRect = _button.GetComponent<RectTransform>();
            _buttonRect.anchoredPosition = positionGrid[x, y];
            _buttonRect.localScale = Vector2.one;

            StageNode node = _button.GetComponent<StageNode>();
            node.nodeType = NodePreset.GetRandomNodeType(y);
            node.InitNode(x, y, false);
            stageNodeGrid[x, y] = node;

            return node;
        }

        #endregion


        #region ��� ���Ἲ �˻� �� ����

        /// <summary>
        /// ��Ģ 1: ����Ʈ�� �޽��� 5�� �̸��� ������ �� ����.
        /// ��Ģ 2: ����Ʈ, ����, �޽��� ���ӵ� �� ����.
        /// ��Ģ 3: �ϳ��� �濡�� 2�� �̻��� ��ΰ� �ִٸ� �ּ� �� ���� ���� �ٸ� ������ �����ؾ��Ѵ�.
        /// ��Ģ 4: 13���� �޽��� �� ����.
        /// ��Ģ 5: 0��, 8��, 14���� ����� Ÿ���� �����Ǿ� �ִ�. �̴� NodePreset ��ũ��Ʈ���� Ȯ�� ����.
        /// </summary>
        void NodeEdit()
        {
            bool iterCheck = true; // ��带 �����ϴ� ���� �ٽ� �ݺ����� ����

            // ��Ģ �ؼ��� �ʿ��� ����� ��ȣ
            int eliteNode = NodePreset.GetEliteNodeNum();
            int restNode = NodePreset.GetRestNodeNum();
            int shopNode = NodePreset.GetShopNodeNum();

            // ���� ������ ����Ʈ
            List<StageNode> startNodes = new List<StageNode>();

            for(int i = 0; i < col; i++)
            {
                if (stageNodeGrid[i, 0] != null)
                {
                    startNodes.Add(stageNodeGrid[i, 0]);
                }
            }
            int c = 0;
            while(iterCheck)
            {
                c++;

                iterCheck = false;

                for(int i = 0; i < startNodes.Count; i++)
                {
                    // BFS�� ���� ��� ��Ģ �˻�
                    Queue<StageNode> nodeQueue = new Queue<StageNode>();
                    nodeQueue.Enqueue(startNodes[i]);

                    while(nodeQueue.Count > 0)
                    {
                        // ������ ��, �����ؾ� �� ��� Ÿ�� ����Ʈ
                        List<int> changeNodeList = new List<int>();
                        StageNode currNode = nodeQueue.Dequeue();
                        int currFloor = currNode.floor;

                        if(currFloor != 0 && currFloor != 8 && currFloor != 14)
                        {
                            int rule;

                            // ��Ģ 1
                            if(currFloor < 5)
                            {
                                rule = Rule_1(eliteNode, restNode, (int)currNode.nodeType);
                                if (rule != -1)
                                {
                                    changeNodeList.Add(rule);
                                    iterCheck = true;
                                }
                            }

                            // ��Ģ 2
                            int type = (int)currNode.nodeType;
                            if (type == eliteNode || type == restNode || type == shopNode)
                            {
                                rule = Rule_2(currNode);
                                if (rule != -1)
                                {
                                    changeNodeList.Add(rule);
                                    iterCheck = true;
                                }
                            }

                            // ��Ģ 3
                        }

                        // ��� ��ȯ
                        if (iterCheck)
                        {
                            //Debug.Log("�۵�");
                            currNode.nodeType = NodePreset.GetRandomNodeType(currFloor);
                            int newNodeType = (int)currNode.nodeType;
                            while (changeNodeList.Contains(newNodeType))
                            {
                                currNode.nodeType = NodePreset.GetRandomNodeType(currFloor);
                                newNodeType = (int)currNode.nodeType;
                            }
                        }

                        foreach(StageNode next in currNode.nextNode)
                        {
                            nodeQueue.Enqueue(next);
                        }
                    }
                }
            }

            Debug.Log(c);
        }

        int Rule_1(int eliteNode, int restNode, int currNodeType)
        {
            if (currNodeType == eliteNode) return eliteNode;
            if (currNodeType == restNode) return restNode;
            return -1;
        }

        int Rule_2(StageNode node)
        {
            foreach(StageNode next in node.nextNode)
            {
                if(node.nodeType == next.nodeType)
                {
                    return (int)node.nodeType;
                }
            }
            return -1;
        }

        #endregion


        public void OnClickRandomSprite()
        {
            for(int x = 0; x < col; x++)
            {
                for(int y = 0; y < row; y++)
                {
                    if (stageNodeGrid[x, y] != null)
                    {
                        //StageNode node = stageNodeGrid[x, y];
                        //node.nodeType = NodePreset.GetRandomNodeType();

                        stageNodeGrid[x, y].selectEnable = true;
                    }
                }
            }
        }
    }
}