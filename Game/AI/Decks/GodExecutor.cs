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
        AddExecutor(ExecutorType.Activate, CardId.UpstartGoblin);
            AddExecutor(ExecutorType.Activate, CardId.PainfulChoice, PainfulChoiceEffect);
            AddExecutor(ExecutorType.Activate, CardId.PotOfGreed, PotOfGreedEffect);
            AddExecutor(ExecutorType.Activate, CardId.PotOfDuality, PotOfDualityEffect);
            AddExecutor(ExecutorType.Activate, CardId.ChickenGame, ChickenGameField);
            AddExecutor(ExecutorType.Activate, CardId.ChickenGame, ChickenGameEffect);
            AddExecutor(ExecutorType.Activate, CardId.OneDayOfPeace);
            AddExecutor(ExecutorType.Activate, CardId.PainfulChoice, PainfulChoiceEffect);
            AddExecutor(ExecutorType.Activate, CardId.GracefulCharity, GracefulCharityEffect);
            AddExecutor(ExecutorType.Activate, CardId.MagicalMallet, MagicalMalletEffect);

            // Power Overwhelming
            AddExecutor(ExecutorType.Activate, CardId.RecklessGreed, RecklessGreedEffect);
            AddExecutor(ExecutorType.Activate, CardId.HopeForEscape, HopeForEscapeEffect);
            AddExecutor(ExecutorType.Activate, CardId.SixthSense, SixthSenseEffect);
            AddExecutor(ExecutorType.Activate, CardId.JarOfAvarice, JarOfAvariceEffect);
        }
        private int[] ExodiaPieces = { CardId.Exodia, CardId.RightArm, CardId.RightLeg, CardId.LeftLeg, CardId.LeftArm };

        private bool ChickenGameField()
        {        
            if ((wasChickenGameActivated || noFieldSpell) && Card.Location == CardLocation.Hand)
            {
                wasChickenGameActivated = false;
                noFieldSpell = false;
                return true;
            }
            return false;
        }

        private bool ChickenGameEffect()
        {
            if (!wasChickenGameActivated&&Card.Location==CardLocation.SpellZone)
            {
                AI.SelectOption(0);
                wasChickenGameActivated = true;
                return true;
            }
            return false;
        }

        public override bool OnSelectHand()
        {
            return true;
        }

        private bool PotOfGreedEffect()
            {
            return Bot.Deck.Count > 1;
            }


        //To rethink!
        private bool GracefulCharityEffect()
        {
            if (Bot.Deck.Count > 2)
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
            else return false;
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
            if (ExodiaPiecesInHand()==4)
            {
                AI.SelectCard(ExodiaPieces);
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
            if (Bot.Deck.Count > 5 && TargetsForPainfulChoise()>=5)
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
            else return false;
        }

        private bool RecklessGreedEffect()
        {
            if (AI.Utils.GetLastChainCard() == null && Bot.Deck.Count >1)
            {
                return wasMakyuraUsedThisTurn;
            }
            return false;
        }

        private bool HopeForEscapeEffect()
        {
            if (Bot.Deck.Count >=((AI.Utils.Enemy.LifePoints - (Bot.LifePoints-1000))/2000) && Bot.LifePoints>1000 && !Bot.HasInHand(CardId.UpstartGoblin)&&!Bot.HasInHand(CardId.ChickenGame) && !AI.Utils.ChainContainsCard(CardId.UpstartGoblin)&&!AI.Utils.ChainContainsCard(CardId.ChickenGame))
            {
                if (Bot.HasInSpellZone(CardId.ChickenGame) && !wasChickenGameActivated)
                {
                    return false;
                }
                if (AI.Utils.ChainContainsCard(CardId.HopeForEscape) && (AI.Utils.Enemy.LifePoints - Bot.LifePoints % 2000 >= 1000))
                {
                    return false;
                }
                else if (AI.Utils.ChainContainsCard(CardId.HopeForEscape) && (AI.Utils.Enemy.LifePoints - Bot.LifePoints % 2000 < 1000))
                {
                    return true;
                }
                return wasMakyuraUsedThisTurn;
            }
            else return false;
        }

        private bool SixthSenseEffect()
        {
            if (Bot.Deck.Count > 5 && AI.Utils.GetLastChainCard()==null)
            {
                if (wasMakyuraUsedThisTurn && Bot.HasInHand(CardId.JarOfAvarice))
                {
                    AI.SelectNumber(6);
                    
                   AI.SelectNextNumber(5);
                    return true;
                }
                else return false;
            }
            else return false;
        }
        //To Rethink!
        private bool JarOfAvariceEffect()
        {
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
            else return false;
        }
        
        
        public override void OnNewTurn()
        {
            base.OnNewTurn();
            wasChickenGameActivated = false;
            wasMakyuraUsedThisTurn = false;
            noFieldSpell = !Bot.HasInSpellZone(CardId.ChickenGame);
        }
        public override IList<ClientCard> OnSelectCard(IList<ClientCard> cards, int min, int max, int hint, bool cancelable)
        {

            if (cards[0].Location == CardLocation.Hand && Duel.Phase == DuelPhase.End)
            {
                List<ClientCard> result = (List<ClientCard>)Bot.Hand;
                
                foreach (ClientCard card in Bot.Hand)
                {
                    foreach(int piece in ExodiaPieces)
                    {
                        if (card.IsCode(piece))
                        {
                            result.Remove(card);
                        }
                    }
                }
                AI.SelectCard(result);
            }
           return base.OnSelectCard(cards,min,max,hint,cancelable);
        }

    }


}