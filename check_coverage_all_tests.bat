@echo off
devenv.exe FastGTD.sln /Rebuild
del coverage.xml
"Tools\PartCover .NET 2\PartCover.exe" --target "Tools\NUnit\bin\nunit-console.exe" --target-args "FastGTD.UnitTests\bin\Debug\FastGTD.UnitTests.dll FastGTD.CustomerTests\bin\Debug\FastGTD.CustomerTests.dll FastGTD.DataAccessTests\bin\Debug\FastGTD.DataAccessTests.dll" --include [FastGTD*]* --output coverage.xml
"Tools\PartCover .NET 2\PartCover.Browser.exe"
