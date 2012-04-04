using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SWTOR.Parser.Domain
{
    // This POCO can easily be stored in RavenDB
    public class CombatLog : ILogMetrics
    {
        public CombatLog()
        {
            Combats = new List<CombatData>();
            AbilityCounts = new List<AbilityMetrics>();
        }

        public string Id { get; set; }
        public List<CombatData> Combats { get; private set; }

        public int TotalDamage { get; set; }
        public int TotalHealing { get; set; }
        public int TotalThreat { get; set; }
        public int CountOfParry { get; set; }
        public int CountOfDeflect { get; set; }
        public List<AbilityMetrics> AbilityCounts { get; set; }

        public static CombatLog CreateCombatLog(string combatLog)
        {
            string hash = ComputeHash(combatLog);
            var logParser = new Parser();
            var log = logParser.Parse(new StringReader(combatLog));
            return CreateCombatLog(hash, log);
        }

        public static CombatLog CreateCombatLog(string hash, List<LogEntry> log)
        {
            var combatParser = new CombatParser();
            var model = combatParser.Parse(log);

            combatParser.Clean(model);
            model.Id = hash;
            return model;
        }

        public static string ComputeHash(string data)
        {
            var MD5 = new MD5CryptoServiceProvider();
            char[] bArr = data.ToCharArray();
            int size = bArr.GetUpperBound(0);
            byte[] arr = new byte[size];
            Encoding.Default.GetEncoder().GetBytes(bArr, 0, size, arr, 0, true);
            var retVal = MD5.ComputeHash(arr);

            var sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
