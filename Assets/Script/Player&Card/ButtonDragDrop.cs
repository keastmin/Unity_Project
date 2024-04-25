using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonDragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform rectTransform; // ��ư�� RectTransform ������Ʈ
    private Canvas canvas; // ��ư�� ���� Canvas
    private Vector2 originalPosition; // �巡�� ���� �� ��ư�� ���� ��ġ
    private bool isDragging = false; // �巡�� ������ ���θ� ��Ÿ���� �÷���
    public Card card;
    private void Awake()
    {
        card = GetComponent<Card>();
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
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
            Debug.Log("���� ã��");
            // ���� �±׸� ���� ���� ������Ʈ�� �߰��ϸ� �ش� ������Ʈ�� ī�� ������ ���� ������ ī�带 ���
            Debug.Log(card.type);
            // ����ġ ���� ����Ͽ� �ش��ϴ� ī���� Play �Լ� ����
            switch (card.type)
            {
                case CardType.Attack:
                    // ����ȯ
                    AttackCard attackCard = (AttackCard)card;
                    Debug.Log("����ī�� ���");
                    attackCard = new AttackCard(card.name, card.description, card.cost, attackCard.damage);
                    if (attackCard != null)
                    {
                        attackCard.Play();
                    }
                    break;
                case CardType.Skill:
                    SkillCard skillCard = card as SkillCard;
                    if (skillCard != null)
                    {
                        skillCard.Play();
                    }
                    break;
                case CardType.Curse:
                    CurseCard curseCard = card as CurseCard;
                    if (curseCard != null)
                    {
                        curseCard.Play();
                    }
                    break;
                default:
                    Debug.LogError("Unknown Card Type");
                    break;
            }
        }
    }
}
