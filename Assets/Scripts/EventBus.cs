using UnityEngine.Events;


/// <summary>
/// class EventBus
///
/// The class is a global event bus with in-game events
/// </summary>

public static class EventBus
{
	public static UnityAction<float> OnBaseHealthChanged;

	public static UnityAction<uint>  OnEnemyDestroyed;

	public static UnityAction<int>   OnNewWaveIsComing;

	public static UnityAction        OnLevelFinished;

	public static void NotifyOnBaseHealthChanged(float health)
	{
		OnBaseHealthChanged?.Invoke(health);
	}	

	public static void NotifyOnEnemyDestroyed(uint reward)
	{
		OnEnemyDestroyed?.Invoke(reward);
	}

	public static void NotifyOnNewWaveIsComing(int waveIndex)
	{
		OnNewWaveIsComing?.Invoke(waveIndex);
	}

	public static void NotifyOnLevelFinished()
	{
		OnLevelFinished?.Invoke();
	}
}