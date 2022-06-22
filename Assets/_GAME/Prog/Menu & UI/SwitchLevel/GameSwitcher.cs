using UnityEngine.Rendering;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameSwitcher : MonoBehaviour
{
    [SerializeField]
    private Transform[] _points;

    [SerializeField]
    private GameObject[] _levels;

    [SerializeField]
    private float _animationSpeed = 0.5f;

    private int[] _indexposition;

    private int _index = 0;

    [SerializeField]
    private UnityEvent _level1Selected;

    [SerializeField]
    private UnityEvent _level2Selected;

    [SerializeField]
    private UnityEvent _level3Selected;

    private void Start()
    {
        if(_levels.Length != _points.Length)
        {
            Debug.LogError("Points and Game Characters are not the same size. An error will occur.");
        }
        
        _indexposition = new int[_points.Length];

        for(int i = 0; i < _levels.Length; i++)
        {
            _levels[i].transform.position = _points[i].position;

            _indexposition[i] = i;

            _levels[i].GetComponent<GameChosenScript>().Index = i;
            _levels[i].GetComponent<GameChosenScript>().Unactivate();
        }

        ChangeObjects();
    }

    public void RightLeftButton(int value)
    {
        _index += value;

        if(_index >= _levels.Length)
        {
            _index = 0;
        }
        else if (_index < 0)
        {
            _index = _levels.Length - 1;
        }

        for (int i = 0; i < _levels.Length; i++)
        {
            int index = _indexposition[i];

            Debug.Log($"index : {index}");

            index+= value;

            if(index >= _levels.Length)
            {
                index = 0;
            }
            else if (index < 0)
            {
                index = _levels.Length - 1;
            }

            Debug.Log(index);

            _levels[i].transform.DOMove(_points[index].position, _animationSpeed);

            _levels[i].GetComponent<GameChosenScript>().Index = index;

            _indexposition[i] = index;

            _levels[i].GetComponent<GameChosenScript>().Unactivate();

            Invoke("ChangeObjects", _animationSpeed);
        }
    }

    private void ChangeObjects()
    {
        foreach(GameObject game in _levels)
        {
            game.GetComponent<GameChosenScript>().OnChanged();
        }
    }

    public void ChangePositionToChoose(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.started)
        {
            RightLeftButton((int)callbackContext.ReadValue<float>());
        }
    }

    public void ChooseLevel(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.started)
        {
            if(_index == 0)
            {
                _level1Selected.Invoke();
            }
            else if(_index == 1)
            {
                _level2Selected.Invoke();
            }
            else if(_index == 2)
            {
                _level3Selected.Invoke();
            }
        }
    }
    
}
