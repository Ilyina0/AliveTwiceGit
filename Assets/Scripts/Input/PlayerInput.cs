using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField,Tooltip("跳跃指令预输入保存时间")] private float jumpInputBufferTime = 0.5f;
    #region 肉体模式输入参数
    private Vector2 axis => playerInputActions.GamePlay.Axis.ReadValue<Vector2>();
    public bool Jump => playerInputActions.GamePlay.Jump.WasPressedThisFrame();
    public bool HasJumpInputBuffer { get; set; }
    public bool StopJump => playerInputActions.GamePlay.Jump.WasReleasedThisFrame();
    public bool SoulSeparation => playerInputActions.GamePlay.SoulSeparation.WasPressedThisFrame();
    public bool FleshPossession => playerInputActions.GamePlay.FleshPossession.WasPressedThisFrame();
    public bool Move => AxisX != 0;
    public float AxisX => axis.x;

    #endregion

    #region 灵魂模式输入参数

    private Vector2 axis_soul => playerInputActions.SoulPlay.Axis.ReadValue<Vector2>();
    public bool ChangeToFlesh => playerInputActions.SoulPlay.ChangeToFlesh.WasPressedThisFrame();
    public bool SpiritPossession => playerInputActions.SoulPlay.SpiritPossession.WasPressedThisFrame();
    public bool Move_Soul => axis_soul != Vector2.zero;
    public float AxisX_Soul => axis_soul.x;
    public float AxisY_Soul => axis_soul.y;

    #endregion

    private WaitForSeconds waitJumpInputBufferTime;
    
    private PlayerInputActions playerInputActions;
    
    
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        waitJumpInputBufferTime = new WaitForSeconds(jumpInputBufferTime);
        EnableGamePlayInputs();
        DisableGamePlayInputs();
    }

    private void OnEnable()
    {
        playerInputActions.GamePlay.Jump.canceled += delegate
        {
            //取消按键时清除按键缓冲值
            HasJumpInputBuffer = false;
        };
    }

    public void EnableGamePlayInputs()
    {
        playerInputActions.GamePlay.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void DisableGamePlayInputs()
    {
        playerInputActions.GamePlay.Disable();
    }

    public void EnableSoulPlayInputs()
    {
        playerInputActions.SoulPlay.Enable();
    }
    
    public void DisableSoulPlayInputs()
    {
        playerInputActions.SoulPlay.Disable();
    }
    /// <summary>
    /// 存储预输入
    /// </summary>
    public void SetJumpInputBuffer()
    {
        StartCoroutine(JumpInputBufferCoroutine());
    }
    
    private IEnumerator JumpInputBufferCoroutine()
    {
        HasJumpInputBuffer = true;
        yield return waitJumpInputBufferTime;
        HasJumpInputBuffer = false;
    }
}
