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

