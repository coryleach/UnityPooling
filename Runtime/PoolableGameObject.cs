using UnityEngine;

namespace Gameframe.Pooling
{
  public class PoolableGameObject : MonoBehaviour
  {
    private GameObjectPool gameObjectPool = null;
    public GameObjectPool SourceGameObjectPool
    {
      get => gameObjectPool;
      set => gameObjectPool = value;
    }

    public virtual void OnDestroy()
    {
      //SourcePool may be null if this is a prefab object
      if ( SourceGameObjectPool != null )
      {
        SourceGameObjectPool.RemoveInstance(this);
      }
    }

    public virtual void OnGetFromPool()
    {

    }

    public virtual void OnReleaseToPool()
    {

    }

    public void Release()
    {
      SourceGameObjectPool.Release(this);
    }

  }
}