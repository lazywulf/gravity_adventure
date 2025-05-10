using System;

public interface IEventListener
{
	/// <summary>
	/// </summary>\
	/// 
	public void Subscribe(Action eventResponse);
	public void Unsubscribe();
	void OnEventTrigger();
}
