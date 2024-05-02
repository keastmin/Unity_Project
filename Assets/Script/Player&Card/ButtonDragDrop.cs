using UnityEngine;
using UnityEngine.EventSystems;

public enum CardType
{
    NullCard,
    Attack, // ���� ī��
    Skill, // ��ų ī��
    Power, // �Ŀ� ī��
    Curse // ���� ī��
}

public class ButtonDragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform rectTransform; // ��ư�� RectTransform ������Ʈ
    private Canvas canvas; // ��ư�� ���� Canvas
    private Vector2 originalPosition; // �巡�� ���� �� ��ư�� ���� ��ġ
    private bool isDragging = false; // �巡�� ������ ���θ� ��Ÿ���� �÷���
    private AttackCardSc attackCardSc;
    private SkillCardSc skillCardSc;
    private int cardType;
    //public Card card;

    private void Start()
    {
        //card = GetComponent<Card>();
        attackCardSc = GetComponent<AttackCardSc>();
        skillCardSc = GetComponent<SkillCardSc>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cardType = FindCardType();
    }

    // ��ư�� Ŭ������ �� ȣ��Ǵ� �ݹ�
    public void OnPointerDown(PointerEventData eventData)
    {
        // �巡�� ���� �� ��ư�� ���� ��ġ�� ����
        originalPosition = rectTransform.anchoredPosition;
        // �巡�� �÷��׸� true�� ����
        isDragging = true;
    }

    // ��ư���� ���� ���� �� ȣ��Ǵ� �ݹ�
    public void OnPointerUp(PointerEventData eventData)
    {
        // �巡�� �÷��׸� false�� �缳��
        isDragging = false;
        // ��ư�� ���� ��ġ�� �ǵ���
        rectTransform.anchoredPosition = originalPosition;

        // ���콺 ��ġ���� ���� �±׸� ���� ���� ������Ʈ�� ã�� ī�带 ���
        UseCardAtMousePosition();
    }

    // ��ư�� �巡���ϴ� ���� ȣ��Ǵ� �ݹ�
    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // �巡���ϴ� ���� ��ư�� ��ġ�� ������Ʈ
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    // ���콺 ��ġ���� ���� �±׸� ���� ���� ������Ʈ�� �ִ��� Ȯ���ϰ� ī�带 ����ϴ� �Լ�
    private void UseCardAtMousePosition()
    {
        // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ���콺 ��ġ���� ���� �±׸� ���� ���� ������Ʈ ã��
        Collider2D collider = Physics2D.OverlapPoint(mouseWorldPosition);
        if (collider != null && collider.CompareTag("Monster"))
        {
            if (cardType == (int)CardType.Attack)
            {
                attackCardSc.Target.Add(collider.gameObject);
                Debug.Log("Play()������");
                attackCardSc.Play(attackCardSc.Target);
            }
            else if (cardType == (int)CardType.Skill)
            {
                Debug.Log("Play()������");
                skillCardSc.Play();
            }
        }
    }
    public int FindCardType() //0 = null, 1 = ����
    {
        if (attackCardSc != null)
            return (int)CardType.Attack;
        else if(skillCardSc != null)
            return (int)CardType.Skill;
        else
            return (int)CardType.NullCard;
    }
}
