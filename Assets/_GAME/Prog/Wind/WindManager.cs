using UnityEngine;

public class WindManager : MonoBehaviour
{
    [SerializeField, Tooltip("material used to render trail")]
    private Material _material;

    [SerializeField, Tooltip("size of trail range")]
    private Vector2 _sizeRange;

    [SerializeField, Tooltip("duration of trail range")]
    private Vector2 _durationRange;

    [SerializeField, Tooltip("lifetime of particle")]
    private Vector2 _lifeTimeRange;

    [SerializeField, Tooltip("value of perlin noise length")]
    private Vector2 _perlinNoiseXRange;

    [SerializeField, Tooltip("value of perlin noise height")]
    private Vector2 _perlinNoiseYRange;

    [SerializeField, Tooltip("probability of looping every sec"), Range(0, 1)]
    private float _loopProba;

    [SerializeField]
    private Vector2Int _loopSizeRange;

    [SerializeField]
    private PatternState _currentPattern = PatternState.curve;

    /// <summary>
    /// trail of Wind
    /// </summary>
    private TrailRenderer _trailRenderer;

    private float _speed = 0;

    private float _life = 0;
    private float _timer = 0;
    private float _loopTimer = 0;
    private int _loopSize = 50;

    private float _perlinNoiseX = 0;
    private float _perlinNoiseY = 0;

    #region Public API

    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public enum PatternState
    {
        curve,
        loop,
    }

    #endregion

    private void Awake()
    {
        if (_trailRenderer == null)
            if (!TryGetComponent(out _trailRenderer))
                _trailRenderer = gameObject.AddComponent<TrailRenderer>();

        _life = Random.Range(_lifeTimeRange.x, _lifeTimeRange.y);
        _perlinNoiseX = Random.Range(_perlinNoiseXRange.x, _perlinNoiseXRange.y);
        _perlinNoiseY = Random.Range(_perlinNoiseYRange.x, _perlinNoiseYRange.y);

        SetTrailAttributes();
    }

    private void Update()
    {
        if (_timer >= _life)
        {
            if (_timer >= _life + 8)
                Destroy(gameObject); 
        }
        else
        {
            transform.position += (transform.forward * _speed) * Time.deltaTime;

            if (_currentPattern == PatternState.curve)
                CurveEffect();
            else if (_currentPattern == PatternState.loop)
                LoopEffect();
        }

        _timer += Time.deltaTime;
        _loopTimer += Time.deltaTime;
    }

    private void SetTrailAttributes()
    {
        _trailRenderer.material = _material;
        _trailRenderer.widthMultiplier = Random.Range(_sizeRange.x, _sizeRange.y);
        _trailRenderer.time = Random.Range(_durationRange.x, _durationRange.y);
    }

    private void CurveEffect()
    {
        transform.localEulerAngles = new Vector3((Mathf.PerlinNoise(transform.localPosition.z * _perlinNoiseX, 0) * _perlinNoiseY) - (_perlinNoiseY / 2), transform.localEulerAngles.y, transform.localEulerAngles.z);

        if (_loopTimer >= 1)
        {
            float proba = Random.Range(0f, 1f);

            if (proba <= _loopProba)
            {
                _currentPattern = PatternState.loop;
                _loopSize = Random.Range(_loopSizeRange.x, _loopSizeRange.y);
                _loopTimer = 0;
            }

            _loopTimer = 0;
        }
    }

    private void LoopEffect()
    {
        if (_loopTimer >= _loopSize)
            _currentPattern = PatternState.curve;

        float moveValue = _loopSize;
        transform.Rotate(new Vector3(-(360 / moveValue), 0, 0), Space.Self);

        _loopTimer++;
    }
}
