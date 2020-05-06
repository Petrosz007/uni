
# 6. gyakorlat megoldva
# Dolgozó
Kik azok a dolgozók, akik osztályának telephelye DALLAS vagy CHICAGO?

    π dnev (σ telephely = 'DALLAS' ∨ telephely = 'CHICAGO' (dolgozo ⨝ osztaly) )

    --select * from dolgozo, osztaly where dolgozo.oazon=osztaly.oazon and osztaly.telephely in ('DALLAS','CHICAGO');

    select dolgozo.dnev from dolgozo, osztaly where dolgozo.oazon=osztaly.oazon and (osztaly.telephely='DALLAS' or osztaly.telephely='CHICAGO');

Kik azok a dolgozók, akik osztályának telephelye nem DALLAS és nem CHICAGO?

    π dnev (σ telephely ≠ 'DALLAS' ∧ telephely ≠ 'CHICAGO' (dolgozo ⨝ osztaly) )

    select dolgozo.dnev from dolgozo, osztaly where dolgozo.oazon=osztaly.oazon and (osztaly.telephely!='DALLAS' and osztaly.telephely!='CHICAGO');

Adjuk meg azoknak a nevét, akiknek a fizetése > 2000 vagy a CHICAGO-i osztályon dolgoznak.

    π dnev (σ fizetes > 2000 ∨ telephely = 'CHICAGO' (dolgozo ⨝ osztaly) )

    select dolgozo.dnev from dolgozo, osztaly where dolgozo.oazon=osztaly.oazon and (dolgozo.fizetes>2000 or osztaly.telephely='CHICAGO');

Melyik osztálynak nincs dolgozója?

    π oazon (osztaly) - π oazon (dolgozo)

    select oazon from osztaly MINUS select oazon from dolgozo;

Adjuk meg azokat a dolgozókat, akiknek van 2000-nél nagyobb fizetésű beosztottja.

    π d2.dnev (σ d1.fonoke = d2.dkod ∧ d1.fizetes > 2000 (ρ d1 (dolgozo) ⨯ ρ d2 (dolgozo)))

    select distinct d2.dnev from dolgozo d1, dolgozo d2 where d1.fonoke=d2.dkod and d1.fizetes>2000;

    --select dnev from dolgozo MINUS select distinct d2.dnev from dolgozo d1, dolgozo d2 where d1.fonoke=d2.dkod and d1.fizetes>2000;

Adjuk meg azokat a dolgozókat, akiknek nincs 2000-nél nagyobb fizetésű beosztottja.

    π d2.dnev (σ d1.fonoke = d2.dkod (ρ d1 (dolgozo) ⨯ ρ d2 (dolgozo))) - 
    π d2.dnev (σ d1.fonoke = d2.dkod ∧ d1.fizetes > 2000 (ρ d1 (dolgozo) ⨯ ρ d2 (dolgozo)))

    (select d2.dnev from dolgozo d1, dolgozo d2 where d1.fonoke=d2.dkod) MINUS (select distinct d2.dnev from dolgozo d1, dolgozo d2 where d1.fonoke=d2.dkod and d1.fizetes>2000);

Adjuk meg azokat a telephelyeket, ahol van elemző (ANALYST) foglalkozású dolgozó.

    π telephely (σ foglalkozas = 'ANALYST' (dolgozo ⨝ osztaly) )

    select distinct osztaly.telephely from dolgozo, osztaly where dolgozo.oazon = osztaly.oazon and dolgozo.foglalkozas='ANALYST';

Adjuk meg azokat a telephelyeket, ahol nincs elemző (ANALYST) foglalkozású dolgozó.

    π telephely (osztaly) - π telephely (σ foglalkozas = 'ANALYST' (dolgozo ⨝ osztaly) )

    (select osztaly.telephely from osztaly) MINUS (select distinct osztaly.telephely from dolgozo, osztaly where dolgozo.oazon = osztaly.oazon and dolgozo.foglalkozas='ANALYST');

Adjuk meg azokra az osztályokra az átlagfizetést, ahol ez nagyobb mint 2000.

    π oazon, avg σ avg>2000 γ oazon; avg(fizetes)->avg (dolgozo)

    SELECT oazon, avg(fizetes) FROM dolgozo group by oazon HAVING avg(fizetes)>2000;

Adjuk meg az átlagfizetést azokon az osztályokon, ahol legalább 4-en dolgoznak (oazon, avg_fiz)

    π oazon, avg σ db ≥ 4 γ oazon; avg(fizetes)->avg, count(*)->db (dolgozo)

    SELECT oazon, avg(fizetes) FROM dolgozo group by oazon having count(*)>4;

Adjuk meg az átlagfizetést és telephelyet azokon az osztályokon, ahol legalább 4-en dolgoznak.

    π oazon, telephely, avg σ db ≥ 2 γ oazon, telephely; avg(fizetes)->avg, count(*)->db (dolgozo ⨝ osztaly)

    SELECT oazon, telephely, avg(fizetes) FROM dolgozo natural join osztaly group by oazon, telephely having count(*)>4;

    --SELECT osztaly.oazon, osztaly.telephely, avg(dolgozo.fizetes) FROM dolgozo, osztaly where dolgozo.oazon=osztaly.oazon group by osztaly.oazon, osztaly.telephely having count(*)>4;

Adjuk meg azon osztályok nevét és telephelyét, ahol az átlagfizetés nagyobb mint 2000. (onev, telephely)

    π oazon, telephely σ avg ≥ 2000 γ oazon, telephely; avg(fizetes)->avg, count(*)->db (dolgozo ⨝ osztaly)

    SELECT osztaly.oazon, osztaly.telephely FROM dolgozo, osztaly where dolgozo.oazon=osztaly.oazon group by osztaly.oazon, osztaly.telephely having avg(dolgozo.fizetes)>2000;

Adjuk meg azokat a fizetési kategóriákat, amelybe pontosan 3 dolgozó fizetése esik.

    π kategoria σ db = 3 γ kategoria; count(*)->db (σ dolgozo.fizetes >= fiz_kategoria.also and fizetes <= fiz_kategoria.felso (dolgozo ⨯ fiz_kategoria))

    SELECT kategoria from (select * from dolgozo, fiz_kategoria where dolgozo.fizetes between fiz_kategoria.also and fiz_kategoria.felso) group by kategoria having count(*)=3;

Adjuk meg azokat a fizetési kategóriákat, amelyekbe eső dolgozók mindannyian ugyanazon az osztályon dolgoznak.

    π kategoria, db σ db =1 γ kategoria; count(oazon)->db (σ dolgozo.fizetes >= fiz_kategoria.also and fizetes <= fiz_kategoria.felso (dolgozo ⨯ fiz_kategoria))

    SELECT kategoria, count(distinct oazon) from (select * from fiz_kategoria, dolgozo where dolgozo.fizetes between fiz_kategoria.also and fiz_kategoria.felso) group by kategoria having count(distinct oazon)=1;

Adjuk meg azon osztályok nevét és telephelyét, amelyeknek van 1-es fizetési kategóriájú dolgozója.

    π oazon, telephely σ kategoria =1 ∧ dolgozo.fizetes>= fiz_kategoria.also ∧ dolgozo.fizetes<=fiz_kategoria.felso (dolgozo⨝osztaly⨯fiz_kategoria)

    SELECT oazon, telephely from (select osztaly.telephely, dolgozo.oazon, fiz_kategoria.kategoria from fiz_kategoria, dolgozo, osztaly where kategoria=1 and dolgozo.oazon=osztaly.oazon and dolgozo.fizetes between fiz_kategoria.also and fiz_kategoria.felso) group by oazon, telephely;

Adjuk meg azon osztályok nevét és telephelyét, amelyeknek legalább 2 fő 1-es fiz. kategóriájú dolgozója van.

    π oazon, telephely σ db ≥ 2 γ oazon, telephely; count(*)->db σ kategoria = 1 ∧ dolgozo.fizetes>= fiz_kategoria.also ∧ dolgozo.fizetes<=fiz_kategoria.felso (dolgozo⨝osztaly⨯fiz_kategoria)

    SELECT oazon, telephely from (select osztaly.telephely, dolgozo.oazon, fiz_kategoria.kategoria from fiz_kategoria, dolgozo, osztaly where kategoria=1 and dolgozo.oazon=osztaly.oazon and dolgozo.fizetes between fiz_kategoria.also and fiz_kategoria.felso) group by oazon, telephely having count(*)>=2;

Készítsünk listát a páros és páratlan azonosítójú (dkod) dolgozók számáról.

    SELECT DECODE(MOD(dkod,2),0,'ptlan', 'paros') AS Paritas, COUNT(*) AS Letszam FROM dolgozo GROUP BY MOD(dkod,2);

 
Listázzuk ki munkakörönként a dolgozók számát, átlagfizetését (kerekítve) numerikusan és grafikusan is. 200-anként jelenítsünk meg egy #-ot

    SELECT foglalkozas, COUNT(*) AS darab, ROUND(AVG(fizetes)) AS atlagfiz, RPAD(' ',ROUND(AVG(fizetes))/200+1, '#') grafika FROM dolgozo GROUP BY foglalkozas ORDER BY ROUND(AVG(fizetes));

 
## HAJO

Kialaktáshoz:

create table hajok as select * from nikovits.hajok;
create table csatak as select * from nikovits.csatak;
create table kimenetelek as select * from nikovits.kimenetelek;

Adjuk meg azokat a hajóosztályokat a gyártó országok nevével együtt, amelyeknek az ágyúi legalább 16-os kaliberűek.

    select osztaly,orszag from hajoosztalyok where kaliber >=16;

Melyek azok a hajók, amelyeket 1921 előtt avattak fel?

    select * from hajok where felavatva < '1921';

Adjuk meg a Denmark Strait-csatában elsüllyedt hajók nevét.

    select * from csatak, kimenetelek where csatak.csatanev=kimenetelek.csatanev and kimenetelek.eredmeny='elsullyedt' and kimenetelek.csatanev='Denmark Strait';

Az 1921-es washingtoni egyezmény betiltotta a 35000 tonnánál súlyosabb hajókat. Adjuk meg azokat a hajókat, amelyek megszegték az egyezményt. (1921 után avatták fel őket)

    select * from hajok, hajoosztalyok where hajok.osztaly=hajoosztalyok.osztaly and felavatva > '1921' and hajoosztalyok.vizkiszoritas>35000;

Adjuk meg a Guadalcanal csatában részt vett hajók nevét, vízkiszorítását és ágyúi­nak a számát.

    select * from hajok, csatak, kimenetelek, hajoosztalyok where hajoosztalyok.osztaly=hajok.osztaly and kimenetelek.hajonev=hajok.hajonev and csatak.csatanev=kimenetelek.csatanev and csatak.csatanev='Guadalcanal';

     --select * from (hajok natural join csatak natural join kimenetelek natural join hajoosztalyok) where csatanev='Guadalcanal';

Adjuk meg az adatbázisban szereplő összes hadihajó nevét. (Ne feledjük, hogy a Hajók relációban nem feltétlenül szerepel az összes hajó!)

    select hajok.hajonev from hajok where osztaly in (select osztaly from hajoosztalyok where tipus='bc') union (select hajonev from kimenetelek);

