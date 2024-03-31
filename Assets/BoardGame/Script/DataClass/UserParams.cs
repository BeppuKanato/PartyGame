using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

//���[�U���L�ɂȂ�p�����[�^�ݒ�
public class UserParams
{
    private static UserParams instance;

    public string nickName { get; private set; }        //���[�U�̃j�b�N�l�[��
    public string charPrefabName { get; private set; }  //�d�l����L�����N�^�[�̃v���n�u

    public bool isOnline { get; private set; }          //�I�����C���v���C��

    public int nPlayer { get; private set; }            //���[�J���̎Q�����Ă���l��
    private UserParams()
    {

    }
    //�V���O���g���݌v
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
        //�K�v�Ȃ�o���f�[�V�������s��
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
