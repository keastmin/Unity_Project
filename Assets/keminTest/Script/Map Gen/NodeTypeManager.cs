using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace keastmin
{
    public class NodeTypeManager : MonoBehaviour
    {
        public static NodeTypeManager instance;

        [SerializeField]
        private NodeSpriteMapping[] nodeSprite;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            if(nodeSprite.Length != System.Enum.GetValues(typeof(NodeType)).Length)
            {
                Debug.LogError("�迭�� ���̰� �������� ���̿� �ٸ��ϴ�.");
            }
        }

        // ��� Ÿ�Կ� �´� �̹��� ��ȯ
        public Sprite GetSprite(NodeType nodeType)
        {
            return nodeSprite[(int)nodeType].sprite;
        }

        // Ȯ���� ���� ��� Ÿ�� ���� ��ȯ
        public NodeType GetRandomNodeType()
        {
            float rand = Random.value;
            if(rand < 0.45f)
            {
                return NodeType.Normal;
            }
            else if(rand < 0.67f)
            {
                return NodeType.Event;
            }
            else if(rand < 0.83f)
            {
                return NodeType.Elite;
            }
            else if(rand < 0.95f)
            {
                return NodeType.Rest;
            }
            return NodeType.Merchant;
        }
    }
}