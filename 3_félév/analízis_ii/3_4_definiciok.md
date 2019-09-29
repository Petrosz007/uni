## Definiálja egy $f \in \R \rarr \R$ függvény pontbeli folytonosságát

Az $f \in \R \rarr \R$ függvény folytonos az $a \in D_f$ pontban, ha:
	$\forall \epsilon > 0, \exists \delta > 0, \forall x \in D_f, |x - a | < \delta : |f(x) - f(a)| < \epsilon$

## Mi a kapcsolat a pontbeli folytonosság és a határérték között?

Legyen $a \in D_f \cap D'_f$
$f \in C(a) \Leftrightarrow \exists \underset{a}{lim} f$ és $\underset{a}{lim} f = f(a)$ 

## Milyen tételt ismer hatványsor összegfüggvényének a folytonosságáról?

Tekintsük a $\sum \alpha_k (x-a)^k$ hatványsort
Ekkor $f(x) = \underset{k=0}{\overset{\infty}{\sum}}\alpha_k (x-a)^k ​$ folytonos a konvergenciahalmaz belsejében.

## Hogyan szól a folytonosságra vonatkozó átviteli elv?

Legyen $a \in D_f$ Ekkor:
$f \in C(a) \Leftrightarrow \forall (x_n) : \N \rarr D_f$ és $\lim x_n = a : lim f(x_n) = f(a)$

## Fogalmazza meg a hányadosfüggvény folytonosságára vonatkozó tételt.

## Milyen tételt ismer az összetett függvény pontbeli folytonosságáról?

Legyen $f,g \in \R \rarr \R, g \in C(a), f \in C(g(a))$. Ekkor $f \circ g \in C(a)$

## Mit jelent az, hogy egy függvény jobbról folytonos egy pontban?

Legyen $f \in \R \rarr \R$ és tfh $a \in (D_f \cap (a, +\infty))'$. $f$-nek $a$-ban van jobb oldali határértéke, ha a $g(x) := f(x)$   $(x \in D_f \cap(a,+\infty))$ függvénynek $a$-ban van határértéke. Ezt nevezzük $f$ jobb oldali határértékének, és így jelöljük:
$\underset{a+0}{\lim} f := \underset{a}{\lim} g \in \overset{\_\_}{\R}$

## Mit tud mondani a korlátos és zárt $[a,b] \subset \R$ intervallumon folytonos függvény értékkészletéről?

Ha $f : [a,b] \rarr \R$ folytonos, akkor $f$ korlátos

## Hogyan szól a *Weierstrass-tétel*?

Ha $f : [a,b] \rarr \R$ folytonos, akkor $\exists$ abszolút maximuma és $\exists$ abszolút minimuma

## Mit mond ki a *Bolzano-tétel*?

Legyen $f : [a,b] \rarr \R$ folytonos.
Ha $f(a) \cdot f(b) < 0$, akkor $\exists \xi \in [a,b] : f(\xi) = 0$





## Fogalmazza meg a *Bolzano Darboux-tételt*.

Tfh.: $f : [a,b] \rarr \R$ folytonos
Ha $f(a) < f(b)$, akkor $\forall c \in (f(a), f(b)) : \exists \xi \in [a,b] : f(\xi) = c$

## Mit jelent az, hogy egy függvény *Darboux-tulajdonságú*?

Az $f : [a,b] \rarr \R$ függvény Darboux-tulajdonságú, ha $\forall x_1 < x_2$-re $(x_1,x_2 \in [a,b])$
amelyre $f(x_1) \ne f(x_2)$ és $\forall c \in \underset{vagy\ (f(x_2), f(x_1))}{(f(x_1), f(x_2))}, \exists \xi \in(x_1, x_2) : f(\xi) = c$

## Mi a kapcsolat a Darboux-tulajdonság és a folytonosság között?

Ha egy $ f : [a,b] \rarr \R$ függvény folytonos, akkor Darboux tulajdonságú.

## Mit tud mondani az $f : [a,b] \rarr \R (a < b, a,  \in \R)$ inverz függvényének folytonosságáról?

Ha $f : [a,b] \rarr \R, a,b \in \R, a < b​$ függvény folytonos és injektív, akkor $f^{-1}​$  folytonos a $D_{f^{-1}} = R_f​$ intervallumon.

## Milyen állítást ismer *tetszőleges intervallumon* értelmezett függvény inverzének a folytonosságáról?

Legyen $I\subset \R$ tetszőleges intervallum, $f : I \rarr \R$ folytonos $I$-n és injektív.
Ekkor $R_f$ intervallum és $f^{-1}$ folytonos rajta.

## Definiálja a *megszüntethető szakadási hely* fogalmát.

Az $f \in \R \rarr \R$ függvénynek az $a \in D_f$ egy megszüntethető szakadási helye, ha 
$\exists \underset{a}{\lim} f \in \R$, de $\underset{a}{\lim} f \ne f(a)$

## Definiálja az *elsőfajú szakadási hely* fogalmát.

Az $f \in \R \rarr \R$ függvénynek $a \in D_f$ pontban elsőfajú szakadási helye van, ha
$\exists \underset{a+0}{\lim} f \in \R, \exists \underset{a-0}{\lim} f \in \R$, de $\underset{a+0}{\lim} f \ne \underset{a-0}{\lim} f$

## Mit tud mondani *monoton* függvény szakadási helyeiről? 

Legyen $\alpha, \beta \in \R : \alpha < \beta, f : (\alpha, \beta) \rarr \R$ monoton, $a \in (\alpha, \beta)$
Ekkor vagy $f \in C(a)$ vagy $f$-nek $a$-ban elsőfajú szakadása van.



