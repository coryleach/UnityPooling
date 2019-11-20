using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameframe.Pooling
{
  /// <summary>
  /// Pool of objects
  /// </summary>
  /// <typeparam name="T">Type of object that will be pooled. Must support a parameter-less constructor.</typeparam>
  public class ObjectPool<T> where T : new()
  {
    private readonly Stack<T> stack = new Stack<T>();
    private readonly UnityAction<T> onGet;
    private readonly UnityAction<T> onRelease;

    /// <summary>
    /// Total size of pool including both allocated and released objects
    /// </summary>
    public int CountAll { get; private set; }
    
    /// <summary>
    /// Number of objects allocated outside the pool
    /// </summary>
    public int CountActive => CountAll - CountInactive;
    
    /// <summary>
    /// Number of objects in the pool but not allocated
    /// </summary>
    public int CountInactive => stack.Count;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="actionOnGet">Action called on the object before it is returned from a Get() call.</param>
    /// <param name="actionOnRelease">Action called on an object when it is released back to the pool.</param>
    public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
    {
      onGet = actionOnGet;
      onRelease = actionOnRelease;
    }

    /// <summary>
    /// Get an object from the pool
    /// </summary>
    /// <returns>re-usable object instance</returns>
    public T Get()
    {
      T element;

      if (stack.Count == 0)
      {
        element = new T();
        CountAll++;
      }
      else
      {
        element = stack.Pop();
      }

      onGet?.Invoke(element);

      return element;
    }

    /// <summary>
    /// Release an object back to the pol
    /// </summary>
    /// <param name="element">Object to be returned to the pool.</param>
    public void Release(T element)
    {
      if (stack.Count > 0 && ReferenceEquals(stack.Peek(), element))
      {
        Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
      }

      onRelease?.Invoke(element);

      stack.Push(element);
    }
  }
}