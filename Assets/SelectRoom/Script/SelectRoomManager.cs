using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SelectRoomManager : MonoBehaviour
{
    [SerializeField]
    Common.PhotonNetWorkManager netWorkManager;
    [SerializeField]
    GameObject roomListUI;
    [SerializeField]
    GameObject roomNameUI;
    [SerializeField]
    TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        UserParams userParams = UserParams.GetInstance();

        //ユーザ名を設定する(仮)
        userParams.SetNickName("プレイヤー1");
        //使用するキャラを選択(仮)
        userParams.SetCharPrefabName("TempModel");


        if (netWorkManager == null)
        {
            netWorkManager = Common.PhotonNetWorkManager.GetInstance();
        }
        netWorkManager.ConnectToServer(userParams.nickName);
    }

    void ShowRoomList()
    {
        foreach (RoomInfo room in netWorkManager.roomInfos)
        {
            GameObject roomNameClone = Instantiate(roomNameUI, Vector3.zero, Quaternion.identity);
            roomNameClone.AddComponent<RectTransform>();
            roomNameClone.transform.SetParent(roomListUI.transform);
        }
    }

    private void Update()
    {
    }

    //入力された部屋名で部屋を新規作成する
    public void CreateRoom()
    {
        netWorkManager.CreateNewRoom(inputField.text);
        StartCoroutine(SceneChangeCoroutine());
    }

    IEnumerator SceneChangeCoroutine()
    {
        yield return new WaitUntil(() => netWorkManager.currentRoom != null);

        ChangeToWaitingScene();
    }
    //待機シーンに遷移する
    void ChangeToWaitingScene()
    {
        Debug.Log($"待機シーンに遷移します");
        SceneManager.LoadScene("MatchMaking");
    }
}
