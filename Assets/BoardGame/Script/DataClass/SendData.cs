using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ۂɑ��M����f�[�^
public class SendData
{
    public int actorNumber; //��ӂ̃��[�UID
    public string content;  //�f�[�^�{��
    public SendData(int actorNumber, string content)
    {
        this.actorNumber = actorNumber;
        this.content = content;
    }
}
