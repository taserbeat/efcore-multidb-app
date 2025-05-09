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

```bash
# 「XXXX」は変更内容がわかるように置き換える
dotnet ef migrations add XXXX --context EmaDbContext --project ../EMA.DB/EMA.DB.csproj

# 例:
# dotnet ef migrations add CreateUserTable --context EmaDbContext --project ../EMA.DB/EMA.DB.csproj
```

## マイグレーションの実行

```bash
dotnet ef database update --context EmaDbContext --project ../EMA.DB/EMA.DB.csproj
```
