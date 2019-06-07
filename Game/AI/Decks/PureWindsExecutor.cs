using System;
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
            public const int WindwitchIceBell = 47322862;
            public const int PilicaDescendantOfGusto = 71175527;
            public const int SpeedroidTaketomborg = 53932291;
            public const int WindaPriestessOfGusto = 54455435;
            public const int WindwitchGlassBell = 71007216;

            public const int GustoGulldo = 65277087;
            public const int GustoEgul = 91662792;
            public const int WindwitchSnowBell = 70117860;
            public const int SpeedroidRedEyedDice = 16725505;
            public const int Raigeki = 12580477;
            public const int MonsterReborn = 83764718;
            public const int Reasoning = 58577036;

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
        }
        ClientCard CosmicCycloneTarget = null;
        ClientCard MistWurmTarget = null;
        ClientCard ForbiddenChaliceTarget = null;

        public PureWindsExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            //counter
            AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, SolemnStrikeeff);
            AddExecutor(ExecutorType.Activate, CardId.SolemnWarning, SolemnWarningeff);
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
            AddExecutor(ExecutorType.Summon, CardId.WindwitchGlassBell, WindwitchGlassBellsummonfirst);
            AddExecutor(ExecutorType.Summon, CardId.PilicaDescendantOfGusto, PilicaDescendantOfGustosu);
            AddExecutor(ExecutorType.Summon, CardId.GustoGulldo, GustoGulldosu);
            AddExecutor(ExecutorType.Summon, CardId.GustoEgul, GustoEgulsu);
            AddExecutor(ExecutorType.Summon, CardId.WindaPriestessOfGusto, WindaPriestessOfGustosu);
            AddExecutor(ExecutorType.Summon, CardId.SpeedroidRedEyedDice, SpeedroidRedEyedDicesu);
            AddExecutor(ExecutorType.Summon, CardId.WindwitchGlassBell, WindwitchGlassBellsummon);
            //other thing
            AddExecutor(ExecutorType.SpSummon, CardId.MistWurm);
            AddExecutor(ExecutorType.SpSummon, CardId.DaigustoGulldos);
            AddExecutor(ExecutorType.SpSummon, CardId.HiSpeedroidChanbara);
            AddExecutor(ExecutorType.SpSummon, CardId.StardustChargeWarrior);
            AddExecutor(ExecutorType.SpSummon, CardId.OldEntityHastorr);
            AddExecutor(ExecutorType.SpSummon, CardId.GreatFly, GreatFlysp);
            AddExecutor(ExecutorType.SpSummon, CardId.WynnTheWindCharmerVerdant, WynnTheWindCharmerVerdantsp);
            AddExecutor(ExecutorType.MonsterSet, CardId.GustoGulldo);
            AddExecutor(ExecutorType.MonsterSet, CardId.GustoEgul);
            AddExecutor(ExecutorType.MonsterSet, CardId.WindaPriestessOfGusto);
            AddExecutor(ExecutorType.Activate, CardId.MonsterReborn);
            AddExecutor(ExecutorType.Activate, CardId.Raigeki);
            AddExecutor(ExecutorType.Activate, CardId.GozenMatch);
            AddExecutor(ExecutorType.Activate, CardId.KingsConsonance);
            AddExecutor(ExecutorType.Activate, CardId.SuperTeamBuddyForceUnite, SuperTeamBuddyForceUniteeff);
            //trap set
            AddExecutor(ExecutorType.SpellSet, CardId.SolemnStrike);
            AddExecutor(ExecutorType.SpellSet, CardId.SolemnWarning);
            AddExecutor(ExecutorType.SpellSet, CardId.SuperTeamBuddyForceUnite, SuperTeamBuddyForceUniteeff);
            AddExecutor(ExecutorType.SpellSet, CardId.ForbiddenChalice);

            AddExecutor(ExecutorType.Repos, MonsterRepos);
        }

        private bool GreatFlysp()
        {
            if(Bot.HasInMonstersZone(CardId.DaigustoSphreez))
                return false;
            if(Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon))
                return false;
            if(Bot.HasInMonstersZone(CardId.ClearWingSynchroDragon))
                return false;
            if(Bot.HasInMonstersZone(CardId.HiSpeedroidChanbara))
                return false;
            if(Bot.HasInMonstersZone(CardId.WindwitchWinterBell))
                return false;
            return true;
        }
        private bool WynnTheWindCharmerVerdantsp()
        {
            if (Bot.HasInMonstersZone(CardId.KnightmareIblee))
                return true;
            return false;
        }
        private bool MistWurmeff()
        {
            if (Bot.HasInSpellZone(CardId.OldEntityHastorr))
            {
                AI.SelectCard(CardId.OldEntityHastorr);
            }
            else
            {
                AI.SelectCard(Util.GetBestEnemyMonster());
            }
            return true;
        }
        private bool GustoGulldosu()
        {
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                return true;
            }
            else if ((Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.MistWurm) ||
                Bot.HasInMonstersZone(CardId.ClearWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.WindwitchWinterBell) ||
                Bot.HasInMonstersZone(CardId.ClearWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.StardustChargeWarrior)) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant) &&
                !Bot.HasInMonstersZone(CardId.GreatFly))
            {
                return false;
            }
            else if (Bot.HasInMonstersZone(CardId.SpeedroidTerrortop) ||
                Bot.HasInMonstersZone(CardId.PilicaDescendantOfGusto) ||
                Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto) ||
                Bot.HasInMonstersZone(CardId.WindwitchIceBell) ||
                Bot.HasInMonstersZone(CardId.SpeedroidTaketomborg) ||
                Bot.HasInMonstersZone(CardId.OldEntityHastorr) ||
                Bot.HasInMonstersZone(CardId.HiSpeedroidChanbara) ||
                Bot.HasInMonstersZone(CardId.DaigustoGulldos) ||
                Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
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
                return true;
            else if ((Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.MistWurm)) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant) &&
                !Bot.HasInMonstersZone(CardId.GreatFly))
                return false;
            else if (Bot.HasInMonstersZone(CardId.SpeedroidTerrortop) ||
                Bot.HasInMonstersZone(CardId.PilicaDescendantOfGusto) ||
                Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto) ||
                Bot.HasInMonstersZone(CardId.WindwitchIceBell) ||
                Bot.HasInMonstersZone(CardId.SpeedroidTaketomborg) ||
                Bot.HasInMonstersZone(CardId.OldEntityHastorr) ||
                Bot.HasInMonstersZone(CardId.HiSpeedroidChanbara) ||
                Bot.HasInMonstersZone(CardId.DaigustoGulldos) ||
                Bot.HasInMonstersZone(CardId.DaigustoSphreez) ||
                Bot.HasInMonstersZone(CardId.StardustChargeWarrior) ||
                Bot.HasInMonstersZone(CardId.WindwitchWinterBell) ||
                Bot.HasInMonstersZone(CardId.ClearWingSynchroDragon))
                return true;
            return false;
        }
        private bool WindaPriestessOfGustosu()
        {
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez) && 
                !Bot.HasInHand(CardId.GustoGulldo) &&
                !Bot.HasInHand(CardId.GustoEgul))
                return true;
            else if ((Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.MistWurm)) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant) &&
                !Bot.HasInMonstersZone(CardId.GreatFly))
                return false;
            else if (Bot.HasInMonstersZone(CardId.GustoGulldo) ||
                Bot.HasInMonstersZone(CardId.WindwitchGlassBell) || 
                (Bot.HasInMonstersZone(CardId.PilicaDescendantOfGusto) &&
                (Bot.HasInMonstersZone(CardId.GustoEgul) ||
                Bot.HasInMonstersZone(CardId.WindwitchSnowBell) ||
                Bot.HasInMonstersZone(CardId.SpeedroidRedEyedDice))) ||
                (Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto) &&
                (Bot.HasInMonstersZone(CardId.GustoEgul) ||
                Bot.HasInMonstersZone(CardId.WindwitchSnowBell) ||
                Bot.HasInMonstersZone(CardId.SpeedroidRedEyedDice))) ||
                (Bot.HasInMonstersZone(CardId.SpeedroidTaketomborg) &&
                (Bot.HasInMonstersZone(CardId.GustoEgul) ||
                Bot.HasInMonstersZone(CardId.WindwitchSnowBell) ||
                Bot.HasInMonstersZone(CardId.SpeedroidRedEyedDice))) ||
                (Bot.HasInMonstersZone(CardId.OldEntityHastorr) &&
                (Bot.HasInMonstersZone(CardId.GustoEgul) ||
                Bot.HasInMonstersZone(CardId.WindwitchSnowBell) ||
                Bot.HasInMonstersZone(CardId.SpeedroidRedEyedDice))))
                return true;
            return false;
        }
        private bool SpeedroidRedEyedDicesu()
        {
            if ((Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.MistWurm)) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant) &&
                !Bot.HasInMonstersZone(CardId.GreatFly))
                return false;
            else if ((Bot.HasInMonstersZone(CardId.SpeedroidTerrortop) ||
                Bot.HasInMonstersZone(CardId.PilicaDescendantOfGusto) ||
                Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto) ||
                Bot.HasInMonstersZone(CardId.WindwitchIceBell) ||
                Bot.HasInMonstersZone(CardId.SpeedroidTaketomborg) ||
                Bot.HasInMonstersZone(CardId.OldEntityHastorr) ||
                Bot.HasInMonstersZone(CardId.HiSpeedroidChanbara) ||
                Bot.HasInMonstersZone(CardId.DaigustoGulldos) ||
                Bot.HasInMonstersZone(CardId.StardustChargeWarrior) ||
                Bot.HasInMonstersZone(CardId.WindwitchWinterBell) ||
                Bot.HasInMonstersZone(CardId.ClearWingSynchroDragon)))
                return true;
            return false;
        }
        private bool PilicaDescendantOfGustosu()
        {
            if ((Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) ||
                Bot.HasInMonstersZone(CardId.MistWurm)) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant) &&
                !Bot.HasInMonstersZone(CardId.GreatFly))
                return false;
            else if (!Bot.HasInMonstersZoneOrInGraveyard(CardId.GustoGulldo)
                && !Bot.HasInMonstersZoneOrInGraveyard(CardId.SpeedroidRedEyedDice)
                && !Bot.HasInMonstersZoneOrInGraveyard(CardId.GustoEgul)
                && !Bot.HasInMonstersZoneOrInGraveyard(CardId.WindwitchGlassBell)
                && !Bot.HasInMonstersZoneOrInGraveyard(CardId.WindwitchSnowBell))
                return false;
            return true;
        }
        private bool SpeedroidRedEyedDiceeff()
        {
            if (Bot.HasInMonstersZone(CardId.SpeedroidTerrortop))
            {
                AI.SelectCard(CardId.SpeedroidTerrortop);
                AI.SelectOption(6);
                return true;
            }
            else if (Bot.HasInMonstersZone(CardId.SpeedroidTaketomborg))
            {
                AI.SelectCard(CardId.SpeedroidTaketomborg);
                AI.SelectOption(6);
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
            if (Bot.GetRemainingCount(CardId.SpeedroidRedEyedDice, 1) >= 1)
            {
                AI.SelectCard(CardId.SpeedroidRedEyedDice);
                return true;
            }
            return false;
        }

        private bool QuillPenOfGulldoseff()
        {
            if (Bot.HasInSpellZone(CardId.OldEntityHastorr))
            {
                AI.SelectCard(CardId.OldEntityHastorr);
            }
            else
            {
                AI.SelectCard(Util.GetBestEnemyMonster());
            }
            return true;
        }

        private bool WindwitchIceBelleff()
        {
            if (lockbird_used) return false;
            if (Enemy.HasInMonstersZone(CardId.ElShaddollWinda)) return false;
            if (maxxc_used) return false;
            if (WindwitchGlassBelleff_used) return false;
            //AI.SelectPlace(Zones.z2, 1);
            if (Bot.GetRemainingCount(CardId.WindwitchGlassBell, 3) >= 1)
                AI.SelectCard(CardId.WindwitchGlassBell);
            else if (Bot.HasInHand(CardId.WindwitchGlassBell))
                AI.SelectCard(CardId.WindwitchSnowBell);
            AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
        private bool SpeedroidTaketomborgsp()
        {
            if (speedroidtaketomborgsp_used) return false;
            else if (Bot.HasInMonstersZone(CardId.CrystalWingSynchroDragon) &&
                !Bot.HasInMonstersZone(CardId.WynnTheWindCharmerVerdant))
            {
                return false;
            }
            else if (Bot.HasInMonsterZone(CardId.WindwitchGlassBell) || 
                Bot.HasInMonstersZone(CardId.GustoGulldo) || 
                Bot.HasInMonstersZone(CardId.GustoEgul) || 
                Bot.HasInMonstersZone(CardId.SpeedroidRedEyedDice) || 
                Bot.HasInMonstersZone(CardId.WindwitchSnowBell))
            {
                AI.SelectPosition(CardPosition.FaceUpDefence);
                speedroidtaketomborgsp_used = true;
                return true;
            }
            return false;

        }
        private bool WindwitchGlassBelleff()
        {
            if (Bot.HasInMonstersZone(CardId.WindwitchIceBell))
            {
                 int ghost_count = 0;
                 foreach (ClientCard check in Enemy.Graveyard)
                 {
                    if (check.IsCode(CardId.Ghost))
                         ghost_count++;
                 }
                 if (ghost_count != ghost_done)
                          AI.SelectCard(CardId.WindwitchIceBell);
                 else
                         AI.SelectCard(CardId.WindwitchSnowBell);
            }
            else
                 AI.SelectCard(CardId.WindwitchIceBell);
                    WindwitchGlassBelleff_used = true;
                     return true;
        }

            private bool OldEntityHastorreff()
            {
                    AI.SelectCard(Util.GetBestEnemyMonster());
            }

            private bool WynnTheWindCharmerVerdanteff()
            {
                    AI.SelectCard(CardId.PilicaDescendantOfGusto);
                    AI.SelectCard(CardId.WindwitchIceBell);
                    AI.SelectCard(CardId.SpeedroidTerrortop);
                    AI.SelectCard(CardId.GustoGulldo);
                    AI.SelectCard(CardId.GustoEgul);
                    AI.SelectCard(CardId.WindaPriestessOfGusto);
            }
            private bool SpeedroidTerrortopeff()
            {
                    AI.SelectCard(CardId.SpeedroidTaketomborg);
                    AI.SelectCard(CardId.SpeedroidRedEyedDice);
                    SpeedroidTerrortop_used = true;
                    return true;
            }
            private bool GreatFlyeff()
            {
                AI.SelectCard(CardId.PilicaDescendantOfGusto);
                AI.SelectCard(CardId.WindwitchIceBell);
                AI.SelectCard(CardId.SpeedroidTerrortop);
                AI.SelectCard(CardId.GustoGulldo);
                AI.SelectCard(CardId.GustoEgul);
                AI.SelectCard(CardId.WindaPriestessOfGusto);
                AI.SelectPosition(CardPosition.FaceUpDefence);
                GreatFlyeff_used = true;
                return true;
            }

            private bool PilicaDescendantOfGustoeff()
            {
                AI.SelectCard(CardId.GustoGulldo);
                AI.SelectCard(CardId.WindwitchGlassBell);
                AI.SelectCard(CardId.WindwitchSnowBell);
                AI.SelectCard(CardId.GustoEgul);
                AI.SelectCard(CardId.SpeedroidRedEyedDice);
                PilicaDescendantOfGustoeff_used = true;
                return true;
            }

            private bool SuperTeamBuddyForceUniteeff()
            {
                if (Bot.HasInGraveyard(CardId.PilicaDescendantOfGusto) && Bot.HasInMonstersZone(CardId.DaigustoSphreez))
			    {
                    AI.SelectCard(CardId.DaigustoSphreez, CardId.PilicaDescendantOfGusto);
                    AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
                }

                else if (Bot.HasInGraveyard(CardId.WindaPriestessOfGusto) && Bot.HasInMonstersZone(CardId.DaigustoSphreez))
			    {
                    AI.SelectCard(CardId.DaigustoSphreez, CardId.WindaPriestessOfGusto);
                    AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
                }

                else if (Bot.HasInGraveyard(CardId.DaigustoSphreez)  && Bot.HasInMonstersZone(CardId.PilicaDescendantOfGusto))
			    {
                    AI.SelectCard(CardId.PilicaDescendantOfGusto, CardId.DaigustoSphreez);
                    AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
                }

                else if (Bot.HasInGraveyard(CardId.DaigustoSphreez)  && Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto))
			    {
                    AI.SelectCard(CardId.WindaPriestessOfGusto, CardId.DaigustoSphreez);
                    AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
                }
                else if (Bot.HasInGraveyard(CardId.DaigustoGulldos)  && Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto))
			    {
                    AI.SelectCard(CardId.WindaPriestessOfGusto, CardId.DaigustoGulldos);
                    AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
                }
                else if (Bot.HasInGraveyard(CardId.DaigustoGulldos)  && Bot.HasInMonstersZone(CardId.PilicaDescendantOfGusto))
			    {
                    AI.SelectCard(CardId.DaigustoGulldos, CardId.PilicaDescendantOfGusto);
                    AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
                }
                else if (Bot.HasInGraveyard(CardId.DaigustoSphreez)  && Bot.HasInMonstersZone(CardId.DaigustoGulldos))
			    {
                    AI.SelectCard(CardId.DaigustoGulldos, CardId.DaigustoSphreez);
                    AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
                }
                else if (Bot.HasInGraveyard(CardId.CrystalWingSynchroDragon))
                {
                    AI.SelectCard();
                    AI.SelectNextCard(CardId.CrystalWingSynchroDragon);
                    AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
                }
                else if (Bot.HasInGraveyard(CardId.CrystalWingSynchroDragon))
                {
                    AI.SelectCard();
                    AI.SelectNextCard(CardId.ClearWingSynchroDragon);
                    AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
                }
                else if (Bot.HasInGraveyard(CardId.WindwitchWinterBell))
                {
                    AI.SelectCard();
                    AI.SelectCard(CardId.WindwitchWinterBell);
                    AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
                }
                else if (Bot.HasInGraveyard(CardId.WindwitchSnowBell) && Bot.HasInMonstersZone(CardId.WindwitchIceBell))
			    {
                    AI.SelectCard(CardId.WindwitchIceBell, CardId.WindwitchSnowBell);
                    AI.SelectPosition(CardPosition.FaceUpDefence);
                return true;
                }

                else if (Bot.HasInGraveyard(CardId.PilicaDescendantOfGusto)  && Bot.HasInMonstersZone(CardId.WindaPriestessOfGusto))
			    {
                    AI.SelectCard(CardId.WindaPriestessOfGusto, CardId.PilicaDescendantOfGusto);
                    AI.SelectPosition(CardPosition.FaceUpDefence);
                }
            return false;
        }

        private bool WindwitchSnowBellsp()
        {
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
                AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
        }
        private bool DaigustoSphreezeff()
        {
            AI.SelectCard(CardId.PilicaDescendantOfGusto);
            AI.SelectCard(CardId.GustoGulldo);
            AI.SelectCard(CardId.GustoEgul);
            AI.SelectCard(CardId.WindaPriestessOfGusto);
                return true;
        }
        private bool WindwitchWinterBelleff()
        {
            AI.SelectCard(CardId.WindwitchGlassBell);
            return true;
        }

        private bool WindwitchWinterBellsp()
        {
            if (Bot.HasInMonstersZone(CardId.WindwitchIceBell) &&
                 Bot.HasInMonstersZone(CardId.WindwitchGlassBell) &&
                 Bot.HasInMonstersZone(CardId.WindwitchSnowBell))
            {
                //AI.SelectPlace(Zones.z5, Zones.ExtraMonsterZones);
                AI.SelectCard(CardId.WindwitchIceBell, CardId.WindwitchGlassBell);
                AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
            }

            return false;
        }

        private bool ClearWingSynchroDragonsp()
        {
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez))
                return false;
            AI.SelectPosition(CardPosition.FaceUpAttack);
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
        private bool SolemnStrikeeff()
        {
            if (Bot.LifePoints > 1500 && Duel.LastChainPlayer == 1)
            {
                return true;
            }
            return false;
        }
        private bool SolemnWarningeff()
        {
            if (Bot.LifePoints > 2000 && Duel.LastChainPlayer == 1)
            {
                return true;
            }
            return false;
        }
        private bool ForbiddenChaliceeff()
        {
            if (Duel.LastChainPlayer == 1)
            {
                ClientCard target = Util.GetProblematicEnemyMonster(0, true);
                if (target != null)
                {
                    ForbiddenChaliceTarget = target;
                    AI.SelectCard(target);
                    return true;
                }
            }
            return false;
        }
        private bool CosmicCycloneeff()
        {
            if (Duel.LastChainPlayer == 1)
                return true;
            return false;
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
                AI.SelectCard(CardId.GustoEgul);
                AI.SelectCard(CardId.WindaPriestessOfGusto);
                AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
            }
            AI.SelectCard(CardId.GustoEgul);
            AI.SelectCard(CardId.WindaPriestessOfGusto);
            AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
		private bool GustoEguleff()
        {
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                AI.SelectCard(CardId.WindaPriestessOfGusto);
                AI.SelectCard(CardId.PilicaDescendantOfGusto);
                AI.SelectPosition(CardPosition.FaceUpAttack);
                return true;
            }
            AI.SelectCard(CardId.WindaPriestessOfGusto);
            AI.SelectCard(CardId.PilicaDescendantOfGusto);
            AI.SelectPosition(CardPosition.FaceUpDefence);
            return true;
        }
		private bool WindaPriestessOfGustoeff()
        {
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                AI.SelectCard(CardId.GustoGulldo);
                AI.SelectCard(CardId.GustoEgul);
                AI.SelectPosition(CardPosition.FaceUpAttack);
            }
            AI.SelectCard(CardId.GustoGulldo);
            AI.SelectCard(CardId.GustoEgul);
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
            else if (!Bot.HasInHand(CardId.WindwitchIceBell))
            {
                return true;
            }
            return false;
        }
        private bool WindwitchGlassBellsummon()
        {
            if (lockbird_used) return false;
            if (!plan_A && (Bot.HasInGraveyard(CardId.WindwitchGlassBell) || Bot.HasInMonstersZone(CardId.WindwitchGlassBell)))
                return false;
            //AI.SelectPlace(Zones.z2, 1);
            if (GlassBell_summon && Bot.HasInMonstersZone(CardId.WindwitchIceBell) &&
                !Bot.HasInMonstersZone(CardId.WindwitchGlassBell))
                return true;
            if (WindwitchGlassBelleff_used) return false;
            if (GlassBell_summon) return true;
            return false;
        }

        public bool MonsterRepos()
        {
            if (Bot.HasInMonstersZone(CardId.DaigustoSphreez))
            {
                if (Card.IsAttack(CardId.GustoGulldo) ||
                    Card.IsAttack(CardId.GustoEgul) ||
                    Card.IsAttack(CardId.WindaPriestessOfGusto) ||
                    Card.IsAttack(CardId.PilicaDescendantOfGusto) ||
                    Card.IsAttack(CardId.DaigustoGulldos))
                    return false;
            }
            if (Card.IsFacedown())
                return true;
            return base.DefaultMonsterRepos();
        }

    }
}