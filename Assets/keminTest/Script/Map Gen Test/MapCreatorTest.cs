using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace keastmin
{
    public class MapCreatorTest : MonoBehaviour
    {
        #region public ����

        public static MapCreatorTest instance;

        public Transform stagePanel; // ��ư�� ������ �г�      
        public Button stageButton; // ��ư ������     
        public RawImage lineImage; // ��ư ������ ���� ������

        #endregion


        #region private ����

        private int row = 15; // ���� ��
        private int col = 7; // ���� ��

        Vector2[,] positionGrid; // ��ġ ������ ��� �ִ� �׸���
        StageNodeTest[,] stageNodeGrid;
        HashSet<int>[,] paths;
        HashSet<int> startIndex;
        StageNodeTest startingNode; // ���� ������ ������ �̾��ִ� ���

        #endregion


        #region MonoBehaviour �޼���

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            InitPositionGrid();
            CreatePath();
            DrawPathLine();
            CreateButton();
            NodeEdit();
            UIManager.instance.AllPanelActiveFalse();
        }

        #endregion


        #region ������ ��ư�� ��ġ ���� �޼���

        void InitPositionGrid()
        {
            // �׸��� �ʱ�ȭ
            positionGrid = new Vector2[col, row];

            // �г��� ũ�� ������� �׸����� �� ���� ��ġ ���
            RectTransform rectTransform = stagePanel.GetComponent<RectTransform>();
            float panelWidth = rectTransform.rect.width;
            float panelHeight = rectTransform.rect.height;

            float spacingX = panelWidth / (col + 1);
            float spacingY = panelHeight / (row + 1);

            float startX = spacingX - rectTransform.rect.width / 2;
            float startY = spacingY - rectTransform.rect.height / 2;

            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    float posX = startX + x * spacingX;
                    float posY = startY + y * spacingY;

                    // �����ϰ� ��ġ ����
                    posX += UnityEngine.Random.Range(-10.0f, 10.1f);
                    posY += UnityEngine.Random.Range(-10.0f, 10.1f);
                    positionGrid[x, y] = new Vector2(posX, posY);
                }
            }
        }

        #endregion


        #region ��� ���� �޼���

        // ��� �׸���� ���� ��ġ ����Ʈ �ʱ�ȭ
        void InitPathList()
        {
            paths = new HashSet<int>[col, row];
            startIndex = new HashSet<int>();
            for (int x = 0; x < col; ++x)
            {
                for (int y = 0; y < row; ++y)
                {
                    paths[x, y] = new HashSet<int>();
                }
            }
        }

        void CreatePath()
        {
            InitPathList(); // ��� ���� �� �׸��� �ʱ�ȭ

            // ù ��° ���� �ε����� �� ��° �ε����� �ߺ��� ���� ���� ����
            int firstStart = 0;

            // ��� ����
            for(int i = 0; i < 6; ++i)
            {
                // ���� ��ġ �ε��� ���� ����
                int x = UnityEngine.Random.Range(0, col);

                // �ּ��� �� ���� ���� ��ġ�� ����
                if (i == 0) firstStart = x;
                else
                {
                    while (x == firstStart)
                    {
                        x = UnityEngine.Random.Range(0, col);
                    }
                }

                // ���� ��ġ �ε��� �߰�
                startIndex.Add(x);

                // ��� ���� ����
                for(int y = 1; y < row; ++y)
                {
                    // ���� ������ ���� ��� ���(�ٸ� ��ο� X�ڷ� �����ϸ� ���� �Ұ�)
                    List<int> possibleIndex = new List<int>() { x };
                    
                    // X�ڷ� �����ϴ��� �˻�
                    if(x > 0)
                    {
                        if (CheckPathCross(x, x - 1, y - 1))
                        {
                            possibleIndex.Add(x - 1);
                        }
                    }
                    if(x < col - 1)
                    {
                        if(CheckPathCross(x, x + 1, y - 1))
                        {
                            possibleIndex.Add(x + 1);
                        }
                    }

                    // ���� x�� �����ϰ� ���� ��θ� ���ο� x�� �ʱ�ȭ
                    int prevX = x;
                    x = GetRandomNextPath(possibleIndex);

                    paths[prevX, y - 1].Add(x);
                }
            }
        }

        // X�ڷ� �����ϴ� ��ο� ���� �˻�
        bool CheckPathCross(int currX, int targetX, int targetY)
        {
            foreach(int x in paths[targetX, targetY])
            {
                if (x == currX) return false;
            }
            return true;
        }

        // ���� ���� ��� �� ���� ��ȯ
        int GetRandomNextPath(List<int> list)
        {
            int listIdx = UnityEngine.Random.Range(0, list.Count);
            return list[listIdx];
        }

        #endregion


        #region ��� ���� ���� �޼���

        // ���� ������ BFS�� ���� ���� ����� ��� ����
        void DrawPathLine()
        {
            Queue<Tuple<int, int>> nodeQ = new Queue<Tuple<int, int>>();
            bool[,] visited = new bool[col, row];
            foreach (int start in startIndex)
            {
                nodeQ.Enqueue(new Tuple<int, int>(start, 0));
                visited[start, 0] = true;
            }

            while (nodeQ.Count > 0)
            {
                Tuple<int, int> currT = nodeQ.Dequeue();
                int x = currT.Item1;
                int y = currT.Item2;
                Vector2 currPos = positionGrid[x, y];

                foreach (int next in paths[x, y])
                {
                    Vector2 targetPos = positionGrid[next, y + 1];
                    DrawLine(currPos, targetPos);

                    if (!visited[next, y + 1])
                    {
                        nodeQ.Enqueue(new Tuple<int, int>(next, y + 1));
                        visited[next, y + 1] = true;
                    }
                }
            }
        }

        // ���� ��ġ�� ���� ��ġ ���̸� dot �̹����� �̿��� ����
        void DrawLine(Vector2 currV, Vector2 targV)
        {
            float lineSpacing = 12.0f;

            Vector2 direction = (targV - currV).normalized;
            currV += direction * 17;
            targV -= direction * 13;

            float distance = Vector2.Distance(currV, targV);
            float angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            int lineCount = Mathf.FloorToInt(distance / lineSpacing);
            for (int k = 0; k <= lineCount; k++)
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


        #region ��ư ���� �޼���

        void CreateButton()
        {
            // ������ ������ �̾��� ���� ���
            GameObject tmpButton = new GameObject("VirtualNode");
            startingNode = tmpButton.AddComponent<StageNodeTest>();

            // ��ư�� ������Ʈ�� �޾ƿ� ��� �׸���
            stageNodeGrid = new StageNodeTest[col, row];

            // BFS�� ���� Queue ����
            Queue<Tuple<int, int>> nodeQ = new Queue<Tuple<int, int>>();
            bool[,] visited = new bool[col, row];

            // ���� ��� Queue�� ����
            foreach (int start in startIndex)
            {
                nodeQ.Enqueue(new Tuple<int, int>(start, 0));
                visited[start, 0] = true;

                // ��ư ���� �� ��� ����
                InstantiateButton(start, 0);
                ConnectPrevNextNode(startingNode, stageNodeGrid[start, 0]);
                
                // ���� ��� Ȱ��ȭ
                stageNodeGrid[start, 0].isActive = true;
            }

            while (nodeQ.Count > 0)
            {
                Tuple<int, int> currT = nodeQ.Dequeue();
                int x = currT.Item1;
                int y = currT.Item2;

                foreach (int next in paths[x, y])
                {
                    if (!visited[next, y + 1])
                    {
                        InstantiateButton(next, y + 1);
                        nodeQ.Enqueue(new Tuple<int, int>(next, y + 1));
                        visited[next, y + 1] = true;
                    }
                    ConnectPrevNextNode(stageNodeGrid[x, y], stageNodeGrid[next, y + 1]);
                }
            }
        }

        // ��ư�� �����ϰ� �� ��ư�� ��� ������ �׸��忡 ��� �Լ�
        void InstantiateButton(int x, int y)
        {
            Button _button = Instantiate(stageButton, stagePanel);
            RectTransform _buttonRect = _button.GetComponent<RectTransform>();
            stageNodeGrid[x, y] = _button.GetComponent<StageNodeTest>();
            stageNodeGrid[x, y].nodeType = NodeTypeManager.instance.GetRandomNodeType();
            _buttonRect.anchoredPosition = positionGrid[x, y];
            _buttonRect.localScale = Vector2.one;
        }

        // ���� ���� ���� ��带 �����ϴ� �Լ�
        void ConnectPrevNextNode(StageNodeTest prev, StageNodeTest next)
        {
            prev.nextNode.Add(next);
            next.prevNode.Add(prev);
        }

        #endregion


        #region ��� ��Ģ ���Ἲ �˻� �� ����

        /// <summary>
        /// ��Ģ 1: ����Ʈ�� �޽��� 5�� �̸��� ������ �� ����.
        /// ��Ģ 2: ����Ʈ, ����, �޽��� ���ӵ� �� ����.
        /// ��Ģ 3: �ϳ��� �濡�� 2�� �̻��� ��ΰ� �ִٸ� �ּ� �� ���� ���� �ٸ� ������ �����ؾ��Ѵ�.
        /// ��Ģ 4: 13���� �޽��� �� ����. �̴� ��Ģ 2�� 5�� ���� �ذ�ȴ�.
        /// ��Ģ 5: 0��, 8��, 14���� ����� Ÿ���� �����Ǿ� �ִ�.
        /// </summary>
        void NodeEdit()
        {
            Rule_5_Edit();

            // ���� ������ ����
            foreach (int idx in startIndex)
            {
                // BFS�� ���� ��� ��Ģ �˻�
                Queue<Tuple<int, int>> nodeQ = new Queue<Tuple<int, int>>();
                nodeQ.Enqueue(new Tuple<int, int>(idx, 0));

                while(nodeQ.Count > 0)
                {
                    HashSet<NodeType> changeTypeHash = new HashSet<NodeType>(); // ��� ��ȯ���� ������ �ؽ�
                    Tuple<int, int> tuple = nodeQ.Dequeue();
                    int x = tuple.Item1;
                    int y = tuple.Item2;

                    // ���� ��� ť�� ����
                    foreach(int nx in paths[x, y])
                    {
                        nodeQ.Enqueue(new Tuple<int, int>(nx, y + 1));
                    }

                    // ��尡 �����Ǿ� �ִ� ���� ����
                    if (y == 0 || y == 8 || y == 14) continue;

                    // ��� �˻� �� ��ȯ ����
                    if (Rule_Check(x, y))
                    {
                        Rule_1_Compliance(y, changeTypeHash);
                        Rule_2_Compliance(x, y, changeTypeHash);
                        Rule_3_Compliance(x, y, changeTypeHash);
                    }

                    // �������� ���� ������ ��� ��尡 ��ȯ ��Ͽ� ���ԵǾ� �ִٸ� ��� ��� Ÿ�� �缳��
                    if(changeTypeHash.Count >= 5)
                    {
                        Debug.Log("��� ���Ἲ �˻� ���з� �� �籸��");
                        ChangeAllNodeType();
                        NodeEdit();
                        return;
                    }
                    else if(changeTypeHash.Count > 0)
                    {
                        ChangeNode(x, y, changeTypeHash);
                    }
                }
            }
        }

        void Rule_5_Edit()
        {
            int[] idxes = { 0, 8, 14 };
            NodeType[] nodes = { NodeType.Normal, NodeType.Treasure, NodeType.Rest };
            for (int i = 0; i < idxes.Length; ++i)
            {
                int y = idxes[i];
                for(int x = 0; x < col; ++x)
                {
                    if (stageNodeGrid[x, y] != null)
                    {
                        stageNodeGrid[x, y].nodeType = nodes[i];
                    }
                }
            }
        }

        bool Rule_Check(int x, int y)
        {
            if (Rule_1_Check(x, y)) return true;
            if (Rule_2_Check(x, y)) return true;
            if (Rule_3_Check(x, y)) return true;
            return false;
        }

        // 5�� �̸� ���� ����Ʈ�� �޽��� �� ����.
        bool Rule_1_Check(int x, int y)
        {
            NodeType currType = stageNodeGrid[x, y].nodeType;
            if ((currType == NodeType.Elite || currType == NodeType.Rest) && y < 5)
            {
                return true;
            }
            return false;
        }

        // ����Ʈ, ����, �޽��� ���ӵ� �� ����.
        bool Rule_2_Check(int x, int y)
        {
            NodeType currType = stageNodeGrid[x, y].nodeType;
            if(currType == NodeType.Elite || currType == NodeType.Rest || currType == NodeType.Merchant)
            {
                foreach(StageNodeTest prev in stageNodeGrid[x, y].prevNode)
                {
                    if (prev.nodeType == currType) return true;
                }
                foreach (StageNodeTest next in stageNodeGrid[x, y].nextNode)
                {
                    if (next.nodeType == currType) return true;
                }
            }
            return false;
        }

        // 2�� �̻��� ���� ��ΰ� ���� ��� ������ 2�� �̻��� ��� Ÿ���� �����ؾ��Ѵ�.
        bool Rule_3_Check(int x, int y)
        {
            foreach (StageNodeTest prev in stageNodeGrid[x, y].prevNode)
            {
                if (prev.nextNode.Count > 1)
                {
                    HashSet<NodeType> checkTypesCount = new HashSet<NodeType>();
                    foreach(StageNodeTest next in prev.nextNode)
                    {
                        checkTypesCount.Add(next.nodeType);
                    }
                    if (checkTypesCount.Count <= 1) return true;
                }
            }
            return false;
        }

        void Rule_1_Compliance(int y, HashSet<NodeType> hash)
        {
            if(y < 5)
            {
                hash.Add(NodeType.Elite);
                hash.Add(NodeType.Rest);
            }
        }

        void Rule_2_Compliance(int x, int y, HashSet<NodeType> hash)
        {
            NodeType[] types = { NodeType.Elite, NodeType.Rest, NodeType.Merchant };
            foreach(StageNodeTest prev in stageNodeGrid[x, y].prevNode)
            {
                for(int i = 0; i < types.Length; i++)
                {
                    if (prev.nodeType == types[i]) hash.Add(types[i]);
                }
            }
            foreach (StageNodeTest next in stageNodeGrid[x, y].nextNode)
            {
                for (int i = 0; i < types.Length; i++)
                {
                    if (next.nodeType == types[i]) hash.Add(types[i]);
                }
            }
        }

        void Rule_3_Compliance(int x, int y, HashSet<NodeType> hash)
        {
            foreach(StageNodeTest prev in stageNodeGrid[x, y].prevNode)
            {
                if(prev.nextNode.Count > 1)
                {
                    HashSet<NodeType> typeCountHash = new HashSet<NodeType>();
                    foreach(StageNodeTest next in prev.nextNode)
                    {
                        if (next != stageNodeGrid[x, y]) typeCountHash.Add(next.nodeType);
                    }

                    if (typeCountHash.Count == 1) hash.Add(typeCountHash.First());
                }
            }
        }

        void ChangeAllNodeType()
        {
            for(int x = 0; x < col; x++)
            {
                for(int y = 0; y < row; y++)
                {
                    if (stageNodeGrid[x, y] != null)
                    {
                        stageNodeGrid[x, y].nodeType = NodeTypeManager.instance.GetRandomNodeType();
                    }
                }
            }
        }

        void ChangeNode(int x, int y, HashSet<NodeType> hash)
        {
            NodeType currNode = stageNodeGrid[x, y].nodeType;
            while (hash.Contains(currNode))
            {
                currNode = NodeTypeManager.instance.GetRandomNodeType();
            }
            stageNodeGrid[x, y].nodeType = currNode;
        }

        #endregion


        #region �ν��Ͻ� ��ȯ �޼���
         

        // StartScrollSet���� ���� Ȱ��ȭ�� �� ���� ��ȯ�ϴµ� ���Ǵ� �޼���
        public int GetMapScrollStartPos()
        {
            if (stageNodeGrid != null)
            {
                for (int y = 0; y < row; ++y)
                {
                    for (int x = 0; x < col; ++x)
                    {
                        if (stageNodeGrid[x, y] != null && stageNodeGrid[x, y].isActive)
                        {
                            return y;
                        }
                    }
                }
            }
            return 0;
        }

        #endregion
    }
}