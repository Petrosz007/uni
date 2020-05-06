
# 10. gyakorlat megoldva
Hanyszor fordul elő egy szövegben egy másik string

    create or replace FUNCTION hanyszor(p1 VARCHAR2, p2 VARCHAR2) RETURN integer IS
        i int;
        res int := 0;
    begin
        for i in 1..length(p1)-length(p2)+1 loop
            if substr(p1, i, length(p2)) = p2 then
                res:=res+1;
            end if;
        end loop;
        return res;
    end;

    SELECT hanyszor ('ab c ab ab de ab fg a', 'ab') FROM dual;

Szövegből összeg

    create or replace FUNCTION osszeg(p_char VARCHAR2) RETURN number IS
        res int := 0;
        i int;
        subs varchar(10);
    begin
        for i in 1..length(p_char) loop
            if substr(p_char,i,1) != '+' then
                subs := subs || substr(p_char,i,1);
            else
                res := res + TO_NUMBER(subs);
                subs := '';
            end if;
        end loop;
        res := res + TO_NUMBER(subs);
        return res;
    end;


    select osszeg('1+2+3+40+5+6') from dual;

ACCEPT

    ACCEPT valtozonev CHAR PROMPT 'Add meg a valtozo erteket';
Későbbiekben: '&valtozonev'

Pl.: 

    ACCEPT dolg_nev CHAR PROMPT 'Add meg a dolgozo nevet';
    SELECT * FROM dolgozo where dnev = '&dolg_nev';

Kurzorok

    CURSOR kurzornev IS SELECT valami FROM valahonnan [WHERE feltetel ORDER BY oszlop];
    rec kurzornev%ROWTYPE;
    OPEN kurzornev;
    LOOP
    FETCH kurzornev INTO rec;
    EXIT WHEN kurzornev%NOTFOUND;
    dbms_output.put_line(rec.oszlopnev);
    END LOOP;
    CLOSE kurzornev;

VAGY

    CURSOR kurzornev IS SELECT valami FROM valahonnan [WHERE feltetel ORDER BY oszlop];
    FOR rec IN kurzornev LOOP
    dbms_output.put_line(rec.oszlopnev);
    END LOOP;

Írjunk meg egy függvényt, amelyik visszaadja egy adott fizetési kategóriába tartozó dolgozók átlagfizetését.

    create or replace FUNCTION kat_atlag(n integer) RETURN number IS
        CURSOR curs1 IS SELECT oazon, dnev, fizetes FROM dolgozo join fiz_kategoria on dolgozo.fizetes>fiz_kategoria.also and dolgozo.fizetes<fiz_kategoria.felso WHERE fiz_kategoria.kategoria = n;
        rec curs1%ROWTYPE;
        atl float;
        db int; 
    BEGIN
        atl:=0.0;
        db:=0;
        OPEN curs1;
        LOOP
            FETCH curs1 INTO rec;
            EXIT WHEN curs1%NOTFOUND;
            db:=db+1;
            atl:=atl+rec.fizetes;
            dbms_output.put_line(to_char(rec.oazon)||' - '||rec.dnev);
        END LOOP;
        CLOSE curs1;
        return atl/db;
    END;

Írjunk meg egy plsql procedúrát, amelyik veszi a dolgozókat ábácé szerinti sorrendben, és minden páratlan sorszámú dolgozó nevét és fizetését kiírja

    CREATE OR REPLACE PROCEDURE proc9 IS
        CURSOR curs1 IS SELECT dnev, fizetes FROM dolgozo order by dnev;
        rec curs1%ROWTYPE;
    BEGIN
        OPEN curs1;
    LOOP
        FETCH curs1 INTO rec;
        EXIT WHEN curs1%NOTFOUND;
        if curs1%ROWCOUNT mod 2 = 1 then
            dbms_output.put_line(to_char(rec.dnev)||' - '||rec.fizetes);
        end if;
        END LOOP;
        CLOSE curs1;
    END;

Írjunk meg egy plsql programot (név nélküli blokkot), amelyik kiírja azon dolgozók nevét és belépési dátumát, akik a felhasználó által megadott osztályon dolgoznak. A felhasználó az osztály nevének elsõ betûjét adja meg (ACCEPT-tel kérjük be). A program írja ki az osztály nevét is. Ha nincs megfelelõ osztály, akkor azt írja ki.

    ACCEPT onev_c VARCHAR2(1) PROMPT 'Add meg az osztály nevének az első betűjét!';
    set serveroutput on;

    ACCEPT onev_c CHAR PROMPT 'Add meg az osztály nevének az első betűjét!';
    set serveroutput on;
    DECLARE
    CURSOR curs1 IS SELECT * FROM dolgozo NATURAL JOIN osztaly
                            --WHERE SUBSTR(onev, 1, 1) = '&onev_c';
                            WHERE onev LIKE '&onev_c%';
    db INT := 0;
    BEGIN
    FOR rec IN curs1 LOOP
        dbms_output.put_line(rec.dnev || ' - ' || rec.belepes);
        db := db + 1;
    END LOOP;
    IF db = 0 THEN
        dbms_output.put_line('Nem kezdődik &onev_c-vel osztalynev');
    END IF;
    END;

