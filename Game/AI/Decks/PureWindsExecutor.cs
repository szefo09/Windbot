using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using System.Linq;

namespace WindBot.Game.AI.Decks
{
    [Deck("PureWinds", "AI_PureWinds")]
    class PureWindsExecutor : DefaultExecutor
    {
        public class CardId
        {
            public const int SpeedroidTerrortop = 81275020;
            public const int WindwitchIceBell = 43722862;
            public const int PilicaDescendantOfGusto = 71175527;
            public const int SpeedroidTaketomborg = 53932291;
            public const int WindaPriestessOfGusto = 54455435;
            public const int WindwitchGlassBell = 71007216;

            public const int GustoGulldo = 65277087;
            public const int GustoEgul = 91662792;
            public const int WindwitchSnowBell = 70117860;
            public const int SpeedroidRedEyedDice = 16725505;
            public const int Raigeki = 12580477;
            public const int MonsterReborn = 83764719;
            public const int Reasoning = 58577036;
            public const int ElShaddollWinda = 94977269;

            public const int QuillPenOfGulldos = 27980138;
            public const int CosmicCyclone = 8267140;
            public const int EmergencyTeleport = 67723438;

            public const int ForbiddenChalice = 25789292;
            public const int SuperTeamBuddyForceUnite = 8608979;
            public const int KingsConsonance = 24590232;
            public const int GozenMatch = 53334471;
            public const int SolemnStrike = 40605147;
            public const int SolemnWarning = 84749824;

            public const int MistWurm = 27315304;
            public const int CrystalWingSynchroDragon = 50954680;
            public const int ClearWingSynchroDragon = 82044279;
            public const int WindwitchWinterBell = 14577226;

            public const int StardustChargeWarrior = 64880894;
            public const int DaigustoSphreez = 29552709;
            public const int DaigustoGulldos = 84766279;

            public const int HiSpeedroidChanbara = 42110604;
            public const int OldEntityHastorr = 70913714;
            public const int WynnTheWindCharmerVerdant = 30674956;
            public const int GreatFly = 90512490;
            public const int KnightmareIblee = 10158145;
            public const int ChaosMax = 55410871;
        }

        List<int> ReposTargets = new List<int>
        { CardId.GustoGulldo,
            CardId.WindaPriestessOfGusto,
            CardId.GustoEgul,
            CardId.PilicaDescendantOfGusto,
            CardId.DaigustoGulldos
        };

        List<int> taketomborgSpList = new List<int>
        {   CardId.WindwitchGlassBell,
            CardId.GustoGulldo,
            CardId.GustoEgul,
            CardId.SpeedroidRedEyedDice,
            CardId.WindwitchSnowBell,
            CardId.SpeedroidTerrortop
        };

        List<int> level1 = new List<int>
        { CardId.GustoEgul,
            CardId.SpeedroidRedEyedDice,
            CardId.WindwitchSnowBell
        };

        List<int> level3 = new List<int>
        { CardId.PilicaDescendantOfGusto,
            CardId.WindwitchIceBell,
            CardId.SpeedroidTaketomborg
        };
        List<int> KeepSynchro = new List<int>
        { CardId.DaigustoSphreez,
            CardId.CrystalWingSynchroDragon,
            CardId.ClearWingSynchroDragon,
            CardId.WindwitchWinterBell,
            CardId.GreatFly,
            CardId.WynnTheWindCharmerVerdant
        };
        List<int> reborn = new List<int>
        { CardId.ClearWingSynchroDragon,
            CardId.DaigustoSphreez,
            CardId.WindwitchWinterBell,
            CardId.PilicaDescendantOfGusto,
            CardId.OldEntityHastorr,
            CardId.HiSpeedroidChanbara,
            CardId.DaigustoGulldos
        };
        List<int> Gulldosulist = new List<int>
        { CardId.CrystalWingSynchroDragon,
            CardId.MistWurm,
            CardId.ClearWingSynchroDragon,
            CardId.WindwitchWinterBell,
            CardId.ClearWingSynchroDragon,
            CardId.StardustChargeWarrior
        };
        List<int> Gulldosulist2 = new List<int>
        {
            CardId.SpeedroidTerrortop,
            CardId.PilicaDescendantOfGusto,
            CardId.WindaPriestessOfGusto,
            CardId.WindwitchIceBell,
            CardId.SpeedroidTaketomborg,
            CardId.OldEntityHastorr,
            CardId.HiSpeedroidChanbara,
            CardId.DaigustoGulldos,
            CardId.DaigustoSphreez
        };
        List<int> EgulsuList = new List<int>
        {
            CardId.SpeedroidTerrortop,
            CardId.PilicaDescendantOfGusto,
            CardId.WindaPriestessOfGusto,
            CardId.WindwitchIceBell,
            CardId.SpeedroidTaketomborg,
            CardId.OldEntityHastorr,
            CardId.HiSpeedroidChanbara,
            CardId.DaigustoGulldos,
            CardId.DaigustoSphreez,
            CardId.StardustChargeWarrior,
            CardId.WindwitchWinterBell,
            CardId.ClearWingSynchroDragon
        };
        List<int> SynchroList = new List<int>
        {
            CardId.SpeedroidTerrortop,
            CardId.PilicaDescendantOfGusto,
            CardId.WindaPriestessOfGusto,
            CardId.WindwitchIceBell,
            CardId.SpeedroidTaketomborg,
            CardId.OldEntityHastorr,
            CardId.HiSpeedroidChanbara,
            CardId.DaigustoGulldos,
            CardId.DaigustoSphreez,
            CardId.StardustChargeWarrior,
            CardId.WindwitchWinterBell,
            CardId.ClearWingSynchroDragon,
            CardId.CrystalWingSynchroDragon
        };
        List<int> LinkList = new List<int>
        {
            CardId.WynnTheWindCharmerVerdant,
            CardId.GreatFly
        };
        List<int> tuner = new List<int>
        {
            CardId.GustoGulldo,
            CardId.GustoEgul,
            CardId.SpeedroidRedEyedDice,
            CardId.WindwitchGlassBell,
            CardId.WindwitchSnowBell
        };
        List<int> gusto = new List<int>
        {
            CardId.GustoGulldo,
            CardId.GustoEgul,
            CardId.WindaPriestessOfGusto,
            CardId.PilicaDescendantOfGusto,
            CardId.DaigustoGulldos,
            CardId.DaigustoSphreez
        };
        List<int> ET = new List<int>
        {
            CardId.ClearWingSynchroDragon,
            CardId.WindwitchWinterBell
        };

        private bool WindwitchGlassBelleff_used;
        private bool Summon_used;
        private bool plan_A;
        //TODO: reset the flags when they should reset ( public override void OnNewTurn() )
        public PureWindsExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            //counter
            AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, base.DefaultSolemnStrike);
            AddExecutor(ExecutorType.Activate, CardId.SolemnWarning, base.DefaultSolemnWarning);
            AddExecutor(ExecutorType.Activate, CardId.ForbiddenChalice, ForbiddenChaliceeff);
            AddExecutor(ExecutorType.Activate, CardId.CrystalWingSynchroDragon, CrystalWingSynchroDragoneff);
            AddExecutor(ExecutorType.Activate, CardId.GustoGulldo, GustoGulldoeff);
            AddExecutor(ExecutorType.Activate, CardId.GustoEgul, GustoEguleff);
            AddExecutor(ExecutorType.Activate, CardId.WindaPriestessOfGusto, WindaPriestessOfGustoeff);
            AddExecutor(ExecutorType.Activate, CardId.PilicaDescendantOfGusto, PilicaDescendantOfGustoeff);
            AddExecutor(ExecutorType.Activate, CardId.OldEntityHastorr, OldEntityHastorreff);
            AddExecutor(ExecutorType.Activate, CardId.WynnTheWindCharmerVerdant, WynnTheWindCharmerVerdanteff);
            AddExecutor(ExecutorType.Activate, CardId.GreatFly, GreatFlyeff);
            AddExecutor(ExecutorType.Activate, CardId.QuillPenOfGulldos, QuillPenOfGulldoseff);
            AddExecutor(ExecutorType.Activate, CardId.CosmicCyclone, CosmicCycloneeff);
            AddExecutor(ExecutorType.Activate, CardId.MonsterReborn, Reborneff);
            //plan A             
            AddExecutor(ExecutorType.Activate, CardId.WindwitchIceBell, WindwitchIceBelleff);
            AddExecutor(ExecutorType.Activate, CardId.WindwitchGlassBell, WindwitchGlassBelleff);
            AddExecutor(ExecutorType.Activate, CardId.WindwitchSnowBell, WindwitchSnowBellsp);
            AddExecutor(ExecutorType.Activate, CardId.StardustChargeWarrior);
            AddExecutor(ExecutorType.Activate, CardId.WindwitchWinterBell, WindwitchWinterBelleff);
            AddExecutor(ExecutorType.Activate, CardId.ClearWingSynchroDragon, ClearWingSynchroDragoneff);
            AddExecutor(ExecutorType.Activate, CardId.DaigustoSphreez, DaigustoSphreezeff);
            AddExecutor(ExecutorType.Activate, CardId.SpeedroidTerrortop, SpeedroidTerrortopeff);
            AddExecutor(ExecutorType.Activate, CardId.SpeedroidTaketomborg, SpeedroidTaketomborgeff);
            AddExecutor(ExecutorType.Activate, CardId.SpeedroidRedEyedDice, SpeedroidRedEyedDiceeff);
            AddExecutor(ExecutorType.Activate, CardId.MistWurm, MistWurmeff);
            AddExecutor(ExecutorType.Activate, CardId.DaigustoGulldos, DaigustoGulldoseff);
            AddExecutor(ExecutorType.Activate, CardId.EmergencyTeleport, EmergencyTeleporteff);
            AddExecutor(ExecutorType.Activate, CardId.Reasoning, Reasoningeff);
            AddExecutor(ExecutorType.SpSummon, CardId.WindwitchWinterBell, WindwitchWinterBellsp);

            AddExecutor(ExecutorType.SpSummon, CardId.CrystalWingSynchroDragon, CrystalWingSynchroDragonsp);
            // if fail
            AddExecutor(ExecutorType.SpSummon, CardId.ClearWingSynchroDragon, ClearWingSynchroDragonsp);
            // if fail
            AddExecutor(ExecutorType.SpSummon, CardId.DaigustoSphreez, DaigustoSphreezsp);
            // plan B
            AddExecutor(ExecutorType.SpSummon, CardId.SpeedroidTerrortop);
            AddExecutor(ExecutorType.SpSummon, CardId.SpeedroidTaketomborg, SpeedroidTaketomborgsp);
            //summon
            AddExecutor(ExecutorType.Summon, CardId.PilicaDescendantOfGusto, PilicaDescendantOfGustosu);
            AddExecutor(ExecutorType.Summon, CardId.GustoGulldo, GustoGulldosu);
            AddExecutor(ExecutorType.Summon, CardId.GustoEgul, GustoEgulsu);
            AddExecutor(ExecutorType.Summon, CardId.WindaPriestessOfGusto, WindaPriestessOfGustosu);
            AddExecutor(ExecutorType.Summon, CardId.SpeedroidRedEyedDice, SpeedroidRedEyedDicesu);
            //other thing
            AddExecutor(ExecutorType.SpSummon, CardId.MistWurm);
            AddExecutor(ExecutorType.SpSummon, CardId.DaigustoGulldos);
            AddExecutor(ExecutorType.SpSummon, CardId.HiSpeedroidChanbara);
            AddExecutor(ExecutorType.SpSummon, CardId.StardustChargeWarrior);
            AddExecutor(ExecutorType.SpSummon, CardId.OldEntityHastorr);
            AddExecutor(ExecutorType.SpSummon, CardId.GreatFly, GreatFlysp);
            AddExecutor(ExecutorType.SpSummon, CardId.WynnTheWindCharmerVerdant, WynnTheWindCharmerVerdantsp);
            AddExecutor(ExecutorType.Activate, CardId.Raigeki);
            AddExecutor(ExecutorType.Activate, CardId.GozenMatch);
            AddExecutor(ExecutorType.Activate, CardId.KingsConsonance, KingsConsonanceeff);
            AddExecutor(ExecutorType.Activate, CardId.SuperTeamBuddyForceUnite, SuperTeamBuddyForceUniteeff);
            //trap set
            AddExecutor(ExecutorType.SpellSet, CardId.KingsConsonance);
            AddExecutor(ExecutorType.SpellSet, CardId.SolemnStrike);
            AddExecutor(ExecutorType.SpellSet, CardId.SolemnWarning);
            AddExecutor(ExecutorType.SpellSet, CardId.ForbiddenChalice);
            AddExecutor(ExecutorType.SpellSet, CardId.SuperTeamBuddyForceUnite);
            AddExecutor(ExecutorType.SpellSet, CardId.GozenMatch);
            AddExecutor(ExecutorType.MonsterSet, CardId.GustoGulldo, gulldoset);
            AddExecutor(ExecutorType.MonsterSet, CardId.GustoEgul, egulset);
            AddExecutor(ExecutorType.MonsterSet, CardId.WindaPriestessOfGusto, windaset);
            AddExecutor(ExecutorType.Summon, CardId.WindwitchGlassBell, WindwitchGlassBellsummonfirst);
            AddExecutor(ExecutorType.Summon, CardId.WindwitchGlassBell, WindwitchGlassBellsummon);
            AddExecutor(ExecutorType.MonsterSet, CardId.SpeedroidRedEyedDice, SpeedroidRedEyedDiceset);
            AddExecutor(ExecutorType.MonsterSet, CardId.WindwitchSnowBell, WindwitchSnowBellset);

            AddExecutor(ExecutorType.Repos, MonsterRepos);
        }
        private bool windaset()
        {
            if (Enemy.HasInMonstersZoneOrInGraveyard(CardId.ChaosMax))
                return false;
            return true;
        }
        private bool egulset()
        {
            if (Enemy.HasInMonstersZoneOrInGraveyard(CardId.ChaosMax))
                return false;
            return true;
        }
        private bool gulldoset()
        {
            if (Enemy.HasInMonstersZoneOrInGraveyard(CardId.ChaosMax))
                return false;
            return true;
        }

        private bool Reasoningeff()
        {
            AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
        private bool KingsConsonanceeff()
        {
            AI.SelectCard(CardId.CrystalWingSynchroDragon,
                CardId.DaigustoSphreez,
                CardId.ClearWingSynchroDragon,
                CardId.HiSpeedroidChanbara,
                CardId.OldEntityHastorr);
            return true;
        }
        private bool Reborneff()
        {
            if (Bot.HasInGraveyard(CardId.CrystalWingSynchroDragon))
            {
                AI.SelectCard(CardId.CrystalWingSynchroDragon);
                return true;
            }
            if (!Util.IsOneEnemyBetter(true)) return false;
            if (!Bot.HasInGraveyard(reborn))
            {
                return false;
            }
            AI.SelectCard(reborn);
            return true;
        }

        private bool SpeedroidRedEyedDiceset()
        {
            if (Enemy.HasInMonstersZone(CardId.ChaosMax))
                return false;
            if (Bot.GetMonsterCount() < 1)
                return true;
            return false;
        }

        private bool WindwitchSnowBellset()
        {
            if (Enemy.HasInMonstersZone(CardId.ChaosMax))
                return false;
            if (Bot.GetMonsterCount() < 1)
                return true;
            return false;
        }

        private bool GreatFlysp()
        {
            if (Bot.HasInMonstersZone(KeepSynchro))
                return false;
            if (Bot.HasInMonstersZone(CardId.HiSpeedroidChanbara))
                return false;
            return true;
        }
        private bool WynnTheWindCharmerVerdantsp()
        { 
            if (Bot.HasInMonstersZone(KeepSynchro))
                return false;
            if (Bot.HasInMonstersZone(CardId.HiSpeedroidChanbara))
                return false;
            return true;
        }
        private bool MistWurmeff()
        {
            AI.SelectCard(Util.GetBestEnemyMonster());
            return true;
        }
        private bool GustoGulldosu()
        {
            if ((Bot.HasInMonstersZone(Gulldosulist)) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant) &&
                !Bot.HasInMonstersZone(CardId.GreatFly))
            {
                return false;
            }
            else if (Bot.HasInMonstersZone(CardId.DaigustoSphreez) ||
                Bot.HasInHand(CardId.EmergencyTeleport))
            {
                Summon_used = true;
                return true;
            }
            else if (Bot.HasInMonstersZone(Gulldosulist2))
            {
                Summon_used = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool GustoEgulsu()
        {
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez) &&
                !Bot.HasInHand(CardId.GustoGulldo))
            {
                Summon_used = true;
                return true;
            }
            else if ((Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.MistWurm)) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant) &&
                !Bot.HasInMonstersZone(CardId.GreatFly))
                return false;
            else if (Bot.HasInMonstersZone(EgulsuList))
            {
                Summon_used = true;
                return true;
            }
            return false;
        }
        private bool WindaPriestessOfGustosu()
        {
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez) &&
                !Bot.HasInHand(CardId.GustoGulldo) &&
                !Bot.HasInHand(CardId.GustoEgul))
            {
                Summon_used = true;
                return true;
            }
            else if ((Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.MistWurm)) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant) &&
                !Bot.HasInMonstersZone(CardId.GreatFly))
                return false;
            else if (Bot.HasInMonstersZone(CardId.GustoGulldo) ||
                Bot.HasInMonstersZone(CardId.WindwitchGlassBell) ||
                ((Bot.HasInMonstersZone(level3) || Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto)) &&
                Bot.HasInMonstersZone(tuner)) ||
                (Bot.HasInMonstersZone(CardId.OldEntityHastorr) && Bot.HasInMonstersZone(level1)))
            {
                Summon_used = true;
                return true;
            }
            return false;
        }
        private bool SpeedroidRedEyedDicesu()
        {
            if ((Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.MistWurm) ||
                Bot.HasInMonstersZone(CardId.DaigustoSphreez)) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant) &&
                !Bot.HasInMonstersZone(CardId.GreatFly))
                return false;
            else if (Bot.HasInMonstersZone(EgulsuList))
            {
                Summon_used = true;
                return true;
            }
            return false;
        }
        private bool PilicaDescendantOfGustosu()
        {
            if ((Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.MistWurm)) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant) &&
                !Bot.HasInMonstersZone(CardId.GreatFly))
                return false;
            else if (!Bot.HasInMonstersZoneOrInGraveyard(tuner))
                return false;
            else {
                Summon_used = true;
                return true;
            }
        }
        private bool EmergencyTeleporteff()
        {
            if ((Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.MistWurm)) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant) &&
                !Bot.HasInMonstersZone(CardId.GreatFly))
                return false;
            else if (Bot.HasInMonstersZone(tuner) && Bot.HasInMonstersZone(level3))
                return false;
            else if (!Bot.HasInHandOrInMonstersZoneOrInGraveyard(tuner))
                return false;
            else if (!Bot.HasInHandOrInMonstersZoneOrInGraveyard(level1) && Bot.HasInMonstersZone(ET))
                return false;
            AI.SelectCard(CardId.PilicaDescendantOfGusto);
            AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
        private bool SpeedroidRedEyedDiceeff()
        {
            if (Bot.HasInMonstersZone(CardId.SpeedroidTerrortop))
            {
                AI.SelectCard(CardId.SpeedroidTerrortop);
                AI.SelectNumber(6);
                return true;
            }
            else if (Bot.HasInMonstersZone(CardId.SpeedroidTaketomborg))
            {
                AI.SelectCard(CardId.SpeedroidTaketomborg);
                AI.SelectNumber(6);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool DaigustoGulldoseff()
        {
            AI.SelectCard();
            AI.SelectNextCard(Util.GetBestEnemyMonster());
            return true;
        }
        private bool SpeedroidTaketomborgeff()
        {
            if ((Bot.GetRemainingCount(CardId.SpeedroidRedEyedDice, 1) >= 1) &&
                Bot.HasInMonstersZone(CardId.SpeedroidTerrortop))
            {
                AI.SelectCard(CardId.SpeedroidRedEyedDice);
                return true;
            }
            return false;
        }

        private bool QuillPenOfGulldoseff()
        {
            var gyTargets = Bot.Graveyard.Where(x => x.Attribute == (int)CardAttribute.Wind).Select(x => x.Id).ToArray();
            if (gyTargets.Count() >= 2)
            {
                AI.SelectCard(gyTargets);
                if (Bot.HasInSpellZone(CardId.OldEntityHastorr))
                {
                    AI.SelectNextCard(CardId.OldEntityHastorr);
                }
                else if (Util.GetBestEnemyMonster() != null)
                {

                    AI.SelectNextCard(Util.GetBestEnemyMonster());
                }
                else
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        private bool WindwitchIceBelleff()
        {
            if (Enemy.HasInMonstersZone(CardId.ElShaddollWinda)) return false;
            if (WindwitchGlassBelleff_used && !Bot.HasInHand(CardId.WindwitchSnowBell)) return false;
            //AI.SelectPlace(Zones.z2, 1);
            if (Bot.GetRemainingCount(CardId.WindwitchGlassBell, 3) >= 1)
            {
                AI.SelectCard(CardId.WindwitchGlassBell);
            }
            else if (Bot.HasInHand(CardId.WindwitchGlassBell))
            {
                AI.SelectCard(CardId.WindwitchSnowBell);
            }
            AI.GetSelectedPosition();
            if (Card.Location == CardLocation.Hand)
            {
                AI.SelectPosition(CardPosition.FaceUpDefence);
                AI.SelectPosition(CardPosition.FaceUpDefence);
            }
            return true;
        }
        private bool SpeedroidTaketomborgsp()
        {
            if ((Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.DaigustoSphreez)) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant) &&
                !Bot.HasInMonstersZone(CardId.GreatFly))
                return false;
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez))
                return false;
            else if (Bot.HasInMonstersZone(taketomborgSpList))
            {
                AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }
            return false;

        }
        private bool WindwitchGlassBelleff()
        {
            if (Bot.HasInMonstersZone(level3))
            {
                if (Bot.HasInHand(CardId.WindwitchSnowBell))
                {
                    AI.SelectCard(CardId.WindwitchIceBell);
                }
                else if (Bot.HasInHand(CardId.WindwitchIceBell))
                {
                    AI.SelectCard(CardId.WindwitchSnowBell);
                }
            }
            else
            {
                if (Bot.HasInHand(CardId.SpeedroidTaketomborg))
                {
                    AI.SelectCard(CardId.WindwitchSnowBell);
                }
                else
                {
                    AI.SelectCard(CardId.WindwitchIceBell);
                }
            }
            WindwitchGlassBelleff_used = true;
            return true;
        }

        private bool OldEntityHastorreff()
        {
            AI.SelectCard(Util.GetBestEnemyMonster());
            return true;
        }

        private bool WynnTheWindCharmerVerdanteff()
        {
            AI.SelectCard(CardId.PilicaDescendantOfGusto, CardId.WindwitchIceBell, CardId.SpeedroidTerrortop, CardId.GustoGulldo, CardId.GustoEgul, CardId.WindaPriestessOfGusto);
            return true;
        }
        private bool SpeedroidTerrortopeff()
        {
            AI.SelectCard(CardId.SpeedroidTaketomborg, CardId.SpeedroidRedEyedDice);
            return true;
        }
        private bool GreatFlyeff()
        {
            AI.SelectCard(CardId.PilicaDescendantOfGusto, CardId.WindwitchIceBell, CardId.SpeedroidTerrortop, CardId.GustoGulldo, CardId.GustoEgul, CardId.WindaPriestessOfGusto);
            return true;
        }

        private bool PilicaDescendantOfGustoeff()
        {
            AI.SelectCard(CardId.GustoGulldo, CardId.WindwitchGlassBell, CardId.WindwitchSnowBell, CardId.GustoEgul, CardId.SpeedroidRedEyedDice);
            return true;
        }

        private bool SuperTeamBuddyForceUniteeff()
        {
            foreach (ClientCard card in Duel.CurrentChain)
                if (card.IsCode(CardId.SuperTeamBuddyForceUnite))
                    return false;
            if (Bot.HasInGraveyard(CardId.PilicaDescendantOfGusto) && Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                AI.SelectPosition(CardPosition.Attack);
                AI.SelectCard(CardId.DaigustoSphreez, CardId.PilicaDescendantOfGusto);
                return true;
            }

            else if (Bot.HasInGraveyard(CardId.WindaPriestessOfGusto) && Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                AI.SelectPosition(CardPosition.Attack);
                AI.SelectCard(CardId.DaigustoSphreez, CardId.WindaPriestessOfGusto);
                return true;
            }

            else if (Bot.HasInGraveyard(CardId.DaigustoSphreez) && Bot.HasInMonstersZone(CardId.PilicaDescendantOfGusto))
            {
                AI.SelectPosition(CardPosition.Attack);
                AI.SelectCard(CardId.PilicaDescendantOfGusto, CardId.DaigustoSphreez);
                return true;
            }

            else if (Bot.HasInGraveyard(CardId.DaigustoSphreez) && Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto))
            {
                AI.SelectCard(CardId.WindaPriestessOfGusto, CardId.DaigustoSphreez);
                AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            else if (Bot.HasInGraveyard(CardId.DaigustoGulldos) && Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto))
            {
                AI.SelectCard(CardId.WindaPriestessOfGusto, CardId.DaigustoGulldos);
                AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            else if (Bot.HasInGraveyard(CardId.DaigustoGulldos) && Bot.HasInMonstersZone(CardId.PilicaDescendantOfGusto))
            {
                AI.SelectCard(CardId.DaigustoGulldos, CardId.PilicaDescendantOfGusto);
                AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            else if (Bot.HasInGraveyard(CardId.DaigustoSphreez) && Bot.HasInMonstersZone(CardId.DaigustoGulldos))
            {
                AI.SelectCard(CardId.DaigustoGulldos, CardId.DaigustoSphreez);
                AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            else if (Bot.HasInGraveyard(CardId.CrystalWingSynchroDragon))
            {
                AI.SelectCard();
                AI.SelectNextCard(CardId.CrystalWingSynchroDragon);
                AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            else if (Bot.HasInGraveyard(CardId.CrystalWingSynchroDragon))
            {
                AI.SelectCard();
                AI.SelectNextCard(CardId.ClearWingSynchroDragon);
                AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            else if (Bot.HasInGraveyard(CardId.WindwitchWinterBell))
            {
                AI.SelectCard();
                AI.SelectCard(CardId.WindwitchWinterBell);
                AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            else if (Bot.HasInGraveyard(CardId.WindwitchSnowBell) && Bot.HasInMonstersZone(CardId.WindwitchIceBell))
            {
                AI.SelectCard(CardId.WindwitchIceBell, CardId.WindwitchSnowBell);
                AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
            }

            else if (Bot.HasInGraveyard(CardId.PilicaDescendantOfGusto) && Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto))
            {
                AI.SelectCard(CardId.WindaPriestessOfGusto, CardId.PilicaDescendantOfGusto);
                AI.SelectPosition(CardPosition.FaceUpDefence);
            }
            else if (Bot.HasInGraveyard(CardId.SuperTeamBuddyForceUnite))
            {
                AI.SelectCard(CardId.SuperTeamBuddyForceUnite);
            }
            return false;
        }

        private bool WindwitchSnowBellsp()
        {
            if ((Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.DaigustoSphreez) ||
                Bot.HasInMonstersZone(CardId.MistWurm)) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant) &&
                !Bot.HasInMonstersZone(CardId.GreatFly))
                return false;
            if (Bot.HasInMonstersZone(CardId.WindwitchSnowBell) &&
                Bot.HasInMonstersZone(level3) &&
                Bot.HasInMonstersZone(CardId.WindwitchGlassBell))
                return false;
            if (Bot.HasInMonstersZone(CardId.PilicaDescendantOfGusto) &&
                Bot.HasInMonstersZone(CardId.GustoGulldo))
                return false;
            if (Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto) &&
                Bot.HasInMonstersZone(CardId.PilicaDescendantOfGusto) &&
                Bot.HasInMonstersZone(level1))
                return false;
            if (Bot.HasInMonstersZone(CardId.WindwitchWinterBell) &&
                Bot.HasInMonstersZone(CardId.WindwitchSnowBell))
                return false;
                AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
        private bool DaigustoSphreezsp()
        {
            //AI.SelectPlace(Zones.z5, Zones.ExtraMonsterZones);
            AI.SelectCard(CardId.WindwitchSnowBell, CardId.PilicaDescendantOfGusto, CardId.WindaPriestessOfGusto);
            AI.SelectCard(CardId.SpeedroidRedEyedDice, CardId.PilicaDescendantOfGusto, CardId.WindaPriestessOfGusto);
            AI.SelectCard(CardId.GustoGulldo, CardId.PilicaDescendantOfGusto);
            AI.SelectCard(CardId.WindwitchSnowBell, CardId.DaigustoGulldos);
            AI.SelectCard(CardId.SpeedroidRedEyedDice, CardId.DaigustoGulldos);
            AI.SelectPosition(CardPosition.Attack);
            return true;
        }
        private bool DaigustoSphreezeff()
        {
            if (Summon_used == true)
            {
                AI.SelectCard(CardId.PilicaDescendantOfGusto, CardId.GustoGulldo, CardId.GustoEgul, CardId.WindaPriestessOfGusto);
                return true;
            }
            AI.SelectCard(CardId.GustoGulldo, CardId.PilicaDescendantOfGusto, CardId.GustoEgul, CardId.WindaPriestessOfGusto);
            return true;
        }
        private bool WindwitchWinterBelleff()
        {
            AI.SelectCard(CardId.WindwitchGlassBell);
            return true;
        }

        private bool WindwitchWinterBellsp()
        {
            if (Bot.HasInHandOrInSpellZone(CardId.SuperTeamBuddyForceUnite) || 
                Bot.HasInHandOrInSpellZone(CardId.MonsterReborn))
                return false;
            if (Bot.HasInMonstersZone(CardId.WindwitchIceBell) &&
                 Bot.HasInMonstersZone(CardId.WindwitchGlassBell) &&
                 Bot.HasInMonstersZone(CardId.WindwitchSnowBell))
            {
                //AI.SelectPlace(Zones.z5, Zones.ExtraMonsterZones);
                AI.GetSelectedPosition();
                AI.SelectPosition(CardPosition.FaceUpAttack);
                AI.SelectCard(CardId.WindwitchIceBell, CardId.WindwitchGlassBell);
                return true;
            }

            return false;
        }

        private bool ClearWingSynchroDragonsp()
        {
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez))
                return false;
            AI.SelectPosition(CardPosition.Attack);
            return true;
        }

        private bool ClearWingSynchroDragoneff()
        {
            if (Duel.LastChainPlayer == 1)
            {
                return true;
            }
            return false;
        }
        private bool CrystalWingSynchroDragonsp()
        {
            if (Bot.HasInMonstersZone(CardId.WindwitchSnowBell) && Bot.HasInMonstersZone(CardId.WindwitchWinterBell))
            {
                //AI.SelectPlace(Zones.z5, Zones.ExtraMonsterZones);
                plan_A = true;
            }
            else if (Bot.HasInMonstersZone(CardId.WindwitchSnowBell) && Bot.HasInMonstersZone(CardId.ClearWingSynchroDragon))
            {
                //AI.SelectPlace(Zones.z5, Zones.ExtraMonsterZones);
                plan_A = true;
            }
            return true;
        }
        private bool ForbiddenChaliceeff()
        {
            if (Duel.LastChainPlayer == 1)
            {
                var target = Util.GetProblematicEnemyMonster(0, true);
                if (target != null && !target.IsShouldNotBeSpellTrapTarget() && Duel.CurrentChain.Contains(target))
                {
                    AI.SelectCard(target);
                    return true;
                }
            }
            return false;
        }
        private bool CosmicCycloneeff()
        {
            foreach (ClientCard card in Duel.CurrentChain)
                if (card.IsCode(_CardId.CosmicCyclone))
                    return false;
            if (Bot.HasInSpellZone(CardId.OldEntityHastorr) && (Bot.LifePoints > 1000))
            {
                AI.SelectCard(CardId.OldEntityHastorr);
                return true;
            }
            return (Bot.LifePoints > 1000) && DefaultMysticalSpaceTyphoon();
        }
        private bool CrystalWingSynchroDragoneff()
        {
            if (Duel.LastChainPlayer == 1)
            {
                return true;
            }
            return false;
        }
        private bool GustoGulldoeff()
        {
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                AI.SelectCard(CardId.GustoEgul, CardId.WindaPriestessOfGusto);
                AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            AI.SelectCard(CardId.GustoEgul, CardId.WindaPriestessOfGusto);
            AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
        private bool GustoEguleff()
        {
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                AI.SelectCard(CardId.WindaPriestessOfGusto, CardId.PilicaDescendantOfGusto);
                AI.SelectPosition(CardPosition.Attack);
                return true;
            }
            AI.SelectCard(CardId.WindaPriestessOfGusto, CardId.PilicaDescendantOfGusto);
            AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
        private bool WindaPriestessOfGustoeff()
        {
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                AI.SelectCard(CardId.GustoGulldo, CardId.GustoEgul);
                AI.SelectPosition(CardPosition.Attack);
            }
            AI.SelectCard(CardId.GustoGulldo, CardId.GustoEgul);
            AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
        private bool WindwitchGlassBellsummonfirst()
        {
            if (Bot.HasInHand(CardId.PilicaDescendantOfGusto) &&
                (Bot.HasInGraveyard(CardId.GustoGulldo) ||
                Bot.HasInGraveyard(CardId.GustoEgul) ||
                Bot.HasInGraveyard(CardId.WindwitchGlassBell) ||
                Bot.HasInGraveyard(CardId.SpeedroidRedEyedDice)))
            {
                return false;
            }
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez))
                return false;
            else if (!Bot.HasInHand(CardId.WindwitchIceBell))
            {
                Summon_used = true;
                return true;
            }
            return false;
        }
        private bool WindwitchGlassBellsummon()
        {
            if (!plan_A && (Bot.HasInGraveyard(CardId.WindwitchGlassBell) || Bot.HasInMonstersZone(CardId.WindwitchGlassBell)))
                return false;
            //AI.SelectPlace(Zones.z2, 1);
            if (Bot.HasInMonstersZone(CardId.WindwitchIceBell) &&
                !Bot.HasInMonstersZone(CardId.WindwitchGlassBell))
            {
                Summon_used = true;
                return true;
            }
            if (WindwitchGlassBelleff_used) return false;
            return false;
        }

        public bool MonsterRepos()
        {
            if (Card.IsCode(CardId.CrystalWingSynchroDragon) || Card.IsCode(CardId.DaigustoSphreez))
                return (!Card.HasPosition(CardPosition.Attack));
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez) && !Card.IsFacedown())
            {
                if (ReposTargets.Contains(Card.Id))
                {
                    if (Card.IsDefense())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                if (Bot.HasInMonstersZone(gusto))
                {
                    if (Card.IsFacedown())
                        return true;
                }
            }
            if (Bot.HasInMonstersZone(LinkList))
            {
                if(Bot.GetMonsterCount() > 2)
                {
                    if (Card.IsFacedown())
                        return true;
                }
            }
            if (!Bot.HasInMonstersZone(SynchroList))
            {
                if (Bot.GetMonsterCount() > 1)
                {
                    if (Card.IsFacedown())
                        return true;
                }
            }
            if (Card.IsFacedown())
                return false;
            return base.DefaultMonsterRepos();
        }
        public override bool OnSelectHand()
        {
            // go first
            return true;
        }
        public override bool OnPreBattleBetween(ClientCard attacker, ClientCard defender)
        {
            if (attacker.IsCode(CardId.CrystalWingSynchroDragon))
            {
                if (defender.Level >= 5)
                    attacker.RealPower = attacker.Attack + defender.Attack;
                return true;
            }
            else if (attacker.IsCode(CardId.DaigustoSphreez))
            {
                attacker.RealPower = attacker.Attack + defender.Attack + defender.Defense;
                return true;
            }
            else if (Bot.HasInMonstersZone(CardId.DaigustoSphreez) &&
                attacker.IsCode(CardId.DaigustoSphreez, CardId.GustoGulldo, CardId.GustoEgul, CardId.WindaPriestessOfGusto, CardId.PilicaDescendantOfGusto, CardId.DaigustoGulldos))
            {
                attacker.RealPower = attacker.Attack + defender.Attack + defender.Defense;
                return true;
            }
            return base.OnPreBattleBetween(attacker, defender);
        }

    }
}