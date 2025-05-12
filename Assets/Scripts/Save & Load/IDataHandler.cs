using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataHandler
{
    /// <summary>把运行时 <see cref="GameData"/> 写入持久化存储</summary>
    void Save(GameData data);

    /// <summary>从持久化存储读取 <see cref="GameData"/>（若无数据返回 null）</summary>
    GameData Load();
}

