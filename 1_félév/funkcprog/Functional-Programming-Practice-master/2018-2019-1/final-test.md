1. Melyik HELYES kifejezés?
    A) `6 div 3`
    B) ``6 `div` 3``
    C) `6 (div) 3`
    D) `(6) div (3)`

2. Melyik HELYES kifejezés, ha `f :: Int -> Int -> String -> Bool`?
    A) `f (1 2 "str") || True`
    B) `f (1 2 "str" || True)`
    C) `(f 1 2 "str") || True`
    D) `f(1,2,"str") || True`

3. Melyik állítás HAMIS az alábbiak közül?
```
    f ([a]:[b])
    f (a:b:[])
    f ((a:[]):b:[])
```
    A) Az 1. és 3. minták ugyanarra illeszkednek
    B) Amire illeszkedik az 1. minta, arra illeszkedik a 2. minta is
    C) Amire illeszkedik az 2. minta, arra illeszkedik a 3. minta is
    D) A minták közül legalább egy illeszkedik a(z) `[[],[]]` értékre

4. Melyik kifejezés NEM lista típusú?
    A) `([[1,2],[]])`
    B) `[(1,2),(3,4)]`
    C) `([1,2]:[])`
    D) `([1,2],[])`

5. Mi az eredménye a következő kifejezésnek: `head [1,2,3] == take 1 [3,2,1]`
    A) `True`
    B) `False`
    C) Fordítási idejű hiba
    D) Futási idejű hiba

6. Melyik sor hagyható el a függvényből úgy, hogy működése változatlan maradjon?
```
  f [x,y] = x `min` y
  f [x] = x
  f (x:rest) = x `min` f rest
```
    A) Az első
    B) A második
    C) A harmadik
    D) Egyik sem

7. Mi lehet a típusa az alábbi kifejezésnek?
```
  map (filter (\x -> x))
```
    A) `[a] -> [a]`
    B) `[Bool] -> [Bool]`
    C) `[[a]] -> [[a]]`
    D) `[[Bool]] -> [[Bool]]`

8. Mi lehet a típusa az alábbi függvénynek?
```
  f (x,xs) = init x
  f _ = "default"
```‌
    A) `f :: [a] -> String`
    B) `f :: [String] -> String`
    C) `f :: (a,b) -> String`
    D) `f :: ([Char],a) -> [Char]`

9. Mi a típusa az alábbi függvénynek?
```
  f [] = []
  f [x] = [x]
  f [x:xs] = [xs]
```
    A) `f :: [a] -> [a]`
    B) `f :: [[a]] -> [a]`
    C) `f :: [a] -> [[a]]`
    D) `f :: [[a]] -> [[a]]`

10. Mi az eredménye a következő kifejezésnek: `zip [1..] "almafa"`
    A) Nem áll le, nem ír ki semmit
    B) Nem áll le, végtelen eredményt ad
    C) Véges eredménnyel leáll
    D) Fordítási hiba

11. Mi igaz a `foldl (++) x ls` és `foldr (++) x ls` kifejezésekre?
    A) Eredményük és futási költségük mindenképp azonos
    B) Eredményük és futási költségük azonos, ha ls véges és kiértékelhető
    C) Csak az egyik lehet típushelyes
    D) Típushelyesek, de eredményük vagy futási költségük eltérő lehet

12. Melyik kifejezés hoz létre helyes `T` típusú értéket, ha `data T = A Int Bool | B String | C`
    A) `A "almafa"`
    B) `A True 3`
    C) `B 3`
    D) `C`
