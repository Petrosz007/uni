
# 11. gyakorlat megoldva

## MÓDOSÍTÁS
Hozza létre a dolgozo2 táblát az dolgozo táblából, és bővítse azt egy sorszám oszloppal. Ezt töltse fel 1től kiindulva egyesével növekvő értékkel minden dolgozó esetén a dolgozók nevének ábécé sorrendje szerint

    create table dolgozo2 as select 1 sorszam, dolgozo.* from dolgozo;

    declare
    cursor curs1 is select * from dolgozo2 for update;
    rec curs1%ROWTYPE;
    db int := 0;
    begin
    for rec in curs1 loop
        db := db + 1;
        update dolgozo2 set sorszam = db where current of curs1;
    end loop;
    end;

Növeljük meg a dolgozo 2 táblában a prímszám sorszámú dolgozók fizeteset 50%-kal.

    declare
    cursor curs1 is select * from dolgozo2 where prim(sorszam) = 1 for update;
    rec curs1%ROWTYPE;
    begin
    for rec in curs1 loop
        update dolgozo2 set fizetes = fizetes * 1.5 where current of curs1;
    end loop;
    end;

Töröljük a dolgozók közül a 3-mas fizetési kategóriájú fizetésűeket

    set serveroutput on;

    declare
    cursor curs1 is select * from dolgozo2 join fiz_kategoria
                            on fizetes between also and felso
                                            where kategoria = 3
                                        for update of sorszam;
    begin
    for rec in curs1 loop
        delete from dolgozo2 where current of curs1;
    end loop;
    end;


    select * from dolgozo2 join fiz_kategoria on fizetes between also and felso;


Írjunk meg egy procedúrát, amelyik megnöveli azoknak a dolgozóknak a fizetését 1-el, akiknek a fizetési kategóriája ugyanaz, mint a procedúra paramétere. A procedúra a módosítás után írja ki a módosított (új) fizetések átlagát két tizedesjegyre kerekítve.

    CREATE OR REPLACE PROCEDURE kat_novel(p_kat NUMBER) IS
    cursor curs1 is select * from dolgozo2 join fiz_kategoria
                            on fizetes between also and felso
                                        where kategoria = p_kat
                                        for update of fizetes;
    db int := 0;
    osszeg int := 0;
    begin
    for rec in curs1 loop
        update dolgozo2 set fizetes = fizetes + 1
                        where current of curs1;
        db := db + 1;
        osszeg := osszeg + rec.fizetes + 1;
    end loop;
    dbms_output.put_line(round(osszeg/db, 2));
    end kat_novel;

    set serveroutput on
    call kat_novel(2);


Írjunk meg egy procedúrát, amelyik módosítja a paraméterében megadott osztályon a fizetéseket, és kiírja a dolgozó nevét és új fizetését. A módosítás mindenki fizetéséhez adjon hozzá n*10 ezret, ahol n a dolgozó nevében levő magánhangzók száma (a, e, i, o, u).

    create or replace function maganhangzok(szo VARCHAR2) return int is
    db int := 0;
    begin
    for i in 1..length(szo) loop
        if LOWER(SUBSTR(szo,i,1)) in ('a','e','i','o','u') then
        db := db + 1;
        end if;
    end loop;
    return db;
    end maganhangzok;


    CREATE OR REPLACE PROCEDURE fiz_mod(p_oazon INTEGER) IS
    cursor curs1 is select * from dolgozo2 where oazon = p_oazon for update;
    begin
    for rec in curs1 loop
        update dolgozo2 set fizetes = (fizetes + 10000*maganhangzok(dnev)) where current of curs1;
    end loop;
    end fiz_mod;

    set serveroutput on
    call fiz_mod(10);

