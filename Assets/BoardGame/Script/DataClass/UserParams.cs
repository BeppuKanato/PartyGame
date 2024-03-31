using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

//ユーザ特有になるパラメータ設定
public class UserParams
{
    private static UserParams instance;

    public string nickName { get; private set; }        //ユーザのニックネーム
    public string charPrefabName { get; private set; }  //仕様するキャラクターのプレハブ

    public bool isOnline { get; private set; }          //オンラインプレイか

    public int nPlayer { get; private set; }            //ローカルの参加している人数
    private UserParams()
    {

    }
    //シングルトン設計
    public static UserParams GetInstance()
    {
        if (instance == null)
        {
            instance = new UserParams();
        }

        return instance;
    }

    public void SetNickName(string nickName)
    {
        //必要ならバリデーションを行う
        this.nickName = nickName;
    }
    public void SetCharPrefabName(string name)
    {
        this.charPrefabName = name;
    }
    public void SetIsOnline(bool trigger)
    {
        isOnline = trigger;
    }
    public void SetNPlayer(int n)
    {
        nPlayer = n;
    }
}
