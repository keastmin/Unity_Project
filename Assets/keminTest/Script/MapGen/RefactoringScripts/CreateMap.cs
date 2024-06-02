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

        // ��ư ������ ���� ������
        public RawImage lineImage;

        #endregion


        #region private ����

        // ��ġ ������ ��� �ִ� �׸���
        Vector2[,] positionGrid;
        StageNode[,] stageNodeGrid;

        // ���� ������ ��� �̾��ִ� �������� -1 ��
        StageNode startNodesPrev;

        #endregion


        #region MonoBehaviour �ż���

        void Start()
        {
            // public ���� �Ҵ� �˻�
            if (stagePanel == null || stageButton == null)
            {
                Debug.LogError("�� ������ �ʿ��� ������Ʈ�� �����ϴ�. " + this.name, this);
            }

            startNodesPrev = new StageNode();
            startNodesPrev.InitNode(-1, -1, false);
            CreatePathNode();
            StartNodeActivation();
            CreatePathLine();
        }

        #endregion

        #region ��� ���� �˰���

        void CreatePathNode()
        {
            InitPositionGrid();
            GeneratePath();
            NodeEdit();
        }

        #endregion


        #region �׸����� ��ġ ����

        // �׸����� �� ���� �������� ���ϴ� �Լ�
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
            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
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

            for (int paths = 0; paths < 6; paths++)
            {
                // ���� ��ġ �ε��� ���� ����
                int x = Random.Range(0, col);
                int y = 0;

                // �ּ��� �� ���� ���� ��ġ�� ����
                if (paths == 0) firstStart = x;
                else if (paths == 1)
                {
                    while (x == firstStart)
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
                for (y = 1; y < row; y++)
                {
                    // ���� �ε��� ����
                    int prevX = x;
                    int prevY = y - 1;

                    List<int> possibleIndex = new List<int>() { x };

                    // X�ڷ� ��ġ�� ��θ� �����ϰ� ����Ʈ�� �ε��� ����
                    if (x < col - 1)
                    {
                        if (CheckPathCross(x, x + 1, prevY))
                        {
                            possibleIndex.Add(x + 1);
                        }
                    }
                    if (x > 0)
                    {
                        if (CheckPathCross(x, x - 1, prevY))
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
                        nextPath.prevNode.Add(stageNodeGrid[prevX, prevY]);
                    }
                    else
                    {
                        if (!CheckPathAlready(prevX, prevY, x, y))
                        {
                            StageNode nextPath = stageNodeGrid[x, y];
                            stageNodeGrid[prevX, prevY].nextNode.Add(nextPath);
                            nextPath.prevNode.Add(stageNodeGrid[prevX, prevY]);
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
                    return true;
                }
            }
            return false;
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
        /// ��Ģ 4: 13���� �޽��� �� ����. �̴� ��Ģ 3�� ���� �ذ�ȴ�.
        /// ��Ģ 5: 0��, 8��, 14���� ����� Ÿ���� �����Ǿ� �ִ�. �̴� NodePreset ��ũ��Ʈ���� Ȯ�� ����.
        /// </summary>
        void NodeEdit()
        {
            // ��Ģ �ؼ��� �ʿ��� ����� ��ȣ
            int eliteNode = NodePreset.GetEliteNodeNum();
            int restNode = NodePreset.GetRestNodeNum();
            int shopNode = NodePreset.GetShopNodeNum();

            // ���� ������ ����Ʈ
            List<StageNode> startNodes = new List<StageNode>();
            for (int i = 0; i < col; i++)
            {
                if (stageNodeGrid[i, 0] != null)
                {
                    startNodes.Add(stageNodeGrid[i, 0]);
                }
            }

            for (int i = 0; i < startNodes.Count; i++)
            {
                // BFS�� ���� ��� ��Ģ �˻�
                Queue<StageNode> nodeQueue = new Queue<StageNode>();
                nodeQueue.Enqueue(startNodes[i]);

                while (nodeQueue.Count > 0)
                {
                    // ������ ��, �����ؾ� �� ��� Ÿ�� �ؽ�
                    HashSet<int> changeNodeHash = new HashSet<int>();
                    StageNode currNode = nodeQueue.Dequeue();
                    int currFloor = currNode.floor;

                    if (currFloor != 0 && currFloor != 8 && currFloor != 14)
                    {
                        int type = (int)currNode.nodeType;

                        // ��Ģ 3
                        Rule_3(currNode, changeNodeHash);

                        // ��Ģ 1
                        if (currFloor < 5)
                        {
                            Rule_1(eliteNode, restNode, type, changeNodeHash);
                        }

                        // ��Ģ 2
                        if (type == eliteNode || type == restNode || type == shopNode)
                        {
                            Rule_2(eliteNode, restNode, shopNode, currNode, changeNodeHash);
                        }
                    }

                    // ��� ��ȯ
                    if(changeNodeHash.Count >= 5)
                    {
                        CreatePathNode();
                        return;
                    }
                    else if(changeNodeHash.Count > 0)
                    {
                        currNode.nodeType = NodePreset.GetRandomNodeType(currFloor);
                        int newNodeType = (int)currNode.nodeType;
                        while (changeNodeHash.Contains(newNodeType))
                        {
                            currNode.nodeType = NodePreset.GetRandomNodeType(currFloor);
                            newNodeType = (int)currNode.nodeType;
                        }
                    }

                    // ���� ��� ť�� ����
                    foreach (StageNode next in currNode.nextNode)
                    {
                        nodeQueue.Enqueue(next);
                    }
                }
            }
        }

        void Rule_1(int eliteNode, int restNode, int currNodeType, HashSet<int> changeHash)
        {
            if (currNodeType == eliteNode || currNodeType == restNode)
            {
                changeHash.Add(eliteNode);
                changeHash.Add(restNode);
            }
        }

        void Rule_2(int eliteNode, int restNode, int shopNode, StageNode node, HashSet<int> changeHash)
        {
            bool check;
            check = Rule_2_Check(node, node.nextNode);
            if (!check) check = Rule_2_Check(node, node.prevNode);

            if (check)
            {
                foreach (StageNode next in node.nextNode)
                {
                    int type = (int)next.nodeType;
                    if (type == eliteNode || type == restNode || type == shopNode)
                    {
                        changeHash.Add(type);
                    }
                }
                foreach(StageNode prev in node.prevNode)
                {
                    int type = (int)prev.nodeType;
                    if (type == eliteNode || type == restNode || type == shopNode)
                    {
                        changeHash.Add(type);
                    }
                }
            }
        }

        bool Rule_2_Check(StageNode node, List<StageNode> targetList)
        {
            foreach(StageNode target in targetList)
            {
                if(node.nodeType == target.nodeType)
                {
                    return true;
                }
            }
            return false;
        }

        void Rule_3(StageNode node, HashSet<int> changeHash)
        {
            foreach(StageNode prev in node.prevNode)
            {
                if (prev.nextNode.Count > 1)
                {
                    // �ߺ��Ǵ� ����� ���� �� ������ Ȯ���ϱ� ���� �ؽ���
                    HashSet<int> typeHash = new HashSet<int>();

                    foreach (StageNode next in prev.nextNode)
                    {
                        typeHash.Add((int)next.nodeType);
                    }

                    if(typeHash.Count < 2)
                    {
                        changeHash.Add((int)node.nodeType);
                    }
                }
            }
        }

        #endregion


        #region ��� �ձ�

        void StartNodeActivation()
        {
            for(int x = 0; x < col; x++)
            {
                if (stageNodeGrid[x, 0] != null)
                {
                    stageNodeGrid[x, 0].selectEnable = true;
                    stageNodeGrid[x, 0].prevNode.Add(startNodesPrev);
                    startNodesPrev.nextNode.Add(stageNodeGrid[x, 0]);
                }
            }
        }

        void CreatePathLine()
        {
            Queue<StageNode> queueNodes = new Queue<StageNode>();
            bool[,] visited = new bool[col, row];
            foreach(StageNode start in startNodesPrev.nextNode)
            {
                queueNodes.Enqueue(start);
                visited[start.x, start.floor] = true;
            }

            while(queueNodes.Count > 0)
            {
                StageNode currNode = queueNodes.Dequeue();
                Vector2 currPos = positionGrid[currNode.x, currNode.floor];

                foreach(StageNode next in currNode.nextNode)
                {
                    Vector2 targetPos = positionGrid[next.x, next.floor];
                    DrawLine(currPos, targetPos);

                    if (!visited[next.x, next.floor])
                    {
                        queueNodes.Enqueue(next);
                        visited[next.x, next.floor] = true;
                    }
                }
            }
        }

        void DrawLine(Vector2 currV, Vector2 targV)
        {
            float lineSpacing = 12.0f;

            float distance = Vector2.Distance(currV, targV);
            Vector2 direction = (targV - currV).normalized;
            float angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            int lineCount = Mathf.FloorToInt(distance / lineSpacing);
            for(int k = 0; k <= lineCount; k++)
            {
                Vector2 pos = currV + direction * (lineSpacing * k);
                RawImage rawImage = Instantiate(lineImage, stagePanel);
                RectTransform rectTransform = rawImage.GetComponent<RectTransform>();
                rectTransform.localPosition = pos;
                rectTransform.localRotation = rotation;
                rectTransform.localScale = Vector2.one;
            }
        }

        #endregion
    }
}