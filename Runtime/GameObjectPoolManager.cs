using System.Collections.Generic;
using UnityEngine;

namespace Gameframe.Pooling
{
  /// <summary>
  /// GameObjectPoolManager serves as a collection of pool objects that can be referenced by the inspector
  /// </summary>
  [CreateAssetMenu(menuName = "Gameframe/Pooling/PoolManager")]
  public class GameObjectPoolManager : ScriptableObject
  {
    
    private readonly Dictionary<PoolableGameObject, GameObjectPool> poolDictionary = new Dictionary<PoolableGameObject, GameObjectPool>();

    private void OnEnable()
    {
      //Clear pool dictionary so we don't carry over any old pools between runs in the editor
      poolDictionary.Clear();
    }

    /// <summary>
    /// Get Pool for a given prefab
    /// If no pool exists for the prefab one will be created
    /// </summary>
    /// <param name="prefab">Prefab that will be pooled</param>
    /// <returns>Pool of instances of the given prefab</returns>
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

