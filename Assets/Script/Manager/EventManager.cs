using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();
    public static EventManager Instance;

    void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void StartListening(string eventName, Action listener) {
        if (eventDictionary.TryGetValue(eventName, out Action thisEvent))
            thisEvent += listener;
        else
            eventDictionary[eventName] = listener;
    }

    public void StopListening(string eventName, Action listener) {
        if (eventDictionary.TryGetValue(eventName, out Action thisEvent))
            thisEvent -= listener;
    }

    public void TriggerEvent(string eventName) {
        if (eventDictionary.TryGetValue(eventName, out Action thisEvent))
            thisEvent.Invoke();
    }
}
