using keastmin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace keastmin {
    public static class NodePreset
    {
        // CreateMap�� CreateButton �Լ����� node�� Type�� ��ȭ�� �� ����� �Լ�
        public static StageNode.NodeType GetRandomNodeType(int floor)
        {
            // ��Ģ: 0���� ������ �Ϲ� ����, 8���� ������ ���� ��, 14���� ������ �޽�
            if (floor != 0 && floor != 8 && floor != 14)
            {
                // StageNode�� ����� enum�� ������� Ȯ���� ����ϸ� �Ʒ��� ����.
                // NormalNode = 45%, EventNode = 22%, EliteNode = 16%, RestNode = 12%, ShopNode = 5%
                int[] probabilities = new int[] { 45, 67, 83, 95, 100 };

                // 0 ~ 100 ������ �� ����
                int randomPoint = UnityEngine.Random.Range(0, 100);

                for (int i = 0; i < probabilities.Length; i++)
                {
                    if (randomPoint < probabilities[i])
                    {
                        return (StageNode.NodeType)(i + 1);
                    }
                }
            }
            else if(floor == 0)
            {
                return StageNode.NodeType.NormalNode;
            }
            else if(floor == 8)
            {
                return StageNode.NodeType.ChestNode;
            }
            else if(floor == 14)
            {
                return StageNode.NodeType.RestNode;
            }
            return StageNode.NodeType.NoneNode;
        }


        #region ��� Ÿ�� ��ȣ ���� �Լ�

        public static int GetEliteNodeNum()
        {
            return (int)StageNode.NodeType.EliteNode;
        }
        public static int GetRestNodeNum()
        {
            return (int)StageNode.NodeType.RestNode;
        }
        public static int GetShopNodeNum()
        {
            return (int)StageNode.NodeType.ShopNode;
        }

        #endregion
    }
}