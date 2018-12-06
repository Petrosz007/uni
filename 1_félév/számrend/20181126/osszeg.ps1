#($args | Measure-Object -Sum).Sum
$sum=0
if ($args.Count -gt 0)
{
    $tomb=$args
}
else
{
    $tomb=$input

}
$tomb | ForEach-Object{
    $sum+=$_
}

$sum