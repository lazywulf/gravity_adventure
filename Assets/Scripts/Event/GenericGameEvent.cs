using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class GenericGameEvent<T> : ScriptableObject {
    protected readonly List<IGenericEventListener<T>> listeners = new List<IGenericEventListener<T>>();
    
    /// <summary>
    /// Raise this event and trigger all listeners.
    /// </summary>
    public virtual void Raise(T input) {
        foreach (var listener in listeners) {
            listener.OnEventTrigger(input);
		}
	}

    /// <summary>
    /// Add a listener to the listener list.
    /// </summary>
    /// <param name="listener"></param>
    public void AddListener(IGenericEventListener<T> listener) {
        if (!listeners.Contains(listener)) listeners.Add(listener);
	}

    /// <summary>
    /// Remove a listener from the listener list.
    /// </summary>
    /// <param name="listener"></param>
    public void RemoveListener(IGenericEventListener<T> listener) {
        if (!listeners.Contains(listener)) listeners.Remove(listener);
	}
}
