using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flesh : MonoBehaviour
{
    private Animator _animator;
    private PlayerSoulDetector _playerSoulDetector;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    
    [SerializeField,Tooltip("肉体重新获取灵魂后存在时间")] private float time = 5f;
    [SerializeField,Tooltip("闪烁时的颜色")] private Color flashingColor;
    [SerializeField, Tooltip("闪烁间隔时间")] private float flashingIntervalTime;
    
    private Coroutine extinguishingCoroutine;
    private Coroutine flashing;
    private WaitForSeconds _waitForSeconds;
    private Color originalColor;

    
    
    #region 属性
    public bool ISHavePossessableSoul => _playerSoulDetector.isHavePossessableSoul;
    public Collider2D[] NearbySouls => _playerSoulDetector.souls;
    #endregion
    


    private void Awake()
    {
        _transform = transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _playerSoulDetector = GetComponentInChildren<PlayerSoulDetector>();
        _waitForSeconds = new WaitForSeconds(flashingIntervalTime);
        _collider = GetComponent<Collider2D>();
        //originalColor = _spriteRenderer.color;
    }
    
    public void Extinguishing()
    {
        gameObject.tag = "Ownerless";
        StopFlash();
        _collider.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Flesh");
        extinguishingCoroutine = StartCoroutine(StartExtinguishing());
    }

    public void StopExtinguishing()
    {
        gameObject.tag = "Player";
        _collider.enabled = false;
        if(extinguishingCoroutine == null) return;
        StopCoroutine(extinguishingCoroutine);
    }

    private IEnumerator StartExtinguishing()
    {
        _transform.SetParent(null);
        yield return new WaitForSeconds(time);
        //TODO 播放肉体消融动画
        //销毁肉体
        Destroy(gameObject);
    }
    public void StartFlash()
    {
        flashing = StartCoroutine(Flashing());
    }
    public void StopFlash()
    {
        if(flashing == null) return;
        StopCoroutine(flashing);
    }
    private IEnumerator Flashing()
    {
        while (true)
        {
            _spriteRenderer.color = flashingColor;
            yield return _waitForSeconds;
            _spriteRenderer.color = originalColor;
            yield return _waitForSeconds;
        }
    }
}
