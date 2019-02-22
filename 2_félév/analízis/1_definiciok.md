## Teljességi Axióma

Legyen $\emptyset \ne A, B \subset \R$, amire $\forall a \in A, \forall b \in B : a \le b$
Ekkor: $\exists \xi \in \R, \forall a \in A, \forall b \in B: a \le \xi \le b$

## Szuprémum elv

##### Tétel

Legyen $\emptyset \ne A \subset \R$, felülről korlátos.
Ekkor $A$-nak van legkisebb felső korlátja, azaz $\exists min B$

#####  Bizonyítás

$\forall a \in A, \forall K \in B: a \le K$
$\Rightarrow \exists \xi \in \R : a \le \xi \le K$
​	$\Rightarrow \forall a \in A : a \le \xi$ felső korlátja $A$-nak $\Rightarrow \xi \in B$
​	$\Rightarrow \forall K \in B : \xi \le K \Rightarrow \xi$ a legkisebb felső korlát

$\Rightarrow \xi = min B = sup B$

## Infimum elv

##### Tétel

Legyen $\emptyset \ne A \subset \R$, alulról korlátos.
Ekkor $A$-nak van legnagyobb alsó korlátja, azaz $\exists max B$

#####  Bizonyítás

$\forall a \in A, \forall K \in B: K \le a$
$\Rightarrow \exists \xi \in \R : K \le \xi \le a$
​	$\Rightarrow \forall a \in A : \xi \le a$ alsó korlátja $A$-nak $\Rightarrow \xi \in B$
​	$\Rightarrow \forall K \in B : K \le \xi \Rightarrow \xi$ a legnagyobb alsó korlát

$\Rightarrow \xi = maxB = inf B$

## Archimedesi-tétel

$\forall a \gt 0, \forall b \in \R, \exists n \in \N : a \cdot n \gt b$

## Cantor-féle közösrész-tétel

Legyen $[a_n, b_n]$ korlátos és zárt intervallum, melyre:
$[a_{n+1}, b_{n+1}] \subset [a_n, b_n] $          $(n \in \N)$

Ekkor: $\bigcap_{n \in \N} [a_n, b_n] \ne \emptyset$ 