using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

namespace keastmin
{
    public class MapCreatorTest : MonoBehaviour
    {
        #region public 변수
        
        public Transform stagePanel; // 버튼이 생성될 패널      
        public Button stageButton; // 버튼 프리팹     
        public RawImage lineImage; // 버튼 사이의 라인 프리팹

        #endregion


        #region private 변수

        private int row = 15; // 행의 수
        private int col = 7; // 열의 수

        Vector2[,] positionGrid; // 위치 정보를 담고 있는 그리드
        StageNodeTest[,] stageNodeGrid;
        HashSet<int>[,] paths;
        HashSet<int> startIndex;
        StageNodeTest startingNode; // 가장 최하층 노드들을 이어주는 노드

        #endregion


        #region MonoBehaviour 메서드

        void Start()
        {
            InitPositionGrid();
            CreatePath();
            DrawPathLine();
            CreateButton();
            NodeEdit();
        }

        #endregion


        #region 생성될 버튼의 위치 지정 메서드

        void InitPositionGrid()
        {
            // 그리드 초기화
            positionGrid = new Vector2[col, row];

            // 패널의 크기 기반으로 그리드의 각 셀의 위치 계산
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

                    // 랜덤하게 위치 조정
                    posX += UnityEngine.Random.Range(-10.0f, 10.1f);
                    posY += UnityEngine.Random.Range(-10.0f, 10.1f);
                    positionGrid[x, y] = new Vector2(posX, posY);
                }
            }
        }

        #endregion


        #region 경로 생성 메서드

        // 경로 그리드와 시작 위치 리스트 초기화
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
            InitPathList(); // 경로 생성 전 그리드 초기화

            // 첫 번째 선택 인덱스와 두 번째 인덱스의 중복을 막기 위한 변수
            int firstStart = 0;

            // 경로 결정
            for(int i = 0; i < 6; ++i)
            {
                // 시작 위치 인덱스 랜덤 선택
                int x = UnityEngine.Random.Range(0, col);

                // 최소한 두 개의 시작 위치를 보장
                if (i == 0) firstStart = x;
                else
                {
                    while (x == firstStart)
                    {
                        x = UnityEngine.Random.Range(0, col);
                    }
                }

                // 시작 위치 인덱스 추가
                startIndex.Add(x);

                // 경로 선택 시작
                for(int y = 1; y < row; ++y)
                {
                    // 선택 가능한 다음 경로 목록(다른 경로와 X자로 교차하면 선택 불가)
                    List<int> possibleIndex = new List<int>() { x };
                    
                    // X자로 교차하는지 검사
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

                    // 이전 x를 저장하고 다음 경로를 새로운 x로 초기화
                    int prevX = x;
                    x = GetRandomNextPath(possibleIndex);

                    paths[prevX, y - 1].Add(x);
                }
            }
        }

        // X자로 교차하는 경로에 대해 검사
        bool CheckPathCross(int currX, int targetX, int targetY)
        {
            foreach(int x in paths[targetX, targetY])
            {
                if (x == currX) return false;
            }
            return true;
        }

        // 선택 가능 경로 중 랜덤 반환
        int GetRandomNextPath(List<int> list)
        {
            int listIdx = UnityEngine.Random.Range(0, list.Count);
            return list[listIdx];
        }

        #endregion


        #region 경로 라인 생성 메서드

        // 시작 노드부터 BFS를 통해 다음 노드들과 경로 연결
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

        // 이전 위치와 다음 위치 사이를 dot 이미지를 이용해 연결
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


        #region 버튼 생성 메서드

        void CreateButton()
        {
            // 최하층 노드들을 이어줄 가상 노드
            GameObject tmpButton = new GameObject("VirtualNode");
            startingNode = tmpButton.AddComponent<StageNodeTest>();

            // 버튼의 컴포넌트를 받아올 노드 그리드
            stageNodeGrid = new StageNodeTest[col, row];

            // BFS를 위한 Queue 생성
            Queue<Tuple<int, int>> nodeQ = new Queue<Tuple<int, int>>();
            bool[,] visited = new bool[col, row];

            // 시작 노드 Queue에 삽입
            foreach (int start in startIndex)
            {
                nodeQ.Enqueue(new Tuple<int, int>(start, 0));
                visited[start, 0] = true;

                // 버튼 생성 후 노드 연결
                InstantiateButton(start, 0);
                ConnectPrevNextNode(startingNode, stageNodeGrid[start, 0]);
                
                // 시작 노드 활성화
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

        // 버튼을 생성하고 그 버튼의 노드 정보를 그리드에 담는 함수
        void InstantiateButton(int x, int y)
        {
            Button _button = Instantiate(stageButton, stagePanel);
            RectTransform _buttonRect = _button.GetComponent<RectTransform>();
            stageNodeGrid[x, y] = _button.GetComponent<StageNodeTest>();
            stageNodeGrid[x, y].nodeType = NodeTypeManager.instance.GetRandomNodeType();
            _buttonRect.anchoredPosition = positionGrid[x, y];
            _buttonRect.localScale = Vector2.one;
        }

        // 다음 노드와 이전 노드를 연결하는 함수
        void ConnectPrevNextNode(StageNodeTest prev, StageNodeTest next)
        {
            prev.nextNode.Add(next);
            next.prevNode.Add(prev);
        }

        #endregion


        #region 노드 규칙 무결성 검사 및 수정

        /// <summary>
        /// 규칙 1: 엘리트와 휴식은 5층 미만에 생성될 수 없다.
        /// 규칙 2: 엘리트, 상점, 휴식은 연속될 수 없다.
        /// 규칙 3: 하나의 방에서 2개 이상의 경로가 있다면 최소 두 개의 서로 다른 방임을 보장해야한다.
        /// 규칙 4: 13층은 휴식일 수 없다. 이는 규칙 2와 5에 의해 해결된다.
        /// 규칙 5: 0층, 8층, 14층은 노드의 타입이 고정되어 있다.
        /// </summary>
        void NodeEdit()
        {
            Rule_5_Edit();

            // 시작 노드부터 시작
            foreach (int idx in startIndex)
            {
                // BFS를 통한 노드 규칙 검사
                Queue<Tuple<int, int>> nodeQ = new Queue<Tuple<int, int>>();
                nodeQ.Enqueue(new Tuple<int, int>(idx, 0));

                while(nodeQ.Count > 0)
                {
                    HashSet<NodeType> changeTypeHash = new HashSet<NodeType>(); // 노드 변환에서 제외할 해쉬
                    Tuple<int, int> tuple = nodeQ.Dequeue();
                    int x = tuple.Item1;
                    int y = tuple.Item2;

                    // 다음 경로 큐에 삽입
                    foreach(int nx in paths[x, y])
                    {
                        nodeQ.Enqueue(new Tuple<int, int>(nx, y + 1));
                    }

                    // 노드가 고정되어 있는 층은 무시
                    if (y == 0 || y == 8 || y == 14) continue;

                    // 노드 검사 과정
                    bool check = Rule_1_Check(x, y);


                    // 노드 변환 과정
                    if (check)
                    {
                        Rule_1_Compliance(y, changeTypeHash);
                    }

                    if(changeTypeHash.Count > 0)
                    {
                        changeNode(x, y, changeTypeHash);
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

        // 5층 미만 노드는 엘리트와 휴식일 수 없다.
        bool Rule_1_Check(int x, int y)
        {
            NodeType currType = stageNodeGrid[x, y].nodeType;
            if ((currType == NodeType.Elite || currType == NodeType.Rest) && y < 5)
            {
                return true;
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

        void changeNode(int x, int y, HashSet<NodeType> hash)
        {
            NodeType currNode = stageNodeGrid[x, y].nodeType;
            while (hash.Contains(currNode))
            {
                currNode = NodeTypeManager.instance.GetRandomNodeType();
            }
            stageNodeGrid[x, y].nodeType = currNode;
        }

        #endregion
    }
}