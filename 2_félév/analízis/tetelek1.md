# Bizonyítással kért tételek az 1.  zh-n

## 1. A szuprémum elv

**Tétel:**
Legyen $\emptyset \ne A \subset \R$, felülről korlátos. Ekkor $A$-nak van legkisebb felső korlátja, azaz $\exists \min B$

**Bizonyítás:**
Világos, hogy  $\forall a \in A, \forall b \in B : a \le K$
$\Rarr $ (Teljességi axióma) $\exists \xi \in \R : a \le \xi \le K\ (a \in A, K \in B)$
Vagyis, $\forall a \in A : a \le \xi \rarr \xi$ felső korlátja $A$-nak $\Rarr \xi \in B$
Ugyanakkor: $\forall K \in B : \xi \le K \Rarr \xi$ a legkisebb felső korlát
$\Rarr \xi = \min B$

## 2. Az Archimedes-tétel

**Tétel:**
$\forall a \gt 0, \forall b \in \R, \exists n \in N : a \cdot n \gt b$

**Bizonyítás:**

1. Ha $b \le 0$, akkor világos, hogy $b \le 0 \lt a = a \cdot 1$, ha $n := 1$
     $\Rarr n = 1$ jó választás

2. Feltehető, hogy $b \gt 0$
   Áll: $\forall b \gt 0, \forall a \gt 0, \exists n \in N: a \cdot n \gt b$
   Indirekt: $\exists b \gt 0, \exists a \gt 0, \forall n \in N : a \cdot n \le b$

   $A := \{a \cdot n \in \R : n \in \N\}$
   $\Rarr b$ egy felső korlátja $A$-nak $\Rarr \xi = sup A$
   $\Rarr \xi - a$ már nem felső korlát, azaz $\exists n_0 \in N : a \cdot n_0 \gt \xi$
   $\Rarr \exists n_0 \in \N : a \cdot n_0 + a \gt \xi \Leftrightarrow a (n_0 + 1) \gt \xi$

   Mivel $n_0 \in \N$ és $\N$ induktív $\Rarr n_0 + 1 \in \N$
   $\Rarr a(n_0 + 1) \in A \Rarr \xi$ nem felső korlát

   Ellentmondás $\Rightarrow\Leftarrow$

## 3. A Cantor-féle közösrész-tétel

**Tétel:**
Legyen $[a_n, b_n]$ korlátos és zárt intervallum, melyre:
$[a_{n+1}, b_{n+1}] \subset [a_n, b_n]\ (n \in \N)$

Ekkor: $\underset{n\in \N}{\cap} [q_n, b_n] \ne \emptyset​$

**Bizonyítás:**
$A := \{a_n \in \R : n \in \N\}$
$B := \{b_n \in \R : n \in \N\}$

Ekkor: $\forall n, m \in \N: a_n \le b_m$
Ha $n \le m : q_n \le a_m \le b_m$
Ha $m \lt n : a_n \le b_n \le b_m$

$\Rarr$ (Teljességi axióma) $\exists \xi \in \R, \forall n, m \in \N : a_n \le \xi \le b_m$
Spec: $n = m$, ekkor:
$\forall n \in \N : a_n \le \xi \le b_n$
$\Rarr \xi \in [a_n, b_n]\ (n \in \N)$
$\Rarr \xi \in \underset{n \in \N}{\cap}[a_n, b_n]$

## 4. Minden sorozatnak van monoton részsorozata

**Tétel:**
Minden sorozatnak van monoton részsorozata

**Bizonyítás:**

1. A sorozatnak végtelen sok csúcsa van

   $\exists a_{n_0}$ csúcs $\Rarr \forall n \ge n_0 : a_{n_0} \ge a_n$
   $\Rarr  \exists n_1 \gt n_0$ és $a_{n_1} $ csúcs $\Rarr a_{n_0} \ge q_{n_1}$ 
   $\Rarr  \forall n \ge n_1 : a_{n_1} \ge a_n$
   $\Rarr  \exists n_2 \gt n_1$ és $a_{n_2} $ csúcs $\Rarr a_{n_1} \ge q_{n_2}$

   $\Rarr \exists a_{n_0} \ge a_{n_1} \ge a_{n_2} \ge ...$

2. A sorozatnak véges sok csúcsa van

   $\exists N \in \N, \forall n \ge N: a_n$ nem csúcs
   Legyen $n_0 = N \Rarr a_{n_0}$ nem csúcs $\Rarr$
   $\Rarr \exists n_1 \ge n_0 : a_{n_0} \lt a_{n_1} \Rarr a_{n_1}$ nem csúcs $\Rarr$

   $\Rarr n_2 \ge n_1 : a_{n_1} \lt a_{n_2} ... $

   $\exists a_{n_0} \lt a_{n_1} \lt a_{n_2} \lt ...$

## 5. Konvergens sorozat határértéke egyértelmű

**Tétel:**
Az $(a_n)​$ konvergens sorozat határértéke egyértelmű.

**Bizonyítás:**
Indirekt, Tfh: $\exists A_1, A_2, A_1 \ne A_2$ határértékek
	$\Rarr \forall \epsilon > 0, \exists n_1 \in \N, \forall n \ge n_1 : | a_n - A_1 | < \epsilon$
	$\Rarr \forall \epsilon > 0, \exists n_2 \in \N, \forall n \ge n_2 : | a_n - A_2 | < \epsilon$

Legyen $n_0 = \max(n_1, n_2) :$
	$\Rarr \forall \epsilon > 0, \exists n_0 \in \N, \forall n \ge n_0 : | a_n - A_1 | < \epsilon$
								$|a_n - A_2 | < \epsilon$

Legyen $\epsilon < \dfrac{|A_1 - A_2|}{2} \Rarr$
$|A_1 - A_2| = |A_1 - a_n + a_n - A_2| \le |A_1 - a_n| + |A_n - A_2| < 2\epsilon < |A_1 - A_2|$

Ellentmondás, $|A_1 - A_2| \not\lt |A_1 - A_2 |$

## 6. A konvergencia és a korlátosság kapcsolata

**Tétel:**
Ha $a_n$ konvergens, akkor korlátos.

**Bizonyítás:**
Legyen $\lim a_n = A \in \R$
$\Rarr \epsilon = 1$-re is $\exists n_1 \in \N, \forall n \ge n_1 : |a_n - A| < 1$
$\Rarr |a_n| = |a_n - A + A| \le |a_n - A| + |A| \lt 1 + |A|,\ \ \forall n \in \N$
$\Rarr |a_n|  \le max(|a_0|, |a_1|, |a_2|,...,|a_{n_0-1}|, |a_{n_0}|, a + |A|), \ \ (n \in \N)$

## 7. Műveletek nullsorozatokkal

**Tétel:**
Legyen $(a_n), (b_n)$ nullsorozat. Ekkor:

1. $(a_n + b_n)$ is nullsorozat.
2. Ha $(c_n)$ korlátos, akkor $(a_n \cdot c_n)$ is nullsorozat.
3. $(a_n \cdot b_n)$ is nullsorozat.

**Bizonyítás:**

1. $(a_n) nullsor \Leftrightarrow \forall \frac{\epsilon}{2} > 0, \exists n_1 \in \N, \forall n \ge n_1 : |a_n| < \frac{\epsilon}{2}$
   $(b_n) nullsor \Leftrightarrow \forall \frac{\epsilon}{2} > 0, \exists n_2 \in \N, \forall n \ge n_2 : |b_n| < \frac{\epsilon}{2}$

   $\Rarr \forall \epsilon > 0, \exists n_0 = max(n_1, n_2), \forall n \ge n_0 : \\ |a_n + b_n| \le |a_n| + |b_n| < \frac{\epsilon}{2}+ \frac{\epsilon}{2} < \epsilon $
   $\Rarr (a_n + b_n)$ nullsor

2. $(c_n)$ korlátos $\Rarr \exists K \in \R, \forall n : |c_n| \le K$
   $(a_n)$ nullsor $\Rarr \forall \epsilon > 0, \exists n_0 \in \N, \forall n \in \N, n \ge n_0 : |a_n| < \frac{\epsilon}{K} $
   $\Rarr \forall \epsilon > 0, \exists n_0 \in \N, \forall n \in \N, n \ge n_0 : \\ |a_n \cdot c_n| < \frac{\epsilon}{K} \cdot K = \epsilon$

3. $(b_n)$ nullsor $\Rarr (b_n)$ konvergens $\Rarr (b_n)$ korlátos
   $(a_n)$ nullsor $\overset{2. miatt}{\Rarr} (a_n \cdot c_n)$ nullsor

## 8. Konvergens sorozatok szorzatára vonatkozó tétel

**Tétel:**
Legyen $(a_n), (b_n)$ konvergens és $A := \lim(a_n), B := \lim(b_n)$. Ekkor:
$(a_n \cdot b_n)$ konvergens és $\lim(a_n \cdot b_n) = A \cdot B$

**Bizonyítás:**


