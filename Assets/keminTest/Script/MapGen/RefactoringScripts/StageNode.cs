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

        public NodeType nodeType;
        public Image nodeImage;
        public List<StageNode> nextNode;

        #endregion
    }
}