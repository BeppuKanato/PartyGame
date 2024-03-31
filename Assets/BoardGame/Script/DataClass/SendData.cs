using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//実際に送信するデータ
public class SendData
{
    public int actorNumber; //一意のユーザID
    public string content;  //データ本体
    public SendData(int actorNumber, string content)
    {
        this.actorNumber = actorNumber;
        this.content = content;
    }
}
