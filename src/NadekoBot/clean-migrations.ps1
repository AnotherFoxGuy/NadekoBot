# WORK IN PROGRESS

# Define the folders to search for designer.cs files
$folders = @("Migrations/PostgreSql", "Migrations/Sqlite")

# Loop through each folder
foreach ($folder in $folders) {
  # Get all designer.cs files in the folder and subfolders
  $files = Get-ChildItem -Path $folder -Filter *.designer.cs -Recurse

  $excludedPattern = "cleanup|mysql-init|squash|rero-cascade"

$filteredFiles = $files | Where-Object { $_.Name -notmatch $excludedPattern }
  # Loop through each file
  foreach ($file in ($files | Where-Object { $_.Name -notmatch $excludedPattern })) {
    # Read the contents of the file
    $content = Get-Content -Path $file.FullName | Select-Object -First 30

    # Find the attribute lines
    $attributes = $content | Where-Object { $_ -match '\[.*\]' } | ForEach-Object { '    ' + $_.Trim() }

    # Find the namespace
    $namespace = $content | Where-Object { $_ -match 'namespace' } | ForEach-Object { $_.Split(' ')[1] }

    # Find the class name
    $class_name = $content | Where-Object { $_ -match 'partial class' } | ForEach-Object { $_.Trim().Split(' ')[2] }

    # Replace the contents with the new template
    $new_content = @"
// <auto-generated />
using NadekoBot.Db;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace $namespace
{
$($attributes -join "`n")
    partial class $class_name 
    {
    }
}
"@

    # Write the new contents to the file
    Set-Content -Path $file.FullName -Value $new_content
  }
}