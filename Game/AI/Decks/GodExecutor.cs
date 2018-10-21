using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("God", "AI_God")]  
    public class GodExecutor : DefaultExecutor
    {
        private bool wasChickenActivated = false;
        private bool firstChicken = true;
        public class CardId
        {
            public const int Exodia = 33396948;
            public const int RightArm = 70903634;
            public const int LeftArm = 7902349;
            public const int LeftLeg = 44519536;
            public const int RightLeg = 8124921;
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
            AddExecutor(ExecutorType.Activate, CardId.GracefulCharity, GracefulCharityEffect);
            AddExecutor(ExecutorType.Activate, CardId.MagicalMallet, MagicalMalletEffect);

            // Power Overwhelming
            AddExecutor(ExecutorType.Activate, CardId.RecklessGreed, RecklessGreedEffect);
            AddExecutor(ExecutorType.Activate, CardId.HopeForEscape, HopeForEscapeEffect);
            AddExecutor(ExecutorType.Activate, CardId.SixthSense, SixthSenseEffect);
            AddExecutor(ExecutorType.Activate, CardId.JarOfAvarice, JarOfAvariceEffect);
        }

        private bool ChickenGameField()
        {        
            if ((wasChickenActivated || firstChicken) && Card.Location == CardLocation.Hand)
            {
                wasChickenActivated = false;
                firstChicken = false;
                return true;
            }
            return false;
        }

        private bool ChickenGameEffect()
        {
            if (!wasChickenActivated&&Card.Location==CardLocation.SpellZone)
            {
                AI.SelectOption(0);
                wasChickenActivated = true;
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
            if (Bot.Deck.Count > 1)
                return true;
            else return false;
            }

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

        private bool PotOfDualityEffect()
        {
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

        private bool MagicalMalletEffect()
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

        private bool PainfulChoiceEffect()
        {
            if (Bot.Deck.Count > 5)
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
                CardId.PotOfGreed
                );
                
              
               
            return true;
            }
            else return false;
        }

        private bool RecklessGreedEffect()
        {
            if (Bot.HasInGraveyard(CardId.Makyura))
               
                return true;
            else return false;
        }

        private bool HopeForEscapeEffect()
        {
            if (Bot.Deck.Count > 5 && !Bot.HasInHand(CardId.UpstartGoblin)&&!Bot.HasInHand(CardId.ChickenGame) && !AI.Utils.ChainContainsCard(CardId.UpstartGoblin)&&!AI.Utils.ChainContainsCard(CardId.ChickenGame))
            {
                if (Bot.HasInGraveyard(CardId.Makyura))
                    return true;
                else return false;
            }
            else return false;
        }

        private bool SixthSenseEffect()
        {
            if (Bot.Deck.Count > 5 && AI.Utils.GetLastChainCard()==null)
            {
                if (Bot.HasInGraveyard(CardId.Makyura) && Bot.HasInHand(CardId.JarOfAvarice))
                {
                    AI.SelectNumber(6);
                    
                   AI.SelectNextNumber(5);
                    return true;
                }
                else return false;
            }
            else return false;
        }

        private bool JarOfAvariceEffect()
        {
            if (Bot.HasInGraveyard(CardId.Makyura)&&Bot.HasInGraveyard(CardId.Exodia)||Bot.HasInGraveyard(CardId.LeftArm)||Bot.HasInGraveyard(CardId.RightArm)||Bot.HasInGraveyard(CardId.LeftLeg)||Bot.HasInGraveyard(CardId.RightLeg))
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
            wasChickenActivated = false;
        }
    }


}