using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace FasterBuilding
{
	public class FasterBuildingItem : GlobalItem
	{
		public override float UseTimeMultiplier(Item item, Player player)
		{
            if (item.createTile > -1)
            {
                return GetInstance<FasterBuildingConfig>().tilePlaceSpeed;
            }
			if (item.createWall > -1)
            {
                return GetInstance<FasterBuildingConfig>().wallPlaceSpeed;
            }
			return 1f;
		}
	}
}