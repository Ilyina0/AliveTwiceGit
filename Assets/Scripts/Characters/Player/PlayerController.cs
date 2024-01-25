using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    #region 字段

    private PlayerInput _playerInput;
    private Rigidbody2D _rigidBody;
    private PlayerGroundDetector _playerGroundDetector;
    
    [Header("事件频道")]
    [SerializeField] private VoidEventChannel LevelClearedEvent;
    [SerializeField] private TransformEventChannel soulSeparationEventChannel;
    [SerializeField] private VoidEventChannel soulPossessEventChannel;
    [SerializeField] private VoidEventChannel FleshPossessEventChannel;
    [Header("灵魂相关")]
    public GameObject soulPrefab;
    public Vector2 soulGenerationOffset;
    [SerializeField, Tooltip("灵魂可脱离肉体时间")] private float soulIndependenceTime;
    [SerializeField, Tooltip("警告时间百分比")] private float waringPercentageOfTime;
    [Header("肉体相关")] 
    public GameObject playerFleshPrefab;
    public float restoreTime;

    private LayerMask originalLayer;

    private Coroutine soulCoroutine;
    
    
    #endregion

    #region 属性
    public float moveSpeed => Mathf.Abs(_rigidBody.velocity.x);
    public bool isGround => _playerGroundDetector.IsGround;
    public bool isOnOneWayPlatform => _playerGroundDetector.IsOneWayPlatformLayer;
    public bool isFalling => _rigidBody.velocity.y < 0.5f && !isGround;
    public AudioSource VoicePlayer { get; private set; }
    public bool Victory { get; private set; }
    public bool CanAirJump { get; set; }
    public Soul Soul {get; set; }
    public Flesh Flesh { get; set; }
    public bool isSoulSeparation { get; set; }

    #endregion
    
    #region Unity生命周期

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerGroundDetector = GetComponentInChildren<PlayerGroundDetector>();
        VoicePlayer = GetComponentInChildren<AudioSource>();
        Flesh = GetComponentInChildren<Flesh>();
        originalLayer = LayerMask.NameToLayer("Completeness");
    }

    private void Start()
    {
        _playerInput.EnableGamePlayInputs();
    }

    private void OnEnable()
    {
        LevelClearedEvent.AddListener(OnLevelCleared);
        soulSeparationEventChannel.AddListener(OnSoulSeparation);
        soulPossessEventChannel.AddListener(OnSoulPossess);
        FleshPossessEventChannel.AddListener(OnFleshPossess);
    }

    private void OnDisable()
    {
        LevelClearedEvent.RemoveListener(OnLevelCleared);
        soulSeparationEventChannel.RemoveListener(OnSoulSeparation);
        soulPossessEventChannel.RemoveListener(OnSoulPossess);
        FleshPossessEventChannel.RemoveListener(OnFleshPossess);
    }

    

    #endregion

    #region 方法

    #region 运动

    public void Move(float speed)
    {
        //镜像翻转
        if (_playerInput.Move)
        {
            transform.localScale = new Vector3(_playerInput.AxisX, 1, 1);
        }
        SetVelocityX(speed * _playerInput.AxisX);
    }

    public void SoulMove(Vector2 move)
    {
        //镜像翻转
        if (_playerInput.AxisX_Soul != 0)
        {
            Soul.transform.localScale = new Vector3(_playerInput.AxisX_Soul, 1, 1);
        }
        //灵魂移动
        Soul.transform.Translate(move);
    }
    public void SetVelocity(Vector3 velocity)
    {
        _rigidBody.velocity = velocity;
    }

    public void SetVelocityX(float velocityX)
    {
        _rigidBody.velocity = new Vector3(velocityX, _rigidBody.velocity.y);
    }
    
    public void SetVelocityY(float velocityY)
    {
        _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, velocityY);
    }
    /// <summary>
    /// 玩家刚体的启用与关闭
    /// </summary>
    /// <param name="value">启用或关闭</param>
    public void SetUseGravity(bool value)
    {
        _rigidBody.gravityScale = value ? 1 : 0;
    }
    #endregion
    
    #region 事件响应

    private void OnLevelCleared()
    {
        Victory = true;
    }
    
    
    public void OnDead()
    {
        //关闭玩家输入
        _playerInput.DisableGamePlayInputs();
        _playerInput.DisableSoulPlayInputs();
        //玩家进入漂浮状态
        SetVelocity(Vector3.zero);
        _rigidBody.gravityScale = 0;
        //状态机进入失败状态
        GetComponent<PlayerStateMachine>().SwitchState(typeof(PlayerState_Dead));
    }
    private void OnSoulSeparation(Transform obj)
    {
        isSoulSeparation = true;
        gameObject.layer = LayerMask.NameToLayer("Flesh");
        soulCoroutine = StartCoroutine(StartSoulSeparation());
    }

    private void OnSoulPossess()
    {
        isSoulSeparation = false;
        gameObject.layer = LayerMask.NameToLayer("Completeness");
        StopCoroutine(soulCoroutine);
        Soul.StopFlash();
        Destroy(Soul.gameObject);
        Soul = null;
    }
    private void OnFleshPossess()
    {
        isSoulSeparation = false;
        gameObject.layer = LayerMask.NameToLayer("Completeness");
        StopCoroutine(soulCoroutine);
        Soul = null;
    }
    private IEnumerator StartSoulSeparation()
    {
        //TODO 开启UI灵魂出体计时器
        yield return new WaitForSeconds(soulIndependenceTime * waringPercentageOfTime);
        //肉体，灵魂,UI开始警告
        Soul.StartFlash();
        yield return new WaitForSeconds(soulIndependenceTime * (1 - waringPercentageOfTime));
        isSoulSeparation = false;
        Soul.StopFlash();
        Destroy(Soul.gameObject);
        Soul = null;
        OnDead();
    }
    #endregion

    #region 单向平台

    public void OnWayPlatformCheck()
    {
        if (isGround && gameObject.layer != originalLayer)
        {
            gameObject.layer = originalLayer;
        }
        
        if (isOnOneWayPlatform && _playerInput.AxisY < -0.5f)
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            
            Invoke(nameof(RestorePlayerLayer),restoreTime);
        }
    }

    private void RestorePlayerLayer()
    {
        if (!isGround && gameObject.layer != originalLayer)
        {
            gameObject.layer = originalLayer;
        }
    }
    
    #endregion
    
    #endregion
    
    
    
}
