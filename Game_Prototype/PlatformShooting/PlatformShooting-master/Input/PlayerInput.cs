using UnityEngine;

namespace Input
{
    public class PlayerInput : MonoBehaviour
    {
        PlayerInputAction _playerInputAction;
        Vector2 axes => _playerInputAction.GamePlay.HorizontalMovement.ReadValue<Vector2>();
        public float AxisX => axes.x;//水平移动
        public float AxisY => axes.y;//垂直方向的移动
    
        public bool Jump => _playerInputAction.GamePlay.Jump.WasPressedThisFrame();         //是否在这一帧按下了跳跃键
        public bool StopJump => _playerInputAction.GamePlay.Jump.WasReleasedThisFrame();    //是否在这一帧松开了跳跃键
        public bool Move => AxisX != 0f;        //水平输入信号

        public bool isProning => AxisY < 0f;//垂直方向信号输入小于0，即接收到玩家下蹲指令时，isProneing为true

        public bool Fire => _playerInputAction.GamePlay.Fire.WasPressedThisFrame();//是否在这帧按下了开火键
        
        
        void Awake()
        {
            _playerInputAction = new PlayerInputAction();
            EnableGamePlayInputs();
        }

        public void EnableGamePlayInputs()
        {
            _playerInputAction.GamePlay.Enable();//启用玩家输入
            Cursor.lockState = CursorLockMode.Locked;//锁定光标
        }
    }
}
