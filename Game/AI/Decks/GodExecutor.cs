﻿using YGOSharp.OCGWrapper.Enums;
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
            AddExecutor(ExecutorType.Activate, CardId.PotOfGreed);
            AddExecutor(ExecutorType.Activate, CardId.UpstartGoblin);
            AddExecutor(ExecutorType.Activate, CardId.ChickenGame, ChickenGameEffect);
            AddExecutor(ExecutorType.Activate, CardId.GracefulCharity, GracefulCharityEffect);
            AddExecutor(ExecutorType.Activate, CardId.PotOfDuality, PotOfDualityEffect);
            AddExecutor(ExecutorType.Activate, CardId.MagicalMallet, MagicalMalletEffect);
            AddExecutor(ExecutorType.Activate, CardId.PainfulChoice, PainfulChoiceEffect);

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
                if (!WindBot.HasInHand(CardId.HopeForEscape))
                    AI.SelectCard(CardId.HopeForEscape);
            else if (!WindBot.HasInHand(CardId.JarOfAvarice))
                    AI.SelectCard(CardId.JarOfAvarice);
            else if (!WindBot.HasInHand(CardId.RecklessGreed))
                    AI.SelectCard(CardId.RecklessGreed);
            else if (!WindBot.HasInHand(CardId.PainfulChoice))
                    AI.SelectCard(CardId.PainfulChoice);
                return true;
            }
            else return false;
        }

        private bool MagicalMalletEffect()
        {
            if (WindBot.HasInHand(CardId.Makyura))
                AI.SelectCard(CardId.Makyura);
            else if (WindBot.HasInHand(CardId.MagicalMallet))
                AI.SelectCard(CardId.MagicalMallet);
            else if (WindBot.HasInHand(CardId.Exodia))
                AI.SelectCard(CardId.Exodia);
            else if (WindBot.HasInHand(CardId.RightArm))
                AI.SelectCard(CardId.RightArm);
            else if (WindBot.HasInHand(CardId.LeftArm))
                AI.SelectCard(CardId.LeftArm);
            else if (WindBot.HasInHand(CardId.RightLeg))
                AI.SelectCard(CardId.RightLeg);
            else if (WindBot.HasInHand(CardId.LeftLeg))
                AI.SelectCard(CardId.LeftLeg);
            else if (WindBot.HasInHand(CardId.SixthSense))
                AI.SelectCard(CardId.SixthSense);
            else if (WindBot.HasInHand(CardId.HopeForEscape))
                AI.SelectCard(CardId.HopeForEscape);
            else if (WindBot.HasInHand(CardId.JarOfAvarice))
                AI.SelectCard(CardId.JarOfAvarice);
            else if (WindBot.HasInHand(CardId.RecklessGreed))
                AI.SelectCard(CardId.RecklessGreed);
            return true;
        }

        private bool PainfulChoiceEffect()
        {
            if (WindBot.HasInDeck(CardId.Makyura))
                AI.SelectCard(CardId.Makyura);
            else if (WindBot.HasInDeck(CardId.MagicalMallet))
                AI.SelectCard(CardId.MagicalMallet);
            else if (WindBot.HasInDeck(CardId.PainfulChoice))
                AI.SelectCard(CardId.PainfulChoice);
            else if (WindBot.HasInDeck(CardId.OneDayOfPeace))
                AI.SelectCard(CardId.OneDayOfPeace);
            else if (WindBot.HasInDeck(CardId.ChickenGame))
                AI.SelectCard(CardId.ChickenGame);
            else if (WindBot.HasInDeck(CardId.UpstartGoblin))
                AI.SelectCard(CardId.UpstartGoblin);
            else if (WindBot.HasInDeck(CardId.HopeForEscape))
                AI.SelectCard(CardId.HopeForEscape);
            return true;
        }

        private bool RecklessGreedEffect()
        {
            if (WindBot.HasInGraveyard(CardId.Makyura))
                return true;
            else return false;
        }

        private bool HopeForEscapeEffect()
        {
            if (WindBot.HasInGraveyard(CardId.Makyura))
                return true;
            else return false;
        }

        private bool SixthSenseEffect()
        {
            if (Bot.Deck.Count > 5)
            {
                if (WindBot.HasInGraveyard(CardId.Makyura)&&WindBot.HasInHand(CardId.JarOfAvarice))
                    return true;
            }
            else return false;
        }

        private bool JarOfAvariceEffect()
        {
            if (!WindBot.HasInGraveyard(CardId.Makyura))
                return true;
            else return false;
        }

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