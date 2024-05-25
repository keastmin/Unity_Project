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

        // ��� ������ �ν��Ͻ�

        //NodePreset nodePreset = NodePreset.nodePresetInstance;

        #endregion

        void Start()
        {
            // public ���� �Ҵ� �˻�
            if(stagePanel == null || stageButton == null)
            {
                Debug.LogError("�� ������ �ʿ��� ������Ʈ�� �����ϴ�. " + this.name, this);
            }

            InitPositionGrid();
            GeneratePath();
        }

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