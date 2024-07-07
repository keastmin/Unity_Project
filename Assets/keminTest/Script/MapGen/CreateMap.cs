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

        // UIManager�� ���� �ν��Ͻ�
        public static CreateMap createMapInstance;

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
        [SerializeField] GameObject startNodesObject;
        StageNode startNodesPrev;

        #endregion


        #region MonoBehaviour �ż���

        void Start()
        {
            createMapInstance = this;

            // public ���� �Ҵ� �˻�
            if (stagePanel == null || stageButton == null)
            {
                Debug.LogError("�� ������ �ʿ��� ������Ʈ�� �����ϴ�. " + this.name, this);
            }

            startNodesPrev = startNodesObject.GetComponent<StageNode>();
            startNodesPrev.InitNode(-1, -1);
            CreatePathNode();
            StartNodeActivation();
            CreatePathLine();
            NodeButtonSetLastSibling();
            //UIManager.uiManagerInstance.AllPanelActiveFalse();
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

                    // �����ϰ� ��ġ ����
                    posX += Random.Range(-10.0f, 10.1f);
                    posY += Random.Range(-10.0f, 10.1f);
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
            node.InitNode(x, y);
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
                        bool check = false;
                      
                        check = Rule_1_Check(eliteNode, restNode, type, currFloor);                         // ��Ģ 1
                        if(!check) check = Rule_2_Check(currNode, eliteNode, restNode, shopNode, type);     // ��Ģ 2                       
                        if(!check) check = Rule_3_Check(currNode);                                          // ��Ģ 3

                        // ��带 ��ȯ�ؾ� �Ѵٸ� ��ȯ �� ��Ģ�� ��� �ؼ��� �� �ֵ��� �Ұ����� ��ȯ ��� �˻�
                        if (check)
                        {
                            Rule_1_Compliance(eliteNode, restNode, currFloor, changeNodeHash);
                            Rule_2_Compliance(currNode, eliteNode, restNode, shopNode, changeNodeHash);
                            Rule_3_Compliance(currNode, changeNodeHash);
                        }
                    }

                    // ��� ��ȯ
                    if(changeNodeHash.Count >= 5)
                    {
                        ResetButton();
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

        bool Rule_1_Check(int eliteNode, int restNode, int currNodeType, int floor)
        {
            if ((currNodeType == eliteNode || currNodeType == restNode) && floor < 5) return true;
            return false;
        }

        bool Rule_2_Check(StageNode node, int eliteNode, int restNode, int shopNode, int currNodeType)
        {
            if (currNodeType == eliteNode || currNodeType == restNode || currNodeType == shopNode)
            {
                foreach (StageNode prev in node.prevNode)
                {
                    if (node.nodeType == prev.nodeType)
                    {
                        return true;
                    }
                }
                foreach (StageNode next in node.nextNode)
                {
                    if (node.nodeType == next.nodeType)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool Rule_3_Check(StageNode node)
        {
            foreach(StageNode prev in node.prevNode)
            {
                if(prev.nextNode.Count > 1)
                {
                    HashSet<int> checkTypes = new HashSet<int>();
                    foreach(StageNode next in prev.nextNode)
                    {
                        checkTypes.Add((int)next.nodeType);
                    }

                    if (checkTypes.Count < 2) return true;
                }
            }

            return false;
        }

        void Rule_1_Compliance(int eliteNode, int restNode, int floor, HashSet<int> hash)
        {
            if(floor < 5)
            {
                hash.Add(eliteNode);
                hash.Add(restNode);
            }
        }

        void Rule_2_Compliance(StageNode node, int eliteNode, int restNode, int shopNode, HashSet<int> hash)
        {
            foreach(StageNode prev in node.prevNode)
            {
                if((int)prev.nodeType == eliteNode || (int)prev.nodeType == restNode || (int)prev.nodeType == shopNode)
                {
                    hash.Add((int)prev.nodeType);
                }
            }
            foreach (StageNode next in node.nextNode)
            {
                if ((int)next.nodeType == eliteNode || (int)next.nodeType == restNode || (int)next.nodeType == shopNode)
                {
                    hash.Add((int)next.nodeType);
                }
            }
        }

        void Rule_3_Compliance(StageNode node, HashSet<int> hash)
        {
            foreach(StageNode prev in node.prevNode)
            {
                if(prev.nextNode.Count > 1)
                {
                    HashSet<int> types = new HashSet<int>();
                    foreach(StageNode next in prev.nextNode)
                    {
                        if(node != next)
                        {
                            types.Add((int)next.nodeType);
                        }
                    }

                    if(types.Count < 2)
                    {
                        foreach(int type in types)
                        {
                            hash.Add(type);
                        }
                    }
                }
            }
        }

        void ResetButton()
        {
            Debug.LogError("��ư ����");
            for(int i = 0; i < col; i++)
            {
                for(int j = 0; j < row; j++)
                {
                    if (stageNodeGrid[i, j] != null)
                    {
                        Destroy(stageNodeGrid[i, j].gameObject);
                    }
                }
            }
        }

        #endregion


        #region ��� �ձ�

        // ���� ������ �����ϰ� ������ ��� ��Ȱ��
        void StartNodeActivation()
        {
            Debug.Log("����");

            for(int x = 0; x < col; x++)
            {
                if (stageNodeGrid[x, 0] != null)
                {
                    stageNodeGrid[x, 0].prevNode.Add(startNodesPrev);
                    startNodesPrev.nextNode.Add(stageNodeGrid[x, 0]);
                    //stageNodeGrid[x, 0].selectEnable = true;
                }
            }

            Debug.Log("����");


            //Queue<StageNode> q = new Queue<StageNode>();
            //bool[,] visited = new bool[col, row];
            //q.Enqueue(startNodesPrev);
            //while(q.Count > 0)
            //{
            //    StageNode node = q.Dequeue();
            //    if (node.floor == 0) node.selectEnable = true;
            //    else if (node.floor > 0) node.selectEnable = false;
            //    foreach(StageNode next in node.nextNode)
            //    {
            //        if (!visited[next.x, next.floor])
            //        {
            //            q.Enqueue(next);
            //        }
            //    }
            //}
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

            Vector2 direction = (targV - currV).normalized;
            currV += direction * 17;
            targV -= direction * 13;

            float distance = Vector2.Distance(currV, targV);
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

        private void NodeButtonSetLastSibling()
        {
            for(int x = 0; x < col; x++)
            {
                for(int y = 0; y < row; y++)
                {
                    if (stageNodeGrid[x, y] != null)
                    {
                        Button _button = stageNodeGrid[x, y].GetComponent<Button>();
                        _button.transform.SetAsLastSibling();
                    }
                }
            }
        }

        #endregion


        #region public �ż���

        // StageNodeGrid ���� ������ ��� ��ȯ �ż���
        public List<StageNode> GetStageNodeList()
        {
            List<StageNode> nodes = new List<StageNode>();
            for(int x = 0; x < col; x++)
            {
                for(int y = 0; y < row; y++)
                {
                    if (stageNodeGrid[x, y] != null && stageNodeGrid[x, y].selectEnable)
                    {
                        nodes.Add(stageNodeGrid[x, y]);
                    }
                }
            }
            return nodes;
        }


        // ���� �� ���� ��ȯ�ϴ� �ż���
        public int GetStageNodeFloor()
        {
            int floor = 0;
            for(int y = 0; y < row; y++)
            {
                for(int x = 0; x<col; x++)
                {
                    if (stageNodeGrid[x, y] != null && stageNodeGrid[x, y].selectEnable)
                    {
                        floor = y; 
                        break;
                    }
                }
            }
            return floor;
        }

        #endregion
    }
}