using System.Collections;
using UnityEngine;

namespace Gameframe.Pooling
{
  /// <summary>
  /// Can be used on a ParticleSystem object
  /// Will return itself to the pool when the particle system is no longer active.
  /// </summary>
  public class PoolableParticleSystem : PoolableGameObject
  {
    [Tooltip("Will include child particle systems which checking if the system is still alive when true")]
    public bool checkChildren = true;
    
    [SerializeField]
    private ParticleSystem particles;
    private Coroutine coroutine = null;
    
    private void OnEnable()
    {
      if ( particles == null )
      {
        particles = GetComponent<ParticleSystem>();
      }
      coroutine = StartCoroutine(WaitForParticleSystem());
    }

    private void OnDisable()
    {
      if (coroutine != null)
      {
        Release();
      }
    }

    private IEnumerator WaitForParticleSystem()
    {
      var system = particles;
      yield return new WaitUntil( ()=> !system.IsAlive(checkChildren) );
      coroutine = null;
      Release();
    }

    private void OnValidate()
    {
      if (particles == null)
      {
        particles = GetComponent<ParticleSystem>();
      }
    }
    
  }
  
}