# 9. gyakorlat megoldva
## PL/SQL programozás

Függvény vagy eljárás lerásának módja:

    CREATE OR REPLACE PROCEDURE procedure_neve IS -- PROCEDURE-NAK NINCS RETURNJE
        valtozonev típus:= 'érték';               -- VÁLTOZÓ LÉTREHOZÁS A DEKLARÁCIÓS RÉSZBEN
    BEGIN                                         -- PROGRAMBLOKK KEZDETE
        dbms_output.put_line();                   -- KIIRATÁS ez felel meg a hagyományos print-eknek
    END;                                          -- FÜGGVÉNY/ELJÁRÁS VÉGE

 

    CREATE OR REPLACE FUNCTION függvény_neve(n integer) RETURN number IS -- FÜGGVÉNYNEK VAN RETURNJE

 
PL/SQL Hello World procedure

    create or replace procedure print_hello is
        message varchar2(40):= 'Hello World!';
    BEGIN
        dbms_output.put_line(message);
    END print_hello;

Futtatás:

    set serveroutput on;  -- BEKAPCSOLJUK HOGY MEGJELENJENEK A DEBUG ÜZENETEK
    call print_hello();   -- ELÁRÁS MEGHIVÁSA A CALL-LAL TÖRTÉNIK

Prím-e Függvény

    CREATE OR REPLACE FUNCTION prim(n integer) RETURN number IS
        i number := 0;
    begin
        if n < 2 then
            return 0;
        end if;
        for i in 2..n/2 loop
            if MOD(n,i)=0 then
            return 0;
            end if;
        end loop;
        return 1;
    end;

Futtatás:

    SELECT prim(2),prim(3),prim(5),prim(6),prim(7) from dual;

Fibonacci sorozat

    create or replace function fib(n integer) return number IS
    begin
        if n=1 then
            return 0;
        elsif n=2 then
            return 1;
        else
            return n+fib(n-1);
        end if;
    end;

    select fib(10) from dual;

Legnagyobb közös osztó

    create or replace FUNCTION lnko(p1 integer, p2 integer) RETURN number IS
        i int;
    begin
        for i in reverse 1..p1 loop
            if MOD(p1,i) = 0 and MOD(p2,1)=0 then
                return i;
            end if;
        end loop;
    end;

    select lnko(80,100) from dual;

Faktoriális

    CREATE OR REPLACE FUNCTION faktor(n integer) RETURN integer IS
        i number;
        res integer := 1;
    begin
        for i in 1..n loop
            res := res*i;
        end loop;
        return res;
    end;

    select faktor(5) from dual;

