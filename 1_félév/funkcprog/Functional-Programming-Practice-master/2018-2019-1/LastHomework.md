# Házi feladat

A házi feladatot egy `LastHomework` nevű modulként kell beadni. Minden definiálandó függvényhez adjuk meg a hozzá tartozó típus szignatúrát is! A feladatban néhány segédfüggvényt kell definiálni a `Maybe` típushoz, valamint egy adatbázis Haskelles reprezentációján kell különböző műveleteket implementálni.

### A megoldáshoz ne használjuk a `Data.Maybe` modult, kizárólag azokat a függvényeket, amelyek a `Prelude`-ból és a `Data.List`-ből elérhetőek.

## `Maybe`-s segédfüggvények

Adjuk meg azt a függvényt, amely eldönti egy `Maybe` típusú értékről, hogy az `Just`-tal lett-e konstruálva!

```haskell
isJust :: Maybe a -> Bool
```

Adjuk meg azt a függvényt, amely egy `Just`-ból kiszedi a benne lévő értéket, `Nothing`-ra pedig hibát dob!

```haskell
fromJust :: Maybe a -> a
```

Adjuk meg azt a függvényt, amely egy `Maybe`-ket tartalmazó listából kiválogatja a `Just`-okat, és egy olyan listát ad vissza, amelyben a `Just`-ok által tárolt értékek szerepelnek (`Nothing`-kat pedig elhagyja)! Példa: `catMaybes [Just 3, Nothing, Just 1] == [3,1]`

```haskell
catMaybes :: [Maybe a] -> [a]
```

Adjuk meg azt a függvényt, amely `Maybe` típusba képező függvényt alkalmaz egy lista elemeire, majd kiválogatja az így kapott listából a `Just`-okat, és azokból egyesével kiszedi a bennük lévő értékeket!

```haskell
mapMaybe :: (a -> Maybe b) -> [a] -> [b]
```

Adjuk meg azt a függvényt, amely biztonságos módon visszaadja egy lista első elemét!

```haskell
safeHead :: [a] -> Maybe a
```


## Adatbázis

A következő feladatokban egy adatbázis Haskelles reprezentációján kell majd különböző műveleteket implementálni.

### Reprezentáció

Egy felhasználót a felhasználónevével és a jelszavál fogunk azonosítani.

```haskell
type Username = String
type Password = String
```

Minden felhasználóhoz fog tartozni egy jogosultsági szint.

```haskell
data Privilege = Simple | Admin
  deriving (Eq, Show)
```

Azt, hogy ki van bejelentkezve, cookie-k segítségével fogjuk számontartani.

```haskell
data Cookie = LoggedOut | LoggedIn Username Privilege
  deriving (Eq, Show)
```

Az adatbázisban egy bejegyzés három dolgot fog tartalmazni: a felhasználóhoz tartozó jelszót, jogosultsági szintet és a felhasználó barátait.

```haskell
data Entry = Entry Password Privilege [Username]
  deriving (Eq, Show)
```

Végül pedig az adatbázist kulcs-érték párok listájával fogjuk reprezentálni, ahol a kulcsok a felhasználónevek, az értékek pedig bejegyzések lesznek.

```haskell
type Database = [(Username, Entry)]
```

### Példa adatok

A következő néhány példa adaton fognak futni a tesztek, úgyhogy majd ezeket is másoljátok be a megoldásba.

```haskell
richard, charlie, carol, david, kate :: (Username, Entry)
richard = ("Richard", Entry "password1" Admin  ["Kate"])
charlie = ("Charlie", Entry "password2" Simple ["Carol"])
carol   = ("Carol",   Entry "password3" Simple ["David", "Charlie"])
david   = ("David",   Entry "password4" Simple ["Carol"])
kate    = ("Kate",    Entry "password5" Simple ["Richard"])

testDB :: Database
testDB = [ richard, charlie, carol, david, kate ]

testDBWithoutCarol :: Database
testDBWithoutCarol =
  [ ("Richard", Entry "password1" Admin  ["Kate"])
  , ("Charlie", Entry "password2" Simple [])
  , ("David",   Entry "password4" Simple [])
  , ("Kate",    Entry "password5" Simple ["Richard"])
  ]
```

### Szelektorfüggvények

Adjuk meg azt a függvényt, amely kiválasztja egy bejegyzésből a felasználó jelszavát!

```haskell
password :: Entry -> Password
```

Adjuk meg azt a függvényt, amely kiválasztja egy bejegyzséből a felasználó jogosultsági szintjét!

```haskell
privilege :: Entry -> Privilege
```

Adjuk meg azt a függvényt, amely kiválasztja egy bejegyzésből a felhasználó barátait!

```haskell
friends :: Entry -> [Username]
```

### Adatbázis műveletek

Adjuk meg azt a függvényt, amely kap egy felhasználónevet, egy jelszót, és egy adatbázis bejegyzést, és ha bejegyzésben lévő jelszó megegyezik a kapott jelszóval, akkor egy `LoggedIn` cookie-t ad vissza a felhasználó számára, egyébként pedig egy `LoggedOut` cookie-t.

```haskell
mkCookie :: Username -> Password -> Entry -> Cookie
```

Adjuk meg azt a függvényt, amely bejelentkeztet egy felhasználót! A felhasználót úgy jelentkeztetjük be, hogy ha szerepel az adatbázisban, akkor visszaadunk egy cookie-t a felhasználónevével és a jogosultsági szintjével, ha pedig nem szerepel az adatbázisban, akkor kijelentkezteve tartjuk. __Segítség__: Használjuk a `lookup`, `maybe` és `mkCookie` függvényeket!

```haskell
login :: Username -> Database -> Cookie
```

Adjuk meg azt a függvényt, amely a következőképpen működik Kap egy törlendő felhasználót, és egy adatbázisban szereplő kulcs-érték párt. Ha a kulcs éppen a törlendő felhasználó, akkor térjen vissza `Nothing`-gal. Egyébként pedig törölje a bejegyzésben lévő barátok közül közül a törlendő felhasználót, és az eredményt egy `Just`-ba csomagolva adja vissza.

```haskell
updateEntry :: Username -> (Username, Entry) -> Maybe (Username, Entry)
```

Adjuk meg azt a függvényt, amely töröl egy felhasználót az adatbázisból! __Segítség__: Használjuk az előző az `updateEntry` és `mapMaybe` függvényeket!

```haskell
deleteUser :: Cookie -> Username -> Database -> Database
```

### Szociális háló (bónusz)

Adjuk meg azt a függvényt, amely kikeres egy felhasználót egy adatbázisból, majd visszaadja a barátait! __Segítség__: Használjuk a `lookup` és `maybe` függvényeket!

```haskell
getFriends :: Username -> Database -> [Username]
```

Adjuk meg azt a függvényt, amely kikeres egy felhasználót egy adatbázisból, majd visszaadja a barátait és a felhasználót magát is! Ügyeljünk arra, hogy abban az esetben, ha a felhasználó eleve nem szerepelt az adatbázisban, akkor üres listát adjunk vissza!

```haskell
getFriendsRefl :: Username -> Database -> [Username]
```

Adjuk meg azt a függvényt, amely kiszámolja egy függvény adott kezdőpontból indított fixpontját! Ez azt jelenti, hogy a függvényt addig alkalmazza az adott értékre, amíg az változik. Amint egymásután kétszer ugyanaz az érték következik, a függvény leáll, és visszaadja az aktuális értéket.


```haskell
fixPoint :: Eq a => (a -> a) -> a -> a
```

Adjuk meg azt a függvényt, amely a követképpen kiválogatja egy listából az egyedi elemeket. Első lépésként lerendezi a listát, majd csoportosítja az egymás mellett lévő azonos elemeket, majd minden csoportból kiválasztja az első elemet. Az így kapott listában az eredeti listában lévő elemek fognak szerepelni, azonban mindegyik pontosan egyszer fog előfordulni, és rendezett sorrendben lesznek.

```haskell
sortUnique :: (Eq a, Ord a) => [a] -> [a]
```

Az előző függvények segítségével adjuk meg azt a függvényt, amely meghatározza egy felhasználó szociális hálóját! Egy felhasználó szoicális hálója tartalmazza magát a felhasználót, az ő barátait, a barátai barátait, és így tovább. Azaz a felhasználóból, baráti reláción keresztül elérhető összes felhasználót (beleértve önmagát is).

```haskell
getSocialNetwork :: Username -> Database -> [Username]
```

