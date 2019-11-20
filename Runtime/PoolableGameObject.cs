using UnityEngine;

namespace Gameframe.Pooling
{
  /// <summary>
  /// MonoBehaviour which can be pooled in a GameObjectPool
  /// </summary>
  public class PoolableGameObject : MonoBehaviour
  {
    private GameObjectPool gameObjectPool = null;
    public GameObjectPool SourceGameObjectPool
    {
      get => gameObjectPool;
      set => gameObjectPool = value;
    }

    /// <summary>
    /// OnDestroy PoolableGameObject removes itself from its source game object pool
    /// </summary>
    protected virtual void OnDestroy()
    {
      //SourcePool may be null if this is a prefab object
      if ( SourceGameObjectPool != null )
      {
        SourceGameObjectPool.RemoveInstance(this);
      }
    }

    /// <summary>
    /// Called just before PoolableGameObject is returned as the result from a call to GameObjectPool.Get()
    /// </summary>
    public virtual void OnGetFromPool()
    {

    }

    /// <summary>
    /// Called when PoolableGameObject is about to be returned to a GameObjectPool
    /// </summary>
    public virtual void OnReleaseToPool()
    {

    }

    /// <summary>
    /// Release this object back to the pool it came from
    /// </summary>
    public void Release()
    {
      SourceGameObjectPool.Release(this);
    }

  }
}