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

        //���[�U����ݒ肷��(��)
        userParams.SetNickName("�v���C���[1");
        //�g�p����L������I��(��)
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

    //���͂��ꂽ�������ŕ�����V�K�쐬����
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
    //�ҋ@�V�[���ɑJ�ڂ���
    void ChangeToWaitingScene()
    {
        Debug.Log($"�ҋ@�V�[���ɑJ�ڂ��܂�");
        SceneManager.LoadScene("MatchMaking");
    }
}
