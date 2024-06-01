using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace keastmin
{
    public class StageNode : MonoBehaviour
    {
        #region enum Node Type

        public enum NodeType
        {
            NoneNode,
            NormalNode,
            EventNode,
            EliteNode,
            RestNode,
            ShopNode,
            ChestNode
        }

        #endregion


        #region public ����

        [SerializeField]
        Animator animator;
        public Sprite nodeSprite;
        public List<StageNode> nextNode;
        public List<StageNode> prevNode;
        //public bool selectEnable;
        public int x;
        public int floor;

        public Sprite[] sprites;

        #endregion


        #region private ����

        [SerializeField] private bool _selectEnable;
        public bool selectEnable
        {
            get { return _selectEnable; }
            set
            {
                _selectEnable = value;
                UpdateNodeActivation();
            }
        }

        [SerializeField] private NodeType _nodeType;
        public NodeType nodeType
        {
            get { return _nodeType; }
            set
            {
                if(_nodeType != value)
                {
                    _nodeType = value;
                    UpdateNodeSprite();
                }
            }
        }

        #endregion


        private void Start()
        {
            UpdateNodeSprite();
        }

        void UpdateNodeActivation()
        {
            if (_selectEnable)
            {
                animator.SetBool("IsActive", true);
            }
            else
            {
                animator.SetBool("IsActive", false);
            }
        }

        void UpdateNodeSprite()
        {
            if(sprites != null && (int)_nodeType < sprites.Length)
            {
                nodeSprite = sprites[(int)_nodeType];
                GetComponent<Image>().sprite = nodeSprite;
            }
        }

        public void InitNode(int x, int floor, bool select)
        {
            this.x = x;
            this.floor = floor;
            this._selectEnable = select;
            this.nextNode = new List<StageNode>();
            this.prevNode = new List<StageNode>();
        }

        public void OnClickNextStageInfo()
        {
            //Debug.Log("���� ���� ��ġ: " + this.floor + " " + this.x);
            //Debug.Log("���� ����� �� = " + this.nextNode.Count);
            //foreach (StageNode next in this.nextNode)
            //{
            //    Debug.Log(next.floor + " " + next.x);
            //}
            //Debug.Log("���� ����� �� = " + this.prevNode.Count);
            //foreach(StageNode prev in this.prevNode)
            //{
            //    Debug.Log(prev.floor + " " + prev.x);
            //}
            //Debug.Log(_selectEnable);

            if (_selectEnable)
            {
                _selectEnable = false;
                UpdateNodeActivation();
                foreach(StageNode next in this.nextNode)
                {
                    next.selectEnable = true;
                }
            }

            //Debug.Log(this.selectEnable);
        }
    }
}