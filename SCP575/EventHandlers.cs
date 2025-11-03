using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using Respawning;
using System;
using System.Collections.Generic;

namespace SCP575
{
	public class EventHandlers
	{
		private Plugin plugin;
		private Random rand = new Random();
		private bool BlackoutActive = false;

		public EventHandlers( Plugin plugin ) => this.plugin = plugin;

		public IEnumerator<float> BlackoutLoop()
		{
			while ( true )
			{
				float delay = ( float ) rand.NextDouble() * ( plugin.Config.MaxDelay - plugin.Config.MinDelay ) + plugin.Config.MinDelay;
				yield return Timing.WaitForSeconds( delay );
				if ( rand.Next( 100 ) < plugin.Config.BlackoutChance )
				{
					float duration = ( float ) rand.NextDouble() * ( plugin.Config.MaxDuration - plugin.Config.MinDuration ) + plugin.Config.MinDuration;
					RespawnEffectsController.PlayCassieAnnouncement( plugin.Config.BlackoutStartSound, false, false );
					Map.TurnOffAllLights( duration );
					BlackoutActive = true;
					Timing.RunCoroutine( AttackLoop(), "575Attack" );
					yield return Timing.WaitForSeconds( duration );
					Timing.KillCoroutines( "575Attack" );
					RespawnEffectsController.PlayCassieAnnouncement( plugin.Config.BlackoutEndSound, false, false );
					BlackoutActive = false;
				}
			}
		}

		public IEnumerator<float> AttackLoop()
		{
			yield return Timing.WaitForSeconds( 5f );
			while ( true )
			{
				foreach ( Player ply in Player.List )
				{
					bool hasWorkingFlashlight = ply.HasFlashlightModuleEnabled || ( ply.CurrentItem is Flashlight flashlight && flashlight.IsEmittingLight );
					if ( ply.CurrentRoom.AreLightsOff && ply.IsHuman && !hasWorkingFlashlight )
						ply.Hurt( plugin.Config.Damage, "SCP-575" );
					yield return Timing.WaitForSeconds( 5f );
				}
			}
		}

		public void OnRoundStart()
		{
			Timing.RunCoroutine( BlackoutLoop(), "575Blackout" );
		}

		public void OnWaitingForPlayers()
		{
			Timing.KillCoroutines( "575Blackout" );
			BlackoutActive = false;
		}

		public void OnTriggerTesla( TriggeringTeslaEventArgs ev )
		{
			if ( BlackoutActive && plugin.Config.DisableTeslas )
				ev.IsAllowed = false;
		}
	}
}
