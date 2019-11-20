using System.Collections.Generic;
using UnityEngine;

namespace Gameframe.Pooling
{
  /// <summary>
  /// Pool of PoolableGameObjects
  /// </summary>
  public class GameObjectPool
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="prefab">Prefab to be used by this pool.</param>
    public GameObjectPool(PoolableGameObject prefab)
    {
      _prefab = prefab;
    }

    private readonly PoolableGameObject _prefab;
    public PoolableGameObject Prefab => _prefab;

    private readonly List<PoolableGameObject> instanceQueue = new List<PoolableGameObject>();

    /// <summary>
    /// Get an instance from the pool.
    /// </summary>
    /// <param name="parent">Transform where instance will be parented or null if no parent.</param>
    /// <typeparam name="T">Type to cast gameobject to</typeparam>
    /// <returns>Instance of pooled prefab.</returns>
    public T Get<T>(Transform parent = null ) where T : PoolableGameObject
    {
      Debug.Assert(_prefab is T, "pool prefab is not the correct type");
      return Get(parent) as T;
    }

    /// <summary>
    /// Get an instance from the pool.
    /// </summary>
    /// <param name="parent">Transform where instance will be parented or null if no parent.</param>
    /// <returns>Instance of pooled prefab.</returns>
    public PoolableGameObject Get(Transform parent = null)
    {
      PoolableGameObject spawnedObject = null;

      if ( instanceQueue.Count > 0 )
      {
        spawnedObject = instanceQueue[0];
        instanceQueue.RemoveAt(0);
      }
      else
      {
        spawnedObject = parent != null ? Object.Instantiate(_prefab,parent) : Object.Instantiate(_prefab);
        spawnedObject.SourceGameObjectPool = this;
      }

      //If prefab is active, then also activate the spawned game object
      if ( Prefab.gameObject.activeSelf )
      {
        spawnedObject.gameObject.SetActive(true);
      }

      spawnedObject.OnGetFromPool();

      return spawnedObject;
    }

    /// <summary>
    /// Release object back to the pool
    /// </summary>
    /// <param name="spawnedObject">Object to be released.</param>
    public void Release(PoolableGameObject spawnedObject)
    {
      Debug.Assert(spawnedObject.SourceGameObjectPool == this, "Attempting to despawn an object to a pool that is not its spawn source");
      spawnedObject.OnReleaseToPool();
      spawnedObject.gameObject.SetActive(false);
      instanceQueue.Add(spawnedObject);
    }

    /// <summary>
    /// Completely remove an instance from the pool. Called by the PooledGameObject OnDestroy.
    /// </summary>
    /// <param name="instance">Object to be removed from the pool.</param>
    internal void RemoveInstance(PoolableGameObject instance)
    {
      instanceQueue.Remove(instance);
    }

  }
}