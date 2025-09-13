using System;

namespace Server.Mobiles
{
  public interface UltimaLiveQuery
  {
    int QueryMobile(Mobile m, int previousBlock);
  }

  public partial class PlayerMobile : Mobile, IHonorTarget
  {
    public static UltimaLiveQuery BlockQuery;
    private int m_PreviousMapBlock = -1;
    private int m_UltimaLiveMajorVersion = 0;
    private int m_UltimaLiveMinorVersion = 0;

    [CommandProperty(AccessLevel.GameMaster, true)]
    public int UltimaLiveMajorVersion
    {
      get
      {
        return m_UltimaLiveMajorVersion;
      }
      set
      {
        m_UltimaLiveMajorVersion = value;
      }
    }

    [CommandProperty(AccessLevel.GameMaster, true)]
    public int UltimaLiveMinorVersion
    {
      get
      {
        return m_UltimaLiveMinorVersion;
      }
      set
      {
        m_UltimaLiveMinorVersion = value;
      }
    }
  }
}
