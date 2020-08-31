using Terraria.ModLoader.Config;
using System.ComponentModel;

namespace FasterBuilding
{
	[Label("Faster Building Configurations")]
	public class FasterBuildingConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ServerSide;
		[Header("Placing Speed")]
		[Label("Block Placement Speeds")]
		[DefaultValue(1f)]
		[Range(1f, 10f)]
        public float tilePlaceSpeed;
	}
}