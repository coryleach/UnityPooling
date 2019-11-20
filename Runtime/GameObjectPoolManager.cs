using System.Collections.Generic;
using UnityEngine;

namespace Gameframe.Pooling
{
  [CreateAssetMenu(menuName = "Gameframe/Pooling/PoolManager")]
  public class GameObjectPoolManager : ScriptableObject
  {
    private readonly Dictionary<PoolableGameObject, GameObjectPool> poolDictionary = new Dictionary<PoolableGameObject, GameObjectPool>();

    public GameObjectPool GetPool(PoolableGameObject prefab)
    {
      if ( poolDictionary.TryGetValue(prefab,out var pool) )
      {
        return pool;
      }

      pool = new GameObjectPool(prefab);

      poolDictionary.Add(prefab, pool);

      return pool;
    }
  }
}

