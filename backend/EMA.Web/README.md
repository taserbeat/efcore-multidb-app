# Entity Framework Core でのマイグレーション

## 事前準備

dotnet tool で`dotnet-ef`コマンドをインストール (または restore)する。

```bash
# カレントディレクトリを移動
cd backend/EMA.Web

# .config/dotnet-tools.jsonが存在しない場合
# dotnet-tools.jsonの作成とdotnet-efをインストール
dotnet new tool-manifest
dotnet tool install dotnet-ef

# .config/dotnet-tools.jsonが存在する場合
dotnet tool restore
```

## テーブル構造の変更

1. `EMA.DB`のソースをテーブル構造の変更に併せて更新する

2. マイグレーションファイルの作成

### PostgreSQL の場合

[appsettings.json](./appsettings.json)の`DbProvider`キーの値を`PostgreSQL`に設定し、以下を実行する。

```bash
# 「XXXX」は変更内容がわかるように置き換える
dotnet ef migrations add XXXX -c EmaPostgresContext -p ../EMA.DB/EMA.DB.csproj -o Migrations/PostgreSQL

# 例:
# dotnet ef migrations add CreateUserTable -c EmaPostgresContext -p ../EMA.DB/EMA.DB.csproj -o Migrations/PostgreSQL
```

### SQLServer の場合

[appsettings.json](./appsettings.json)の`DbProvider`キーの値を`SQLServer`に設定し、以下を実行する。

```bash
# 「XXXX」は変更内容がわかるように置き換える
dotnet ef migrations add XXXX -c EmaSqlServerContext -p ../EMA.DB/EMA.DB.csproj -o Migrations/SQLServer

# 例:
# dotnet ef migrations add CreateUserTable -c EmaSqlServerContext -p ../EMA.DB/EMA.DB.csproj -o Migrations/SQLServer
```

## マイグレーションの実行

## PostgreSQL の場合

```bash
dotnet ef database update -c EmaDbContext -p ../EMA.DB/EMA.DB.csproj
```

## SQLServer の場合

```bash

```
