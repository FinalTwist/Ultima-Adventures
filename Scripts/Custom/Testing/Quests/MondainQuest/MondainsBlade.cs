using System;
using Server;

namespace Server.Items

{
              [FlipableAttribute( 0x13FF, 0x13FE )]
              public class MondainsBlade : Katana
              {
              public override int ArtifactRarity{ get{ return 30; } } 
              public override int AosMinDamage{ get{ return 45; } }
              public override int AosMaxDamage{ get{ return 50; } }
	      public override float MlSpeed{ get{ return 3.00f; } }
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.Disarm; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override int InitMinHits{ get{ return 500; } }
		public override int InitMaxHits{ get{ return 500; } }
              
                      [Constructable]
                      public MondainsBlade() 
                      {
                                        Weight = 10;
                                        Name = "Mondains Blade";
                                        Hue = 1153;
              
                                        WeaponAttributes.DurabilityBonus = 45;
                                        //WeaponAttributes.HitColdArea = 45;
                                        WeaponAttributes.HitDispel = 45;
                                        //WeaponAttributes.HitEnergyArea = 45;
                                        //WeaponAttributes.HitFireArea = 45;
                                        WeaponAttributes.HitHarm = 45;
                                        WeaponAttributes.HitLeechHits = 45;
                                        WeaponAttributes.HitLeechMana = 55;
                                        WeaponAttributes.HitLeechStam = 45;
                                        WeaponAttributes.HitLightning = 45;
                                        WeaponAttributes.HitMagicArrow = 45;
                                        //WeaponAttributes.HitPhysicalArea = 45;
                                        //WeaponAttributes.HitPoisonArea = 45;
                                        WeaponAttributes.LowerStatReq = 45;
                                        //WeaponAttributes.ResistColdBonus = 45;
                                        //WeaponAttributes.ResistEnergyBonus = 45;
                                        //WeaponAttributes.ResistPhysicalBonus = 45;
                                        //WeaponAttributes.ResistPoisonBonus = 45;
                                        //WeaponAttributes.ResistFireBonus = 45;
                                        WeaponAttributes.SelfRepair = 52;
                                        WeaponAttributes.UseBestSkill = 1;
              
                                        Attributes.AttackChance = 25;
                                        Attributes.BonusDex = 15;
                                        Attributes.BonusHits = 11;
                                        //Attributes.BonusInt = 39;
                                        //Attributes.BonusMana = 30 ;
                                        //Attributes.BonusStam = 37;
                                        //Attributes.CastRecovery = 51;
                                        //Attributes.CastSpeed = 54;
                                        //Attributes.DefendChance = 34;
                                        //Attributes.EnhancePotions = 30;
                                        Attributes.LowerManaCost = 20;
                                        Attributes.LowerRegCost = 30;
                                        Attributes.Luck = 420;
                                        //Attributes.NightSight = 1;
                                        //Attributes.ReflectPhysical = 45;
                                        //Attributes.RegenHits = 45;
                                        //Attributes.RegenMana = 56;
                                        //Attributes.RegenStam = 56;
                                        Attributes.SpellChanneling = 1;
                                        Attributes.SpellDamage = 5;
                                        Attributes.WeaponDamage = 65;
                                        Attributes.WeaponSpeed = 40;
              
                                    }
              
                      public MondainsBlade( Serial serial ) : base( serial )  
                                    {
                                    }
              
                      public override void Serialize( GenericWriter writer )
                                    {
                                                      base.Serialize( writer );
              
                                                      writer.Write( (int) 0 );
                                    }
              
                      public override void Deserialize(GenericReader reader)
                                    {
                                                      base.Deserialize( reader );
                            
                                                      int version = reader.ReadInt();
                                    }
                  }
}
