using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;



public class MB_ApertureBehavior : MonoBehaviour
{
    [SerializeField]
    private VolumeProfile _volume;

    [SerializeField]
    private float _minValue;

    [SerializeField]
    private float _maxValue;

    private MB_CameraBehavior _cameraBehavior;

    private float _testFloat;

    private void Awake()
    {
        _cameraBehavior = FindObjectOfType<MB_CameraBehavior>();

        if(_volume == null)
        {
            _volume = GetComponent<Volume>().profile;
        }
    }

    private void FixedUpdate()
    {
        _volume.TryGet<DepthOfField>(out DepthOfField depthOfField);

        _testFloat = (Camera.main.fieldOfView - _cameraBehavior.MaxZoom) / (_cameraBehavior.MaxZoom - _cameraBehavior.MaxDezoom);

        depthOfField.aperture.value = Mathf.Lerp(_minValue, _maxValue, Mathf.Abs(_testFloat));

    }


}
