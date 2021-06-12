$projectName = "Argus" #New Project Name
$projectDir = "D:\Git Workspace\Personal" #New Project Directory


$textToSearch = "SOCATemplate"
$textToReplace = $projectName

$sourcePath = "$($PSScriptRoot)\"
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

XCopy-Item -Path $sourcePath -Destination $targetPath -Exclude $exclude

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