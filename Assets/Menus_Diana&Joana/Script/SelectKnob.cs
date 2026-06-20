using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectKnob : MonoBehaviour
{
    [Header("References")]
    public RectTransform knob;          // The selection knob
    public RectTransform[] buttons;     // Buttons in order (Play, Options, Credits, Exit)
    public float fixedX = 0f;           // The X position you want (set to 0)
    public float smoothSpeed = 10f;

    private float targetY;
    private Vector2 targetPosition;

    void Start()
    {
        if (knob == null) knob = GetComponent<RectTransform>();

        // Set initial position
        if (buttons.Length > 0 && buttons[0] != null)
        {
            targetY = buttons[0].anchoredPosition.y;
            targetPosition = new Vector2(fixedX, targetY);
            knob.anchoredPosition = targetPosition;
        }

        // Add hover events
        foreach (RectTransform btnRect in buttons)
        {
            if (btnRect == null) continue;

            Button btn = btnRect.GetComponent<Button>();
            if (btn == null) continue;

            EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = btn.gameObject.AddComponent<EventTrigger>();

            trigger.triggers.Clear();

            int index = System.Array.IndexOf(buttons, btnRect);

            EventTrigger.Entry enter = new EventTrigger.Entry();
            enter.eventID = EventTriggerType.PointerEnter;
            enter.callback.AddListener((data) => { MoveTo(index); });
            trigger.triggers.Add(enter);
        }
    }

    void Update()
    {
        // Smoothly move the knob
        knob.anchoredPosition = Vector2.Lerp(knob.anchoredPosition, targetPosition, smoothSpeed * Time.deltaTime);
    }

    void MoveTo(int index)
    {
        if (index >= 0 && index < buttons.Length && buttons[index] != null)
        {
            // Get the button's Y position
            float buttonY = buttons[index].anchoredPosition.y;

            // Keep X fixed, only change Y
            targetPosition = new Vector2(fixedX, buttonY);
        }
    }
}
