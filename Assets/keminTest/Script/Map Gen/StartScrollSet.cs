using UnityEngine;

namespace keastmin
{
    public class StartScrollSet : MonoBehaviour
    {
        // ��ũ�Ѻ��� ������ ������Ʈ�� ���� ��ġ�� �����ϱ� ���� ����
        private RectTransform _scrollViewContent;

        // ��ũ���� ���� ��ġ�� ���� �ö� ������ ������ ��ġ ��
        private int _startHeight = 450;
        private int _floorHeight = 80;

        private void Awake()
        {
            _scrollViewContent = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            if (MapCreatorTest.instance != null)
            {
                int currFloor = MapCreatorTest.instance.GetCurrentActiveFloor();
                Vector2 newPos = _scrollViewContent.anchoredPosition;
                newPos.y = _startHeight - (currFloor * _floorHeight);
                _scrollViewContent.anchoredPosition = newPos;
            }
        }
    }
}