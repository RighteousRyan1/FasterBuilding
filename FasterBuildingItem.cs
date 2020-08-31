using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace FasterBuilding
{
	public class FasterBuildingItem : GlobalItem
	{
		public override float UseTimeMultiplier(Item item, Player player)
		{
            if (item.createTile > 0)
            {
                return GetInstance<FasterBuildingConfig>().tilePlaceSpeed;
            }
			return 1f;
		}
	}
}