# SQL Server セットアップガイド

## すぐに使う方法（Docker Run）

Codespaceで既存の環境にSQL Serverを追加する場合：

```bash
# SQL Server 2022 を起動
docker run -d \
  --name sqlserver \
  -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
  -e "MSSQL_PID=Developer" \
  -p 1433:1433 \
  -v sqlserver-data:/var/opt/mssql \
  mcr.microsoft.com/mssql/server:2022-latest

# 起動確認
docker ps

# ログ確認
docker logs sqlserver
```

## 接続情報

- **Server**: `localhost,1433`
- **User**: `sa`
- **Password**: `YourStrong@Passw0rd`
- **接続文字列**: 
  ```
  Server=localhost,1433;Database=YourDatabase;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True
  ```

## .NET アプリケーションから接続

### 1. NuGet パッケージのインストール

```bash
cd BlazorApp02/BlazorApp02
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

### 2. appsettings.json の設定

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=BlazorApp;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True"
  }
}
```

## 接続確認

```bash
# SQL Server CLIツールのインストール（オプション）
docker exec -it sqlserver /opt/mssql-tools/bin/sqlcmd \
  -S localhost -U sa -P YourStrong@Passw0rd \
  -Q "SELECT @@VERSION"
```

## コンテナ管理

```bash
# 停止
docker stop sqlserver

# 起動
docker start sqlserver

# 削除
docker rm -f sqlserver

# データボリューム削除（データも削除されます）
docker volume rm sqlserver-data
```

## Dev Container 設定（推奨）

次回 Codespace 作成時に自動でSQL Serverが起動されるよう、`.devcontainer`フォルダの設定を更新しました。

- `docker-compose.yml`: SQL Serverコンテナの定義
- `devcontainer.json`: 開発環境の構成

**Rebuild Container** を実行すると、SQL Server が自動的に追加されます。

## セキュリティ注意事項

⚠️ **本番環境では必ず強力なパスワードに変更してください**

- パスワードには大文字、小文字、数字、記号を含める
- 最低12文字以上を推奨
- 環境変数やシークレット管理サービスを使用
