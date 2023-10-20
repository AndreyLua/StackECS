using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickOffcetTransmitter : MonoBehaviour
{
    [SerializeField] private float _maxStickDistance;

    private JoystickUIScreen _screen;

    private Vector2 Offcet => ((Vector2)_screen.Stick.transform.position - stickPivot) / CalculateRealMaxStickDistance();
    private Vector2 stickPivot;
    private Vector2 _joystickOffcet;

    public Vector2 JoystickOffcet => _joystickOffcet;


    private void Awake()
    {
        _screen = gameObject.GetComponent<JoystickUIScreen>();

        _screen.Area.OnPointerDownEvent += OnJoystickEnable;
        _screen.Area.OnDraggingEvent += Dragging;
        _screen.Area.OnPointerUpEvent += OnJoystickDisable;
        OnJoystickDisable(null);

        _screen.OnActiveChange += OnActiveChange;
    }

    private void OnActiveChange(bool value)
    {
        OnJoystickDisable(null);
    }

    private void OnJoystickEnable(PointerEventData eventData)
    {
        _screen.Stick.gameObject.SetActive(true);
        _screen.StickArea.gameObject.SetActive(true);
        stickPivot = eventData.position;
        _screen.Stick.position = stickPivot;
        _screen.StickArea.position = _screen.Stick.position;
    }

    private void Dragging(PointerEventData eventData)
    {
        float distance = Vector2.Distance(eventData.position, stickPivot);
        if (distance <= CalculateRealMaxStickDistance())
            _screen.Stick.position = eventData.position;
        else _screen.Stick.position =  stickPivot + (eventData.position - stickPivot).normalized * CalculateRealMaxStickDistance();

        _joystickOffcet = Offcet;
    }

    private void OnJoystickDisable(PointerEventData eventData)
    {
        _screen.Stick.gameObject.SetActive(false);
        _screen.StickArea.gameObject.SetActive(false);

        _joystickOffcet = Vector2.zero;
    }

    private float CalculateRealMaxStickDistance()
    {
        return _maxStickDistance * (((Camera.main.pixelWidth / 1080f) + (Camera.main.pixelHeight / 1920f)) / 2f);
    }
}
