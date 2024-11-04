if ($args.Length -eq 0) {
    Write-Host "Please provide a migration name." -ForegroundColor Red
}
else {
    $migrationName = $args[0]
    dotnet ef migrations add $migrationName -c SqliteContext -p NadekoBot.csproj
    dotnet ef migrations add $migrationName -c PostgreSqlContext -p NadekoBot.csproj
}

