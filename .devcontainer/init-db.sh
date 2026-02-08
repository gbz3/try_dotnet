#!/bin/bash
set -e

# SQL Serverが起動するまで待機
echo "Waiting for SQL Server to start..."
sleep 30s

# pubsデータベースを作成
echo "Creating pubs database..."
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -C -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'pubs') CREATE DATABASE pubs"

# SQLスクリプトを実行
echo "Running database initialization script..."
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P $MSSQL_SA_PASSWORD -C -d pubs -i /docker-entrypoint-initdb.d/instpubs.sql

echo "Database initialization completed."
