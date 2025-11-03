using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using events = Exiled.Events.Handlers;

namespace SCP575
{
	public class Plugin : Plugin<Config>
	{
		private EventHandlers EventHandlers;
		public override Version Version { get; } = new Version( 2, 0, 0 );
		public override Version RequiredExiledVersion { get; } = new Version( 9, 10, 0 );
		public override string Author { get; } = "OPGman";
		public override PluginPriority Priority { get; } = PluginPriority.Medium;

		public override void OnEnabled()
		{
			base.OnEnabled();
			EventHandlers = new EventHandlers( this );
			events.Server.RoundStarted += EventHandlers.OnRoundStart;
			events.Server.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
			events.Player.TriggeringTesla += EventHandlers.OnTriggerTesla;
			events.Scp079.GainingExperience += EventHandlers.OnGainExperience;
		}

		public override void OnDisabled()
		{
			base.OnDisabled();
			events.Server.RoundStarted -= EventHandlers.OnRoundStart;
			events.Server.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
			events.Player.TriggeringTesla -= EventHandlers.OnTriggerTesla;
			events.Scp079.GainingExperience -= EventHandlers.OnGainExperience;
			EventHandlers = null;
		}
	}
}
