using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering;

namespace Common
{
    public class InputSystemManager : MonoBehaviour
    {
        public delegate void InputCallBack(InputAction.CallbackContext context);
        [field: SerializeField]
        public PlayerInputManager playerInputManger { get; private set; }
        [SerializeField]
        public PlayerInputManagerCallBackHandler callBacks;

        [field: SerializeField]
        public Dictionary<int, PlayerInput> playerInput { get; private set; } = new Dictionary<int, PlayerInput>();

        private void Awake()
        {
            playerInputManger.playerJoinedEvent.AddListener(callBacks.OnPlayerJoined);
        }
        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
        }
        //�S�Ă�PlayerInput�ɃR�[���o�b�N��ݒ�
        public void AddCallBackToAllPlayerInput(string mapName, string actionName, System.Action<InputAction.CallbackContext> callBack)
        {
            foreach (int id in playerInput.Keys)
            {
                AddCallBack(mapName, actionName, callBack, id);
            }
        }

        //�w��A�N�V�����ɃR�[���o�b�N��ǉ�
        public void AddCallBack(string mapName, string actionName, System.Action<InputAction.CallbackContext> callBack, int id)
        {
            InputActionMap actionMap = playerInput[id].actions.FindActionMap(mapName);
            if (actionMap == null)
            {
                Debug.LogError($"{mapName}�A�N�V�����}�b�v�͍쐬����Ă��܂���");
                return;
            }
            else
            {
                Debug.Log($"�A�N�V�����}�b�v = {actionMap}");
            }

            InputAction action = actionMap.FindAction(actionName);
            if (action == null)
            {
                Debug.LogError($"{actionName}�A�N�V�����͍쐬����Ă��܂���");
                return;
            }

            action.performed += callBack;
            //�������i�K�ł͑S��Disable��Ԃɐݒ肷��
            DisableActionMap(mapName, id);
        }
        //�w��A�N�V�����̃R�[���o�b�N���폜
        public void RemoveCallBack(string mapName, string actionName, System.Action<InputAction.CallbackContext> callBack, int id)
        {
            InputActionMap actionMap = playerInput[id].actions.FindActionMap(mapName);
            if (actionMap == null)
            {
                Debug.LogError($"{mapName}�A�N�V�����}�b�v�͍쐬����Ă��܂���");
                return;
            }

            InputAction action = actionMap.FindAction(actionName);
            if (action == null)
            {
                Debug.LogError($"{actionName}�A�N�V�����͍쐬����Ă��܂���");
                return;
            }

            action.performed -= callBack;
        }

        //ActionMap��Enable�ɐݒ�
        public void EnableActionMap(string mapName, int id)
        {
            InputActionAsset actionAsset = playerInput[id].actions;
            InputActionMap actionMap = actionAsset.FindActionMap(mapName);
            if (actionMap == null)
            {
                Debug.LogError($"ActionMap{mapName}�͍쐬����Ă��܂���");
                return;
            }
            if (!actionMap.enabled)
            {
                actionMap.Enable();
            }
        }

        //ActionMap��Disable�ɐݒ�
        public void DisableActionMap(string mapName, int id)
        {
            InputActionAsset actionAsset = playerInput[id].actions;
            InputActionMap actionMap = actionAsset.FindActionMap(mapName);
            if (actionMap == null)
            {
                Debug.LogError($"ActionMap{mapName}�͍쐬����Ă��܂���");
                return;
            }
            if (actionMap.enabled)
            {
                actionMap.Disable();
            }
        }
        //PlayerInput�̔z��ɐV�K�ǉ�
        public void AddPlayerInput(int key, PlayerInput value)
        {
            playerInput.Add(key, value);
        }
    }
}