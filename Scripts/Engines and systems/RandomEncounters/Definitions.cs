//------------------------------------------------------------------------------
///  <summary>
///   
///  </summary>
//------------------------------------------------------------------------------
namespace Server.Misc
{
    public enum RegionType      { Dungeon, Wilderness, Guarded, House, Public, Jail };
    public enum EncounterTime   { Day, Night, Twilight, AnyTime };
    public enum LandType        { Water, OnRoad, OffRoad, AnyLand };
    public enum LevelType       { Fighter, Ranger, Mage, Necromancer, Thief, Overall };
    public enum EffectType      { Smoke, Fire, Vortex, Swirl, Glow, Explosion, None };

    struct EffectEntry
    {
        public int         Animation;
        public int         Speed;
        public int         Duration;
        public int         RenderMode;
        public int         Effect;

        EffectEntry (
            int animation,
            int speed,
            int duration,
            int renderMode,
            int effect
            )
        {
            Animation   = animation;
            Speed       = speed;
            Duration    = duration;
            RenderMode  = renderMode;
            Effect      = effect;
        }

        public static EffectEntry[] Lookup =
        {
            new EffectEntry ( /* Smoke     */ 0x3728, 10, 10, 3, 2023 ),
            new EffectEntry ( /* Fire      */ 0x3709, 10, 30, 3, 5052 ),
            new EffectEntry ( /* Vortex    */ 0x37cc,  1, 40, 3, 9917 ),
            new EffectEntry ( /* Swirl     */ 0x3789,  1, 40, 3, 9907 ),
            new EffectEntry ( /* Glow      */ 0x37b9,  1, 30, 3, 9502 ),
            new EffectEntry ( /* Explosion */ 0x36b0,  1, 14, 3, 9915 )
        };
    }

    //public enum EffectType      { None=0, Smoke=0x3728, Fire=0x3709, Vortex=0x37cc, Swirl=0x3789, Glow=0x37b9 };
}
