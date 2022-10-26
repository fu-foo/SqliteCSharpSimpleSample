using System;
using System.Data.SQLite;

namespace Fu.SqliteCSharpSimpleSample
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// (1)DB作成
			Console.WriteLine("■DB作成");

			// ファイル作成(既に同じファイル名であれば初期化される)
			//SQLiteConnection.CreateFile(@"c:\temp\test.db");

			var sqlConnStr = new SQLiteConnectionStringBuilder
			{
				// メモリー上に展開
				DataSource = ":memory:"
				// 既存DBを読む場合
				//DataSource = @"c:\temp\test.db"

			};

			using (var conn = new SQLiteConnection(sqlConnStr.ToString()))
			{
				// (2)接続
				Console.WriteLine("■DB接続");
				conn.Open();

				using (var cmd = new SQLiteCommand(conn))
				{
					// (3)create table
					Console.WriteLine("■create table");
					cmd.CommandText = "create table if not exists user(id integer primary key, name text, updttm text)";
					cmd.ExecuteNonQuery();

					// (4)insert x 3
					Console.WriteLine("■insert x 3");
					cmd.CommandText = "insert into user(id, name, updttm) values(1,'hoge', '2022-10-26')";
					cmd.ExecuteNonQuery();
					cmd.CommandText = "insert into user(id, name, updttm) values(2,'fuga', '2022-10-27')";
					cmd.ExecuteNonQuery();
					cmd.CommandText = "insert into user(id, name, updttm) values(3,'piyo', '2022-10-28')";
					cmd.ExecuteNonQuery();

					// (5)トランザクションありのinsert
					Console.WriteLine("■insert with transation");
					var tran = conn.BeginTransaction();

					Console.WriteLine("■パラメーター指定のinsert");
					cmd.CommandText = "insert into user(id, name, updttm) values(@id, @name, @updttm)";
					cmd.Parameters.Add(new SQLiteParameter("@id", 4));
					cmd.Parameters.Add(new SQLiteParameter("@name", "poco"));
					cmd.Parameters.Add(new SQLiteParameter("@updttm", "2024-01-01"));
					if (cmd.ExecuteNonQuery() != 1)
					{
						Console.WriteLine("■トランザクション失敗はロールバック");
						tran.Rollback();
						return;
					}
					Console.WriteLine("■コミット");
					tran.Commit();

					// (6)select
					Console.WriteLine("■select");
					cmd.CommandText = "select * from user";
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Console.WriteLine($"id={reader["id"].ToString()} name={reader["name"]} updttm={reader["updttm"]}");
						}
					}

					// (7)パラメーター指定のselect
					Console.WriteLine("■パラメーター指定のselect");
					cmd.CommandText = "select * from user where id = @id";
					cmd.Parameters.Add(new SQLiteParameter("@id", 1));
					var result = cmd.ExecuteNonQuery();
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Console.WriteLine($"id={reader["id"].ToString()} name={reader["name"]} updttm={reader["updttm"]}");
						}
					}
				}

				// (8)close
				Console.WriteLine("■close");
				conn.Close();
			}
		}
	}
}
