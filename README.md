# SqliteCSharpSimpleSample
C#でSqliteを使って以下の操作を行うサンプルです。

1．DB作成
2. 接続
3. create table
4. insert
5. トランザクションありのinsert
6. select
7. パラメーター指定のselect
8. close

詳しい説明はQiita上に書いています。
https://qiita.com/__fu__/items/f02eb5a0758e3c911567

# お断り

Sqliteの操作理解用サンプルなので、エラー処理は皆無です。
参考にする際はエラー処理の実装をお願いします。

# 環境

.Net 4.6.2 + C#、コンソールアプリ。

# 補足

※下記の例外が出た場合
ハンドルされていない例外: System.DllNotFoundException: DLL 'SQLite.Interop.dll' を読み込めません:指定されたモジュールが 見つかりません。 (HRESULT からの例外:0x8007007E)

package以下の中から環境に即したSQLite.Interop.dllを取得して、実行ファイルと同じ場所においてください。

プロジェクトのSqliteCSharpSimpleSample\packages\Stub.System.Data.SQLite.Core.NetFramework.1.0.116.0\build\net46\　からx86,x64フォルダーを中身ごとコピー

デバッグであればexeの作成される
プロジェクトのSqliteCSharpSimpleSample\SqliteCSharpSimpleSample\bin\Debug にコピーしてください。
