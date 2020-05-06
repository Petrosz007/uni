# 7. gyakorlat megoldva
## TÁBLA LÉTREHOZÁS

CREATE TABLE proba123(
u_id INT NOT NULL,
--oszlopnév típus megszorítás
nev VARCHAR(10) DEFAULT 'Joe' NOT NULL
--...
);

IDEGEN KULCS

CREATE TABLE supplier(
  supplier_id numeric(10) not null,
  supplier_name varchar2(50) not null,
  contact_name varchar2(50),
  CONSTRAINT supplier_pk PRIMARY KEY (supplier_id)
);


CREATE TABLE products(
  product_id numeric(10) not null,
  supplier_id numeric(10) not null,
  CONSTRAINT fk_supplier
  FOREIGN KEY (supplier_id)
  REFERENCES supplier(supplier_id)
);

NÉZETTÁBLA LÉTREHOZÁS

    CREATE VIEW viewproba123 AS

    SELECT dnev, oazon FROM dolgozo WHERE oazon=20;

 
TÁBLA OSZLOP TÖRLÉS/MÓDOSÍTÁS/HOZZÁADÁS

    ALTER TABLE proba123 DROP COLUMN nev;

    ALTER TABLE proba123 ADD / MODIFY nev VARCHAR(30);

TÁBLA ÖSSZES SORÁNAK TÖRLÉSE

    TRUNCATE TABLE proba123;

TÁBLA TELJES TÖRLÉSE

    DROP TABLE proba123;

SOR BESZÚRÁS

    INSERT INTO proba123 VALUES (2, 'Feri')

    INSERT INTO proba123 (nev, uid) VALUES ('Jozsi', 3)

SOROK MÓDOSÍTÁSA

    UPDATE proba123 SET nev='Peti', id=1 WHERE nev='Jozsi'

SOROK TÖRLÉSE

    DELETE FROM proba123 WHERE nev='Peti'

## Feladatok módosításra, törlésre, beszúrásra
## CREATE
Készítsünk két táblát az egyikben legyenek sportcsapatok csapat_id, név. A másikban a játékosok, id, név, mezszám, csapat_id. A csapat azonosító legyen idegen kulcs.

CREATE TABLE sportcs(
  csapat_id numeric(10) not null,
  nev varchar(50) not null,
  CONSTRAINT sportcs_pk PRIMARY KEY (csapat_id)
);


CREATE TABLE jatekosok(
  j_id numeric(10) not null PRIMARY KEY,
  nev varchar(50) not null,
  mezszam numeric(2) not null,
  csapat_id numeric(10) not null,
  CONSTRAINT jatekosok_fk FOREIGN KEY (csapat_id) REFERENCES sportcs(csapat_id)
);

A módosítást egy másodpéldányon végezzük, hogy a tábla eredeti tartalma megmaradjon

    CREATE TABLE dolg2 AS SELECT * FROM nikovits.dolgozo;
    CREATE TABLE oszt2 AS SELECT * FROM nikovits.osztaly;

 
## DELETE
Töröljük azokat a dolgozókat, akiknek jutaléka NULL.

    delete from dolg2 where jutalek is null;

Töröljük azokat a dolgozókat, akiknek a belépési dátuma 1982 előtti.

    delete from dolg2 where belepes < to_date('1982-01-01','YYYY-MM-DD');

Töröljük azokat a dolgozókat, akik osztályának telephelye DALLAS.

    delete from dolg2 where dkod in (select dkod from dolg2 natural join osztaly where telephely='DALLAS');

Töröljük azokat a dolgozókat, akiknek a fizetése kisebb, mint az átlagfizetés.

    delete from dolg2 where fizetes < (select avg(fizetes) from dolgozo);

Töröljük a jelenleg legjobban kereső dolgozót.

    delete from dolg2 where fizetes = (select max(fizetes) from dolgozo);

Töröljük ki azon osztályokat, amelyeknek 2 olyan dolgozója van, aki a 2-es fizetési kategóriába esik.

    delete from oszt2 where oazon in (select oazon from (dolg2 d natural join osztaly o) inner join fiz_kategoria f on d.fizetes between f.also and f.felso where f.kategoria=2 group by oazon having count(*)=2);

## INSERT
Vigyünk fel egy Kovacs nevű új dolgozót a 10-es osztályra a következő értékekkel: dkod=1, dnev=Kovacs, oazon=10, belépés=aktuális dátum, fizetés=a 10-es osztály átlagfizetése. A többi oszop legyen NULL.

    insert into dolg2 (dkod, dnev,oazon, belepes, fizetes) VALUES (1, 'KOVACS', 10, SYSDATE, (select avg(fizetes) from dolg2));

## UPDATE
Növeljük meg a 20-as osztályon a dolgozók fizetését 20%-kal.

    update dolg2 set fizetes = fizetes*1.25 where oazon = 20;

Növeljük meg azok fizetését 500-zal, akik jutaléka NULL vagy a fizetésük kisebb az átlagnál.

    update dolg2 set fizetes = fizetes+500 where jutalek is null or fizetes < (select avg(fizetes) from dolg2);

Növeljük meg mindenkinek a jutalékát a jelenlegi maximális jutalékkal. (NULL tekintsük 0-nak)

    update dolg2 set jutalek = NVL(jutalek,0)+(select max(jutalek) from dolg2);

Módosítsuk Loser-re a legrosszabbul kereső dolgozó nevét.

    update dolg2 set dnev = 'LOSER' where fizetes = (select min(fizetes) from dolg2); 

Növeljük meg azoknak a dolgozóknak a jutalékát 3000-rel, akiknek legalább 2 közvetlen beosztottjuk van. Az ismeretlen (NULL) jutalékot vegyük úgy, mintha 0 lenne.

    update dolg2 set jutalek=nvl(jutalek,0)+3000 where dkod in (select d1.fonoke from dolg2 d1 inner join dolg2 d2 on d1.fonoke=d2.dkod group by d1.fonoke having count(*)>=2);

Növeljük meg azoknak a dolgozóknak a fizetését, akiknek van beosztottja, a minimális fizetéssel

    update dolg2 set fizetes=fizetes+(select min(fizetes) from dolg2) where dkod in (select d1.fonoke from dolg2 d1 inner join dolg2 d2 on d1.fonoke=d2.dkod group by d1.fonoke having count(*)>=1);

