using System.Collections.Generic;
using UnityEngine;

namespace Gameframe.Pooling
{
  [CreateAssetMenu(menuName = "Gameframe/Pooling/PoolManager")]
  public class PoolManager : ScriptableObject
  {
    private readonly Dictionary<PoolableGameObject, Pool> poolDictionary = new Dictionary<PoolableGameObject, Pool>();

    public Pool GetPool(PoolableGameObject prefab)
    {
      if ( poolDictionary.TryGetValue(prefab,out var pool) )
      {
        return pool;
      }

      pool = new Pool(prefab);

      poolDictionary.Add(prefab, pool);

      return pool;
    }
  }
}

