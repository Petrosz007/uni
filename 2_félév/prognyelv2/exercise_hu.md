# JAVA gyakorlás


## TV app

A feladat egy olyan JAVA program írása, amely bemutatja egy *TV app* nevű alkalmazás működését. A (részben) kész megoldásod a melléket `Test` osztállyal ellenőrizheted. Részleges megoldás teszteléséhez, kommenteld ki a még nem kész programrészekre vonatkozó teszteket!

A forradalmi *TV app* új megközelítést visz a tévénézés világába, egységes platformot biztosítva tv-, film- és sorozatnézésre. Az alkalmazásban elérhető műsorokat egy gombnyomásra elindíthatjuk, és előfizethetünk a hozzájuk tartozó csatornára. *(Például, ha a legújabb Trónok harca részt szeretnéd megnézni, nem kell letöltened a HBO GO alkalmazást majd regisztrálni és fizetni, mivel az alkalmazás mindent elintéz helytted. Így azonnal nézni kezdheted.)* Természetesen, ezen funkciókhoz először meg kell adnod valamilyen fizetési módot, de minden más automatikus lesz. Hogy ez megvalósulhasson, a szükséges programrészeket Neked kell implementálni.


### `tv.Wallet`

Készíts egy `tv.Wallet` (*továbbiakban tárca*) osztály, amely az alkalmazáson belüli fizetést teszi lehetővé. Az tárca a `tv.CreditCard` osztállyal (*ezt mindenki előre megkapja a feladattal*) fog kommunikálni és pénzt kérni onnan, ha valamiért fizetnünk kell. Ez azért szükséges, hogy az alkalmazásnak ne kelljen közvetlenül bankkártyákat kezelnie, a fizetéshez elegendő legyen a csak tárcából "kivenni" a pénzt.

A tárca fogja nyilvántartani a rendelkezésre álló összegünket (*egy adattag formályában*).

Az osztálynak két metódusa van: egy a tárcából való fizetésre (`draw`), és egy a tárcába való pénz betételre (`topUp`). A metódusok paramétereinek típusát a mellékelt `test.Test` osztály `testWallet` függvényében leírt tesztek alapján határozd meg.

* A `draw` metódusnak nincs visszatérési értéke. A paraméterként kapott összegről először ellenőrzi, hogy nem-e nagatív, majd ha van elegendő pénz a tárcában, levonja a "kivett" összeget. Ha a kivenni kívánt összeg nagatív vagy nincs elegendő pénz a tárcában, kivételt dob (*plusz pontot ér, ha saját kivételt definiálsz err a célra*).
* A `topUp` metódus létrehoz egy `CreditCard` objektumot egy érvényes kártyaszámmal és a nem negatív összeggel, amivel meg szeretnénk terhelni a kártyát (*negatív összeg esetén ugyan úgy hibát kell dobni, mint az előző metódusban*). Mivel a `CreditCard` osztály egy erőforrás (implementálja a `AutoClosable` interfészt), a használat után fel kell szabadítani (*be kell zárni*).


### `tv.Channel`

A *TV app* alkalmazásban a műsorokat, műsor szolgáltatókon keresztül érhetjük el, akik a tartalmaikat saját "csatornájukon" teszik elérhetővé. Mivel a csatornák belső implementációját nem szeretnénk kiszivárogtatni vagy megszabni, szükség lesz egy közös interfésztre (`tv.Channel`) a velük való kommunikációhoz. *(A `tv.Channel` interfészt megvalósító osztályokat egy későbbi feladatrészben részletezzük.)*

Egy csatornának a három adatot kell biztosítania a következő metódusok segítségével:

* `getName()`, mely a csatorna nevével tér vissza
* `getPrice()`, mely a csatorna előfizetés árával tér vissza
* `getShows()`, mely a csatorna által kínált műsorok listájával tér vissza (*`java.util.List`-et kell használni a megfelelő típusparaméterrel: `tv.Show`, ld. lejjebb*)


### `tv.Show`

Készíts egy `tv.Show` osztályt, amely egy TV műsort fog reprezentálni. Az osztálynak egyetlen értéket kell ("*konstansként*") tárolnia: a műsor nevét. Ehhez rendelkezik egy műsornevet (*string*) váró konstruktorral és egy `getTitle` getter metódussal. *(A `toString` metódus felüldefiniálása plusz pontot ér.)* A tárolt névnek megváltoztathatatlannak kell lennie!


### `tv.providers.*`

Az elérhető szolgáltatók implementációja a `tv.providers` csomagba fog kerülni. Ez jelen esetben három, *package-private* osztály létrehozását jelenti a megadott csomagon belül: `Starz`, `Showtime` és `HBO`. A szolgáltatók csatirnáinak le lehet kérni a nevét, előfizetés árát valamint a műsorlistájukat (*ld.* `tv.Channel`).
 
 * `Starz`
 csatorna neve: *Starz*
 előfizetés ára: *10*
 műsorok: *Outlander*, *Battlestar Galactica*
 * `Showtime`
 csatorna neve: *Showtime*
 előfizetés ára: *7*
 műsorok: *Shameless*
 * `HBO`
 csatorna neve: *HBO*
 előfizetés ára: *5*
 műsorok: *Game of Thrones*, *Westworld*

Ezek az adatok -- jelen esetben -- nem fognak változni, így -- egymástól függetlenül -- közvetlenül az osztályok implementációjába is beleírhatjuk. *(Szebb megoldásként egy abszztrakt szülőosztályban definiálhatjuk az egységes metódusokat, plusz pont fejében. A szintén plusz pontot érő `toString` metódus felüldefiniálása szintén opcionális.)*


### `tv.providers.ChannelFactory`

Mivel egy csatornát csak egyszer szabad létrehozni (*két egyforma csatorna nem megengedett*), ezért létre kell hoznunk egy *publikus* csatornákat példányosító osztályt (`tv.providers.ChannelFactory`).

Ehhez szükség lesz továbbá egy `enum Provider` felsoroló is, mely tartalmazza a meglévő csatornáinkat (`HBO`, `SHOWTIME`, `STARZ`). *(A felsorolót a `ChannelFactory` osztályban is definiálhatod.)*

A `ChannelFactory` osztály egyetlen osztály szintű (*statikus*) `getChannel` metódussal rendelkezzen. A metódus egyetlen `Provider` típusú paramétert vár és visszatér a paraméterben megadott osztály egy példányával.

Ahhoz, hogy minden csatorna csak egyszer legyen példányosítva, hozz létre egy szintén *statikus*, *rejtett* `java.util.Map` típusú adattagot, melyben minden szolgáltatóhoz (*`Provider` értékei*) eltárolod a csatornája (*`Channel` implementációi*) egyetlen példányát. *(Alternatív megoldásként a switch-case szerkezet szintén elfogadott, de kevesebb pontot ér.)*


### `tv.TvApp`

Végül a `tv.TvApp` osztály implementációja következik. Ez az osztály fogja használni a tárcánkat, így adattag szinten rendelkeznie kell egy tárca (`Wallet`) objektummal, melyet konstruktor paraméterként kap meg. Szintén itt lesznek nyilvántartva az elérhető csatornák is és az is, hogy ezek közül melyekre fizettünk elő (*erre egy `Map<Channel,Boolean>` adatszerkezet jó választás lehet, melyet a konstruktorban inicializálhatunk `false` értékekre*).

Az osztály két publikus metódussal rendelkezik:

* `browseAllShows()`: visszatér az összes műsorral (`List<TvShow>`, *minden csatornát beleértve*)
* `searchShow(String)`: Egy műsor címet vár paraméterül és visszatér egy `TvShow` példánnyal ha van egyező nevű műsor; ellenkező esetben hibát dob


### `tv.TvShowImpl`

Azt szeretnénk elérni, hogy a felhasználó anélkül tudjon megnézni egy műsort, hogy foglalkoznia kelljen azzal, hogy melyik csatornán érhető el és előfizetett-e már oda. E célra szükség lesz egy "összekötő" (`tv.TvShowImpl`) osztályra, amely tartalmazza, hogy egy adott műsor melyik csatornához tartozik.

Hozz létre egy *package-private* `tv.TvShowImpl` osztályt, mely a `Show` osztályból származik és egyben implementálja a `TvShow` interfészt is (*utóbbit mindenki előre megkapja a feladattal*). *(A `TvShowImpl` osztályt privát beágyazott osztályként a `tv.TvApp` osztályba is elhelyezhető.)*

Az osztáy konstruktora két paramétert vár: a műsor címét (*string*, *a szülő osztályt is ezzel kell inicializálni /super/*) és a csatornát (*`Channel` objektum*) melyen az adott műsor elérhető.

Az implementálni kívánt `TvShow` interfész miatt, a következő metódusokat kell megvalósítani `TvShowImpl` osztályban:

* `isSubscribed()`: feliratkoztunk-e az adott műsor csatornájára
* `subscribe()`: ha még nem iratkoztunk fel az adott műsor csatornájára, akkor kiveszi a szükséges összeget a tárcából és feliratkozik (*az ezt vezető `Map`-be `true` értéket állít*) ha a volt elég pénzünk a művelethez (*azaz pénzkivétel során nem dobódott kivétel, ld. `tv.Wallet`*)
* `getChannel()`: getter metódus a konstruktorban kapott csatornához
* `watch(OutputStream)`: ha nem vagyunk feliratkozva a műsor csatornájára kivételt dob; ellenkező esetben létrehoz egy `java.io.PrintWriter` példányt a paraméterként kapott `OutputStream` stream objektummal és kiírja a `"You are watching TITLE on CHANNEL"` szöveget (*ahol a `TITLE` a műsor neve, a `CHANNEL` pedig a csatorna neve*)
*(Miután kiírtad az üzenetet, ne feledd meghívni a `flush()` metódust a példányosított `PrintWriter` osztályon.)*




## Konvenciók

### Must have

#### Elnevezések

-   csomag neve csupa kisbetűs, speciális karaktert nem tartalmaz (a "." karaktert leszámítva) pl.  `java.util`,  `hu.elte.java.course2.practical10`
-   osztály neve nagy betűvel kezdődik, szóhatáron ismét nagy betű (camelCase), pl.  `ArrayList`,  `LinkedList`,  `String`
-   metódus és változó (akár adattag, akár lokális változó) neve kis betűvel kezdődik, szóhatáron nagy betű (camelCase), pl.  `toString`,  `add`,  `multipleByTwo`
-   osztály neve tömör és kifejező, jelzi, hogy milyen célt szolgálnak az osztály egyes példányai, pl.  `String`,  `Comparator`
-   metódus neve tömör és kifejező, jelzi, hogy a metódus milyen feladatot végez el, az adott paraméterek alapján mit módosít az objektumon, illetve milyen visszatérési értéket ad, pl.  `add`,  `remove`,  `equals`,  `clone`
-   adattag neve tömör és kifejező, jelzi, hogy milyen információt tárol, mi a szerepe a mutatott objektumnak, primitívnek vagy tömbnek (semmiképp sem egy-két betű), pl.  `elements`  (mondjuk egy lista esetén),  `employees`  (mondjuk egy cég esetén)
-   lokális változó neve tömör, és utal a változóban tárolt információra, vagy legalább a változó típusára, a különböző célokra használt változók egyértelműen megkülönböztethetők, pl. megfelelő:
    
    ```
        String str; // ha egy Stringet kell összerakni
        int i; // ciklusváltozó esetén
        String part1, part2, part3; // ha mondjuk egy tömb első, második és
        //harmadik elemét mentjük ki beléjük, tehát közös célra használatosak
        kerülendő:
        String st, str; // a kettő együtt
        String str1, str2, str3; // ha a három változónak később semmi köze egymáshoz
        int a; // nem utal sem a szerepére, sem a típusára
    ```
    
-   a hallgató által írt elnevezések vagy egységesen magyar, vagy egységesen angol nyelvűek
    

#### Indentálás

-   indentálásra egységesen szóközöket vagy egységesen tabulátorokat használ, a kettőt nem vegyíti
-   minden kapcsos zárójellel körülzárt blokk belseje egy szinttel beljebb van húzva
-   nyitó kapcsos az előző sor végén áll, vagy új sorban egymagában, de az adott kódban egységesen
-   záró kapcsos új sorban áll egymagában
-   rövid utasításblokk esetén (pl. kivételkezelés catch ága, ha rövid) elfogadható a nyitó kapcsos, utasítás és záró kapcsos egy sorban
-   nincs 80 karakternél hosszabb sor
-   lambdák és névtelen osztályok esetén a záró kapcsos után folytatódhat a sor, illetve rövid lambda írható egy sorba az őt tartalmazó utasítással

#### Strukturálás

-   ciklusok egymásba ágyazva legfeljebb 2 szint mélyen (ciklusban ciklus, de nincs ciklusban ciklusban ciklus) Ismert algoritmusok (pl. mátrixszorzás) esetén és különösen indokolt esetben a 3 szintű egymásba ágyazás még elfogadható, több semmiképpen.
-   ```
    Ha szükséges lenne ennél mélyebb egymásba ágyazás, akkor egy részét ki kell emelni egy metódusba.
    ```
    
-   elágazások és ciklusok egymásba ágyazva legfeljebb 4 szint mélyen
-   ```
    Ha szükséges lenne ennél mélyebb egymásba ágyazás, akkor egy részét ki kell emelni egy metódusba.
    ```
    
-   nincs 5 sornál hosszabb ismétlődő rész a kódban (ismétlődés az is, ha az eltérés minimális a két kódrészletben, pl. egy-egy változó, általánosítható típus vagy valamilyen rövid részeredmény, ami lokális változóba kiemelhető lenne)
-   nincs hosszú, több részletből álló számítás, ami ismétlődne, pl. rossz
    
    ```
    if ( /*...*/ ) {
        String result = "A person whose name is " + name + " and who has a " +
            salary + "$ salary bought a " + price + "$ car".
    } else {
        String result = "A person whose name is " + name + " and who has a " +
            salary + "$ salary bought a " + price + "$ boat".
    }
    // A sorok végén lévő eltérő rész egy lokális változóba kiszámítható előre.
    ```
    
-   nincs 25 sornál hosszabb metódus
-   nincs 1000 sornál hosszabb osztály
    

#### Láthatóságok

-   az adott osztálytól elvárt publikus műveleteken kívül az osztály nem tartalmaz más publikus műveleteket
-   az osztály nem tartalmaz publikus adattagokat, kivéve, ha a feladat ennek ellenkezőjét kimondottan kéri

#### Biztonság

-   a belső állapotot semelyik metódus sem szivárogtatja ki
-   ```
        Egy objektum ha egy nem rejtett metódusában paraméterül kap egy másik objektumot, amit le akar menteni egy adattagjába, vagy kiad egy adattagjában tárolt objektumot, azokat másolni kell, hogy a belső állapot kintről ne legyen módosítható.
    ```
    
-   ```
      A másolásra értelemszerűen nincs szükség (és ezért nem is szabad), ha a kapott, illetve kiadott objektum immutable, azaz módosíthatatlan, pl. `String`, `Integer`.
    ```
    

#### Hatékonyság

-   immutable objektumokat (pl.  `String`) ne másoljunk
-   a kód nem számítja ki többször ugyanazt, ha közben a kiszámítandó érték biztosan nem változott, pl. rossz
    
    ```
    Point first = line.getPoints()[0];
    Point second = line.getPoints()[1];
    Point third = line.getPoints()[2];
    ...
    // Itt a getPoints() egy potenciálisan bonyolult számítást végző művelet,
    // és feleslegesen hívódik meg háromszor. Az utasítássor elején egyszer
    // kellene meghívni, és az eredményül kapott tömböt lementeni, aztán azt
    // feldolgozni.
    
    int len = line.getPoints().length;
    for (int i = 0; i < len; ++i) {
        Point point = line.getPoints()[i];
        ...
    }
    // Itt minden iterációban meghívódik a getPoints() utasítás, teljesen
    //feleslegesen. Itt is kimenthető lenne az értéke egy lokális változóba előre.
    
    // Figyelem! A fenti példák természetesen NEM csak tömbök esetén igazak,
    // bármilyen visszatérési értékű metódus esetén felléphet, ha tudjuk, hogy
    // az eredmény minden hívásnál ugyanaz.
    ```
    
-   ha tudhatóan sok vagy hosszú, illetve ha ismeretlen darabszámú  `String`-eket konkatenálunk össze, azokat  `StringBuilder`-rel kell összekapcsolni; pl. ha egy fájl sorait fűzzük össze, és nem tudjuk, milyen hosszú a fájl
    

#### Általános elvek

-   statikus mezőt vagy statikus metódust nem hivatkozunk objektumreferencián keresztül; pl. rossz:
    
    ```
        Integer i = 5;
        int j = i.parseInt("123");
    ```
    
-   rövid magyarázó kommentek a bonyolult kódrészletekhez (az időkorlátok miatt ezt zárthelyin nem várjuk el)
    

### Nice to have

#### Elnevezések

-   static final adattag csupa nagy betűs, szóhatáron aláhúzás pl.  `STATIC_FINAL_VARIABLE`
-   típusparaméter egyetlen nagy betűből áll pl.  `T`,  `E`,  `W`
-   minden elnevezés angol nyelvű

#### Indentálás

-   minden utasítás külön sorban
-   minden ciklus és elágazás utasításai kapcsos zárójelek közé vannak zárva, akkor is, ha csak egy utasítás van
-   lambdák és névtelen osztályok esetén a záró kapcsos után folytatódhat a sor, illetve rövid lambda írható egy sorba az őt tartalmazó utasítással

#### Strukturálás

-   nincs 2 sornál hosszabb ismétlődő rész a kódban

#### Láthatóságok

-   minden eleme a kódnak a legszigorúbb láthatósággal van védve, ami még a kód működéséhez elegendő
-   adattagok mindig privátak, kivéve, ha a feladat ennek ellenkezőjét kimondottan kéri

#### Biztonság

-   a  `Closable`  interfészt megvalósító osztályok (pl.  `Scanner`,  `PrintWriter`) objektumain minden esetben meghívja a használatuk végén a  `close()`  metódust  `try`-`catch`-`finally`  vagy  _try-with-resources_  szerkezettel

#### Hatékonyság

-   ha van az adott feladatra egy ismert vagy egyszerű algoritmus, amely nagyságrendekkel gyorsabb, mint az elsőre megírt, akkor használjuk azt!

#### Általános elvek

-   törekedjünk a kód tömörségére (nem minden áron!) A rövid kód általában könnyebben olvasható, értelmezhető, később egyszerűbb módosítani. Ha olyan kódrészleteket látunk, melyek egy gyors változtatással rövidebben is megírhatóak, azt érdemes megtenni.
-   az osztályok egy-egy egyértelmű feladatot látnak el (Single Responsibility Principle)
-   a kód "öndokumentáló", elolvasva könnyen értelmezhető, hogy milyen feladatot végez el
-   dokumentációs kommentek (JavaDoc) az összetett metódusokhoz és osztályokhoz (az időkorlátok miatt ezt zárthelyin nem várjuk el)

