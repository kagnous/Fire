using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class MB_SelfDestroy : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeDestroy = 5;

    [SerializeField]
    private PlayableDirector _playableDirector;

    [SerializeField]
    private UnityEvent _controlsEnable;

    private bool _switch = true;

    void Start()
    {
        if (_playableDirector == null)
            if (!TryGetComponent(out _playableDirector))
                _playableDirector = gameObject.AddComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (_playableDirector.state != PlayState.Playing && _switch)
        {
            _controlsEnable.Invoke();
            Destroy(gameObject);
            _switch = false;
        }
    }
}