using System.Collections;
using System.Collections.Generic;
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
            EliteNode,
            EventNode,
            ShopNode,
            RestNode,
            ChestNode
        }

        #endregion


        #region public º¯¼ö

        //public NodeType nodeType;
        public Sprite nodeSprite;
        public List<StageNode> nextNode;
        public bool selectEnable;
        public int x;
        public int floor;

        public Sprite[] sprites;

        #endregion

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

        private void Start()
        {
            UpdateNodeSprite();
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
            this.selectEnable = select;
            this.nextNode = new List<StageNode>();
        }
    }
}