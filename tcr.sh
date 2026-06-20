#!/usr/bin/env bash
cd "$(dirname "$0")"

echo "Running LegacyCode.Console for multiple orderIds..."

dotnet run --project ./LegacyCode.Console/LegacyCode.Console.csproj -- 1001
echo "----------------------------------------"

dotnet run --project ./LegacyCode.Console/LegacyCode.Console.csproj -- 1002
echo "----------------------------------------"

dotnet run --project ./LegacyCode.Console/LegacyCode.Console.csproj -- 1003
echo "----------------------------------------"

echo "Done."
read -r -p "Press enter to continue..."
