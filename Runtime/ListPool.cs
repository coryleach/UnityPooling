﻿using System.Collections.Generic;

namespace Gameframe.Pooling
{
  public static class ListPool<T>
  {
    // Object pool to avoid allocations.
    private static readonly ObjectPool<List<T>> listPool = new ObjectPool<List<T>>(null, l => l.Clear());

    public static List<T> Get()
    {
      return listPool.Get();
    }

    public static void Release(List<T> toRelease)
    {
      listPool.Release(toRelease);
    }
  }
}