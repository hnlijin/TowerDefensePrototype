using System;
using UnityEngine;


/// <summary>
/// class GameUIController
///
/// The class provides a logic for in-game UI and updates it
/// using available data
/// </summary>

public class GameUIController
{
	protected GameUIView         mView;

	protected GamePersistentData mPersistentData;

	public GameUIController(GameUIView view, GamePersistentData data)
	{
		mView           = view ?? throw new ArgumentNullException("view");
		mPersistentData = data ?? throw new ArgumentNullException("data");

		EventBus.OnBaseHealthChanged   += _onHealthChanged;
		EventBus.OnEnemyDestroyed      += _onEnemyDestroyed;
		EventBus.OnNewWaveIsComing     += _onNewWaveIsComming;
		EventBus.OnNewTurretWasCreated += _onNewTurretWasCreated;

		_updateAvailableTurretsUIList(mPersistentData.mTurrets);

		mView.ScoreValue = mPersistentData.mCurrScore;
	}

	~GameUIController()
	{		
		EventBus.OnBaseHealthChanged   -= _onHealthChanged;
		EventBus.OnEnemyDestroyed      -= _onEnemyDestroyed;
		EventBus.OnNewWaveIsComing     -= _onNewWaveIsComming;
		EventBus.OnNewTurretWasCreated -= _onNewTurretWasCreated;
	}

	protected void _onHealthChanged(float value)
	{
		mView.HealthValue = value;
	}

	protected void _onEnemyDestroyed(uint reward)
	{
		mPersistentData.mCurrScore += reward;

		mView.ScoreValue = mPersistentData.mCurrScore;

		_updateAvailableTurretsUIList(mPersistentData.mTurrets);
	}

	protected void _onNewWaveIsComming(int waveIndex)
	{
		mView.WavesProgress = waveIndex;
	}

	protected void _onNewTurretWasCreated(uint turretPrice)
	{
		mPersistentData.mCurrScore = Math.Max(0, mPersistentData.mCurrScore - turretPrice);

		mView.ScoreValue = mPersistentData.mCurrScore;

		_updateAvailableTurretsUIList(mPersistentData.mTurrets);		
	}

	protected void _updateAvailableTurretsUIList(TurretsCollection turrets)
	{
		TurretUIEntityView[] turretsUISlots = mView.TurretUIEntityViewArray;

		TurretUIEntityView currTurretUISlot = null;

		uint currScore = mPersistentData.mCurrScore;

		GameObject currTurretGO = null;

		for (int i = 0; i < turretsUISlots.Length; ++i)
		{
			currTurretUISlot = turretsUISlots[i];

			currTurretGO = turrets[Convert.ToInt32(currTurretUISlot.mTurretEntityId)];

			currTurretUISlot.IsEnabled = (currTurretGO != null) && (currScore >= currTurretGO.GetComponentInChildren<GunComponent>().mConfigs.mPrice);
		}		
	}
}