using System.Collections.Generic;
using YGOSharp.OCGWrapper.Enums;

namespace WindBot.Game.AI.Decks
{
    [Deck("God", "AI_God")]  
    public class GodExecutor : DefaultExecutor
    {
        private bool wasChickenGameActivated = false;
        private bool noFieldSpell = true;
        private bool wasMakyuraUsedThisTurn=false;
        private int MakyuraCount = 0;
        private bool chickenGameCantDraw = false;
        //potential draw in one chain.
        private int potentialDraw = 0;
        
        public class CardId
        {
            public const int Makyura = 21593977;
            public const int PotOfGreed = 55144522;
            public const int GracefulCharity = 79571449;
            public const int PainfulChoice = 74191942;
            public const int OneDayOfPeace = 33782437;
            public const int UpstartGoblin = 70368879;
            public const int ChickenGame = 67616300;
            public const int MagicalMallet = 85852291;
            public const int PotOfDuality = 98645731;
            public const int RecklessGreed = 37576645;
            public const int HopeForEscape = 80036543;
            public const int JarOfAvarice = 98954106;
            public const int SixthSense = 3280747;
            public const int Exodia = 33396948;
            public const int RightArm = 70903634;
            public const int LeftArm = 7902349;
            public const int LeftLeg = 44519536;
            public const int RightLeg = 8124921;
        }

        public GodExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
         
        // Cycle begins
            AddExecutor(ExecutorType.Activate, CardId.UpstartGoblin, UpstartGoblinEffect);
            AddExecutor(ExecutorType.Activate, CardId.PainfulChoice, PainfulChoiceEffect);
            AddExecutor(ExecutorType.Activate, CardId.PotOfGreed, PotOfGreedEffect);
            AddExecutor(ExecutorType.Activate, CardId.PotOfDuality, PotOfDualityEffect);
            AddExecutor(ExecutorType.Activate, CardId.ChickenGame, ChickenGameField);
            AddExecutor(ExecutorType.Activate, CardId.ChickenGame, ChickenGameEffect);
            AddExecutor(ExecutorType.Activate, CardId.OneDayOfPeace, OneDayOfPeaceEffect);
            
            // Power Overwhelming
            AddExecutor(ExecutorType.Activate, CardId.RecklessGreed, RecklessGreedEffect);
            AddExecutor(ExecutorType.Activate, CardId.HopeForEscape, HopeForEscapeEffect);
            AddExecutor(ExecutorType.Activate, CardId.SixthSense, SixthSenseEffect);
            AddExecutor(ExecutorType.Activate, CardId.GracefulCharity, GracefulCharityEffect);
            AddExecutor(ExecutorType.Activate, CardId.MagicalMallet, MagicalMalletEffect);
            AddExecutor(ExecutorType.Activate, CardId.JarOfAvarice, JarOfAvariceEffect);
        }

        private bool OneDayOfPeaceEffect()
        {
            updatePotentialDraw(1);
            return true;
        }

        private bool UpstartGoblinEffect()
        {
            updatePotentialDraw(1);
            return true;
        }

        private void updatePotentialDraw(int draw)
        {
            potentialDraw+=draw;
        }

        private int[] ExodiaPieces = { CardId.Exodia, CardId.RightArm, CardId.RightLeg, CardId.LeftLeg, CardId.LeftArm };

        private bool ChickenGameField()
        {        
            if ((wasChickenGameActivated || noFieldSpell) && Card.Location == CardLocation.Hand &&!chickenGameCantDraw)
            {
                wasChickenGameActivated = false;
                noFieldSpell = false;
                return true;
            }
            return false;
        }

        private bool ChickenGameEffect()
        {
            updatePotentialDraw(1);
            if (!wasChickenGameActivated&&Card.Location==CardLocation.SpellZone && Bot.LifePoints>1000 && ActivateDescription == AI.Utils.GetStringId(CardId.ChickenGame, 0))
            {
                AI.SelectOption(0);
                wasChickenGameActivated = true;
                return true;
            }
            if (!wasChickenGameActivated && Card.Location == CardLocation.SpellZone && Bot.LifePoints > 1000 && ActivateDescription != AI.Utils.GetStringId(CardId.ChickenGame, 0))
            {
                wasChickenGameActivated = true;
                chickenGameCantDraw = true;
            }
            updatePotentialDraw(-1);
            return false;
        }

        public override bool OnSelectHand()
        {
            return true;
        }

        private bool PotOfGreedEffect()
            {
            updatePotentialDraw(2);
            if(Bot.Deck.Count >= potentialDraw)
            {
                return true;
            }
            updatePotentialDraw(-2);
            return false;
            }


        //To rethink!
        private bool GracefulCharityEffect()
        {
            updatePotentialDraw(3);
            if (Bot.Deck.Count >= potentialDraw)
            {
                AI.SelectCard(
               CardId.Makyura,
               CardId.SixthSense,
               CardId.HopeForEscape,
               CardId.JarOfAvarice,
               CardId.RecklessGreed,
               CardId.OneDayOfPeace,
               CardId.ChickenGame,
               CardId.UpstartGoblin,
               CardId.PainfulChoice,
               CardId.MagicalMallet

               );
                return true;
            }
            updatePotentialDraw(-3);
            return false; 
        }
        private int ExodiaPiecesInHand()
        {
           int result = 0;
         foreach(int piece in ExodiaPieces)
            {
                if (Bot.HasInHand(piece))
                {
                    result++;
                }
            }
            return result;
        }
        private bool PotOfDualityEffect()
        {
            updatePotentialDraw(1);
            if (ExodiaPiecesInHand()==4)
            {
                AI.SelectCard(
                CardId.Exodia,
                CardId.RightArm,
                CardId.LeftArm,
                CardId.RightLeg,
                CardId.LeftLeg,
                CardId.PainfulChoice,
                CardId.PotOfGreed,
                CardId.GracefulCharity,
                CardId.UpstartGoblin,
                CardId.ChickenGame,
                CardId.OneDayOfPeace,
                CardId.MagicalMallet,
                CardId.RecklessGreed,
                CardId.HopeForEscape,
                CardId.JarOfAvarice,
                CardId.SixthSense
                );
                return true;
            }
            AI.SelectCard(
                CardId.PainfulChoice,
                CardId.PotOfGreed,
                CardId.GracefulCharity,
                CardId.UpstartGoblin,
                CardId.ChickenGame,
                CardId.OneDayOfPeace,
                CardId.MagicalMallet,
                CardId.RecklessGreed,
                CardId.HopeForEscape,
                CardId.JarOfAvarice,
                CardId.SixthSense,
                CardId.Exodia,
                CardId.RightArm,
                CardId.LeftArm,
                CardId.RightLeg,
                CardId.LeftLeg
                );
            return true;
        }
        //To Rethink
        private bool MagicalMalletEffect()
        {
            if (ExodiaPiecesInHand() < 4 || Bot.Hand.Count<=5)
            {
                AI.SelectCard(
                     CardId.Makyura,
                     CardId.MagicalMallet,
                     CardId.Exodia,
                     CardId.RightArm,
                     CardId.LeftArm,
                     CardId.RightLeg,
                     CardId.LeftLeg,
                     CardId.SixthSense,
                     CardId.HopeForEscape,
                     CardId.JarOfAvarice,
                     CardId.RecklessGreed
                     );
                return true;
            }
            else 
            {
                    AI.SelectCard(
                    CardId.Makyura,
                    CardId.MagicalMallet,
                    CardId.SixthSense,
                    CardId.HopeForEscape,
                    CardId.JarOfAvarice,
                    CardId.RecklessGreed
                    );
                return true;
            }
        }


        private int MakyuraGraveyardCount()
        {
            return Bot.Graveyard.GetCardCount(CardId.Makyura);
        }
        public override void OnChainEnd()
        {
            if (MakyuraCount < MakyuraGraveyardCount())
            {
                MakyuraCount = MakyuraGraveyardCount();
                wasMakyuraUsedThisTurn = true;
            }
            potentialDraw = 0;
            base.OnChainEnd();
        }
        public override void OnChaining(int player, ClientCard card)
        {
            base.OnChaining(player, card);
        }
        private int TargetsForPainfulChoise()
        {
            int result = 0;
            result += Bot.GetRemainingCount(CardId.Makyura, 3);
            result += Bot.GetRemainingCount(CardId.MagicalMallet, 3);
            result += Bot.GetRemainingCount(CardId.PainfulChoice, 3);
            result += Bot.GetRemainingCount(CardId.OneDayOfPeace, 2);
            result += Bot.GetRemainingCount(CardId.ChickenGame, 3);
            result += Bot.GetRemainingCount(CardId.UpstartGoblin, 3);
            result += Bot.GetRemainingCount(CardId.HopeForEscape, 3);
            result += Bot.GetRemainingCount(CardId.SixthSense, 3);

            return result;
        }
        
        private bool PainfulChoiceEffect()
        {
            updatePotentialDraw(5);
            if (Bot.Deck.Count > potentialDraw && TargetsForPainfulChoise()> potentialDraw)
            {
                AI.SelectCard(
                CardId.Makyura,
                CardId.MagicalMallet,
                CardId.PainfulChoice,
                CardId.OneDayOfPeace,
                CardId.ChickenGame,
                CardId.UpstartGoblin,
                CardId.HopeForEscape,
                CardId.SixthSense,
                CardId.GracefulCharity,
                CardId.PotOfGreed,
                CardId.RecklessGreed
                );
            return true;
            }
            updatePotentialDraw(-5);
            return false;
        }

        private bool RecklessGreedEffect()
        {
            updatePotentialDraw(2);
            if (Bot.Deck.Count >=potentialDraw&&wasMakyuraUsedThisTurn)
            {
                return true;
            }
            updatePotentialDraw(-2);
            return false;
        }

        private bool HopeForEscapeEffect()
        {
            int draw = (AI.Utils.Enemy.LifePoints - (Bot.LifePoints - 1000)) / 2000;
             updatePotentialDraw(draw);
            if (wasMakyuraUsedThisTurn && Bot.Deck.Count >=potentialDraw && Bot.LifePoints>1000 && !Bot.HasInHand(CardId.UpstartGoblin)&&!Bot.HasInHand(CardId.ChickenGame) && !AI.Utils.ChainContainsCard(CardId.UpstartGoblin)&&!AI.Utils.ChainContainsCard(CardId.ChickenGame)&& !AI.Utils.ChainContainsCard(CardId.PainfulChoice))
            {
                if (Bot.HasInSpellZone(CardId.ChickenGame) && (!wasChickenGameActivated || chickenGameCantDraw))
                {
                    updatePotentialDraw(-draw);
                    return false;
                }
                int difference = (AI.Utils.Enemy.LifePoints - Bot.LifePoints) % 2000;
                if (AI.Utils.ChainContainsCard(CardId.HopeForEscape) && (difference >= 1000))
                {
                    return true;
                }
                else if (AI.Utils.ChainContainsCard(CardId.HopeForEscape) && (difference < 1000))
                {
                    updatePotentialDraw(-draw);
                    return false;
                }
                    return true;
            }
            updatePotentialDraw(-draw);
            return false;
        }

        private bool SixthSenseEffect()
        {
            updatePotentialDraw(6);
            if (Bot.Deck.Count > potentialDraw)
            {
                if (wasMakyuraUsedThisTurn && Bot.HasInHand(CardId.JarOfAvarice))
                {
                    AI.SelectNumber(6);
                    
                   AI.SelectNextNumber(5);
                    return true;
                }
                updatePotentialDraw(-6);
                return false;
            }
            updatePotentialDraw(-6);
            return false;
        }
        //To Rethink!
        private bool JarOfAvariceEffect()
        {
            updatePotentialDraw(-4);
            if (AI.Utils.ChainContainsCard(CardId.SixthSense) || AI.Utils.ChainContainsCard(CardId.PainfulChoice)) { return false; }
            if (wasMakyuraUsedThisTurn&&Bot.HasInGraveyard(CardId.Exodia)||Bot.HasInGraveyard(CardId.LeftArm)||Bot.HasInGraveyard(CardId.RightArm)||Bot.HasInGraveyard(CardId.LeftLeg)||Bot.HasInGraveyard(CardId.RightLeg))
                {
                AI.SelectCard(
                CardId.Exodia,
                CardId.LeftArm,
                CardId.RightArm,
                CardId.LeftLeg,
                CardId.RightLeg,
                CardId.UpstartGoblin,
                CardId.ChickenGame,
                CardId.OneDayOfPeace,
                CardId.MagicalMallet,
                CardId.PotOfGreed,
                CardId.GracefulCharity,
                CardId.RecklessGreed
                );
                return true;
                }
            updatePotentialDraw(4);
            return false;
        }
        
        
        public override void OnNewTurn()
        {
            base.OnNewTurn();
            wasChickenGameActivated = false;
            wasMakyuraUsedThisTurn = false;
            chickenGameCantDraw = false;
            noFieldSpell = !Bot.HasInSpellZone(CardId.ChickenGame);
        }
        public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
        {

            if (cards[0].Location == CardLocation.Hand && Duel.Phase == DuelPhase.End)
            {
                List<ClientCard> result = new List<ClientCard>();
                result.AddRange(cards);
                foreach (ClientCard card in cards)
                {
                    foreach(int piece in ExodiaPieces)
                    {
                        if (card.IsCode(piece))
                        {
                            result.Remove(card);
                        }
                    }
                }
                return AI.Utils.CheckSelectCount(result, cards, min, max);
            }
           return base.OnSelectCard(cards,min,max,hint,cancelable);
        }
    }


}