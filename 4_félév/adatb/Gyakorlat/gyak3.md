
# 3. gyakorlat megoldva
## SZERET
Kik szeretnek legalább kétféle gyümölcsöt?

    π sz1.nev σ sz1.gyumolcs < sz2.gyumolcs (ρ sz1 (szeret) ⨯ ρ sz2 (szeret))

    select distinct s1.nev from szeret s1, szeret s2 where s1.nev=s2.nev and s1.gyumolcs<s2.gyumolcs;

Kik szeretnek legalább háromféle gyümölcsöt?

    π sz1.nev σ sz1.gyumolcs < sz2.gyumolcs ∧ sz2.gyumolcs < sz3.gyumolcs (ρ sz1 (szeret) ⨯ ρ sz2 (szeret) ⨯ ρ sz3 (szeret))

    select distinct s1.nev from szeret s1, szeret s2, szeret s3 where s1.nev=s2.nev and s1.nev=s3.nev and s1.gyumolcs<s2.gyumolcs and s2.gyumolcs<s3.gyumolcs;

Kik szeretnek legfeljebb kétféle gyümölcsöt?

    π nev (szeret) - (π sz1.nev σ sz1.gyumolcs < sz2.gyumolcs ∧ sz2.gyumolcs < sz3.gyumolcs (ρ sz1 (szeret) ⨯ ρ sz2 (szeret) ⨯ ρ sz3 (szeret)))

    select nev from szeret minus select distinct s1.nev from szeret s1, szeret s2, szeret s3 where s1.nev=s2.nev and s1.nev=s3.nev and s1.gyumolcs<s2.gyumolcs and s1.gyumolcs<s3.gyumolcs and s2.gyumolcs < s3.gyumolcs;

Kik szeretnek pontosan kétféle gyümölcsöt?

    π sz1.nev σ sz1.gyumolcs < sz2.gyumolcs (ρ sz1 (szeret) ⨯ ρ sz2 (szeret))
    -
    π sz1.nev σ sz1.gyumolcs < sz2.gyumolcs ∧ sz2.gyumolcs < sz3.gyumolcs (ρ sz1 (szeret) ⨯ ρ sz2 (szeret) ⨯ ρ sz3 (szeret))

    select distinct s1.nev from szeret s1, szeret s2 where s1.nev=s2.nev and s1.gyumolcs<s2.gyumolcs minus select distinct s1.nev from szeret s1, szeret s2, szeret s3 where s1.nev=s2.nev and s1.nev=s3.nev and s1.gyumolcs<s2.gyumolcs and s1.gyumolcs<s3.gyumolcs and s2.gyumolcs < s3.gyumolcs;

## DOLGOZO

Segtség

    select * from dolgozo;
    select * from osztaly;
    --select * from dolgozo natural join osztaly;
    --select * from dolgozo inner join osztaly on dolgozo.oazon=osztaly.oazon;
    select * from dolgozo, osztaly where dolgozo.oazon=osztaly.oazon;

Kik azok a dolgozók, akiknek a főnöke KING? (nem leolvasva)

    π d1.dnev σ d1.fonoke=d2.dkod and d2.dnev='KING' (ρ d1 dolgozo ⨯ ρ d2 dolgozo)

    select d1.dnev from dolgozo d1, dolgozo d2 where d1.fonoke=d2.dkod and d2.dnev='KING';

Adjuk meg azoknak a főnököknek a nevét, akiknek a foglalkozása nem MANAGER

    π d1.dnev σ d1.foglalkozas!='MANAGER' and d1.dkod=d2.fonoke (ρ d1 dolgozo ⨯ ρ d2 dolgozo)

    select distinct d1.dnev from dolgozo d1, dolgozo d2 where d1.foglalkozas!='MANAGER' and d1.dkod=d2.fonoke;

Adjuk meg azokat a dolgozókat, akik többet keresnek a főnöküknél.

    π d1.dnev σ d1.fonoke=d2.dkod and d1.fizetes>d2.fizetes (ρ d1 dolgozo ⨯ ρ d2 dolgozo)

    select d1.dnev from dolgozo d1, dolgozo d2 where d1.fonoke=d2.dkod and d1.fizetes>d2.fizetes;

Kik azok a dolgozók, akik főnökének a főnöke KING?

    π d1.dnev σ d1.fonoke=d2.dkod and d2.fonoke = d3.dkod and d3.dnev='KING' (ρ d1 dolgozo ⨯ ρ d2 dolgozo ⨯ ρ d3 dolgozo)

    select d1.dnev from dolgozo d1, dolgozo d2, dolgozo d3 where d1.fonoke=d2.dkod and d2.fonoke = d3.dkod and d3.dnev='KING';

Adjuk meg a maximális fizetésű dolgozó(k) nevét.

    π dnev (dolgozo)
    -
    π d1.dnev σd1.fizetes < d2.fizetes (ρ d1 dolgozo ⨯ ρ d2 dolgozo)

    select dnev from dolgozo
    MINUS

    select d1.dnev from dolgozo d1, dolgozo d2 where d1.fizetes < d2.fizetes

Csoportképzés

Adjuk meg az átlagfizetést és azt, hogy hányan dolgoznak az egyes osztályokon

    π avg, db γ oazon; avg(fizetes)->avg, count(*)->db (dolgozo)

    SELECT avg(fizetes),count(*) FROM dolgozo GROUP BY oazon

Adjuk meg az átlagfizetést és azt, hogy hányan dolgoznak az egyes osztályokon, de csak azokra ahol legalább ketten dolgoznak

    π avg, db σ db>=2 γ oazon; avg(fizetes)->avg, count(*)->db (dolgozo)

    SELECT avg(fizetes),count(*) FROM dolgozo GROUP BY oazon HAVING count(*)>=2

Adjuk meg az átlagfizetést és azt, hogy hányan dolgoznak az egyes osztályokon, de csak azokra ahol legalább ketten dolgoznak. Csak azoknak a dolgozóknak számoljuk bele a fizetését akiknek nagyobb mint 1000

    π avg, db σ db>=2 γ oazon; avg(fizetes)->avg, count(*)->db σ fizetes > 1000 (dolgozo)

    SELECT avg(fizetes),count(*) FROM dolgozo WHERE fizetes > 1000 GROUP BY oazon HAVING count(*)>=2

