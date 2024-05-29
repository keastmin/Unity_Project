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
            EventNode,
            EliteNode,
            RestNode,
            ShopNode,
            ChestNode
        }

        #endregion


        #region public 변수

        //public NodeType nodeType;
        public Sprite nodeSprite;
        public List<StageNode> nextNode;
        public List<StageNode> prevNode;
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
            this.prevNode = new List<StageNode>();
        }

        public void OnClickNextStageInfo()
        {
            Debug.Log("현재 층과 위치: " + this.floor + " " + this.x);

            Debug.Log("다음 경로의 수 = " + this.nextNode.Count);
            foreach (StageNode next in this.nextNode)
            {
                Debug.Log(next.floor + " " + next.x);
            }

            Debug.Log("이전 경로의 수 = " + this.prevNode.Count);
            foreach(StageNode prev in this.prevNode)
            {
                Debug.Log(prev.floor + " " + prev.x);
            }

            //Debug.Log(this.selectEnable);
        }
    }
}