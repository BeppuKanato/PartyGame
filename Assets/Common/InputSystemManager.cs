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
                Debug.LogError("playerInputが設定されていません");
            }

            Debug.Log($"PlayerInput = {playerInput}");
        }

        //指定アクションにコールバックを追加
        public void AddCallBack(string mapName, string actionName, System.Action<InputAction.CallbackContext> callBack)
        {
            InputActionMap actionMap = playerInput.actions.FindActionMap(mapName);
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
            DisableActionMap(mapName);
        }
        //指定アクションのコールバックを削除
        public void RemoveCallBack(string mapName, string actionName, System.Action<InputAction.CallbackContext> callBack)
        {
            InputActionMap actionMap = playerInput.actions.FindActionMap(mapName);
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
        public void EnableActionMap(string mapName)
        {
            InputActionAsset actionAsset = playerInput.actions;
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
        public void DisableActionMap(string mapName)
        {
            InputActionAsset actionAsset = playerInput.actions;
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
    }
}