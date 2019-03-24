using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
    [Deck("Salamangreat", "AI_Salamangreat", "NotFinished")]
    class SalamangreatExecutor : DefaultExecutor
    {
        bool foxyPopEnemySpell = false;
        bool falcoNoSummon = false;
        bool wasGazelleSummonedThisTurn = false;
        bool wasFieldspellUsedThisTurn = false;
        bool wasWolfSummonedUsingItself = false;
        int sunlightPosition = 0;
        bool wasVeilynxSummonedThisTurn = false;
        List<int> CombosInHand;


        List<int> Impermanence_list = new List<int>();
        public class CardId
        {
            public const int JackJaguar = 56003780;
            public const int EffectVeiler = 97268402;
            public const int LadyDebug = 16188701;
            public const int Foxy = 94620082;
            public const int Gazelle = 26889158;
            public const int Fowl = 89662401;
            public const int Falco = 20618081;
            public const int Spinny = 52277807;
            public const int MaxxC = 23434538;
            public const int AshBlossom = 14558127;

            public const int FusionOfFire = 25800447;
            public const int Circle = 52155219;
            public const int HarpieFeatherDuster = 18144507;
            public const int FoolishBurial = 81439174;
            public const int Sanctuary = 1295111;
            public const int SalamangreatRage = 14934922;
            public const int SalamangreatRoar = 51339637;
            public const int Impermanence = 10045474;
            public const int SolemnJudgment = 41420027;
            public const int SolemnStrike = 40605147;

            public const int SalamangreatVioletChimera = 37261776;
            public const int ExcitionKnight = 46772449;
            public const int MirageStallio = 87327776;
            public const int SunlightWolf = 87871125;
            public const int BorrelloadDragon = 31833038;
            public const int HeatLeo = 41463181;
            public const int Veilynx = 14812471;
            public const int Charmer = 48815792;
            public const int KnightmarePheonix = 2857636;

            public const int GO_SR = 59438930;
            public const int DarkHole = 53129443;
            public const int NaturalBeast = 33198837;
            public const int SwordsmanLV7 = 37267041;
            public const int RoyalDecreel = 51452091;
            public const int Anti_Spell = 58921041;
            public const int Hayate = 8491308;
            public const int Raye = 26077387;
            public const int Drones_Token = 52340445;
            public const int Iblee = 10158145;
            public const int ImperialOrder = 61740673;
        }

        List<int> Combo_cards = new List<int>()
        {
            CardId.Spinny,
            CardId.JackJaguar,
            CardId.Fowl,
            CardId.Foxy,
            CardId.Falco,
            CardId.Circle,
            CardId.Gazelle,
            CardId.FoolishBurial
        };

        List<int> normal_counter = new List<int>
        {
            53262004, 98338152, 32617464, 45041488, CardId.SolemnStrike,
            61257789, 23440231, 27354732, 12408276, 82419869, CardId.Impermanence,
            49680980, 18621798, 38814750, 17266660, 94689635,CardId.AshBlossom,
            74762582, 75286651, 4810828,  44665365, 21123811, _CardId.CrystalWingSynchroDragon,
            82044279, 82044280, 79606837, 10443957, 1621413,
            90809975, 8165596,  9753964,  53347303, 88307361, _CardId.GamecieltheSeaTurtleKaiju,
            5818294,  2948263,  6150044,  26268488, 51447164, _CardId.JizukirutheStarDestroyingKaiju,
            97268402
        };

        List<int> should_not_negate = new List<int>
        {
            81275020, 28985331
        };

        List<int> salamangreat_links = new List<int>
        {
            CardId.HeatLeo,
            CardId.SunlightWolf,
            CardId.Veilynx
        };

        List<int> JackJaguarTargets = new List<int>
        {
            CardId.SunlightWolf,
            CardId.MirageStallio,
            CardId.HeatLeo
        };


        List<int> salamangreat_combopieces = new List<int>
        {
            CardId.Gazelle,
            CardId.Spinny,
            CardId.JackJaguar,
            CardId.Foxy,
            CardId.Circle,
            CardId.Falco
        };

        List<int> WolfMaterials = new List<int>
        {
            CardId.Veilynx,
            CardId.JackJaguar,
            CardId.Gazelle
        };

        List<int> salamangreat_spellTrap = new List<int>
        {
            CardId.SalamangreatRoar,
            CardId.SalamangreatRage,
            CardId.Circle,
            CardId.FusionOfFire,
            CardId.Sanctuary
        };

        public SalamangreatExecutor(GameAI ai, Duel duel) : base(ai, duel)
        {
            AddExecutor(ExecutorType.Activate, CardId.HarpieFeatherDuster);
            AddExecutor(ExecutorType.Activate, CardId.MaxxC, G_activate);
            AddExecutor(ExecutorType.Activate, CardId.Impermanence, Impermanence_activate);
            AddExecutor(ExecutorType.Activate, CardId.AshBlossom, Hand_act_eff);
            AddExecutor(ExecutorType.Activate, CardId.EffectVeiler, DefaultBreakthroughSkill);
            AddExecutor(ExecutorType.Activate, CardId.SalamangreatRoar, SolemnJudgment_activate);
            AddExecutor(ExecutorType.Activate, CardId.SolemnStrike, SolemnStrike_activate);
            AddExecutor(ExecutorType.Activate, CardId.SolemnJudgment, SolemnJudgment_activate);
            AddExecutor(ExecutorType.Activate, CardId.SalamangreatRage, Rage_activate);
            AddExecutor(ExecutorType.Activate, CardId.FoolishBurial, FoolishBurial_activate);

            AddExecutor(ExecutorType.Activate, CardId.LadyDebug, Fadydebug_activate);
            AddExecutor(ExecutorType.Activate, CardId.Foxy, Foxy_activate);
            AddExecutor(ExecutorType.Activate, CardId.Falco, Falco_activate);
            AddExecutor(ExecutorType.Activate, CardId.Circle, Circle_activate);
            
            AddExecutor(ExecutorType.Activate, CardId.Veilynx);
            AddExecutor(ExecutorType.Activate, CardId.Gazelle, Gazelle_activate);
            AddExecutor(ExecutorType.Activate, CardId.Fowl, Fowl_activate);
            AddExecutor(ExecutorType.Activate, CardId.Spinny, Spinny_activate);
            AddExecutor(ExecutorType.Activate, CardId.JackJaguar, JackJaguar_activate);

            AddExecutor(ExecutorType.Summon, CardId.LadyDebug);
            AddExecutor(ExecutorType.Summon, CardId.Foxy);
            AddExecutor(ExecutorType.Summon, CardId.Gazelle);
            AddExecutor(ExecutorType.Summon, CardId.Spinny);
            AddExecutor(ExecutorType.Summon, CardId.JackJaguar);
            AddExecutor(ExecutorType.Summon, CardId.Fowl);



            AddExecutor(ExecutorType.SpSummon, CardId.Veilynx, Veilynx_summon);
            AddExecutor(ExecutorType.SpSummon, CardId.MirageStallio, Stallio_summon);
            AddExecutor(ExecutorType.Activate, CardId.MirageStallio, Stallio_activate);
            AddExecutor(ExecutorType.SpSummon, CardId.SunlightWolf, SunlightWolf_summon);




            AddExecutor(ExecutorType.Activate, CardId.Sanctuary, Sanctuary_activate);

            AddExecutor(ExecutorType.Activate, CardId.SunlightWolf, Wolf_activate);


            AddExecutor(ExecutorType.Repos, DefaultMonsterRepos);

            AddExecutor(ExecutorType.SpellSet, SpellSet);

        }

        private bool Stallio_summon()
        {
            AI.SelectMaterials(CardId.Spinny);
            return true;
        }

        private bool SunlightWolf_summon()
        {
            if (CombosInHand.Where(x => x != CardId.Foxy).Where(x => x != CardId.Spinny).Count() == 0)
            {
                if (Bot.HasInMonstersZone(CardId.MirageStallio)
                    && Bot.HasInMonstersZone(CardId.Veilynx)
                    && Bot.HasInMonstersZone(CardId.Gazelle))
                {
                    AI.SelectOption(0);
                    AI.SelectNextCard(CardId.MirageStallio);
                    sunlightPosition = SelectSetPlace(new List<int>() { CardId.Veilynx }, true);
                    AI.SelectPlace(sunlightPosition);
                    return true;
                }
                return false;
            }

            if (Bot.HasInMonstersZone(CardId.SunlightWolf))
            {
                if (wasWolfSummonedUsingItself)
                {
                    return false;
                }

                if (!wasFieldspellUsedThisTurn)
                {
                    AI.SelectOption(1);
                    AI.SelectMaterials(new List<int>() {
                        CardId.SunlightWolf,
                        CardId.Veilynx,
                        CardId.JackJaguar,
                        CardId.Gazelle
                    });
                    wasWolfSummonedUsingItself = true;
                    AI.SelectPlace(sunlightPosition);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            wasWolfSummonedUsingItself = false;
            if (Bot.HasInMonstersZone(CardId.Veilynx))
            {

                AI.SelectMaterials(WolfMaterials);
                sunlightPosition = SelectSetPlace(new List<int>() { CardId.Veilynx }, true);

                AI.SelectPlace(sunlightPosition);
            }
            return true;
        }

        private bool Wolf_activate()
        {
            AI.SelectCard(new List<int>() {
                CardId.Gazelle,
                CardId.SalamangreatRoar,
                CardId.SalamangreatRage,
                CardId.Foxy,
                CardId.AshBlossom,
                CardId.SunlightWolf,
                CardId.HeatLeo,
                CardId.Veilynx,
                CardId.Fowl,
                CardId.Spinny
            });
            return true;
        }

        private bool Stallio_activate()
        {
            if (Card.Location == CardLocation.MonsterZone)
            {
                if (!wasGazelleSummonedThisTurn)
                {
                    AI.SelectCard(CardId.Gazelle, CardId.Spinny);
                    AI.SelectNextCard(CardId.Gazelle);
                    return true;
                }
                if (!Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.JackJaguar))
                {
                    AI.SelectCard(CardId.Gazelle);
                    AI.SelectNextCard(CardId.JackJaguar);
                    return true;
                }
                if (!Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.Falco))
                {
                    AI.SelectCard(CardId.Gazelle);
                    AI.SelectNextCard(CardId.Falco);
                    return true;
                }
                AI.SelectCard(CardId.Gazelle);
                return true;
            }
            else
            {
                if (AI.Utils.GetBestEnemyMonster(canBeTarget: true) != null)
                {
                    AI.SelectCard();
                    return true;
                }
            }
            return false;
        }

        private bool Veilynx_summon()
        {
            if (Bot.HasInHand(CardId.Gazelle) && !wasGazelleSummonedThisTurn)
            {

                var monsters = Bot.GetMonstersInMainZone();
                if (Bot.HasInMonstersZone(CardId.Veilynx) && monsters.Count == 2)
                {
                    return false;
                }

                monsters.Sort(AIFunctions.CompareMonsterLevel);
                monsters.Reverse();
                AI.SelectMaterials(monsters);
                return true;
            }
            if (!Bot.HasInMonstersZone(CardId.Veilynx)
                &&
                Bot.GetMonstersInMainZone().Count >= 3
                &&
                (Bot.GetMonstersInExtraZone().Where(x => x.Owner == 0).Count() == 0))
            {
                return true;
            }


            if (CombosInHand.Where(x => x != CardId.Foxy).Where(x => x != CardId.Spinny).Count() == 0)
            {
                if (Bot.HasInMonstersZone(CardId.Gazelle) && Bot.HasInMonstersZone(CardId.SunlightWolf))
                {
                    AI.SelectMaterials(CardId.Gazelle);
                    return true;
                }
                if (!wasVeilynxSummonedThisTurn)
                {
                    wasVeilynxSummonedThisTurn = true;
                    return true;
                }
            }

            return false;
        }

        private bool JackJaguar_activate()
        {
            if (Card.Location == CardLocation.Grave)
            {
                if (Bot.HasInGraveyard(JackJaguarTargets))
                {
                    AI.SelectCard(JackJaguarTargets);
                    return true;
                }
            }
            return false;
        }

        private bool Fowl_activate()
        {
            if (Card.Location == CardLocation.Hand)
            {
                return Bot.HasInMonstersZone(CardId.JackJaguar);
            }
            return false;
        }

        private bool Spinny_activate()
        {
            if (Card.Location == CardLocation.Hand)
            {
                if (CombosInHand.Where(x => x != CardId.Foxy).Where(x => x != CardId.Spinny).Count() == 0)
                {
                    return false;
                }

                if (!Bot.HasInMonstersZoneOrInGraveyard(CardId.Spinny))
                {
                    AI.SelectCard(AI.Utils.GetBestBotMonster(true));
                    return true;
                }
            }
            return true;
        }

        private bool Falco_activate()
        {
            if (!falcoNoSummon)
            {
                if (Bot.HasInGraveyard(salamangreat_spellTrap))
                {
                    AI.SelectCard(salamangreat_spellTrap);
                    return true;
                }
                else
                {
                    falcoNoSummon = true;
                }
            }
            return false;
        }

        private bool Gazelle_activate()
        {
            wasGazelleSummonedThisTurn = true;
            if (!Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.Spinny))
            {
                AI.SelectCard(CardId.Spinny);
                return true;
            }
            if (!Bot.HasInSpellZoneOrInGraveyard(CardId.SalamangreatRoar))
            {
                AI.SelectCard(CardId.SalamangreatRoar);
                return true;
            }
            if (!Bot.HasInSpellZoneOrInGraveyard(CardId.SalamangreatRage))
            {
                AI.SelectCard(CardId.SalamangreatRage);
                return true;
            }
            if (!Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.JackJaguar))
            {
                AI.SelectCard(CardId.JackJaguar);
                return true;
            }
            if (!Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.Foxy))
            {
                AI.SelectCard(CardId.Foxy);
                return true;
            }
            if (!Bot.HasInHandOrInMonstersZoneOrInGraveyard(CardId.Falco))
            {
                AI.SelectCard(CardId.Falco);
                return true;
            }
            return true;
        }

        private bool Foxy_activate()
        {
            if (Card.Location == CardLocation.MonsterZone)
            {

                if (CombosInHand.Where(x => x != CardId.Foxy).Where(x => x != CardId.Spinny).Count() == 0)
                {
                    return false;
                }
                AI.SelectCard(salamangreat_combopieces);
                return true;
            }
            else
            {
                if (Bot.HasInHand(CardId.Spinny) || FalcoToGY(false))
                {
                    if (Bot.HasInHand(CardId.Spinny) && !Bot.HasInGraveyard(CardId.Spinny))
                    {
                        AI.SelectCard(CardId.Spinny);
                    }
                    else
                    {
                        if (FalcoToGY(false))
                        {
                            AI.SelectCard(CardId.Falco);
                        }
                    }
                    if (AI.Utils.GetBestEnemySpell(true) != null)
                    {
                        AI.SelectNextCard(AI.Utils.GetBestEnemySpell(true));
                        foxyPopEnemySpell = true;
                    }

                    return true;
                }
                return false;
            }
        }

        private bool FalcoToGY(bool FromDeck)
        {
            if (FromDeck && Bot.Deck.ContainsCardWithId(CardId.Falco))
            {
                if (Bot.HasInGraveyard(salamangreat_spellTrap))
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (Bot.HasInHand(CardId.Falco) && Bot.HasInGraveyard(salamangreat_spellTrap))
                {
                    return true;
                }
                return false;
            }
        }
        private bool Fadydebug_activate()
        {
            if (!Bot.HasInHand(CardId.Gazelle))
            {
                AI.SelectCard(CardId.Gazelle);
                return true;
            }
            if (!Bot.HasInHandOrInGraveyard(CardId.Spinny))
            {
                AI.SelectCard(CardId.Spinny);
                return true;
            }
            if (!Bot.HasInHand(CardId.Foxy))
            {
                AI.SelectCard(CardId.Foxy);
                return true;
            }
            return true;
        }

        private bool Circle_activate()
        {
            var x = ActivateDescription;
            if (ActivateDescription == AI.Utils.GetStringId(CardId.Circle, 0) || ActivateDescription == 0)
            {
                AI.SelectOption(0);
                if (!Bot.HasInHand(CardId.Gazelle))
                {
                    AI.SelectCard(CardId.Gazelle);
                    return true;
                }
                if (!Bot.HasInHandOrInGraveyard(CardId.Spinny))
                {
                    AI.SelectCard(CardId.Spinny);
                    return true;
                }
                if (!Bot.HasInHand(CardId.Foxy))
                {
                    AI.SelectCard(CardId.Foxy);
                    return true;
                }
                if (!Bot.HasInHand(CardId.Fowl))
                {
                    AI.SelectCard(CardId.Fowl);
                    return true;
                }
                if (!Bot.HasInHand(CardId.JackJaguar))
                {
                    AI.SelectCard(CardId.JackJaguar);
                    return true;
                }
                if (!Bot.HasInHand(CardId.Falco))
                {
                    AI.SelectCard(CardId.Falco);
                    return true;
                }
                return false;
            }

            return false;
        }

        private bool FoolishBurial_activate()
        {
            if (FalcoToGY(true) && Bot.HasInHandOrInGraveyard(CardId.Spinny))
            {
                AI.SelectCard(CardId.Falco);
                return true;
            }
            AI.SelectCard(CardId.Spinny, CardId.JackJaguar, CardId.Foxy);
            return true;
        }

        private bool Sanctuary_activate()
        {
            if (Card.Location == CardLocation.Hand)
            {
                return true;
            }
            return false;
        }

        private bool Rage_activate()
        {
            if (ActivateDescription == AI.Utils.GetStringId(CardId.SalamangreatRage, 1))
            {
                AI.SelectCard(salamangreat_links);
                AI.SelectOption(1);
                IList<ClientCard> targets = new List<ClientCard>();

                ClientCard target1 = AI.Utils.GetBestEnemyMonster(canBeTarget: true);
                if (target1 != null)
                    targets.Add(target1);
                ClientCard target2 = AI.Utils.GetBestEnemySpell();
                if (target2 != null)
                    targets.Add(target2);

                foreach (ClientCard target in Enemy.GetMonsters())
                {
                    if (targets.Count >= 3)
                        break;
                    if (!targets.Contains(target))
                        targets.Add(target);
                }
                foreach (ClientCard target in Enemy.GetSpells())
                {
                    if (targets.Count >= 3)
                        break;
                    if (!targets.Contains(target))
                        targets.Add(target);
                }
                if (targets.Count == 0)
                    return false;
                AI.SelectNextCard(targets);
                return true;
            }
            else
            {
                if (AI.Utils.GetProblematicEnemyCard(canBeTarget: true) != null)
                {
                    AI.SelectCard(AI.Utils.GetProblematicEnemyCard(canBeTarget: true));
                    return true;
                }
            }
            return false;
        }

        public bool G_activate()
        {
            return (Duel.Player == 1);
        }
        public bool Hand_act_eff()
        {
            return (Duel.LastChainPlayer == 1);
        }

        public bool Impermanence_activate()
        {
            if (!Should_counter()) return false;
            if (!spell_trap_activate()) return false;
            // negate before effect used
            foreach (ClientCard m in Enemy.GetMonsters())
            {
                if (m.IsMonsterShouldBeDisabledBeforeItUseEffect() && !m.IsDisabled() && Duel.LastChainPlayer != 0)
                {
                    if (Card.Location == CardLocation.SpellZone)
                    {
                        for (int i = 0; i < 5; ++i)
                        {
                            if (Bot.SpellZone[i] == Card)
                            {
                                Impermanence_list.Add(i);
                                break;
                            }
                        }
                    }
                    if (Card.Location == CardLocation.Hand)
                    {
                        AI.SelectPlace(SelectSTPlace(Card, true));
                    }
                    AI.SelectCard(m);
                    return true;
                }
            }

            ClientCard LastChainCard = AI.Utils.GetLastChainCard();

            // negate spells
            if (Card.Location == CardLocation.SpellZone)
            {
                int this_seq = -1;
                int that_seq = -1;
                for (int i = 0; i < 5; ++i)
                {
                    if (Bot.SpellZone[i] == Card) this_seq = i;
                    if (LastChainCard != null
                        && LastChainCard.Controller == 1 && LastChainCard.Location == CardLocation.SpellZone && Enemy.SpellZone[i] == LastChainCard) that_seq = i;
                    else if (Duel.Player == 0 && AI.Utils.GetProblematicEnemySpell() != null
                        && Enemy.SpellZone[i] != null && Enemy.SpellZone[i].IsFloodgate()) that_seq = i;
                }
                if ((this_seq * that_seq >= 0 && this_seq + that_seq == 4)
                    || (AI.Utils.IsChainTarget(Card))
                    || (LastChainCard != null && LastChainCard.Controller == 1 && LastChainCard.IsCode(_CardId.HarpiesFeatherDuster)))
                {
                    List<ClientCard> enemy_monsters = Enemy.GetMonsters();
                    enemy_monsters.Sort(AIFunctions.CompareCardAttack);
                    enemy_monsters.Reverse();
                    foreach (ClientCard card in enemy_monsters)
                    {
                        if (card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget())
                        {
                            AI.SelectCard(card);
                            Impermanence_list.Add(this_seq);
                            return true;
                        }
                    }
                }
            }
            if ((LastChainCard == null || LastChainCard.Controller != 1 || LastChainCard.Location != CardLocation.MonsterZone
                || LastChainCard.IsDisabled() || LastChainCard.IsShouldNotBeTarget() || LastChainCard.IsShouldNotBeSpellTrapTarget()))
                return false;
            // negate monsters
            if (is_should_not_negate() && LastChainCard.Location == CardLocation.MonsterZone) return false;
            if (Card.Location == CardLocation.SpellZone)
            {
                for (int i = 0; i < 5; ++i)
                {
                    if (Bot.SpellZone[i] == Card)
                    {
                        Impermanence_list.Add(i);
                        break;
                    }
                }
            }
            if (Card.Location == CardLocation.Hand)
            {
                AI.SelectPlace(SelectSTPlace(Card, true));
            }
            if (LastChainCard != null) AI.SelectCard(LastChainCard);
            else
            {
                List<ClientCard> enemy_monsters = Enemy.GetMonsters();
                enemy_monsters.Sort(AIFunctions.CompareCardAttack);
                enemy_monsters.Reverse();
                foreach (ClientCard card in enemy_monsters)
                {
                    if (card.IsFaceup() && !card.IsShouldNotBeTarget() && !card.IsShouldNotBeSpellTrapTarget())
                    {
                        AI.SelectCard(card);
                        return true;
                    }
                }
            }
            return true;
        }

        public bool is_should_not_negate()
        {
            ClientCard last_card = AI.Utils.GetLastChainCard();
            if (last_card != null
                && last_card.Controller == 1 && last_card.IsCode(should_not_negate))
                return true;
            return false;
        }

        public bool SolemnStrike_activate()
        {
            if (!Should_counter()) return false;
            return (DefaultSolemnStrike() && spell_trap_activate(true));
        }

        public bool SolemnJudgment_activate()
        {
            if (AI.Utils.IsChainTargetOnly(Card)) return false;
            if (!Should_counter()) return false;
            if ((DefaultSolemnJudgment() && spell_trap_activate(true)))
            {
                ClientCard target = AI.Utils.GetLastChainCard();
                if (target != null && !target.IsMonster() && !spell_trap_activate(false, target)) return false;
                return true;
            }
            return false;
        }

        public bool spell_trap_activate(bool isCounter = false, ClientCard target = null)
        {
            if (target == null) target = Card;
            if (target.Location != CardLocation.SpellZone && target.Location != CardLocation.Hand) return true;
            if (!isCounter && !Bot.HasInSpellZone(CardId.SolemnStrike)) return false;
            if (target.IsSpell())
            {
                if (Enemy.HasInMonstersZone(CardId.NaturalBeast, true) && !Bot.HasInHandOrHasInMonstersZone(CardId.GO_SR) && !isCounter && !Bot.HasInSpellZone(CardId.SolemnStrike)) return false;
                if (Enemy.HasInSpellZone(CardId.ImperialOrder, true) || Bot.HasInSpellZone(CardId.ImperialOrder, true)) return false;
                if (Enemy.HasInMonstersZone(CardId.SwordsmanLV7, true) || Bot.HasInMonstersZone(CardId.SwordsmanLV7, true)) return false;
                return true;
            }
            if (target.IsTrap())
            {
                if (Enemy.HasInSpellZone(CardId.RoyalDecreel, true) || Bot.HasInSpellZone(CardId.RoyalDecreel, true)) return false;
                return true;
            }
            // how to get here?
            return false;
        }

        public bool Should_counter()
        {
            if (Duel.CurrentChain.Count < 2) return true;
            ClientCard self_card = Duel.CurrentChain[Duel.CurrentChain.Count - 2];
            if (self_card?.Controller != 0
                || !(self_card.Location == CardLocation.MonsterZone || self_card.Location == CardLocation.SpellZone)) return true;
            ClientCard enemy_card = Duel.CurrentChain[Duel.CurrentChain.Count - 1];
            if (enemy_card?.Controller != 1
                || !enemy_card.IsCode(normal_counter)) return true;
            return false;
        }
        public int SelectSTPlace(ClientCard card = null, bool avoid_Impermanence = false)
        {
            List<int> list = new List<int>();
            list.Add(0);
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            int n = list.Count;
            while (n-- > 1)
            {
                int index = Program.Rand.Next(n + 1);
                int temp = list[index];
                list[index] = list[n];
                list[n] = temp;
            }
            foreach (int seq in list)
            {
                int zone = (int)System.Math.Pow(2, seq);
                if (Bot.SpellZone[seq] == null)
                {
                    if (card != null && card.Location == CardLocation.Hand && avoid_Impermanence && Impermanence_list.Contains(seq)) continue;
                    return zone;
                };
            }
            return 0;
        }

        public override bool OnSelectYesNo(int desc)
        {
            if (desc == AI.Utils.GetStringId(CardId.Sanctuary, 0))
            {
                wasFieldspellUsedThisTurn = true;
            }
            if (desc == AI.Utils.GetStringId(CardId.Foxy, 3))
            {
                return foxyPopEnemySpell;
            }
            return base.OnSelectYesNo(desc);
        }

        public override void OnNewTurn()
        {
            CombosInHand = Bot.Hand.Select(h => h.Id).Intersect(Combo_cards).ToList();
            wasFieldspellUsedThisTurn = false;
            wasGazelleSummonedThisTurn = false;
            base.OnNewTurn();
        }

        public override bool OnSelectHand()
        {
            return true;
        }

        public bool SpellSet()
        {
            if (Card.Id == CardId.Circle)
            {
                return false;
            }
            if (Duel.Phase == DuelPhase.Main1 && Bot.HasAttackingMonster() && Duel.Turn > 1) return false;
            if (Card.IsCode(CardId.SolemnStrike) && Bot.LifePoints <= 1500) return false;
            if ((Card.IsTrap() || Card.HasType(CardType.QuickPlay)))
            {
                List<int> avoid_list = new List<int>();
                int Impermanence_set = 0;
                for (int i = 0; i < 5; ++i)
                {
                    if (Enemy.SpellZone[i] != null && Enemy.SpellZone[i].IsFaceup() && Bot.SpellZone[4 - i] == null)
                    {
                        avoid_list.Add(4 - i);
                        Impermanence_set += (int)System.Math.Pow(2, 4 - i);
                    }
                }
                if (Bot.HasInHand(CardId.Impermanence))
                {
                    if (Card.IsCode(CardId.Impermanence))
                    {
                        AI.SelectPlace(Impermanence_set);
                        return true;
                    }
                    else
                    {
                        AI.SelectPlace(SelectSetPlace(avoid_list));
                        return true;
                    }
                }
                else
                {
                    AI.SelectPlace(SelectSTPlace());
                }
                return true;
            }
            return false;
        }

        public int SelectSetPlace(List<int> avoid_list = null, bool avoid = true)
        {
            List<int> list = new List<int>();
            list.Add(5);
            list.Add(6);
            int n = list.Count;
            while (n-- > 1)
            {
                int index = Program.Rand.Next(n + 1);
                int temp = list[index];
                list[index] = list[n];
                list[n] = temp;
            }
            foreach (int seq in list)
            {
                int zone = (int)System.Math.Pow(2, seq);

                if (Bot.MonsterZone[seq] == null || !avoid)
                {
                    if (avoid)
                    {
                        if (avoid_list != null && avoid_list.Contains(seq)) continue;
                        return zone;
                    }
                    else
                    {
                        if (avoid_list != null && avoid_list.Contains(seq))
                        {
                            return list.First(x => x == seq);
                        }
                        continue;
                    }

                };
            }
            return 0;
        }
    }
}
