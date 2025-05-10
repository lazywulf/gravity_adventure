using System;

public interface IGenericEventListener<T> : IEventListener {
	/// <summary>
	/// </summary>\
	/// 
	public void Subscribe(Action<T> eventResponse);
	public new void Unsubscribe();
	public new void OnEventTrigger();
	public void OnEventTrigger(T input);
}
