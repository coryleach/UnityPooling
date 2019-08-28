using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gameframe.Pooling
{
  public class ObjectPool<T> where T : new()
  {
    private readonly Stack<T> stack = new Stack<T>();
    private readonly UnityAction<T> onGet;
    private readonly UnityAction<T> onRelease;

    public int CountAll { get; private set; }
    public int CountActive => CountAll - CountInactive;
    public int CountInactive => stack.Count;

    public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
    {
      onGet = actionOnGet;
      onRelease = actionOnRelease;
    }

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