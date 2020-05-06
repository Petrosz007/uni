# Gyak 5
## Szeret
Kik szeretnek minden gyümölcsöt

    π nev szeret - (π nev ((π nev (szeret) ⨯ π gyumolcs (szeret)) - szeret))

Kik azok, akik legalább azokat a gyümölcsöket szeretik, mint Micimackó

    π nev szeret - (π nev ((π nev (szeret) ⨯ π gyumolcs σ nev='Micimacko' (szeret)) - szeret))

    (select nev from szeret) MINUS(
    select nev from ((select * from (select nev from szeret) gy, (select gyumolcs from szeret where nev='Micimackó') n)
     MINUS (select * from szeret)));

Kik azok, akik legfeljebb azokat a gyümölcsöket szeretik, mint Micimackó

    π nev szeret - (π nev (szeret - (π nev (szeret) ⨯ π gyumolcs σ nev='Micimacko' (szeret))))

    (select nev from szeret) MINUS(
    select nev from ((select * from szeret)
    MINUS (select * from (select nev from szeret) gy, (select gyumolcs from szeret where nev='Micimackó') n)));

Kik azok, akik pontosan azokat a gyümölcsöket szeretik, mint Micimackó

    előző kettő metszete

    π nev szeret - (π nev ((π nev (szeret) ⨯ π gyumolcs σ nev='Micimacko' (szeret)) - szeret))
    ∩
    π nev szeret - (π nev (szeret - (π nev (szeret) ⨯ π gyumolcs σ nev='Micimacko' (szeret))))

## Függvények

TO_DATE('1994-11-30', 'YYYY-MM-DD') -> DATE
SUBSTR("valami',2,1) -> a
SUBSTR('valami',-2,1) -> m
INSTR('alma','a', 1, 2) -> 2. a betű pozíciója (ha > 0 van ilyen)
ROUND(SQRT(4.4),2) -> 2.09
TO_CHAR(DATE, 'month') -> hónapnév
LENGTH('alma') -> 4
RPAD(' ', 4, '#') -> ' ###'
UPPER('alma') -> ALMA
LOWER('ALMA') -> alma
NVL(NULL, 'valami') -> valami (első nem null eredményt adja vissza)
WHERE mezo LIKE '_I%' -> A mező 2. betűje I, és a hossza >=2
FLOOR(1.91) -> 1
CEIL(1.1) -> 2
SYSDATE -> Aktualis datum

 
Kik azok a dolgozók, akik 1982.01.01 után léptek be a céghez?

    select * from dolgozo where belepes > TO_DATE('1982.01.01', 'YYYY-MM-DD');

Adjuk meg azon dolgozókat, akik nevének második betűje A.

    SELECT dnev FROM dolgozo WHERE SUBSTR(dnev,2,1)='A';

Adjuk meg azon dolgozókat, akik nevében van legalább két L betű.

    SELECT dnev, INSTR(dnev,'L', 1, 2) FROM dolgozo WHERE INSTR(dnev,'L', 1, 2)>0;
    --SELECT dnev FROM dolgozo WHERE dnev like '%L%L%';

Adjuk meg a dolgozók nevének utolsó három betűjét.

    SELECT substr(dnev, -3, 3) from dolgozo;

Adjuk meg a dolgozók fizetéseinek négyzetgyökét két tizedesre, és ennek egészrészét.

    SELECT fizetes, round(sqrt(fizetes),2), round(sqrt(fizetes),0) from dolgozo;

Adjuk meg, hogy hány napja dolgozik a cégnél ADAMS és milyen hónapban lépett be.

    SELECT TRUNC(SYSDATE - belepes) napok, TO_CHAR(belepes, 'month') honap FROM dolgozo WHERE dnev='ADAMS';

Adjuk meg azokat a (név, főnök) párokat, ahol a két ember neve ugyanannyi betűből áll.

    select * from dolgozo d1, dolgozo d2 where d1.fonoke=d2.dkod and length(d1.dnev)=length(d2.dnev);

Listázzuk ki a dolgozók nevét és fizetését, valamint jelenítsük meg a fizetést grafikusan úgy, hogy a fizetést 1000 Ft-ra kerekítve, minden 1000 Ft-ot egy # jel jelöl.

    SELECT dnev, fizetes, rpad(' ', round((fizetes/1000),0)+1, '#') as grafika FROM dolgozo ORDER BY fizetes desc;

Listázzuk ki azoknak a dolgozóknak a nevét, fizetését, jutalékát, és a jutalék/fizetés arányát, akiknek a foglalkozása eladó (salesman). Az arányt két tizedesen jelenítsük meg.

    SELECT dnev, fizetes, jutalek, TO_CHAR(NVL(jutalek, 0)/fizetes, '990.99') AS arany FROM dolgozo WHERE UPPER(foglalkozas) IN ('SALESMAN') ORDER BY arany DESC;

## Összekapcsolások

    select * from dolgozo natural join osztaly;

    select * from dolgozo join osztaly on dolgozo.oazon=osztaly.oazon;

    select * from dolgozo, osztaly where dolgozo.oazon=osztaly.oazon;

