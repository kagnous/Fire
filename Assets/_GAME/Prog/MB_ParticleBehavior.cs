using UnityEngine;
using UnityEngine.InputSystem;

public class MB_ParticleBehavior : MonoBehaviour
{
    private Controls _inputsInstance;

    private ParticleSystem _particles;

    private void Awake()
    {
        _particles = GetComponentInChildren<ParticleSystem>();
        _inputsInstance = new Controls();
    }

    private void OnEnable()
    {
        _inputsInstance.Player.Enable();
        _inputsInstance.Player.Move.performed += ParticleEffective;
    }

    private void OnDisable()
    {
        _inputsInstance.Player.Move.performed -= ParticleEffective;
    }

    private void ParticleEffective(InputAction.CallbackContext context)
    {
            //Debug.Log(context.ReadValue<Vector2>());
        if(context.ReadValue<Vector2>() != Vector2.zero)
        {
            if(_particles.isStopped)
            _particles.Play();
        }
        else
        {
            _particles.Stop();
        }
    }
}
