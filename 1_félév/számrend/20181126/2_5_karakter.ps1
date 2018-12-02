#Get-Content fajl | ForEach-Object{
#    $_.ToString().Substring(2,3);
#}

$fajl=Get-Content fajl.txt

for ($i=0;$i -lt $fajl.Length;$i++){
    $fajl[$i].Substring(2,3);
}