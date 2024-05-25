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

        // 그리드 사이즈
        public int row = 15; // 행의 수
        public int col = 7;  // 열의 수

        // 버튼이 생성될 패널
        public Transform stagePanel;

        // 버튼 프리팹
        public Button stageButton;

        #endregion


        #region private 변수

        // 위치 정보를 담고 있는 그리드
        Vector2[,] positionGrid;
        StageNode[,] stageNodeGrid;

        #endregion


        #region MonoBehaviour 매서드

        void Start()
        {
            // public 변수 할당 검사
            if(stagePanel == null || stageButton == null)
            {
                Debug.LogError("맵 생성에 필요한 오브젝트가 없습니다. " + this.name, this);
            }

            InitPositionGrid();
            GeneratePath();
        }

        #endregion


        #region private 매서드

        // 그리드의 각 포지션을 정하는 함수
        void InitPositionGrid()
        {
            // 패널의 크기를 기반으로 그리드의 각 셀의 위치 계산
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

        // 경로 지정 함수
        void GeneratePath()
        {
            // 첫 번째 선택 인덱스와 두 번째 선택 인덱스의 중복을 막기 위한 변수
            int firstStart = 0;

            for(int paths = 0; paths < 6; paths++)
            {
                // 시작 위치 인덱스 랜덤 선택
                int x = Random.Range(0, col);
                int y = 0;

                // 최소한 두 개의 시작 위치를 보장
                if (paths == 0) firstStart = x;
                else if(paths == 1)
                {
                    while(x == firstStart)
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
                for(y = 1; y < row; y++)
                {
                    // 이전 인덱스 저장
                    int prevX = x;
                    List<int> possibleIndex = new List<int>() { x };

                    if(x < col - 1)
                    {
                        if(CheckPathCross(x, x + 1))
                        {
                            possibleIndex.Add(x + 1);
                        }
                    }
                    if(x > 0)
                    {
                        if(CheckPathCross(x, x - 1))
                        {
                            possibleIndex.Add(x - 1);
                        }
                    }
                }
            }
        }

        // X자로 겹치는 경로가 존재하는지 검사
        bool CheckPathCross(int currentX, int checkX)
        {
            return false;
        }


        // 버튼 생성 함수
        void CreateButton(int x, int y)
        {
            Button _button = Instantiate(stageButton, stagePanel);
            RectTransform _buttonRect = _button.GetComponent<RectTransform>();
            _buttonRect.anchoredPosition = positionGrid[x, y];
            _buttonRect.localScale = Vector2.one;

            StageNode node = _button.GetComponent<StageNode>();
            node.nodeType = NodePreset.GetRandomNodeType();
            node.InitNode(x, y, false);
            stageNodeGrid[x, y] = node;
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
                        StageNode node = stageNodeGrid[x, y];
                        node.nodeType = NodePreset.GetRandomNodeType();
                    }
                }
            }
        }
    }
}