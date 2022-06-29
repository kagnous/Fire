using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MB_BackButton : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onBackButton;

    private void OnBackButton(InputAction.CallbackContext callbackContext)
    {
        _onBackButton.Invoke();
    }
}
