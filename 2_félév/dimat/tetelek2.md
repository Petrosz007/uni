## Ismétlés nélküli permutációinak számáról szóló tétel

**Tétel:**
Egy $n$ elemű $A$ halmaz ismétlés nélküli permutációinak száma:
		$P_n = n! = \overset{n}{\underset{k=1}{\prod}} k$

**Bizonyítás:**
Teljes indukcióval.
n = 0,1 : 1 féle, 0! = 1! = 1 $\checkmark$
Ha $A$ egy $(n+1)$ elemű halmaz, $f : \{ 1,2,...,n+1\} \rarr A$ bijektív
Ha $f(1) = x \in A$, akkor $f(2), f(3), ..., f(n+1)$ az az $A \text{\\} \{x\}$ halmaz egy permutációját adja.	

$A \text{\\} \{x\}$ elemszáma $n$, tehát $n!$ módon fejezhetem be azt a felsorolást, ami $x$-el kezdődött.

De $x$ választási lehetősége $(n+1)$ féle $\Rarr$ Az $A$ permutációinak száma: 
				$(n+1)\cdot n! = (n+1)!$

**Bizonyítás2:**
Az $n$ elemből az első helyre $n$ féleképpen választhatunk, a másodikra $n-1$ féleképpen, ... Így az összes lehetőségek száma $n \cdot (n-1) \cdot ... \cdot 1 = n!$

## Ismétléses permutációk számáról szóló tétel

**Tétel:**
Ha egy $n$ hosszú sorozatban $r$ db különböző elem van és ezek $i_1, i_2,..., i_r$-szer ismétlődnek $(i_1+i_2+...+i_r = n)$, akkor a képezhető ismétléses permutációk száma:
		$^iP_{n}^{i_1, i_2,...i_r} = \dfrac{n!}{i_1!\cdot i_2! \cdot ... \cdot i_r!}$

**Bizonyítás:**
Tfh.: az $i_1$ darab egyforma elem mégis különböző.
Ekkor ezeknek az egymás közti sorrendje $i_1!$ féle lehetne, és az ismétléses permutációk száma $i_1!$ -szorosára nőne.
Ugyanezt eljátszva a többi elemmel a permutációk száma $i_1!\cdot i_2! \cdot ... \cdot i_r!$-szorosára nőne, és akkor kapnánk meg az $n!$ lehetőséget.

**Bizonyítás2:**
Ha minden elem között különbséget teszünk, akkor $n!$ lehetséges sorrend van.
Ha azonban az azonos típusú elemek köött nem teszünk különbséget, akkor ebben a számításban többször számoltuk az egyes sorrendeket.

Mivel minden $1 \le k \le r$-re adott $i_k$ db pozíción $i_k!$ különböző sorrendben helyezhetjük el a $k$-adik típusú elemeket, ezért minden sorrendet $i_1!\cdot i_2! \cdot ... \cdot i_r!$-szor számoltunk. Így a különböző sorrendek száma: $ \dfrac{n!}{i_1!\cdot i_2! \cdot ... \cdot i_r!}$

## Ismétlés nélküli variációk számáról szóló tétel

**Tétel:**
Egy $n$ elemű halmaz $k$-ad osztályú ismétlés nélküli variációinak száma:
		$V_n^k = \dfrac{n!}{(n-k)!}$

**Bizonyítás:**
Jelölje a keresett mennyiséget $V_n^k$
Minden felsorolása $A$-nak ($n!$ összesen) felbontható az első $k$ és a következő $(n-k)$ elemre.

$\underset{k\ db}{\underbrace{a_1\ a_2\ ...\ a_k}}$          $\underset{n-k\ db}{\underbrace{a_{k+1} ... a_n}}$
Itt az első $k$ elem $V_n^k$-féle lehet, az utolsó $(n-k)$ elem $(n-k)!$-féle lehet.

Ekkor: $V_n^k \cdot (n-k)! = n! \Leftrightarrow V_n^k = \dfrac{n!}{(n-k)!}$

## Ismétléses variációk számáról szóló tétel

**Tétel:**
Egy $n$ elemű halmaz $k$-ad osztályú ismétléses veriációinak száma:
		$^iV_n^k = n^k$

**Bizonyítás2:**
A sorozat első elemét $n$ féleképpen választhatjuk, a második elemét $n$-féleképpen választhatjuk ..., a sorozat $k$-adik elemét $n$-féleképpen választhatjuk. $\Rarr n\cdot n \cdot ... \cdot n = n^k$

## Ismétlés nélküli kombinációk számáról szóló tétel

**Tétel:**
Egy $n$ elemű halmaz $k$-ad osztályú ismétlés nélküli kombinációinak száma:
		$C_n^k = {{n}\choose{k}} = \dfrac{n!}{k!\cdot(n-k)!}$

**Bizonyítás:**
A keresett szám: $C_n^k$
A variációt megadhatjuk 2 lépésben:
	1, Melyik elemet választom $\rarr$ $C_n^k$
	2, Milyen sorrendben $\rarr$ $k!$

$V_n^k = C_n^k \cdot k! \Leftrightarrow C_n^k = \dfrac{n!}{k! \cdot (n-k)!}$

**Bizonyítás2:**
Először válasszuk a halmaz elemei közül $k$ darabot a sorrendet figyelembevéve. Ezt $\dfrac{n!}{(n-k)!}$-féleképpen tehetjük meg.
Ha a sorrendtól eltekintünk, akkor az előző számlálásnál minden $k$ elemű részhalmaz pontosan $k!$-szor szerepel. Ezzel leosztva kapjuk a $k$ elemű részhalmazok számát.

## Ismétléses kombinációk számáról szóló tétel

**Tétel:**
Egy $n$ elemű halmaz $k$-ad osztályú ismétléses kombinációinak száma:
		$^iC_n^k = {{n+k-1}\choose{k}}$

**Bizonyítás:**
Legyen $A = \{a_1, a_2,...,a_n\}$ Ekkor minden egyes lehetőségnek megfeleltetünk egy $0-1$ sorozatot:

​		$\underset{a_1\text{-ek száma}}{\underbrace{1,1,...,1}},0,\underset{a_2\text{-ek száma}}{\underbrace{1,1,...,1}},0,...,0,\underset{a_n\text{-ek száma}}{\underbrace{1,1,...,1}}$

Ekkor a sorozatban $k$ db 1-es van (választott elemek száma), $n-1$ darab 0 van (szeparátorok száma). Összesen $n-1 + k$ pozíció, ezekból k-t választunk. Ilyen sorozat ${{n+k-1}\choose{k}}$ db van.

## Binomiális tétel

**Tétel:**
Ha $x$ és $y$ számok (pl.: $\R$), akkor $ 2 \lt n \in \N$ esetén: $(x+y)^n = \underset{k=0}{\overset{n}{\sum}} {{n}\choose{k}}x^{n-k}y^k$

**Bizonyítás:**
$(x+y)^n = (x+y)\cdot(x+y)\cdot...\cdot(x+y)$
Ha elvégezzük a beszorzást, akkor $x^ny^{n-k}$ alakú tagokat kapunk, és ezen tagot annyiszor kapjuk meg, ahányszor $n$ tényezőből $k$ darab $x$-et választunk.

## Polinomiális tétel

**Tétel:**
$(x_1+x_2+...+x_r)^n$ kifejezésben olyan $x_1^{i_1} \cdot x_2^{i_2}\cdot ... \cdot x_r^{i_r}$ tagok vannak, melyekre $i_1 + i_2 + ... + i_r = n$ és ennek a tagnak az együtthatója:
		$\dfrac{n!}{i_1! \cdot i_2! \cdot ... \cdot i_r!}$

**Bizonyítás:**
$x_1^{i_1} \cdot x_2^{i_2}\cdot ... \cdot x_r^{i_r}$ hányféle változatban jelenik meg = hányféle permutációja van ismétlésesen = $\dfrac{n!}{i_1! \cdot i_2! \cdot ... \cdot i_r!}$

## Gráf csúcsainak fokszámösszegére vonatkozó tétel

**Tétel:**
Egy $G = (V, E, \varphi)$ véges gráfban $\underset{v \in V}{\sum} deg(v) = 2 \cdot| E |$

**Bizonyítás:**
Készítsünk egy táblázatot:

|       | $e_1$ | $e_2$ | ...  |
| ----- | ----- | ----- | ---- |
| $v_1$ | 0     | 1     | ...  |
| $v_2$ | 2     | 0     | ...  |
| ...   | ...   | ...   |      |

Sorok = $V$, Oszlopok = $E$, írjunk majdnem mindenhova 0-t, kivéve oda, ha a megfelelő csúcs az él egyik végpontja akkor 1-est, ha hurokél akkor 2-est írjunk oda.

A $v_j$ sorának összege: $deg(v_j)$
Az $e_k$ oszlopának összege: 2
A táblázat összege: $\underset{v \in V}{\sum} deg(v) = 2 \cdot| E |$

**Bizonyítás2:**
Élszám szerinti teljes indukció:
$|E| = 0$ esetén az egyenlet mindkét oldala 0 $\checkmark$
Tfh.: $|E| = n$ esetén igaz az állítás.
Ekkor kell, hogy ha adott egy gráf, melynek $n+1$ éle van, akkor annak egy élét elhagyva egy $n$ élű gráfot kapunk. Erre teljesül az állítás az indukciós feltevés miatt. Az elhagyott élt hozzátéve az egyenlőség mindkét oldala 2-vel nő.

## Állítás út létrehozásáról sétából gráf két csúcsa között

**Állítás:**
Ha egy $G$ gráfban $v_1$ és $v_2$ között van séta, akkor van közöttük út is.

**Bizonyítás:**
Ha van séta és van csúcs amit kétszer is meglátogatok, akkor a két csúcs közötti séta-részt hagyjuk el. Ezt ismételjük addig amég van olyan csúcs amit kétszer látogatunk meg. Végül így egy utat kapunk.

$v_1$ ---> $v_i$ ---> $v_i$ ---> $v_2$     $\Rarr$      $v_1$ ---> $v_i$ ---> $v_2$