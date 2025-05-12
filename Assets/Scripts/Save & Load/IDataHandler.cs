using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataHandler
{
    /// <summary>������ʱ <see cref="GameData"/> д��־û��洢</summary>
    void Save(GameData data);

    /// <summary>�ӳ־û��洢��ȡ <see cref="GameData"/>���������ݷ��� null��</summary>
    GameData Load();
}

