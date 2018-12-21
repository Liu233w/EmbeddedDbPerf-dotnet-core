using System;
using LiteDB;

namespace TestPerfLiteDB
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTest("LiteDB: default", new LiteDB_Test(5000, null, new FileOptions { Journal = true, FileMode = FileMode.Shared }));
            RunTest("LiteDB: encrypted", new LiteDB_Test(5000, "mypass", new FileOptions { Journal = true, FileMode = FileMode.Shared }));
            RunTest("LiteDB: exclusive no journal", new LiteDB_Test(5000, null, new FileOptions { Journal = false, FileMode = FileMode.Exclusive }));

            RunTest("SQLite: default", new SQLite_Test(5000, null, true));
            RunTest("SQLite: encrypted", new SQLite_Test(5000, "mypass", true));
            RunTest("SQLite: no journal", new SQLite_Test(5000, null, false));

            RunTest("SQLite in memory: default", new SQLiteInMemory_Test(5000, null, true));
            RunTest("SQLite in memory: no journal", new SQLiteInMemory_Test(5000, null, false));

            RunTest("LiteDB in memory: default", new LiteDBInMemory_Test(5000, null));

            RunTest("SQLite in memory using dapper: default", new SQLiteInMemoryUsingDapper_Test(5000, null, true));
            RunTest("SQLite in memory using dapper: no journal", new SQLiteInMemoryUsingDapper_Test(5000, null, false));

            Console.ReadKey();
        }

        static void RunTest(string name, ITest test)
        {
            var title = name + " - " + test.Count + " records";
            Console.WriteLine(title);
            Console.WriteLine("=".PadLeft(title.Length, '='));

            test.Prepare();

            GC.Collect();
            test.Run("Insert", test.Insert);
            GC.Collect();
            test.Run("Bulk", test.Bulk);
            GC.Collect();
            test.Run("Update", test.Update);
            GC.Collect();
            test.Run("CreateIndex", test.CreateIndex);
            GC.Collect();
            test.Run("Query", test.Query);
            GC.Collect();
            test.Run("Delete", test.Delete);
            GC.Collect();
            test.Run("Drop", test.Drop);

            Console.WriteLine("FileLength     : " +
                              Math.Round((double) test.FileLength / (double) 1024, 2).ToString().PadLeft(5, ' ') +
                              " kb");

            test.Dispose();

            Console.WriteLine();
        }
    }
}