## Mi a belső pont definíciója?

Legyen $H \subset \R$ halmaz, $ a \in \R$
Az $a$ pont a $H$ halmaz belső pontja, ha $\exists r > 0 : (a-r, a+r) \subset H$

## Mikor mondja azt, hogy egy $f \in \R \rarr \R$ függvény differenciálható valamely pontban?

Legyen $f \in \R \rarr \R, a \in int(D_f)$
Ekkor az $f$ differenciálható $a$ -ban, ha:

​		$\exists \underset{h->0}{\lim} \dfrac{f(a+h) - f(a)}{h} \in \R$

## Mi a kapcsolat a pontbeli differenciálhatóság és a folytonosság között?

Legyen $f \in \R \rarr \R, a \in int(D_f):$

1. $f \in D[a] \Rarr f \in C[a]$
2. $f \in C[a] \not\Rarr f \in D[a]$

## Mi a jobb oldali derivált fogalma?

Legyen $f \in \R \rarr \R, a \in int(d_f)$
Ekkor a következő kifejezés az $f$ jobb oldali deriváltja, ha az létezik és valós:
			$\underset{h\rarr0+0}{\lim} \dfrac{f(a+h) - f(a)}{h}$

## Milyen ekvivalens átfogalmazást ismer a pontbeli deriválhatóságra a lineáris közelítéssel?

Legyen $f \in \R \rarr \R, a \in int(D_f)$

​		$f \in D[a] \Leftrightarrow \exists A \in \R \and \exists \omega : D_f \rarr \R:$
​										i, $\omega \in C[a] \and \omega(a) = 0$
​										ii, $\forall x \in D_f : f(x) - f(a) = (A + \omega(x))(x-a)$









## Mi az érintő definíciója?

Legyen $f \in \R \rarr  \R, a \in int(D_f)$
Ekkor az $f$ függvény grafikonjának az $(a, f(a))$ pontban van érintője, ha $f \in D[a]$, és ez az érintő alatt a következő egyenletű egyenest értjük:
				$y = f'(a)(x-a) + f(a)$

## Milyen tételt ismer két függvény szorzatának valamely pontbeli differenciálhatóságáról és a deriváltjáról?

Legyen $f,g \in \R \rarr \R, a \in int(D_f \cap D_g)$
Ekkor ha $f,g \in D[a]$, akkor:
		 $f \cdot g \in D[a] \and (f \cdot g)'(a) = f'(a)\cdot g(a) + f(a) \cdot g'(a)$

## Milyen tételt ismer két függvény hányadosának valamely pontbeli differenciálhatóságáról és deriváltjáról?

Legyen $f,g \in \R \rarr \R, a \in int(D_f \cap D_g)$
Ekkor ha $f,g \in D[a]$, akkor:
		$\left(\dfrac{f}{g}\right) \in D[a] \and \left(\dfrac{f}{g}\right)'(a) = \dfrac{f'(a) \cdot g(a) - f(a) \cdot g'(a)}{(g(a))^2}$

## Milyen tételt ismer két függvény kompozíciójának valamely pontbeli differenciálhatóságáról és deriváltjáról?

Legyen $f,g \in \R \rarr \R, a \in int(D_f \cap D_g)$
Ekkor ha $f,g \in D[a]$, akkor:
		$f \circ g \in D[a] \and (f \circ g)'(a) = f'(g(a)) \cdot g'(a)$

## Írja fel az $\exp_a (a \in \R, a > 0)$ függvény deriváltját valamely helyen.

$\exp_a'(x) = a^x \cdot \ln(a)$

## Írja fel a $\log_a (a \in \R, 0 < a \ne 1)$ függvény deriváltját valamely helyen.

$\log_a'(x) = \dfrac{1}{x \cdot \ln(a)}$

