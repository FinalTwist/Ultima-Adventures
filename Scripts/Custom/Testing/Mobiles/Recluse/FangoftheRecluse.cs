// Created by David aka EvilPounder
// Server: Lords of UO
using System;
using Server;

namespace Server.Items

{
              
              public class FangoftheRecluse : Kryss
              {
              public override int ArtifactRarity{ get{ return 1000; } }

	      public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ArmorIgnore; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }

	      public override float MlSpeed{ get{ return 2.50f; } } 
              public override int AosMinDamage{ get{ return 15; } }
              public override int AosMaxDamage{ get{ return 21; } }
              
                      [Constructable]
                      public FangoftheRecluse() 
                      {
                                        Weight = 1;
                                        Name = "Fang of the Recluse";
                                        Hue = 1111;
              
                                        WeaponAttributes.DurabilityBonus = 100;
                                        WeaponAttributes.HitHarm = 25;
                                        WeaponAttributes.HitLeechHits = 25;
                                        WeaponAttributes.HitDispel = 20;
                                        WeaponAttributes.HitEnergyArea = 20;
                                        WeaponAttributes.HitLeechMana = 25;
                                        WeaponAttributes.HitLeechStam = 45;
                                        WeaponAttributes.HitLightning = 45;
                                        WeaponAttributes.HitLowerAttack = 25;
                                        WeaponAttributes.HitMagicArrow = 25;
                                        //WeaponAttributes.HitPoisonArea = 100;
                                        WeaponAttributes.LowerStatReq = 100;
                                        WeaponAttributes.SelfRepair = 55;
                                        WeaponAttributes.ResistColdBonus = 10;
                                        WeaponAttributes.ResistEnergyBonus = 10;
                                        WeaponAttributes.ResistPhysicalBonus = 10;
                                        WeaponAttributes.ResistPoisonBonus = 10;
                                        WeaponAttributes.ResistFireBonus = 10;
              
                                        Attributes.AttackChance = 25;
                                        Attributes.DefendChance = 25;
                                        Attributes.LowerManaCost = 25;
                                        //Attributes.LowerRegCost = 25;
                                        //Attributes.RegenHits = 25;
                                        //Attributes.RegenMana = 25;
                                        //Attributes.RegenStam = 25;
                                        Attributes.SpellChanneling = 1;
                                        Attributes.WeaponDamage = 75;
                                        Attributes.WeaponSpeed = 35;
              
                                    }
              
                      public FangoftheRecluse( Serial serial ) : base( serial )  
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
