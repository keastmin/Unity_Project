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


        #region public 변수

        public Animator animator;
        public Sprite nodeSprite;
        public List<StageNode> nextNode;
        public List<StageNode> prevNode;
        //public bool selectEnable;
        public int x;
        public int floor;

        public Sprite[] sprites;

        #endregion


        #region private 변수

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
            if (this.floor >= 0)
            {
                Button _button = GetComponent<Button>();
                if (_selectEnable)
                {
                    animator.SetBool("IsActive", true);
                    _button.interactable = true;
                }
                else
                {
                    animator.SetBool("IsActive", false);
                    _button.interactable = false;
                }
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
            this.selectEnable = false;
            this.nextNode = new List<StageNode>();
            this.prevNode = new List<StageNode>();
        }

        public void OnClickNextStageInfo()
        {
            if (_selectEnable)
            {
                foreach(StageNode next in this.nextNode)
                {
                    next.selectEnable = true;
                }
                foreach(StageNode prev in this.prevNode)
                {
                    foreach(StageNode stopNode in prev.nextNode)
                    {
                        stopNode.selectEnable = false;
                    }
                }
            }
        }
    }
}