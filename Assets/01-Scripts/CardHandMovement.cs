using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHandMovement : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public enum SelectionState { Unselected, Selected, Dragging, Targeting};

    SelectionState selectionState = SelectionState.Unselected;

    RectTransform rectTransform;
    Vector2 canvasOffset;
    Canvas canvas;

    public Hand hand;
    public Vector2 targetPos;
    public Vector2 dragTargetPos;
    public float targetAngle;
    public int order;
    public Card card;
    public CardData cardData;
    
    Vector3 currFacingPoint = new Vector3(0.0f, 0.0f, -40.0f);
    Vector3 facingPointInHand = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 facingPointNoPlay = new Vector3(0.0f, 4.0f, -40.0f);
    Vector3 facingPointPlay = new Vector3(0.0f, 20.0f, -40.0f);

    float defaultScale = 0.9f;
    float selectScale = 1.2f;
    float smoothMoveRate = 0.30f;
    float smoothScaleRate = 0.30f;
    float smoothRotationRate = 0.30f;
    float orderDepthDiff = 1f;
    float orderDepthBase = 15f;
    float minY = -140f;
    Rect playArea = new Rect(new Vector2(-400f, -50f), new Vector2(800f, 350f));

    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        Rect canvasRect = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().pixelRect;
        rectTransform = GetComponent<RectTransform>();
        canvasOffset = new Vector2(canvasRect.width / 2, canvasRect.height / 2);
        card = GetComponent<Card>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        SetRotation();
        SetScale();
    }

    void Move()
    {
        Vector2 currTargetPos;
        float orderValue;
        if (selectionState == SelectionState.Dragging ||
            selectionState == SelectionState.Targeting)
        {
            currTargetPos = dragTargetPos;
            
        }
        else
        {
            currTargetPos = targetPos;
        }

        if (selectionState == SelectionState.Selected ||
            selectionState == SelectionState.Dragging ||
            selectionState == SelectionState.Targeting)
        {
            currTargetPos.y = Mathf.Max(minY, currTargetPos.y);
            orderValue = orderDepthBase - (20 * orderDepthDiff);
        }
        else
        {
            orderValue = orderDepthBase - (order * orderDepthDiff);
        }

        rectTransform.localPosition = new Vector3(
            (currTargetPos.x * smoothMoveRate) + (transform.localPosition.x * (1.0f - smoothMoveRate)),
            (currTargetPos.y * smoothMoveRate) + (transform.localPosition.y * (1.0f - smoothMoveRate)),
            orderValue
            );
    }

    void SetRotation()
    {
        Quaternion targetRotation;
        if (selectionState == SelectionState.Selected ||
            selectionState == SelectionState.Dragging ||
            selectionState == SelectionState.Targeting)
        {
            targetRotation = Quaternion.identity;
            targetRotation = Quaternion.identity *
                Quaternion.LookRotation(rectTransform.position - currFacingPoint, Vector3.up);
        }
        else
        {
            targetRotation = Quaternion.identity *
                Quaternion.AngleAxis(targetAngle, Vector3.forward * -1);
        }
        rectTransform.localRotation = Quaternion.Lerp(rectTransform.localRotation, targetRotation, smoothRotationRate);
    }

    void SetScale()
    {
        float targetScale;
        if (selectionState == SelectionState.Selected ||
            selectionState == SelectionState.Dragging ||
            selectionState == SelectionState.Targeting)
        {
            targetScale = selectScale;
        }
        else
        {
            targetScale = defaultScale;
        }

        transform.localScale = new Vector3(
            (targetScale * smoothScaleRate) + (transform.localScale.x * (1 - smoothScaleRate)),
            (targetScale * smoothScaleRate) + (transform.localScale.y * (1 - smoothScaleRate)),
            (targetScale * smoothScaleRate) + (transform.localScale.z * (1 - smoothScaleRate))
            );
    }

    void OnEnable()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    void OnDisable()
    {
        //transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (InputManager.Instance.IsInputAvailable())
        {
            InputManager.Instance.SelectedCard = gameObject;
            Vector2 cursorPos = eventData.position;
            Vector2 unscaledCursorPos = canvas.GetComponent<CanvasHelper>().ToReferenceScale(cursorPos);
            currFacingPoint = (unscaledCursorPos.y > 0) ? facingPointPlay : facingPointNoPlay;
            selectionState = SelectionState.Selected;
            Cursor.visible = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //InputManager.Instance.SelectedCard = null;
        if (selectionState == SelectionState.Selected)
        {
            currFacingPoint = facingPointInHand;
            selectionState = SelectionState.Unselected;
            Cursor.visible = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(InputManager.Instance.IsInputAvailable())
        {
            Field field = GameObject.FindGameObjectWithTag(Con.Tags.gameController).GetComponent<Field>();
            field.PlaySlotParticleAvailable(cardData);
            selectionState = SelectionState.Dragging;
            Cursor.visible = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (InputManager.Instance.IsInputAvailable())
        {
            Vector2 cursorPos = eventData.position;
            Vector2 refCursorPos =
                canvas.GetComponent<CanvasHelper>().ToReferenceScale(cursorPos);
            //print(selectionState);
            //Vector2 canvasPos = ToCanvasPos(cursorPos);

            //dragTargetPos = new Vector3(
            //    canvasPos.x, canvasPos.y, orderDepthBase - orderDepthDiff);
            Vector2 unscaledCursorPos = canvas.GetComponent<CanvasHelper>().ToReferenceScale(cursorPos);

            Field field =
                GameObject.FindGameObjectWithTag(Con.Tags.gameController).GetComponent<Field>();

            if (playArea.Contains(refCursorPos))
            {
                selectionState = SelectionState.Targeting;
                dragTargetPos = new Vector3(
                    refCursorPos.x, refCursorPos.y - 900, orderDepthBase - orderDepthDiff);
                currFacingPoint = facingPointPlay;
            }
            else
            {
                field.StopSlotParticleTargeted();
                selectionState = SelectionState.Dragging;
                dragTargetPos = new Vector3(
                    refCursorPos.x, refCursorPos.y, orderDepthBase - orderDepthDiff);
                currFacingPoint = facingPointNoPlay;
            }

            if (playArea.Contains(refCursorPos))
            {
                RaycastHit hit = InputManager.Instance.ShootRay();


                if (!hit.collider) 
                {
                    field.StopSlotParticleTargeted();
                    return; 
                }

                CharacterSlot slot = hit.collider.GetComponent<CharacterSlot>();

                if (hit.collider.CompareTag(Con.Tags.slot) && slot)
                {
                    if(slot.IsCardTarget(cardData))
                    {

                        field.PlaySlotParticleTargeted(cardData, slot);
                    }
                }
                else
                {
                    field.StopSlotParticleTargeted();
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        currFacingPoint = facingPointInHand;
        Cursor.visible = true;
        if (!InputManager.Instance.IsInputAvailable() ||
            selectionState == SelectionState.Unselected ||
            selectionState == SelectionState.Selected)
        {
            return;
        }
        else
        { 
            Vector2 unscaledCursorPos = canvas.GetComponent<CanvasHelper>().ToReferenceScale(eventData.position);
            selectionState = SelectionState.Unselected;
            //print(playArea.min + " / " + playArea.max);
            //print(unscaledCursorPos);
            Field field = GameObject.FindGameObjectWithTag(Con.Tags.gameController).GetComponent<Field>();
            field.StopSlotParticle(cardData);
            field.StopSlotParticleTargeted();

            if (playArea.Contains(unscaledCursorPos))
            {
                RaycastHit hit = InputManager.Instance.ShootRay();

                if (!hit.collider) { return; }

                CharacterSlot slot = hit.collider.GetComponent<CharacterSlot>();

                if (hit.collider.CompareTag(Con.Tags.slot) && slot)
                {
                    if(slot.IsCardTarget(cardData))
                    {
                        Play(slot);
                    }
                }
            }
        }
    }

    public void Play(CharacterSlot slot)
    {
        GetComponent<Card>().Play(slot);
    }

    //public Vector2 ToCanvasPos(Vector2 from)
    //{
    //    return new Vector2(from.x - canvasOffset.x, from.y - canvasOffset.y);
    //}
}
