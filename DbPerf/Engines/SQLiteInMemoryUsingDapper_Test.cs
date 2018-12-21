using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using TestPerfLiteDB.Entities;
using Dapper;

namespace TestPerfLiteDB
{
    public class SQLiteInMemoryUsingDapper_Test : ITest
    {
        private SQLiteConnection _db;
        private int _count;
        private List<Doc> _docs;

        public int Count
        {
            get { return _count; }
        }

        public int FileLength { get; } = 0;

        public SQLiteInMemoryUsingDapper_Test(int count, string password, bool journal)
        {
            _count = count;
            var cs = "Data Source=:memory:";
            if (password != null) cs += "; Password=" + password;
            if (journal == false) cs += "; Journal Mode=Off";
            _db = new SQLiteConnection(cs);
            _docs = Helper.GetDocs(_count).ToList();
        }

        public void Prepare()
        {
            _db.Open();

            var table = new SQLiteCommand("CREATE TABLE col (id INTEGER NOT NULL PRIMARY KEY, name TEXT, lorem TEXT)",
                _db);
            table.ExecuteNonQuery();

            var table2 =
                new SQLiteCommand("CREATE TABLE col_bulk (id INTEGER NOT NULL PRIMARY KEY, name TEXT, lorem TEXT)",
                    _db);
            table2.ExecuteNonQuery();
        }

        public void Insert()
        {
            foreach (var doc in _docs)
            {
                _db.Execute("INSERT INTO col (id, name, lorem) VALUES (@id, @name, @lorem)", doc);
            }
        }

        public void Bulk()
        {
            using (var trans = _db.BeginTransaction())
            {
                _db.Execute("INSERT INTO col_bulk (id, name, lorem) VALUES (@id, @name, @lorem)", _docs, trans);

                trans.Commit();
            }
        }

        public void Update()
        {
            foreach (var doc in _docs)
            {
                _db.Execute("UPDATE col SET name = @name, lorem = @lorem WHERE id = @id", doc);
            }
        }

        public void CreateIndex()
        {
            _db.Execute("CREATE INDEX idx1 ON col (name)");
        }

        public void Query()
        {
            for (var i = 0; i < _count; i++)
            {
                var r = _db.Query<Doc>("SELECT * FROM col WHERE id = @id", new {id = i}).First();
                var name = r.Name;
                var lorem = r.Lorem;
            }
        }

        public void Delete()
        {
            _db.Execute("DELETE FROM col");
        }

        public void Drop()
        {
            _db.Execute("DROP TABLE col_bulk");
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}