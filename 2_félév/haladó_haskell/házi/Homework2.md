# Házi feladat

A házi feladat beadásánál ügyeljetek arra, hogy minden szükséges definíció látható legyen, tehát ha a feladat definiál új típusokat vagy függvényeket, akkor azokat is másoljátok át. A feladat megoldásához használhatóak külső (a Haskell Platform részét képező) könyvtárak is.

__Megjegyzés__: A házi feladat opcionális, de segíti a felkészülést a következő óra eleji +/-ra, ezért erősen ajánlott az elkészítése.

# Elméleti áttekintés

Az első feladatotok, hogy tanulmányozzátok a `Semigroup`, `Monoid` és `Foldable` típusosztályokat. A definíciójukát megtaláljátok az alábbi linkeken:

  - `Semigroup`: http://hackage.haskell.org/package/base-4.12.0.0/docs/Data-Semigroup.html
  - `Monoid`: http://hackage.haskell.org/package/base-4.12.0.0/docs/Data-Monoid.html
  - `Foldable`: http://hackage.haskell.org/package/base-4.12.0.0/docs/Data-Foldable.html

A következő kérdésekre keressetek választ:

  - Hogyan értelmezzük a félcsoport és monoid struktúrákat a függvényekre?
  - Milyen speciális jelentést tulajdonít a `Dual` wrapper az `a -> a` függvényeknek?
  - Hogyan fejezhetőek ki a `foldr` ls `foldl` műveletek a `foldMap` segítségével?

A kérdésekre nem szükséges leírnotok a válaszotokat, csak értsétek meg őket.

# `RoseTree` adatszerkezet

A `RoseTree` egy tetszőlegesen elágazó fá adatszerkezetet jelent. A következőképpen definiálható Haskell-ben:

```haskell
data RoseTree a = Node { value    :: a
                       , children :: [RoseTree a]
                       }
  deriving (Eq, Show)
```

__Példa__:

```haskell
haskellTree :: RoseTree String
haskellTree = Node "Haskell"
              [ Node "is"
                [ Node "a" []
                , Node "purely" []
                ]
              , Node "functional"
                [ Node "programming" []
                , Node "language" []
                ]
              ]
```

## Bejárások

Egy `RoseTree` pre-order bejárása alatt azt értjük, hogy először a jelenlegi elemet "látogatjuk" meg, majd rekurzívan bejárjuk az összes gyerekét. A post-order bejárásán pedig, hogy először a gyerekeit járjunk be rekurzívan, majd csak ezután látogatjuk meg a jelenlegi elemet.

Az előzőleg definiált fa preorder bejárása a következő string-et adja eredményül:

```haskell
"Haskell is a purely functional programming language" == unwords (preOrder haskellTree)
```

A post-order bejárás pedig:

```haskell
"a purely is programming language functional Haskell" == unwords (postOrder haskellTree)
```

## Feladat

A feladat, hogy megadjunk a `RoseTree` adatszerkezethez egy pre-order és egy post-order bejárási algoritmust.

```haskell
preOrder  :: RoseTree a -> [a]
postOrder :: RoseTree a -> [a]
```

__Megjegyzés__: A listákban a megfelelő sorrend szerint szerepeljenek az elemek.


A fenti két függvényt a `Foldable` típusosztáy `toList` műveletének segítségével definiáljuk! Ehhez arra van szükség, hogy a `Sum` és `Product` típusokhoz hasonló wrapper-eket hozzunk létre a `RoseTree`-hez is. Ezek a wrapper-ek a következők legyenek:

```haskell
newtype PreOrder  t a = PreOrder  { getPreOrder  :: t a } deriving (Eq, Show)
newtype PostOrder t a = PostOrder { getPostOrder :: t a } deriving (Eq, Show)
```

Adjuk meg a következő `Foldable` példányokat!

```haskell
instance Foldable (PreOrder  RoseTree)
instance Foldable (PostOrder RoseTree)
```

__Megjegyzés__: A fenti példányok definiálásához szükséges a `FlexibleInstances` nyelvi kiterjesztés bekapcsolása. Egészítsük ki a modul deklarációt a következőképpen:

```haskell
{-# LANGUAGE FlexibleInstances #-}
module Homework2 where
```

A `toList` függvény segítségével definiáljuk a fentebb említett `preOrder` és `postOrder` függvényeket!