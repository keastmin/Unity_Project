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

        // 노드 프리셋 인스턴스

        //NodePreset nodePreset = NodePreset.nodePresetInstance;

        #endregion

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

        void GeneratePath()
        {
            for(int x = 0; x < col; x++)
            {
                for(int y = 0; y < row; y++)
                {
                    CreateButton(x, y);
                }
            }
        }

        void CreateButton(int x, int y)
        {
            Button _button = Instantiate(stageButton, stagePanel);
            RectTransform _buttonRect = _button.GetComponent<RectTransform>();
            _buttonRect.anchoredPosition = positionGrid[x, y];
            _buttonRect.localScale = Vector2.one;

            StageNode node = _button.GetComponent<StageNode>();
            node.nodeType = NodePreset.GetRandomNodeType();
            node.InitNode(x, y, false);
            //StageNode randomNode = nodePreset.GetRandomStageNode();
            //StageNode newNode = new StageNode(randomNode.nodeType, randomNode.nodeSprite, y);
        }
    }
}