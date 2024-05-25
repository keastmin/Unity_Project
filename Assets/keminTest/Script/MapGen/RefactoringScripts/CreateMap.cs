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
        }

        #endregion


        #region private �ż���

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

        // X�ڷ� ��ġ�� ��ΰ� �����ϴ��� �˻�
        bool CheckPathCross(int currentX, int checkX)
        {
            return false;
        }


        // ��ư ���� �Լ�
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