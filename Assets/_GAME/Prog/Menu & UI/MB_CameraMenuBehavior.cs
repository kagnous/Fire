using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;

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

    [SerializeField]
    private float _depthOfFieldValue = 1.5f;

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

        VolumeProfile volume = FindObjectOfType<Volume>().profile;
        volume.TryGet(out DepthOfField depthOfField);
        depthOfField.focusDistance.value = _depthOfFieldValue;
    }

    public void OnHighlight(BaseEventData eventData)
    {
        _camera.DOMove(_target.position, _timeToMove);
        _camera.DORotateQuaternion(_target.rotation, _timeToMove);
        Debug.Log("Ici");
        _onHighLight.Invoke();
    }
}