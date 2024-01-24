using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    private PlayerFlashDetector _playerFlashDetector;
    private SpriteRenderer _spriteRenderer;
    private Color originalColor;
    [SerializeField,Tooltip("灵魂重新获取肉体后存在时间")] private float time = 5f;
    [SerializeField,Tooltip("闪烁时的颜色")] private Color flashingColor;
    [SerializeField, Tooltip("闪烁间隔时间")] private float flashingIntervalTime;
    private WaitForSeconds _waitForSeconds;
    private Coroutine flashing;
    private Coroutine extinguishingCoroutine;

    public bool ISHavePossessableFlesh => _playerFlashDetector.isHavePossessableFlesh;

    public Collider2D[] nearbyFlesh => _playerFlashDetector.flesh;
    
    private void Awake()
    {
        _playerFlashDetector = GetComponentInChildren<PlayerFlashDetector>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = _spriteRenderer.color;
        _waitForSeconds = new WaitForSeconds(flashingIntervalTime);
    }
    
    public void Extinguishing()
    {
        gameObject.tag = "Ownerless";
        StopFlash();
        extinguishingCoroutine = StartCoroutine(StartExtinguishing());
    }

    public void StopExtinguishing()
    {
        gameObject.tag = "Player";
        if(extinguishingCoroutine == null) return;
        StopCoroutine(extinguishingCoroutine);
    }

    private IEnumerator StartExtinguishing()
    {
        yield return new WaitForSeconds(time);
        //TODO 播放肉体消融动画
        //销毁灵魂
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
