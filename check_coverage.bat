@echo off
devenv.exe FastGTD.sln /Rebuild
del coverage.xml
"Tools\PartCover .NET 2\PartCover.exe" --target "Tools\NUnit\bin\nunit-console.exe" --target-args "FastGTD.UnitTests\bin\Debug\FastGTD.UnitTests.dll FastGTD.IntegrationTests\bin\Debug\FastGTD.IntegrationTests.dll" --include [FastGTD*]* --output coverage.xml
"Tools\PartCover .NET 2\PartCover.Browser.exe"
