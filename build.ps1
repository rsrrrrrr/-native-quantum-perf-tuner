
#requires -version 5
Write-Host "Building backend..."
Push-Location backend
dotnet publish -c Release -r win-x64
Pop-Location

Write-Host "Installing frontend deps..."
Push-Location frontend
npm install
npm run pack
Pop-Location
