using UnityEngine;
using UnityEngine.InputSystem;

public class MB_CameraBehavior : MonoBehaviour
{
    [Header("Diorama Rotation")]
    [SerializeField]
    [Tooltip("Diorama that will rotate")]
    private Transform _diorama;
    
    // Final speed applied to the rotation
    private float _movementRot;

    [SerializeField]
    [Tooltip("Speed which the rotation will get the maximal speed")]
    private float _rotAcceleration = 0.1f;

    [SerializeField]
    [Tooltip("Speed of the rotation")]
    private float _maxRotSpeed = 3f;

    // Getter of the rotation Input
    private float _rotInput;

    [Header("Camera Up and Down")]
    [SerializeField]
    [Tooltip("Maximal position of the camera position up")]
    private float _maxCameraUp;
    [SerializeField]
    [Tooltip("Maximal position of the camera position down")]
    private float _maxCameraDown;

    [SerializeField]
    [Tooltip("Speed which the movement will get the maximal speed")]
    private float _upAcceleration = 0.1f;

    [SerializeField]
    [Tooltip("Speed of the camera movement")]
    private float _maxUpSpeed = 3f;

    // Final speed applied to the camera movement
    private float _upMovement;

    // getting the camera position Input
    private float _upInput;

    // Copy of the rotation (way more precise than the original euler angles that are fucking shit)
    private float _baseMove = 0;

    [Header("Zoom/Dezoom")]

    [SerializeField]
    [Tooltip("Maximal FOV of the zoom")]
    private float _maxZoom;
    [SerializeField]
    private float _maxDezoom;

    [SerializeField]
    [Tooltip("Speed to get the maximal speed")]
    private float _zoomAcceleration = 0.1f;

    [SerializeField]
    [Tooltip("Maximal speed of the zoom")]
    private float _maxZoomSpeed = 3f;


    // Final number applied to the zoom
    private float _zoomMovement;

    // Input getter of the zoom
    private float _zoomInput;



    private void FixedUpdate()
    {
        #region Diorama Rotation

        //Gets the Rotation to apply
        _movementRot = CreateMovement(_rotInput, _movementRot, _maxRotSpeed * Mathf.Abs(_rotInput), _rotAcceleration);

        //Applies the rotation to the diorama
        _diorama.Rotate(new Vector2(0, _movementRot));

        #endregion

        #region Camera Movement

        // Gets the camera movement to apply
        _upMovement = CreateMovement(_upInput,_upMovement, _maxUpSpeed * Mathf.Abs(_upInput), _upAcceleration);

        // Verifies if the next movement does not exceeds the limits
        if(_baseMove + _upMovement > _maxCameraDown && _baseMove + _upMovement < _maxCameraUp)
        {
            // Applies the camera rotation
            transform.Rotate(new Vector2(_upMovement, 0));
            // Adds to the fake rotation
            _baseMove += _upMovement;
        }
        else
        {
            // Reset the movement
            _upMovement = 0;
        }

        #endregion

        #region Camera Zoom

        // Gets the rotation to apply
        _zoomMovement = CreateMovement(_zoomInput, _zoomMovement, _maxZoomSpeed * Mathf.Abs(_zoomInput), _zoomAcceleration);

        // Gets the main camera
        Camera camera = Camera.main;

        // Verifies if the next move does not exceed the limits
        if(camera.fieldOfView + _zoomMovement < _maxDezoom && camera.fieldOfView + _zoomMovement > _maxZoom)
        {
            // Applies the zoom movement
            camera.fieldOfView += _zoomMovement;
        }
        else
        {
            // Reset the movement if the move exceeds the limits
            _zoomMovement = 0;
        }

        #endregion
    }

    #region Movement function

    /// <summary>
    /// Gets a float and returns the next movement to do with inertia
    /// </summary>
    /// <param name="input"></param base input to get (with a value between -1 and 1 or 0 and 1)>
    /// <param name="movement"></param float that will be modified>
    /// <param name="maxSpeed"></param maximal speed obtainable>
    /// <param name="acceleration"></param speed to get to the float speed>
    /// <returns>float to apply to the movement float</returns>
    public static float CreateMovement(float input, float movement, float maxSpeed, float acceleration)
    {
        // Gets if the input is positive
        if (input > 0)
        {
            // verifies if the movement is inferior to the maximal speed
            if (movement < maxSpeed)
            {
                // adds to the input with the acceleration
                movement += input * acceleration;
            }
        }
        // Gets if the input is negative
        else if (input < 0)
        {
            // verifies if the movement is supperior to the negative of the maximal speed
            if (movement > -maxSpeed)
            {
                movement += input * acceleration;
            }
        }
        // if there is no Input to the player
        else
        {
            // bring the movement closer to the 0 until it's inferior to the acceleration so it gets to 0
            if (movement > acceleration)
            {
                movement -= acceleration;
            }
            else if (movement < -acceleration)
            {
                movement += acceleration;
            }
            else if (movement != 0)
            {
                movement = 0;
            }
        }

        return movement;
    }

    #endregion

    #region InputsFunctions

    // Gets the rotation input
    public void GetRotationInput(InputAction.CallbackContext callbackContext)
    {
        _rotInput = callbackContext.ReadValue<float>();
    }

    // Gets the camera movement input
    public void GetUpDownInput(InputAction.CallbackContext callbackContext)
    {
        _upInput = callbackContext.ReadValue<float>();
    }

    // Gets the Zoom Input
    public void GetZoomInput(InputAction.CallbackContext callbackContext)
    {
        _zoomInput = callbackContext.ReadValue<float>();
    }

    // Gets the dezoom Input
    public void GetDezoomInput(InputAction.CallbackContext callbackContext)
    {
        _zoomInput = -callbackContext.ReadValue<float>();
    }

    #endregion

}
