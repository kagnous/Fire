using UnityEngine;

public class WindInstancier : MonoBehaviour
{
    [SerializeField, Tooltip("wind prefab")]
    private GameObject _windPrefab;

    [SerializeField, Tooltip("Range of time between instanciations")]
    private Vector2 _windTimeRange;

    [SerializeField, Tooltip("range of start position, 1")]
    private Vector3 _posRangeOne;

    [SerializeField, Tooltip("range of start position, 2")]
    private Vector3 _posRangeTwo;

    [SerializeField, Tooltip("speed of wind")]
    private float _speed = 1;

    private float _timer = 0;
    private float _time = 0;

    private void Awake()
    {
        Init();

        _time = Random.Range(_windTimeRange.x, _windTimeRange.y);
    }

    private void Update()
    {
        if(_timer >= _time)
        {
            InstantiateWind();

            _time = Random.Range(_windTimeRange.x, _windTimeRange.y);
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Init();
    }

    public void Init()
    {
        if (_windPrefab == null)
        {
            _windPrefab = new GameObject();
            _windPrefab.name = "WindPrimitive";
            _windPrefab.AddComponent<WindManager>();
            _windPrefab.SetActive(false);
            _windPrefab.transform.SetParent(transform);
        }
    }

    private void InstantiateWind()
    {
        GameObject wind = Instantiate(_windPrefab, _windPrefab.transform.position, Quaternion.identity);
        wind.SetActive(true);

        wind.transform.position += (transform.right * Random.Range(_posRangeOne.x, _posRangeTwo.x)) + (transform.up * Random.Range(_posRangeOne.y, _posRangeTwo.y)) + (transform.forward * Random.Range(_posRangeOne.z, _posRangeTwo.z));
        wind.transform.eulerAngles = _windPrefab.transform.eulerAngles;

        wind.GetComponent<WindManager>().Speed = _speed;
        wind.transform.SetParent(transform);
    }
}
