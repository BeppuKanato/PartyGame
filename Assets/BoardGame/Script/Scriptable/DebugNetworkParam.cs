using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DebugNetworkParam : ScriptableObject
{
    [Tooltip("オンラインでデバッグモードを実行するか")]
    public bool isOnline;
    [Tooltip("プレイヤーの参加人数")]
    public int nPlayer;
    [Tooltip("ルームの名前")]
    public string roomName;
    public int playerID;
    [Tooltip("使用するキャラクタープレハブの名前")]
    public string charPrefabName;
}
