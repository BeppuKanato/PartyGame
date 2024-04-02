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
        //全てのPlayerInputにコールバックを設定
        public void AddCallBackToAllPlayerInput(string mapName, string actionName, System.Action<InputAction.CallbackContext> callBack)
        {
            foreach (int id in playerInput.Keys)
            {
                AddCallBack(mapName, actionName, callBack, id);
            }
        }

        //指定アクションにコールバックを追加
        public void AddCallBack(string mapName, string actionName, System.Action<InputAction.CallbackContext> callBack, int id)
        {
            InputActionMap actionMap = playerInput[id].actions.FindActionMap(mapName);
            if (actionMap == null)
            {
                Debug.LogError($"{mapName}アクションマップは作成されていません");
                return;
            }
            else
            {
                Debug.Log($"アクションマップ = {actionMap}");
            }

            InputAction action = actionMap.FindAction(actionName);
            if (action == null)
            {
                Debug.LogError($"{actionName}アクションは作成されていません");
                return;
            }

            action.performed += callBack;
            //初期化段階では全てDisable状態に設定する
            DisableActionMap(mapName, id);
        }
        //指定アクションのコールバックを削除
        public void RemoveCallBack(string mapName, string actionName, System.Action<InputAction.CallbackContext> callBack, int id)
        {
            InputActionMap actionMap = playerInput[id].actions.FindActionMap(mapName);
            if (actionMap == null)
            {
                Debug.LogError($"{mapName}アクションマップは作成されていません");
                return;
            }

            InputAction action = actionMap.FindAction(actionName);
            if (action == null)
            {
                Debug.LogError($"{actionName}アクションは作成されていません");
                return;
            }

            action.performed -= callBack;
        }

        //ActionMapをEnableに設定
        public void EnableActionMap(string mapName, int id)
        {
            InputActionAsset actionAsset = playerInput[id].actions;
            InputActionMap actionMap = actionAsset.FindActionMap(mapName);
            if (actionMap == null)
            {
                Debug.LogError($"ActionMap{mapName}は作成されていません");
                return;
            }
            if (!actionMap.enabled)
            {
                actionMap.Enable();
            }
        }

        //ActionMapをDisableに設定
        public void DisableActionMap(string mapName, int id)
        {
            InputActionAsset actionAsset = playerInput[id].actions;
            InputActionMap actionMap = actionAsset.FindActionMap(mapName);
            if (actionMap == null)
            {
                Debug.LogError($"ActionMap{mapName}は作成されていません");
                return;
            }
            if (actionMap.enabled)
            {
                actionMap.Disable();
            }
        }
        //PlayerInputの配列に新規追加
        public void AddPlayerInput(int key, PlayerInput value)
        {
            playerInput.Add(key, value);
        }
    }
}