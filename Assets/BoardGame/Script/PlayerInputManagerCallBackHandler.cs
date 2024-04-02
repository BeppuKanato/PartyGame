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
    //PlayerInput���V�����������ꂽ���̏���
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        int id = (int)playerInput.user.id;
        Debug.Log($"�ϊ��O = {playerInput.user.id}, �ϊ��� = {id}");
        inputSystem.AddPlayerInput(id, playerInput);
    }
    //PlayerInput�������Ȃ������̏���
    public void LeftPlayer()
    {
        Debug.Log("�v���C���[���ގ����܂���");
    }
}
