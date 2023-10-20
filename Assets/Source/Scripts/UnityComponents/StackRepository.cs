using System;
using System.Collections.Generic;
using UnityEngine;

public class StackRepository<T, ID> where T : IStackable<ID> where ID : struct
{
    private Dictionary<IStackHolder, Data> _data;

    public event Action<IStackHolder, T> OnAdd;
    public event Action<IStackHolder, T> OnExtract;
    public event Action<IStackHolder, T> OnStateChange;

    public void Init()
    {
        _data = new Dictionary<IStackHolder, Data>();
    }

    public void TickAnimation(float frameTime)
    {
        foreach (Data data in _data.Values)
            data.StackAnimationCore.TickAnimation(frameTime);
    }

    public Pose CalculateNewItemLocalPose(IStackHolder stackHolder, T t)
    {
        CheckType(stackHolder);
        return _data[stackHolder].StackAnimationCore.CalculateNewElementLocalPose(t);
    }

    public bool Contains(IStackHolder stackHolder, Predicate<T> predicate)
    {
        CheckType(stackHolder);

        foreach (T t in _data[stackHolder].Elements)
            if (predicate(t))
                return true;

        return false;
    }

    public void AddElement(IStackHolder stackHolder, T t)
    {
        CheckType(stackHolder);
        _data[stackHolder].Elements.Add(t);
        t.Transform.SetParent(stackHolder.StackParent);
        OnAdd?.Invoke(stackHolder, t);
        OnStateChange?.Invoke(stackHolder, t);
    }

    public T GetLastElement(IStackHolder stackHolder, bool extract = false)
    {
        CheckType(stackHolder);
        int index = _data[stackHolder].Elements.Count - 1;
        T t = _data[stackHolder].Elements[index];

        if (extract)
        {
            _data[stackHolder].Elements.RemoveAt(index);
            t.Transform.parent = null;
            OnExtract?.Invoke(stackHolder, t);
            OnStateChange?.Invoke(stackHolder, t);
        }

        return t;
    }

    public T GetElement(IStackHolder stackHolder, ID? type, bool extract = false, Predicate<T> predicate = null)
    {
        CheckType(stackHolder);

        int index = _data[stackHolder].Elements
            .FindLastIndex(s => (!type.HasValue || EqualityComparer<ID>.Default.Equals(s.Identifier, type.Value)) 
            && (predicate is null || predicate(s)));

        T t = _data[stackHolder].Elements[index];

        if (extract)
        {
            _data[stackHolder].Elements.RemoveAt(index);
            t.Transform.parent = null;
            OnExtract?.Invoke(stackHolder, t);
            OnStateChange?.Invoke(stackHolder, t);
        }

        return t;
    }

    public int GetElementsCount(IStackHolder stackHolder)
    {
        CheckType(stackHolder);
        return _data[stackHolder].Elements.Count;
    }

    private void CheckType(IStackHolder stackHolder)
    {
        if (!_data.ContainsKey(stackHolder))
            _data.Add(stackHolder, new Data(stackHolder));
    }


    private class Data
    {
        public readonly IStackHolder StackHolder;
        public readonly List<T> Elements;
        public readonly StackAnimationCore StackAnimationCore;

        public Data(IStackHolder stackHolder)
        {
            StackHolder = stackHolder;
            Elements = new List<T>();
            StackAnimationCore = new StackAnimationCore(StackHolder, (IReadOnlyList<IStackable>)Elements);
        }
    }
}
