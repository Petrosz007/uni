## Írja le a hatványsor definícióját.

A $(\alpha_n) : \N \rarr \R$ sorozattal és az $a \in \R$ számmal képzett
$\sum \alpha_n(x-a)^n\ \ (x \in \R)$ végtelen sort $a$ középpontú, $(\alpha_n)$ együtthatós hatványsornak nevezzük.

## Fogalmazza meg a Cauchy Hadamard-tételt.

Tekintsük a $\underset{n=0}{\sum}\alpha_n(x-a)^n$ hatványsort, és tfh.: $\exists lim(\root{n}\of{|\alpha_n|}) =: A \in \overset{\_\_}{\R}$
Ekkor:
$R := \left\{ \begin{array}{ll}
\frac{1}{A}, & ha\ 0 < A < + \infty \\
 0, & ha\ A = + \infty \\
+\infty, & ha\ A = 0 
\end{array} \right\}$
a hatványsor konvergenciasugara. Ez azt jelenti, hogy:

1. Ha $0 < R < + \infty$, akkor a hatványsor $x \in \R$ esetén abszolút konvergens, ha $|x-a| < R$ és divergens, ha $|x-a| > R$
2. Ha $R = 0$, akkor a hatványsor csak az $x = a$ pontban konvergens.
3. Ha $R = +\infty$, akkor a hatványsor $\forall x \in \R$ pontban konvergens.

## Adjon meg egy olyan hatványsort, amelyiknek a konvergenciahalmaza a $(-1, 1]$ intervallum.

$\underset{n = 0}{\sum} \dfrac{(-1)^n}{n}x^n$

## Adjon meg egy olyan hatványsort, amelyiknek a konvergenciahalmaza a $[-1, 1]$ intervallum.

$\underset{n = 0}{\sum} \dfrac{x^n}{n^2}$

## Definiálja a sin függvényt.

$\sin(x) := \underset{n=0}{\overset{\infty}{\sum}}(-1)^n \dfrac{x^{2n+1}}{(2n+1)!}$

## Definiálja a cos függvényt.

$\cos(x) := \underset{n=0}{\overset{\infty}{\sum}}(-1)^n \dfrac{x^{2n}}{(2n)!}$