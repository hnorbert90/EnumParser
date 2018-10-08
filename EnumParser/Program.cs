#define Measure
namespace EnumParser
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using System.Diagnostics;

    public class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(new EnumManager(new EnumTypeGenerator(new EnumValueResolver())));
            client.Run();
        }
    }

    public class Client
    {
        public static string s_FlagEnumName = "FlagTeszt";
        public static string s_StandardEnumName = "StandardEnum";
        private IEnumManager EnumManager;

        public Client(IEnumManager enumManager)
        {
            EnumManager = enumManager;
        }

        public void Run()
        {
            #region Measure Memory and Time
#if Measure
            var memoryBefore = GC.GetTotalMemory(false);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
#endif
            #endregion

            #region CreateEnumDescriptors
            List<string> enumerations = new List<string>
            {
                "f1,1", "f2,2", "f3,4", "f4,8"
            };

            List<string> enumerations2 = new List<string>
            {
                "e1,0x1", "e2,0x2", "e3,0x4", "e4,0x8"
            };

            EnumDescriptor enumDescriptor = new EnumDescriptor(typeof(uint), enumerations, s_FlagEnumName, true);

            EnumDescriptor enumDescriptor2 = new EnumDescriptor(typeof(long), enumerations2, s_StandardEnumName, false);
            #endregion

            #region Create 1000 pc EnumType
#if Measure
            for (int i = 0; i < 1000; i++)
            {
                EnumDescriptor enumDescriptor3 = new EnumDescriptor(typeof(uint), enumerations, s_FlagEnumName + i, true);
                EnumManager.AddEnumType(enumDescriptor3);
                Console.WriteLine(enumDescriptor3.EnumName + ": Created!");
            }
#endif
            #endregion

            EnumManager.AddEnumType(enumDescriptor);
            var value = EnumManager[s_FlagEnumName].GetValue("f2");
            var name = EnumManager[s_FlagEnumName].GetName(3);
            var list = EnumManager[s_FlagEnumName].ToList();
            var kvp = EnumManager[s_FlagEnumName].ParseEnum("f1,1");
            var rawEnum = EnumManager[s_FlagEnumName].GetRawEnumData(1);
            var displayValue = EnumManager[s_FlagEnumName].GetDisplayValue(11);
            var displayValue2 = EnumManager[s_FlagEnumName].GetDisplayValue(1);

            EnumManager.AddEnumType(enumDescriptor2);
            var value2 = EnumManager[s_StandardEnumName].GetValue("e2");
            var name2 = EnumManager[s_StandardEnumName].GetName(2);
            var list2 = EnumManager[s_StandardEnumName].ToList();
            var kvp2 = EnumManager[s_StandardEnumName].ParseEnum("e2,0x2");
            var rawEnum2 = EnumManager[s_StandardEnumName].GetRawEnumData(2);
            var displayValue3 = EnumManager[s_StandardEnumName].GetDisplayValue(1);
            var displayValue4 = EnumManager[s_StandardEnumName].GetDisplayValue(5);

            #region Measure Results
            #if Measure
            var memoryAfter = GC.GetTotalMemory(false);
            var elapsedTimeInMilliseconds = stopwatch.ElapsedMilliseconds;
            var inc = $"{(memoryAfter - memoryBefore) / 1024} Kb";
            #endif
            #endregion
        }
    }
}
