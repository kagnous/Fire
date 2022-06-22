using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class GameChosenScript : MonoBehaviour
{
    private int _index;

    [SerializeField]
    private UnityEvent _onChosen;

    [SerializeField]
    private UnityEvent _onUnchosen;

    public int Index 
    { 
        get { return _index; }
        set { if (value >= 0) { _index = value; } else { _index = 0; } }
    }

    public void OnChanged()
    {
        if (_index == 0)
        {
            _onChosen.Invoke();
        }
    }

    public void Unactivate()
    {
        _onUnchosen.Invoke();
    }

}
