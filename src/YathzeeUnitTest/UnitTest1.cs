﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Yathzee;

namespace Yathzee
{
    [TestClass]
    public class DieTests
    {
        [TestMethod]
        public void DieTest()
        {
            for (int i = 0; i < 6; i++)
            {
                Die die = new Die(i);
                Assert.AreEqual(i, die.getDieValue());
            }
        }

        /*   [TestMethod]
           public void DieImageTest()
           {
               Die die = new Die(0);
               DieImage image = new DieImage();
               System.Drawing.Image expected = image.getDieBlank();
               System.Drawing.Image actual = die.getDieImage();
               Assert.AreEqual(expected, actual);
           }*/

        [TestMethod]
        public void DiceGeneratorTest()
        {
            DiceGenerator generator = new DiceGenerator();
            for (int i = 0; i < 100; i++)
            {
                Die die = generator.generateDice();
                Assert.IsTrue(die.getDieValue() <= 6, "Die shows a value too high.");
                Assert.IsTrue(die.getDieValue() > 0, "Die shows a value too low.");
            }
        }
    }

    [TestClass]
    public class UpperScoreSectionTest
    {
        [TestMethod]
        public void OneThreeFourTest()
        {
            UpperScoreSection upperSection = new UpperScoreSection();
            Die[] dice = new Die[5] { new Die(3), new Die(1), new Die(1), new Die(4), new Die(6) };
            Dictionary<ScoreTypeUpper, int> upper = new Dictionary<ScoreTypeUpper, int>();
            upper = upperSection.checkScore(dice, 1);
            Assert.AreEqual(2, upper[ScoreTypeUpper.One], "Error in ones");
            upper = upperSection.checkScore(dice, 3);
            Assert.AreEqual(3, upper[ScoreTypeUpper.Three], "Error in threes");
            upper = upperSection.checkScore(dice, 4);
            Assert.AreEqual(4, upper[ScoreTypeUpper.Four], "Error in fours");
        }

        [TestMethod]
        public void ZeroScoreTest()
        {
            UpperScoreSection upperSection = new UpperScoreSection();
            Die[] dice = new Die[5] { new Die(5), new Die(2), new Die(3), new Die(6), new Die(6) };
            Dictionary<ScoreTypeUpper, int> upper = new Dictionary<ScoreTypeUpper, int>();
            upper = upperSection.checkScore(dice, 1);
            Assert.AreEqual(0, upper[ScoreTypeUpper.One], "Error in ones, zero score expected");
        }

        [TestMethod]
        public void TotalTest()
        {

            UpperScoreSection upperSection = new UpperScoreSection();
            Dictionary<ScoreTypeUpper, int> scoreValues = new Dictionary<ScoreTypeUpper, int>();
            Die[] dice = new Die[5] { new Die(6), new Die(6), new Die(6), new Die(2), new Die(4) };
            scoreValues = upperSection.checkScore(dice, 6);
            Assert.AreEqual(18, scoreValues[ScoreTypeUpper.Total]);
            dice = new Die[5] { new Die(6), new Die(6), new Die(2), new Die(2), new Die(4) };
            scoreValues = upperSection.checkScore(dice, 2);
            Assert.AreEqual(22, scoreValues[ScoreTypeUpper.Total]);
        }
        [TestMethod]
        public void BonusTestEarned()
        {
            UpperScoreSection upperSection = new UpperScoreSection();
            Dictionary<ScoreTypeUpper, int> upperBonusEarned = new Dictionary<ScoreTypeUpper, int>();

            Die[] dice = new Die[5] { new Die(1), new Die(1), new Die(1), new Die(2), new Die(4) };
            upperBonusEarned = upperSection.checkScore(dice, 1);
            Assert.AreEqual(0, upperBonusEarned[ScoreTypeUpper.Bonus], "Bonus is given when it shouldn't");

            dice = new Die[5] { new Die(2), new Die(6), new Die(2), new Die(2), new Die(4) };
            upperBonusEarned = upperSection.checkScore(dice, 2);
            Assert.AreEqual(0, upperBonusEarned[ScoreTypeUpper.Bonus], "Bonus is given when it shouldn't");

            dice = new Die[5] { new Die(3), new Die(3), new Die(6), new Die(3), new Die(4) };
            upperBonusEarned = upperSection.checkScore(dice, 3);
            Assert.AreEqual(0, upperBonusEarned[ScoreTypeUpper.Bonus], "Bonus is given when it shouldn't");

            dice = new Die[5] { new Die(4), new Die(5), new Die(4), new Die(2), new Die(4) };
            upperBonusEarned = upperSection.checkScore(dice, 4);
            Assert.AreEqual(0, upperBonusEarned[ScoreTypeUpper.Bonus], "Bonus is given when it shouldn't");

            dice = new Die[5] { new Die(6), new Die(5), new Die(6), new Die(5), new Die(5) };
            upperBonusEarned = upperSection.checkScore(dice, 5);
            Assert.AreEqual(0, upperBonusEarned[ScoreTypeUpper.Bonus], "Bonus is given when it shouldn't");

            Assert.AreEqual(45, upperBonusEarned[ScoreTypeUpper.Total], "Bonus is not given when it should");
            dice = new Die[5] { new Die(6), new Die(6), new Die(6), new Die(2), new Die(4) };
            upperBonusEarned = upperSection.checkScore(dice, 6);

            Assert.AreEqual(35, upperBonusEarned[ScoreTypeUpper.Bonus], "Bonus is not given when it should");
        }

        [TestMethod]
        public void BonusTestNotEarned()
        {
            UpperScoreSection upperSection = new UpperScoreSection();
            Dictionary<ScoreTypeUpper, int> upperBonusNotEarned = new Dictionary<ScoreTypeUpper, int>();

            Die[] dice = new Die[5] { new Die(1), new Die(2), new Die(1), new Die(2), new Die(4) };
            upperBonusNotEarned = upperSection.checkScore(dice, 1);
            Assert.AreEqual(2, upperBonusNotEarned[ScoreTypeUpper.Total], "Bonus is given when it shouldn't");

            dice = new Die[5] { new Die(2), new Die(6), new Die(1), new Die(2), new Die(4) };
            upperBonusNotEarned = upperSection.checkScore(dice, 2);
            Assert.AreEqual(6, upperBonusNotEarned[ScoreTypeUpper.Total], "Bonus is given when it shouldn't");

            dice = new Die[5] { new Die(3), new Die(4), new Die(6), new Die(3), new Die(4) };
            upperBonusNotEarned = upperSection.checkScore(dice, 3);
            Assert.AreEqual(12, upperBonusNotEarned[ScoreTypeUpper.Total], "Bonus is given when it shouldn't");

            dice = new Die[5] { new Die(4), new Die(5), new Die(4), new Die(2), new Die(4) };
            upperBonusNotEarned = upperSection.checkScore(dice, 4);
            Assert.AreEqual(24, upperBonusNotEarned[ScoreTypeUpper.Total], "Bonus is given when it shouldn't");

            dice = new Die[5] { new Die(6), new Die(5), new Die(6), new Die(5), new Die(5) };
            upperBonusNotEarned = upperSection.checkScore(dice, 5);
            Assert.AreEqual(39, upperBonusNotEarned[ScoreTypeUpper.Total], "Bonus is given when it shouldn't");

            dice = new Die[5] { new Die(6), new Die(5), new Die(6), new Die(2), new Die(4) };
            upperBonusNotEarned = upperSection.checkScore(dice, 6);
            Assert.AreEqual(51, upperBonusNotEarned[ScoreTypeUpper.Total], "Bonus is given when it shouldn't");
            Assert.AreEqual(0, upperBonusNotEarned[ScoreTypeUpper.Bonus], "Bonus is given when it shouldn't");
        }
    }
    [TestClass]
    public class LowerBoundScoreSectionTest
    {
        [TestMethod]
        public void ThreeOfKindTest()
        {
            LowerScoreSection lowerSection = new LowerScoreSection();
            Dictionary<ScoreTypeLower, int> threeOfKind = new Dictionary<ScoreTypeLower, int>();

            Die[] dice = new Die[5] { new Die(1), new Die(1), new Die(1), new Die(2), new Die(4) };
            threeOfKind = lowerSection.checkScore(dice, ScoreTypeLower.ThreeOfKind);
            Assert.AreEqual(9, threeOfKind[ScoreTypeLower.ThreeOfKind]);

            dice = new Die[5] { new Die(4), new Die(2), new Die(1), new Die(2), new Die(2) };
            threeOfKind = lowerSection.checkScore(dice, ScoreTypeLower.ThreeOfKind);
            Assert.AreEqual(11, threeOfKind[ScoreTypeLower.ThreeOfKind]);

            dice = new Die[5] { new Die(6), new Die(5), new Die(6), new Die(6), new Die(2) };
            threeOfKind = lowerSection.checkScore(dice, ScoreTypeLower.ThreeOfKind);
            Assert.AreEqual(25, threeOfKind[ScoreTypeLower.ThreeOfKind]);

            dice = new Die[5] { new Die(1), new Die(3), new Die(1), new Die(2), new Die(4) };
            threeOfKind = lowerSection.checkScore(dice, ScoreTypeLower.ThreeOfKind);
            Assert.AreEqual(0, threeOfKind[ScoreTypeLower.ThreeOfKind]);
        }

        [TestMethod]
        public void FourOfKindTest()
        {
            LowerScoreSection lowerSection = new LowerScoreSection();
            Dictionary<ScoreTypeLower, int> fourOfKind = new Dictionary<ScoreTypeLower, int>();

            Die[] dice = new Die[5] { new Die(1), new Die(1), new Die(1), new Die(1), new Die(4) };
            fourOfKind = lowerSection.checkScore(dice, ScoreTypeLower.FourOfKind);
            Assert.AreEqual(8, fourOfKind[ScoreTypeLower.FourOfKind]);

            dice = new Die[5] { new Die(3), new Die(4), new Die(4), new Die(4), new Die(4) };
            fourOfKind = lowerSection.checkScore(dice, ScoreTypeLower.FourOfKind);
            Assert.AreEqual(19, fourOfKind[ScoreTypeLower.FourOfKind]);

            dice = new Die[5] { new Die(1), new Die(3), new Die(1), new Die(2), new Die(4) };
            fourOfKind = lowerSection.checkScore(dice, ScoreTypeLower.FourOfKind);
            Assert.AreEqual(0, fourOfKind[ScoreTypeLower.FourOfKind]);
        }
        [TestMethod]
        public void FullHouseTest()
        {
            LowerScoreSection lowerSection = new LowerScoreSection();
            Dictionary<ScoreTypeLower, int> fullHouse = new Dictionary<ScoreTypeLower, int>();

            Die[] dice = new Die[5] { new Die(1), new Die(1), new Die(1), new Die(2), new Die(2) };
            fullHouse = lowerSection.checkScore(dice, ScoreTypeLower.FullHouse);
            Assert.AreEqual(25, fullHouse[ScoreTypeLower.FullHouse]);

            dice = new Die[5] { new Die(3), new Die(3), new Die(3), new Die(2), new Die(2) };
            fullHouse = lowerSection.checkScore(dice, ScoreTypeLower.FullHouse);
            Assert.AreEqual(25, fullHouse[ScoreTypeLower.FullHouse]);

            dice = new Die[5] { new Die(1), new Die(5), new Die(1), new Die(4), new Die(2) };
            fullHouse = lowerSection.checkScore(dice, ScoreTypeLower.FullHouse);
            Assert.AreEqual(0, fullHouse[ScoreTypeLower.FullHouse]);
        }
    }
}
