using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;

namespace WindBot.Game.AI.Decks
{
    [Deck("Exodia", "AI_Exodia", "Death")]
    public class GodExecutor : DefaultExecutor
    {
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

        private int RockCount = 0;

        public override bool OnSelectHand()
        {
            // go first
            return true;
        }

        public GodExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // Cycle begins
            AddExecutor(ExecutorType.Activate, CardId.UpstartGoblin);
            AddExecutor(ExecutorType.Activate, CardId.PotOfGreed);
            AddExecutor(ExecutorType.Activate, CardId.PainfulChoice, PainfulChoiceEffect);
            AddExecutor(ExecutorType.Activate, CardId.PotOfDuality, PotOfDualityEffect);
            AddExecutor(ExecutorType.Activate, CardId.ChickenGame, ChickenGameEffect);
            AddExecutor(ExecutorType.Activate, CardId.GracefulCharity, GracefulCharityEffect);
            AddExecutor(ExecutorType.Activate, CardId.MagicalMallet, MagicalMalletEffect);

            // Power Overwhelming
            AddExecutor(ExecutorType.Activate, CardId.RecklessGreed, RecklessGreedEffect);
            AddExecutor(ExecutorType.Activate, CardId.HopeForEscape, HopeForEscapeEffect);
            AddExecutor(ExecutorType.Activate, CardId.JarOfAvarice, JarOfAvariceEffect);
            AddExecutor(ExecutorType.Activate, CardId.SixthSense, SixthSenseEffect);
        }

        private bool GracefulCharityEffect()
        {
            if (Bot.Deck.Count > 2)
            {
                if (Bot.HasInHand(CardId.HopeForEscape))
                    AI.SelectCard(CardId.HopeForEscape);
            else if (Bot.HasInHand(CardId.JarOfAvarice))
                    AI.SelectCard(CardId.JarOfAvarice);
            else if (Bot.HasInHand(CardId.RecklessGreed))
                    AI.SelectCard(CardId.RecklessGreed);
            else if (Bot.HasInHand(CardId.PainfulChoice))
                    AI.SelectCard(CardId.PainfulChoice);
                return true;
            }
            else return false;
        }

        private bool MagicalMalletEffect()
        {
            if (Bot.HasInHand(CardId.Makyura))
                AI.SelectCard(CardId.Makyura);
            else if (Bot.HasInHand(CardId.MagicalMallet))
                AI.SelectCard(CardId.MagicalMallet);
            else if (Bot.HasInHand(CardId.Exodia))
                AI.SelectCard(CardId.Exodia);
            else if (Bot.HasInHand(CardId.RightArm))
                AI.SelectCard(CardId.RightArm);
            else if (Bot.HasInHand(CardId.LeftArm))
                AI.SelectCard(CardId.LeftArm);
            else if (Bot.HasInHand(CardId.RightLeg))
                AI.SelectCard(CardId.RightLeg);
            else if (Bot.HasInHand(CardId.LeftLeg))
                AI.SelectCard(CardId.LeftLeg);
            else if (Bot.HasInHand(CardId.SixthSense))
                AI.SelectCard(CardId.SixthSense);
            else if (Bot.HasInHand(CardId.HopeForEscape))
                AI.SelectCard(CardId.HopeForEscape);
            else if (Bot.HasInHand(CardId.JarOfAvarice))
                AI.SelectCard(CardId.JarOfAvarice);
            else if (Bot.HasInHand(CardId.RecklessGreed))
                AI.SelectCard(CardId.RecklessGreed);
            return true;
        }

        private bool PainfulChoiceEffect()
        {
            if (Bot.Deck.Count > 5)
            {
                if (Bot.HasInHand (CardId.Makyura))
                AI.SelectCard(CardId.Makyura);
            else if (Bot.HasInHand(CardId.MagicalMallet))
                AI.SelectCard(CardId.MagicalMallet);
            else if (Bot.HasInDeck(CardId.PainfulChoice))
                AI.SelectCard(CardId.PainfulChoice);
            else if (Bot.HasInDeck(CardId.OneDayOfPeace))
                AI.SelectCard(CardId.OneDayOfPeace);
            else if (Bot.HasInDeck(CardId.ChickenGame))
                AI.SelectCard(CardId.ChickenGame);
            else if (Bot.HasInDeck(CardId.UpstartGoblin))
                AI.SelectCard(CardId.UpstartGoblin);
            else if (Bot.HasInDeck(CardId.HopeForEscape))
                AI.SelectCard(CardId.HopeForEscape);
            else if (Bot.HasInDeck(CardId.SixthSense))
                AI.Selectcard(CardId.SixthSense);
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
            if (WindBot.deck.count > 5)
            {
                if (Bot.HasInGraveyard(CardId.Makyura))
                    return true;
            }
            else return false;
        }

        private bool SixthSenseEffect()
        {
            if (Bot.Deck.Count > 5)
            {
                if (Bot.HasInGraveyard(CardId.Makyura)&&Bot.HasInHand(CardId.JarOfAvarice))
                    return true;
            }
            else return false;
        }

        private bool JarOfAvariceEffect()
        {
            if (!Bot.HasInGraveyard(CardId.Makyura))
                return true;
            else return false;
        }
        public override int OnRockPaperScissors()
        {
            RockCount++;
            if (RockCount <= 3)
                return 2;
            else
                return base.OnRockPaperScissors();
        }
    }


}