# Házi feladat

A házi feladat beadásánál ügyeljetek arra, hogy minden szükséges definíció látható legyen, tehát ha a feladat definiál új típusokat vagy függvényeket, akkor azokat is másoljátok át. A feladat megoldásához használhatóak külső (a Haskell Platform részét képező) könyvtárak is.

__Megjegyzés__: A házi feladat opcionális, de segíti a felkészülést a következő óra eleji +/-ra, ezért erősen ajánlott az elkészítése.

## Természetes számok

Tekintsük a természetes számok Peano-féle definícióját:

```haskell
data Nat = Zero | Suc Nat
  deriving Show
```

Definiáljuk a természetes számokon értelmezett összeadást!

```haskell
addNat :: Nat -> Nat -> Nat
```

Definiáljuk a természetes számokon értelmezett szorzást!

```haskell
mulNat :: Nat -> Nat -> Nat
```

## Listák

Tekintsük a következő lista típust:

```haskell
data List a = Nil | Cons a (List a)
  deriving Show
```

Definiáljuk azt a függvényt, amely összeszorozza egy természetes számokból álló lista elemeit!

```haskell
product' :: List Nat -> Nat
```

Definiáljuk azt a függvényt, amely egy lista minden elemére alkalmaz egy függvényt!

```haskell
mapList :: (a -> b) -> (List a) -> (List b)
```

Definiáljuk a listákon értelmezett összefűzés műveletet (konkatenációt)!

```haskell
(+++) :: List a -> List a -> List a
```

Példányosítjuk az `Eq` és `Ord` típusosztályokat a `List a` típusra!

## Bináris fák

Definiáljunk egy új típust, amellyel bináris fákat reprezentálhatunk! A típus neve legyen `Tree`, valamint két típusváltozóval legyen paraméterezhető. Továbbá rendelkezzen pontosan két adatkonstruktorral: `Leaf :: a -> Tree a b` és `Bin :: b -> Tree a b -> Tree a b -> Tree a b`.

__Megjegyzés__: Ez egy olyan bináris fa, melynek csúcsaiban és leveleiben is lehet adat, valamint a levelekben és a csúcsokban tárolt adatok típusa eltérő is lehet.

__Példa__: Egy fa, melynek leveleiben `String`-ek vannak, csúcsaiban pedig az adott részfa magassága: `Bin 2 (Leaf "Haskell") (Bin 1 (Leaf "is") (Leaf "great!"))`


Definiáljuk azt a függvényt, amely egy `Tree` belső elemeit transzformálja!

__Megjegyzés__: A függvény a `mapList`-hez hasonló lesz, azonban most két átlakító függvényt kapunk paraméternek. Az egyiket a leveleken kell alkalmazni, a másikat a csúcsokban.

```haskell
mapTree :: (a -> c) -> (b -> d) -> Tree a b -> Tree c d
```