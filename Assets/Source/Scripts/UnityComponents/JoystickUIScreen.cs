using System;
using UnityEngine;

public class JoystickUIScreen : MonoBehaviour
{
    [SerializeField] private Transform stick;
    [SerializeField] private Transform stickArea;
    [SerializeField] private EventTriggerActionsHub area;

    public Transform Stick => stick;
    public Transform StickArea => stickArea;
    public EventTriggerActionsHub Area => area;

    public event Action<bool> OnActiveChange;

    public void SetJoystickActive(bool value)
    {
        area.gameObject.SetActive(value);
        OnActiveChange?.Invoke(value);
    }
}
