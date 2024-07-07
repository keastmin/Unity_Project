using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace keastmin
{
    public class CreateMap : MonoBehaviour
    {
        #region public 변수

        // UIManager를 위한 인스턴스
        public static CreateMap createMapInstance;

        // 그리드 사이즈
        public int row = 15; // 행의 수
        public int col = 7;  // 열의 수

        // 버튼이 생성될 패널
        public Transform stagePanel;

        // 버튼 프리팹
        public Button stageButton;

        // 버튼 사이의 라인 프리팹
        public RawImage lineImage;

        #endregion


        #region private 변수

        // 위치 정보를 담고 있는 그리드
        Vector2[,] positionGrid;
        StageNode[,] stageNodeGrid;

        // 시작 층들을 모두 이어주는 내부적인 -1 층
        [SerializeField] GameObject startNodesObject;
        StageNode startNodesPrev;

        #endregion


        #region MonoBehaviour 매서드

        void Start()
        {
            createMapInstance = this;

            // public 변수 할당 검사
            if (stagePanel == null || stageButton == null)
            {
                Debug.LogError("맵 생성에 필요한 오브젝트가 없습니다. " + this.name, this);
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

        #region 경로 생성 알고리즘

        void CreatePathNode()
        {
            InitPositionGrid();
            GeneratePath();
            NodeEdit();
        }

        #endregion


        #region 그리드의 위치 선언

        // 그리드의 각 셀의 포지션을 정하는 함수
        void InitPositionGrid()
        {
            // 패널의 크기를 기반으로 그리드의 각 셀의 위치 계산
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

                    // 랜덤하게 위치 조정
                    posX += Random.Range(-10.0f, 10.1f);
                    posY += Random.Range(-10.0f, 10.1f);
                    positionGrid[x, y] = new Vector2(posX, posY);
                }
            }
        }

        #endregion


        #region 경로 생성 및 버튼 생성

        // 경로 지정 함수
        void GeneratePath()
        {
            // 첫 번째 선택 인덱스와 두 번째 선택 인덱스의 중복을 막기 위한 변수
            int firstStart = 0;

            for (int paths = 0; paths < 6; paths++)
            {
                // 시작 위치 인덱스 랜덤 선택
                int x = Random.Range(0, col);
                int y = 0;

                // 최소한 두 개의 시작 위치를 보장
                if (paths == 0) firstStart = x;
                else if (paths == 1)
                {
                    while (x == firstStart)
                    {
                        x = Random.Range(0, col);
                    }
                }

                // 시작 위치 인덱스 추가
                if (stageNodeGrid[x, y] == null)
                {
                    CreateButton(x, y);
                }

                // 경로 선택 시작
                for (y = 1; y < row; y++)
                {
                    // 이전 인덱스 저장
                    int prevX = x;
                    int prevY = y - 1;

                    List<int> possibleIndex = new List<int>() { x };

                    // X자로 겹치는 경로를 제외하고 리스트에 인덱스 저장
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

                    // 다음 경로 랜덤 결정
                    int listIdx = Random.Range(0, possibleIndex.Count);
                    x = possibleIndex[listIdx];

                    // 다음 경로가 이미 존재하는 경로인지 확인한 후 아니라면 생성
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

        // X자로 겹치는 경로가 존재하는지 검사
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

        // 선택된 다음 경로가 이미 존재하는 경로인지 검사
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

        // 버튼 생성 함수
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


        #region 경로 무결성 검사 및 수정

        /// <summary>
        /// 규칙 1: 엘리트와 휴식은 5층 미만에 생성될 수 없다.
        /// 규칙 2: 엘리트, 상점, 휴식은 연속될 수 없다.
        /// 규칙 3: 하나의 방에서 2개 이상의 경로가 있다면 최소 두 개의 서로 다른 방임을 보장해야한다.
        /// 규칙 4: 13층은 휴식일 수 없다. 이는 규칙 3을 통해 해결된다.
        /// 규칙 5: 0층, 8층, 14층은 노드의 타입이 고정되어 있다. 이는 NodePreset 스크립트에서 확인 가능.
        /// </summary>
        void NodeEdit()
        {
            // 규칙 준수에 필요한 노드의 번호
            int eliteNode = NodePreset.GetEliteNodeNum();
            int restNode = NodePreset.GetRestNodeNum();
            int shopNode = NodePreset.GetShopNodeNum();

            // 시작 노드들의 리스트
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
                // BFS를 통한 노드 규칙 검사
                Queue<StageNode> nodeQueue = new Queue<StageNode>();
                nodeQueue.Enqueue(startNodes[i]);

                while (nodeQueue.Count > 0)
                {
                    // 변경할 때, 제외해야 할 노드 타입 해쉬
                    HashSet<int> changeNodeHash = new HashSet<int>();
                    StageNode currNode = nodeQueue.Dequeue();
                    int currFloor = currNode.floor;

                    if (currFloor != 0 && currFloor != 8 && currFloor != 14)
                    {
                        int type = (int)currNode.nodeType;
                        bool check = false;
                      
                        check = Rule_1_Check(eliteNode, restNode, type, currFloor);                         // 규칙 1
                        if(!check) check = Rule_2_Check(currNode, eliteNode, restNode, shopNode, type);     // 규칙 2                       
                        if(!check) check = Rule_3_Check(currNode);                                          // 규칙 3

                        // 노드를 변환해야 한다면 변환 후 규칙이 모두 준수될 수 있도록 불가능한 변환 노드 검사
                        if (check)
                        {
                            Rule_1_Compliance(eliteNode, restNode, currFloor, changeNodeHash);
                            Rule_2_Compliance(currNode, eliteNode, restNode, shopNode, changeNodeHash);
                            Rule_3_Compliance(currNode, changeNodeHash);
                        }
                    }

                    // 노드 변환
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

                    // 다음 경로 큐에 삽입
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
            Debug.LogError("버튼 리셋");
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


        #region 경로 잇기

        // 시작 지점을 제외하고 나머지 모두 비활성
        void StartNodeActivation()
        {
            Debug.Log("이전");

            for(int x = 0; x < col; x++)
            {
                if (stageNodeGrid[x, 0] != null)
                {
                    stageNodeGrid[x, 0].prevNode.Add(startNodesPrev);
                    startNodesPrev.nextNode.Add(stageNodeGrid[x, 0]);
                    //stageNodeGrid[x, 0].selectEnable = true;
                }
            }

            Debug.Log("이후");


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


        #region public 매서드

        // StageNodeGrid 선택 가능한 노드 반환 매서드
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


        // 현재 층 수를 반환하는 매서드
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