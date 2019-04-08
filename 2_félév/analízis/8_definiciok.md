## Mit jelent az, hogy a $\sum a_n$ végtelen sor konvergens, és hogyan értelmezzük az összegét?

A $\sum a_n​$ sor konvergens, ha részletösszegeinek az $s_n = a_1 + ... + a_n\ \ (n \in \N)​$ sorozata konvergens. A $\lim(s_n)​$ számot nevezzük a sor összegének, jele: $\overset{+\infty}{\underset{n=0}{\sum}} a_n​$

## Milyen tételt ismer a $q \in \R$ esetén a $\underset{n=0}{\sum} q^n$ geometriai sor konvergenciájáról?

A $\underset{n=0}{\sum} q^n$ akkor és csak akkor konvergens, ha $|q| < 1$
Ekkor a sorösszeg: $\overset{+\infty}{\underset{n=0}{\sum}} q^n = \dfrac{1}{1 - q}$

## Mi a teleszkópikus sor és mi az összege?

A $\underset{n=1}{\sum} \dfrac{1}{n(n+1)}$ sor a teleszkópikus sor, sorösszege: 
$\overset{+\infty}{\underset{n=1}{\sum}} \dfrac{1}{n(n+1)}$ = 1

## Milyen állítást ismer a $\sum \dfrac{1}{n^\alpha}$ hiperharmonikus sor konvergenciájával kapcsolatban?

A sor $1 < \alpha \in \R$ esetén konvergens, ha $\R \ni \alpha \le 1$ akkor pedig divergens.

## Mondjon szükséges feltételt arra nézve, hogy a $\sum a_n$ végtelen sor konvergens legyen.

$\sum a_n$ konvergens $\Leftrightarrow \sum a_n​$ Cauchy 

## Fogalmazza meg a végtelen sorokra vonatkozó összehasonlító kritériumokat.

Legyen $\sum a_n, \sum b_n$ pozitív tagú sorok, melyekre $\exists N \in \N, \forall n \in \N, n \ge N: $
$0 \le a_n \le b_n$

1. Ekkor ha $\sum b_n$ konvergens, akkor $\sum a_n$ is konvergens
2. Ekkor ha $\sum a_n$ divergens, akkor $\sum b_n$ is divergens.

## Fogalmazza meg a végtelen sorokra vonatkozó Cauchy-féle gyökkritériumot.

Tekintsük a $\sum a_n$ sort és Tfh: $\root{n}\of{|a_n|}$ sorozat is konvergens és $A := \lim\root{n}\of{|a_n|} \in \overset{\_\_}{\R}$
Ekkor:

1. Ha $0 \le A \lt 1$, akkor $\sum a_n$ abszolút konvergens, tehát konvergens is.
2. Ha  $A \gt 1$, akkor $\sum a_n$ divergens.
3. Ha $A = 1$, akkor lehet konvergens és divergens is.

## Fogalmazza meg a végtelen sorokra vonatkozó D'Alembert-féle hányadoskritériumot.

Tekintsük a $\sum a_n$ sort, ahol $a_n \ne 0\ \ (n \in \N)$
Tfh: $\left(\dfrac{|a_{n+1}|}{|a_n|}\right)$ sorozat konvergens és $A := \lim\left(\dfrac{|a_{n+1}|}{|a_n|}\right)$
Ekkor, ha:

1. $0 \le A < 1$, akkor $\sum a_n$ sor abszolút konvergens, tehát konvergens is.
2. $A > 1$, akkor a $\sum a_n$ sor divergens.
3. $A = 1$ esetén lehet konvergens és divergens is.

## Mik a Leibniz-típusú sorok és milyen konvergenciatételt ismer ezekkel kapcsolatban?

Legyen $(a_n) : \N \rarr[0, \infty), $ monoton fogyó sorozat (azaz $0 \le a_{n+1} \le a_n\ \ (n \in \N)$)
Ekkor a $\sum (-1)^{n+1} \cdot a_n$ sort Leibniz-típusú sornak nevezzük.
Ezek akkor és csak akkor konvergensek, ha $lim(a_n) = 0$. 
Ha $A := \overset{+\infty}{\underset{n = 0}{\sum}} (-1)^{n+1} \cdot a_n$, $s_n := A := \overset{n}{\underset{k = 0}{\sum}} (-1)^{k+1} \cdot a_k$, akkor: $| s_n - A| \le a_n$

