# Paste this script to your directory where you want your new project to be created

$projectName = Read-Host 'Enter your new project name'
$templatePath = "C:\Project Repositories\GitHub\Personal Projects\SOCATemplate" # Template Path

$projectDir = "$($PSScriptRoot)\"
$textToSearch = "SOCATemplate"
$textToReplace = $projectName

$targetPath = Join-Path $projectDir "$($textToReplace)"

$exclude = @(".vs", ".git", "obj", "bin", "Clone-Project.ps1")

Function XCopy-Item {
    param (
        [string]$Path,
        [string]$Destination,
        [string[]]$Exclude
    )

    Get-ChildItem -Path $Path -Exclude $Exclude |
        Copy-Item -Destination {
            Join-Path $Destination $_.FullName.Substring($Path.length)
        }

    Get-ChildItem -Path $Path -Directory -Exclude $Exclude | 
        ForEach-Object {
            XCopy-Item -Path "$($_.FullName)\" -Destination "$($Destination)\$($_.FullName.Substring($Path.Length))\" -Exclude $Exclude            
        }
}

if (Test-Path -Path $targetPath -PathType Container) {
    Remove-Item $targetPath -Recurse
}

XCopy-Item -Path $templatePath -Destination $targetPath -Exclude $exclude

Get-ChildItem -Path $targetPath -Recurse | ForEach-Object  {
    if ($_.FullName.Contains($textToSearch)) {
        Rename-Item -Path $_.FullName -NewName $_.FullName.Replace($textToSearch, $textToReplace)
    }
}

Get-ChildItem -Path $targetPath -Recurse | ForEach-Object {
    $isDir = Test-Path -Path $_.FullName -PathType Container

    if ($isDir -eq $false) {
        ((Get-Content -path $_.FullName -Raw) -replace $textToSearch,$textToReplace) | Set-Content -Path $_.FullName
    }
}