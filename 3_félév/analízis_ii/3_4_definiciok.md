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
Ha $f(a) - f(b) < 0$, akkor $\exists \xi \in [a,b] : f(\xi) = 0$





