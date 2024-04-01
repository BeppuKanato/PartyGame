using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Common
{
    public class InputSystemManager : MonoBehaviour
    {
        public delegate void InputCallBack(InputAction.CallbackContext context);

        [field: SerializeField]
        public PlayerInput playerInput { get; private set; }

        Dictionary<string, bool> addedActions = new Dictionary<string, bool>();

        private void Awake()
        {
            if (playerInput == null)
            {
                playerInput.GetComponent<PlayerInput>();
                Debug.LogError("playerInput���ݒ肳��Ă��܂���");
            }
            Debug.Log($"PlayerInput = {playerInput}");

            InputSystem.onDeviceChange += OnDeviceChange;
        }
        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            // �f�o�C�X���ڑ����ꂽ�ꍇ
            if (change == InputDeviceChange.Added)
            {
                Debug.Log($"�V�K�R���g���[�����ڑ�����܂���");
            }
        }
                //�w��A�N�V�����ɃR�[���o�b�N��ǉ�
        public void AddCallBack(string mapName, string actionName, System.Action<InputAction.CallbackContext> callBack)
        {
            InputActionMap actionMap = playerInput.actions.FindActionMap(mapName);
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
            DisableActionMap(mapName);
        }
        //�w��A�N�V�����̃R�[���o�b�N���폜
        public void RemoveCallBack(string mapName, string actionName, System.Action<InputAction.CallbackContext> callBack)
        {
            InputActionMap actionMap = playerInput.actions.FindActionMap(mapName);
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
        public void EnableActionMap(string mapName)
        {
            InputActionAsset actionAsset = playerInput.actions;
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
        public void DisableActionMap(string mapName)
        {
            InputActionAsset actionAsset = playerInput.actions;
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
    }
}