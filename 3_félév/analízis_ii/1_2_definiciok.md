## Definiálja az $A \in \overset{\_\_}{\R}$ elem $r > 0$ sugarú környezetét.

Ha $A\in \R$, akkor: $K_r(A) := (A-r, A+r)$
Ha $A = +\infty$, akkor $K_r(A) := (\dfrac{1}{r}, + \infty)$
Ha $A = -\infty$, akkor $K_r(A) := (-\infty, \dfrac{1}{r})$

## Mikor mondja azt, hogy egy $f \in \R \rarr \R$ függvénynek valamely $a \in \overset{\_\_}{\R}$ helyen van határértéke?

$f \in \R \rarr \R$-nek $a \in \overset{\_\_}{\R}$-ban van határértéke, ha $\exists A \in \overset{\_\_}{\R}, \forall \epsilon > 0, \exists \delta > 0, \forall x \in K_\delta(a) \backslash \{a\} \cap D_f : $
								$f(x) \in K_\epsilon(A)$

## Adja meg egyenlőtlenségek segítségével a *végesben vett véges* határérték definícióját.

Legyen $f \in R \rarr \R, a \in D_f, A \in \R$. Ekkor:
$\underset{a}{lim} f = A \in \R \Leftrightarrow $
$ \forall \epsilon > 0, \exists \delta > 0, \forall x \in D_f, 0 < |x - a| < \delta : $
									$|f(x) - A| <\epsilon$

## Adja meg egyenlőtlenségek segítségével a *plusz végtelenben vett plusz végtelen* hatérérték definícióját.

Legyen $f \in \R \rarr \R, +\infty \in D'_f$. Ekkor:
$\underset{+\infty}{\lim} f = +\infty\Leftrightarrow$
$\forall P > 0, \exists x_0 > 0, \forall x \in D_f, x > x_0 : f(x) > P$

## Írja le a *hatványsor* definícióját.

A $(\alpha_n) : \N \rarr \R$ sorozattal és az $a \in \R$ számmal képzett $\sum \alpha_n(x-a)^n \ (x \in \R)$ végtelen sort $a$ középpontú, $(\alpha_n)$ együtthatós hatványsornak nevezzük.

## Definiálja az $\exp$ függvényt.

$\exp(x) :=  \underset{n = 0}{\overset{\infty}{\sum}}\dfrac{x^n}{n!}$

## Mit tud mondani a hatványsor összegfüggvényének a határértékéről?

Tfh: a $\sum \alpha_n (x-a)^n$ hatványsor konvergencia sugara $R > 0$ és $f(x) := \overset{\infty}{\underset{n=0}{\sum}} \alpha_n (x-a)^n \ \  (x \in K_R(a))$
Ha $x_0\in K_R(a)$, akkor $\underset{x\rarr x_0}{\lim} f(x) = f(x_0)$

