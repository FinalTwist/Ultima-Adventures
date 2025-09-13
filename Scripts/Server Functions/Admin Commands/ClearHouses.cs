using System.Collections.Generic;
using System.Linq;
using Knives.TownHouses;
using Server.Lokai;
using Server.Mobiles;
using Server.Multis;

namespace Server.Commands
{
    public class ClearHousesCommand
    {
        public static void Initialize()
        {
            CommandSystem.Register("ClearHouses", AccessLevel.Player, new CommandEventHandler(ClearHouses));
        }

        public static void ClearHouses(CommandEventArgs args)
        {
            // Warning: This does not work on decayed Townhomes

            var toDelete = new List<IEntity>();
            foreach (var entity in World.Items.Values.Where(x => x is BaseHouse).Cast<BaseHouse>().ToList())
            {
                // Keep ageless townhomes
                if (entity is TownHouse && entity.DecayType == DecayType.Ageless) continue;

                var entities = entity.GetHouseEntities();

                // Keep any house with a LinkedGate
                // if (entities.Any(i => i is LinkedGate)) continue;

                entity.GetMobiles().Where(mobile => (mobile is PlayerMobile) == false).ToList().ForEach(mobile => mobile.Delete());
                entity.Items.ForEach(item => item.Delete());

                entities.ForEach(e => e.Delete());

                if (entity is TownHouse) continue;

                toDelete.Add(entity);
            }

            // Clean up lingering items
            World.Items.Values
                .Where(item => item.Parent == null && (item is TownHouse) == false && !item.IsLockedDown && !item.IsSecure && BaseHouse.FindHouseAt(item) != null)
                .ToList()
                .ForEach(item => item.Delete());

            toDelete.ForEach(e => e.Delete());

            foreach (var entity in World.Items.Values.Where(x => x is BaseBoat).Cast<BaseBoat>().ToList())
            {
                if (entity.Ageless) continue;

                entity.GetMovingEntities().ForEach(e => e.Delete());
                entity.Items.ForEach(item => item.Delete());
                entity.Delete();
            }
        }
    }
}