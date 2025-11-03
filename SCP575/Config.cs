using System.ComponentModel;
using Exiled.API.Interfaces;

namespace SCP575
{
	public sealed class Config : IConfig
	{
		[Description( "Indicates whether the plugin is enabled or not" )]
		public bool IsEnabled { get; set; } = true;

		[Description( "Whether or not debug messages should be shown in the console." )]
		public bool Debug { get; set; } = false;

		[Description( "Minimum amount of seconds between each check to activate a blackout." )]
		public float MinDelay { get; set; } = 300f;

		[Description( "Maxmimum amount of seconds between each check to activate a blackout." )]
		public float MaxDelay { get; set; } = 600f;

		[Description( "Percentage chance that a blackout will happen after each check." )]
		public int BlackoutChance { get; set; } = 40;

		[Description( "Minimum amount of seconds the blackout can last." )]
		public float MinDuration { get; set; } = 30f;

		[Description( "Maximum amount of seconds the blackout can last." )]
		public float MaxDuration { get; set; } = 90f;

		[Description( "Amount of damage that SCP-575 deals every 5 seconds during a blackout." )]
		public float Damage { get; set; } = 10f;

		[Description( "Whether or not tesla gates should be disabled during a blackout." )]
		public bool DisableTeslas { get; set; } = true;

		[Description( "Cassie message or sound effect that plays when a blackout starts." )]
		public string BlackoutStartSound { get; set; } = "pitch_1.00 .G1 pitch_0.70 .G1 pitch_0.15 .G7";

		[Description( "Cassie message or sound effect that plays when a blackout ends." )]
		public string BlackoutEndSound { get; set; } = "pitch_0.70 .G1 pitch_1.00 .G1";
	}
}
