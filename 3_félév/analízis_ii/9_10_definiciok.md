## Definiálja a $\pi$ számot.

$\exists! \xi \in [0,2] : \cos \xi = 0$, $\pi := 2 \xi$

## Értelmezze az $arc \sin$ függvényt, és ábrázolja egy koordináta-rendszerben a $\sin$ és $arc \sin$ függvényeket.

A $\sin$ függvény szig. mon. növekedő $[-\dfrac{\pi}{2}, \dfrac{\pi}{2}]$ intervallumon, ezért invertálható. Ennek a leszűkítésnek az inverze az $arc \sin$

![](assets/2019-11-17-17-32-34.png)

## Értelmezze a $arc tg$ függvényt, és ábrázolja egy koordináta-rendszerben a $tg$ és az $arc tg$ függvényeket.

A $tg$ függvény szig. mon. nő a $(-\dfrac{\pi}{2}, \dfrac{\pi}{2})$ intervallumon, ezért invertálható. Ennek a leszűkítésének az inverze az $arc tg$ függvény.

![](assets/2019-11-17-17-32-59.png)

 ## Mi a kétszer deriválható függvény fogalma?

Az $f \in \R \rarr \R$ függvény kétszer deriválható az $a \in int(D_f)$ pontban, ha
		$\exists K(a) \subset D_f$, hogy $f \in D[K(a)]$ és $f' \in D[a]$

## Mi a konvex függvény definíciója?

Az $f: (a,b) \rarr \R$ függvény konvex, ha 
$\forall x_1, x_2 \in (a,b), x_1 < x_2, \forall \lambda \in (0,1) :$
			$f(\lambda x_1 + (1-\lambda)x_2) \le \lambda f(x_1) + (1-\lambda)f(x_2)$

## Jellemezze egy függvény *konvexitását* az első derivált segítségével.

Legyen $I \subset \R$ tetsz. nyílt intervallum és $f : I \rarr \R$. Tfh.: $f \in D^2(I)$. 
Ekkor$f$ konvex (szig. konvex) $I$-n $\Leftrightarrow f' \nearrow (\uparrow)\ I$-n



## Jellemezze egy függvény *konkávitását* a második derivált segítségével.

Legyen $I \subset \R$ tetsz. nyílt intervallum és $f : I \rarr \R$. Tfh.: $f \in D^2(I)$. 
Ekkor: i, $f$ konkáv $I$-n $\Leftrightarrow f'' \le 0\ I$-n
			ii, Ha $f'' < 0\ I$-n $\Rarr f$ szig. konkáv $I$-n 



## Milyen állítást ismer a $(-\infty)$-beli aszimptota meghatározására?
Legyen $f : (-\infty, a) \rarr \R, a \in \R$
$f$-nek van aszimptotája $\Leftrightarrow$ $\exists A, B \in \R : \underset{x \rarr -\infty}{\lim} \dfrac{f(x)}{x} = A \ \land \ \underset{x \rarr -\infty}{\lim} (f(x) - Ax) = B$

## Milyen ekvivalens átfogalmazást ismer a pontbeli deriválhatóságra a lineáris közelítéssel?
Legyen $f \in \R \rarr \R, a \in int(D_f)$
Ekkor $f \in D[a] \Leftrightarrow \exists A \in \R, \exists \epsilon : D_f \rarr \R, \underset{0}{\lim} \epsilon = 0 : $
$\qquad$$f(a + h) - f(a) = A \cdot h + \epsilon(h) \cdot h \qquad (a+h \in D_f)$

## Milyen feltételt ismer differenciálható függvény *szigorú monoton csökkenésével* kapcsolatban?
Legyen: $a,b \in \R, f : (a,b) \rarr \R$
Tfh.: $f \in D(a,b)$
Ekkor 
1. Ha $f' \le 0 (a,b)$-n $\Rightarrow f \searrow (a,b)$-n
2. Ha $f' <   0 (a,b)$-n $\Rightarrow f \downarrow (a,b)$-n

## Hogyan szól a lokális minimumra vonatkozó *elsőrendű elégséges* feltétel?
Legyen $a,b \in \R, f : (a,b) \rarr \R$
Tfh.: $f \in D(a,b), \exists c \in (a,b) : f'(c) = 0, f'$ előjelet vált $c$-ben
Ekkor ha az $f'$ negatívba pozitívból megy át, akkor a $c$-pontban $f$-nek lokális minimuma van 

## Mi a *konkáv* függvény definíciója?
Legyen $f \in \R \rarr \R, I \subset D_f$
$\qquad\forall a,b \in I, a < b : f(x) \ge \dfrac{f(b) - f(a)}{b - a}(x-a) + f(a) \qquad (\forall x \in (a,b))$

## Jellemezze egy függvény *konvexitását* a második derivált segítségével.
Legyen $I \subset \R$ nyílt intervallum, $f : I \rarr \R$
Tfh.: $f \in D^2(I)$
Ekkor 
1. $f$ konvex $I$-n $\Leftrightarrow f'' \ge 0 \ I$-n
2. Ha $f'' > 0 I$-n $\Rightarrow$ $f$ szigorúan konvex $I$-n

## Milyen állítást ismer a $(+\infty)$-beli aszimptota meghatározására?
Legyen $f : (a, +\infty) \rarr \R, a \in \R$
$f$-nek van aszimptotája $\Leftrightarrow$ $\exists A, B \in \R : \underset{x \rarr +\infty}{\lim} \dfrac{f(x)}{x} = A \ \land \ \underset{x \rarr +\infty}{\lim} (f(x) - Ax) = B$
