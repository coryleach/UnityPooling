using System.Collections;
using UnityEngine;

namespace Gameframe.Pooling
{
  public class PoolableParticleSystem : PoolableGameObject
  {
    [Tooltip("Will include child particle systems which checking if the system is still alive when true")]
    public bool checkChildren = true;
    
    [SerializeField]
    private ParticleSystem particles;
    public ParticleSystem Particles => particles;

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
      var system = Particles;
      yield return new WaitUntil( ()=> !system.IsAlive(checkChildren) );
      coroutine = null;
      Release();
    }
  }
}