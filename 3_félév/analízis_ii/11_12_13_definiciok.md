## Írja le a $\frac{0}{0}$ esetre vonatkozó *L'Hospital-szabályt.*
Tfh.: 
1. $g,f \in D(a,b), -\infty \le a < b < \infty$
2. $g'(x) \ne 0, x \in (a,b)$
3. $\exists \underset{a + 0}{\lim} f = \exists \underset{a + 0}{\lim} g = 0$
4. $\exists \underset{a + 0}{\lim} \dfrac{f'}{g'} = A \in \overset{\_\_}{\R}$

Ekkor: $\exists \underset{a + 0}{\lim} \dfrac{f}{g} = A \qquad \Big( = \underset{a + 0}{\lim} \dfrac{f'}{g'} \Big) $ 

## Írja le a $\frac{+\infty}{-\infty}$ esetre vonatkozó *L'Hospital-szabályt.*
Tfh.: 
1. $g,f \in D(a,b), -\infty \le a < b < \infty$
2. $g'(x) \ne 0, x \in (a,b)$
3. $\exists \underset{a + 0}{\lim} f = \exists \underset{a + 0}{\lim} g = \infty$
4. $\exists \underset{a + 0}{\lim} \dfrac{f'}{g'} = A \in \overset{\_\_}{\R}$

Ekkor: $\exists \underset{a + 0}{\lim} \dfrac{f}{g} = A \qquad \Big( = \underset{a + 0}{\lim} \dfrac{f'}{g'} \Big) $ 

## Fogalmazza meg a hatványsor összegfüggvényének a deriválására vonatkozó tételt.
Tfh.: a $\sum \alpha_k (x-a)^k$ hatványsor konvergens és $f(x) = \underset{k=n}{\overset{\infty}{\sum}} \alpha_k (x-a)^k \qquad (x \in K_R(a), R > 0)$
Ekkor: $f^{(n)}(x) = \underset{k=n}{\overset{\infty}{\sum}} \alpha_k k (k-1)(k-2)...(k-n+1)(x-a)^{k-n} \qquad (x \in K_R(a))$

## Mi a kapcsolat a hatványsor összegfüggvénye és a hatványsor együtthatói között?
Tfh.: a $\sum \alpha_k (x-a)^k$ hatványsor konvergens és $f(x) = \underset{k=n}{\overset{\infty}{\sum}} \alpha_k (x-a)^k \qquad (x \in K_R(a), R > 0)$
Ekkor: $\alpha_n = \dfrac{f^{(n)}(a)}{n!} \qquad (n \in \N)$

## Hogyan definiálja egy függvény *Taylor-sorát?*
Ha $f \in D^\infty(a)$, akkor a $\underset{k=0}{\sum} \dfrac{f^{(k)}(a)}{k!}(x - a)^k$ sort $f$ Taylor-sorának nevezzük.

## Mi a *Taylor-polinom* definíciója?
Ha $f \in D^n(a)$, akkor a $\underset{k = 0}{\overset{n}{\sum}} \dfrac{f^{(k)}(a)}{k!}(x-a)^k$ polinomot $f$ $n$-edik Taylor-polinomjának nevezzük.

## Fogalmazza meg a *Taylor-formula Lagrange maradéktaggal* névvel tanult tételt.
Tfh.: $f \in D^{n + 1}(K(a))$
Ekkor $\forall x \in K(a), \exists \xi \in (a, x) : f(x) = \underset{k = 0}{\overset{n}{\sum}} \dfrac{f^{(k)}(a)}{k!}(x-a)^k + \dfrac{f^{(n+1)}(\xi)}{(n+1)!}(x-a)^{n+1}$

## Milyen *elégséges feltételt* ismer arra, hogy egy függvény Taylor-sora előállítja a függvényt?
Legyen $f \in D^\infty(K(a))$, Tfh.: $\exist 0 < M \in \R : |f^{(n)}(x)| \le M \qquad (\forall x \in K(a), \forall n \in \N)$
Ekkor $f(x) = \underset{n \rarr +\infty}{\lim} \underset{k=0}{\overset{n}{\sum}} \dfrac{f^{(k)}(a)}{k!}(x-a)^k = \underset{k=0}{\overset{+\infty}{\sum}} \dfrac{f^{(k)}(a)}{k!}(x-a)^k \qquad (x \in K(a))$