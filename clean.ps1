$dirs = Get-ChildItem .\hxc.AspNetCoreTemplate -recurse | Where-Object {$_.PSIsContainer -eq $true -and ($_.Name -match "obj" -or $_.Name -match "bin")}
foreach ($item in $dirs) {
    Write-Host $item.FullName
}

$dirs | Remove-Item -Recurse -Confirm
