using System.Collections.Generic;

namespace Gameframe.Pooling
{
  /// <summary>
  /// Pool of lists for the given type.
  /// </summary>
  /// <typeparam name="T">Any object or value type that the list will contain</typeparam>
  public static class ListPool<T>
  {
    // Object pool to avoid allocations.
    private static readonly ObjectPool<List<T>> listPool = new ObjectPool<List<T>>(null, l => l.Clear());

    /// <summary>
    /// Get a list instance from the static pool
    /// </summary>
    /// <returns>List removed from the pool or a new list if pool was empty.</returns>
    public static List<T> Get()
    {
      return listPool.Get();
    }

    /// <summary>
    /// Release a list instance back to the static pool
    /// </summary>
    /// <param name="toRelease">List to be returned</param>
    public static void Release(List<T> toRelease)
    {
      listPool.Release(toRelease);
    }
  }
}