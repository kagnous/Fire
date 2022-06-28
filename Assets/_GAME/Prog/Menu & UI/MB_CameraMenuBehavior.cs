using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;


public class MB_CameraMenuBehavior : MonoBehaviour, ISelectHandler
{
    [SerializeField]
    private Transform _camera;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _timeToMove = 1f;
    [SerializeField]
    private UnityEvent _onHighLight;

    private void Awake()
    {
        if( _camera == null )
        {
            _camera = FindObjectOfType<Camera>().transform;
        }
    }
    public void OnSelect(BaseEventData eventData)
    {
        _camera.DOMove(_target.position, _timeToMove);
        _camera.DORotateQuaternion(_target.rotation, _timeToMove);
            //Debug.Log("Là");
        _onHighLight.Invoke();
    }

    public void OnHighlight(BaseEventData eventData)
    {
        _camera.DOMove(_target.position, _timeToMove);
        _camera.DORotateQuaternion(_target.rotation, _timeToMove);
        Debug.Log("Ici");
        _onHighLight.Invoke();
    }
}