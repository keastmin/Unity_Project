using keastmin;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace keastmin {
    public static class NodePreset
    {
        // CreateMap�� CreateButton �Լ����� node�� Type�� ��ȭ�� �� ����� �Լ�
        public static StageNode.NodeType GetRandomNodeType()
        {
            // NodeType���� ��� �� ��������
            Array types = Enum.GetValues(typeof(StageNode.NodeType));

            // NoneNode�� ������ ���� NodeType ��ȯ
            int randomType = UnityEngine.Random.Range(1, types.Length);
            return (StageNode.NodeType)types.GetValue(randomType);
        }
    }
}