using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWTOR.Parser.Domain;

namespace SWTOR.Parser.Tests
{
    [TestClass]
    public class AnalyzerTest
    {
        [TestMethod]
        public void AnalyzeDpsPerCharacter_OneCharacterOver2Seconds_Good()
        {
            // Arrange
            var entries = CreateLogEntries(1);

            // Act
            var analyzer = new Analyzer();
            var result = analyzer.AnalyzeDpsPerCharacter(entries);
            Assert.AreEqual(200, result[0].damage);
            Assert.AreEqual("Player1", result[0].character);
            Assert.AreEqual(200, result[1].damage);
            Assert.AreEqual("Player1", result[1].character);
        }

        [TestMethod]
        public void AnalyzeDpsPerCharacter_OneCharacterOver4Seconds_Good()
        {
            // Arrange
            var entries = CreateLogEntries(3);

            // Act
            var analyzer = new Analyzer();
            var result = analyzer.AnalyzeDpsPerCharacter(entries);
            Assert.AreEqual(200, result[0].damage);
            var elementsWithDamage = new List<Int32> {0, 3};
            elementsWithDamage.ForEach(e =>
            {
                Assert.AreEqual("Player1", result[e].character);
                Assert.AreEqual(200, result[e].damage);
            });
            var elementsWithNoDamage = new List<Int32> {1, 2};
            elementsWithNoDamage.ForEach(e =>
            {
                Assert.AreEqual("Player1", result[e].character);
                Assert.AreEqual(0, result[e].damage);
            });
        }

        private static IList<LogEntry> CreateLogEntries(int secondsBetweenEntries)
        {
            var startTime = DateTime.Now;
            var nextEntry = startTime.AddSeconds(secondsBetweenEntries);
            IList<LogEntry> entries = new List<LogEntry>();
            var actor1 = new Actor {isPlayer = true, name = "Player1", number = 1};
            entries.Add(new LogEntry
                            {
                                timestamp = startTime,
                                effect = new Effect {name = "Damage", number = 100, subtype = "None"},
                                result = new Result {amount = 200, isCritical = false, name = "anattack", number = 3122},
                                source = actor1
                            });
            entries.Add(new LogEntry
                            {
                                timestamp = nextEntry,
                                effect = new Effect {name = "Damage", number = 100, subtype = "None"},
                                result = new Result {amount = 200, isCritical = false, name = "anattack", number = 3122},
                                source = actor1
                            });
            return entries;
        }
    }
}