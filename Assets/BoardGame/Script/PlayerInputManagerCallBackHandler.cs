using Common;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManagerCallBackHandler : MonoBehaviour
{
    [SerializeField]
    InputSystemManager inputSystem;
    //PlayerInputが新しく生成された時の処理
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        int id = (int)playerInput.user.id;
        Debug.Log($"変換前 = {playerInput.user.id}, 変換後 = {id}");
        inputSystem.AddPlayerInput(id, playerInput);
    }
    //PlayerInputが無くなった時の処理
    public void LeftPlayer()
    {
        Debug.Log("プレイヤーが退室しました");
    }
}
