# efcore-multidb-app

Entity Framework Core を使用して複数のデータベースを切り替えるサンプルアプリケーション (.NET 8 を使用)

# Docker

### 起動

```bash
docker compose up -d
```

### 終了 & 起動

```bash
docker compose down --volumes --remove-orphans && docker compose up -d
```

### 終了

```bash
docker compose down --volumes --remove-orphans
```

## SQLServer で 空の DB 作成

Docker で起動した PostgreSQL は空の DB を自動作成するが、SQLServer は手動で作成しなければならない。

```bash
# コンテナにログイン
docker compose exec sqlserver bash

# データベースにログイン
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'StrongP@ssw0rd' -N -C

# 空のDBを作成
CREATE DATABASE [EmaDb];
GO

# 確認
SELECT name FROM sys.databases;
GO

# データベースからログアウト
exit

# コンテナからログアウト
exit
```
