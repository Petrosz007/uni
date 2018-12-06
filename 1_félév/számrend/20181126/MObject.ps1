$szamok= 6, 1, 2, 3, 4, 5;
#$szamok2=Write-Output $szamok | Measure-Object -maximum
$szamok2=Write-Output $szamok | Sort-Object
Write-Host "-------------------------"
$szamok=$szamok2

$szamok