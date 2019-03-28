# Bizonyítással kért tételek az 1.  zh-n

## 1. A szuprémum elv

**Tétel:**
Legyen $\emptyset \ne A \subset \R$, felülről korlátos. Ekkor $A$-nak van legkisebb felső korlátja, azaz $\exists \min B$

**Bizonyítás:**
Világos, hogy  $\forall a \in A, \forall b \in B : a \le K$
$\Rarr $ (Teljességi axióma) $\exists \xi \in \R : a \le \xi \le K\ (a \in A, K \in B)$
Vagyis, $\forall a \in A : a \le \xi \rarr \xi$ felső korlátja $A$-nak $\Rarr \xi \in B$
Ugyanakkor: $\forall K \in B : \xi \le K \Rarr \xi$ a legkisebb felső korlát
$\Rarr \xi = \min B​$

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
Legyen $(a_n), (b_n)​$ konvergens és $A := \lim(a_n), B := \lim(b_n)​$. Ekkor:
$(a_n \cdot b_n)​$ konvergens és $\lim(a_n \cdot b_n) = A \cdot B​$

**Bizonyítás:**
$|a_n \cdot b_n - A \cdot B| = |a_nb_n - A \cdot b_n + A \cdot b_n - AB| \le \\ \le|a_n b_n - A b_n| + |A b_n - AB| = \underset{nullsor}{\underset{nullsor}{\underset{konvergens\\korlátos}{|b_n|} \cdot \underset{nullsor}{|a_n - A|}} + \underset{nullsor}{\underset{korlátos}{|A|} \cdot \underset{nullsor}{|b_n - B|}}}​$

## 9. Konvergens sorozatok hányadosára vonatkozó tétel

**Tétel:**
Legyen $(a_n), (b_n)​$ konvergens, $b_n \ne 0​$ és $A := \lim(a_n), B := \lim(b_n)​$ és $B \ne 0​$. Ekkor:
$\left(\dfrac{a_n}{b_n}\right)​$ konvergens és $\lim\left(\dfrac{a_n}{b_n}\right) = \dfrac{A}{B}​$

**Bizonyítás:**
$\left|\dfrac{a_n}{b_n} - \dfrac{A}{B}\right| = \left|\dfrac{a_nB - Ab_n}{b_nB}\right| = \dfrac{|a_nB - AB + AB - Ab_n|}{|b_nB} \le \\ \le \underset{nullsor}{\underset{korlátos}{\dfrac{|B|}{|b_n||B|}} \cdot \underset{nullsor}{|a_n - A|} + \underset{korlátos}{\dfrac{|A|}{|b_n||B|}} \cdot \underset{nullsor}{|b_n - B|}}$
$\Rarr  \left(\dfrac{a_n}{b_n} - \dfrac{A}{B}\right) nullsor \Rarr \lim\left(\dfrac{a_n}{b_n}\right) = \dfrac{A}{B}$

## 10. A közrefogási elv

**Tétel:**
Tfh: $\exists N \in \N, \forall n \ge N : a_n \le b_n \le c_n$
Ha $\lim a_n = \lim c_n$, akkor $\lim b_n = \lim a_n$

**Bizonyítás:**
$\lim a_n = A \in \overset{\_\_}{\R}$

1. $A \in \R \Rarr \forall \epsilon > 0, \exists n_1, \forall n \ge n_1: A - \epsilon < a_n < A + \epsilon$
   	     $\Rarr \forall \epsilon > 0, \exists n_2, \forall n \ge n_2: A - \epsilon < c_n < A + \epsilon$

   Legyen $n_0 = \max(n_1, n_2, N) \Rarr \\ \Rarr \forall \epsilon > 0, \exists n_0, \forall n \ge n_0 : A - \epsilon < a_n \le b_n \le c_n < A + \epsilon$
   $\Rarr \lim b_n = A$

2. $A = \infty : lim a_n = \infty \Rarr \forall P \in \R, \exists n_1, \forall n \ge n_1 : a_n > P$
   De $b_n \ge a_n, \forall n \ge N \Rarr \\ \forall P \in \R, \exists n_0 = \max(n_1, N), \forall n \ge n_0 : b_n \ge a_n \ge P$
   $\Rarr \lim b_n = \infty$

## 11. Monoton növő sorozatok határértéke (véges és végtelen eset)

**Tétel:**

1. Ha $(a_n)$ monoton nő és korlátos, akkor konvergens és $\lim a_n = \sup\{a_n : n \in \N \}$
2. Ha $(a_n)$ mon nő és nem korlátos, akkor ​$\lim a_n = \infty$

**Bizonyítás:**

1. $(a_n)$ korlátos $\Rarr  \exists \xi = \sup\{a_n : n \in \N \} < \infty$
   $\Rarr a_n \le \xi, \forall n$ és $\forall \epsilon > 0, \exists n_0 : \xi - \epsilon < a_{n_0} \le \xi \Rarr \\ \Rarr \forall \epsilon > 0, \exists n_0, \forall n \ge n_0 : \xi -  \epsilon < a_{n_0} \le a_n \le \xi$
   										$= | a - \xi| < \epsilon$
   $\Rarr \lim a_n = \xi​$

   $(a_n)​$ nem korlátos $\Rarr (a_n)​$ felülről nem korlátos
   $\Rarr \forall P \in \R, \exists n_0, \forall n \ge n_0: a_n > P \Rarr​$
   $\Rarr \forall P \in \R, \exists n_0, \forall n \ge n_0: a_n \le a_{n_0}> P \Rarr \\ \Rarr \lim a_n = \infty​$

## 12. A Cauchy-féle konvergencia kritérium sorozatokra

 **Tétel:**
$(a_n)$ konvergens $\Leftrightarrow$ $(a_n)$ Cauchy

**Bizonyítás:**
$(\Rarr)$ bizonyítása:
Tfh: $(a_n)$ konvergens. Megmutatjuk, hogy $(a_n)$ Cauchy 
$A := \lim(a_n),\ \ \forall \epsilon > 0, \exists N \in \N, \forall n \in \N, n \ge \N : |a_n - A| < \epsilon$
	Legyen $m, n \in \N, m, n \ge N $ Ekkor:
		$| a_n - a_m| = |a_n - A + A - a_m| \le \underset{\rarr \epsilon}{|a_n - A|} + \underset{\rarr \epsilon}{|a_m - A|} < 2 \epsilon$
	Tehát $\forall \epsilon > 0, \exists N \in \N, \forall n, m \in \N, m, n \ge \N : |a_n - a_m| < 2\epsilon$
		$\Rarr (a_n)$ Cauchy

$(\Larr) bizonyítása:$
Tfh: $(a_n)$ Cauchy. Megmutatjuk, hogy $(a_n)$ korlátos.
	Mivel $(a_n)$ Cauchy, ezért $\exists N \in \N, \forall n, m \in \N, m, n \ge N : |a_n - a_m | < 1$
	$\Rarr \forall n \in \N, n \ge N : |a_n| = |a_n - a_N + a_N| \le \underset{= a_n - a_m}{|a_n - a_N|} + |a_N| < 1 + |a_N|$
	$\Rarr \forall n \in \N, n \ge N : | a_n| \le K := \max\{|a_0|, |a_1|, ..., |a_{N-1}|, |a_N| + 1\}$
	$\Rarr (a_n)$ korlátos
	$\Rarr$ (ld.: Bolzano - Weierstrass): $\exists(a_{n_k})$ konvergens részsorozat és 
													$A := \lim(a_{n_k})$
Megmutatjuk, hogy $(a_{n})$ is konvergens és $\lim(a_n) = A$
	$|a_n - A| = |a_n - a_{n_k} + a_{n_k} - A| \le |a_n - a_{n_k}| + |a_{n_k} - A|$
Mivel $(a_n)$ Cauchy : $\exists N \in \N, \forall n, n_k \in \N, n, n_k \ge N_0 : |a_n - a_{n_k}| < \epsilon$
Mivel $\lim(a_{n_k}) = A$ ezért $\forall \epsilon > 0, \exists N_1 \in \N, \forall n \in \N, n \ge N_1: | a_{n_k} - A| < \epsilon$

Tehát: $\forall \epsilon > 0, \exists N := \max\{N_0, N_1\} \in \N, \forall n \in \N, n \ge N: |a_n - A | < 2 \cdot \epsilon$
	$\Rarr (a_n)$ konvergens és $\lim(a_n) = A $

## 13. A geometriai sorozat határértékére vonatkozó tétel

**Tétel:**
Legyen $q \in \R$. Ekkor:
$lim(q^n) = \left\{\begin{array}{ll}
+\infty, & ha\ q > 1 \\
1, & ha\ q = 1 \\
0, & ha\ |q| < 1 \\
\nexists, & ha\ q \le -1 \end{array}\right\}$

**Bizonyítás:**
Ha $q = 1,\ q = 0,\ q = -1$ akkor triviális.
Tfh: $q > 1$. Ekkor $\exists h \in \R, h > 0 : q = 1 + h$
	$\Rarr q^n = (1 + h)^n \ge 1 + nh \ge n\cdot h \rarr + \infty$
	
Tfh: $q \in (-1, 1)\text{\\} \{0\}, \text{azaz } 0 < |q| < 1$
	$\Rarr \dfrac{1}{|q|} > 1 \Rarr \dfrac{1}{|q|} = 1 + h \Rarr \left(\dfrac{1}{|q|}\right)^n = (1 + h)^n \ge$
	$\ge 1 + nh \ge n \cdot h \Rarr 0 \le |q|^n \le \dfrac{1}{nh} \rarr 1$
	$\Rarr \lim(|q|^n) = 0$ és $\lim(q^n) = 0$

Tfh: $q < -1​$, akkor $q^2 > 1​$. Ekkor:

- $q^{2n} = (q^2)^n \rarr + \infty$

- $q^{2n+1} = q(q^2)^n \rarr -\infty$


  $\Rarr \not\exists \lim(q^n)$


## 14. Az $(\root{n}\of{a})$ és az $(\root{n}\of{n})$ sorozat határértéke

**Tétel:**
$\forall a \in \R, a > 0, : \lim(\root{n}\of{a}) = 1​$

**Bizonyítás:**
Ha $a = 1 \checkmark$
Tfh: $a > 1 \Rarr \forall n \in \N, n \ge 2 : \root{n}\of{a} \gt 1$
	$\Rarr \exists h_n > 0 : \root{n}\of{a} = 1 + h_n$
	$\Rarr a = (1 + h_n)^n \ge 1 + nh_n \Rarr 0 < h_n \le \dfrac{a - 1}{n} \rarr 0$
	$\Rarr \lim(h_n) = 0$
	$\Rarr  \lim(\root{n}\of{a}) = \lim(1 + h_n) = \lim(1) + \underset{nullsor}{\lim(h_n)} = 1 \checkmark$
Tfh: $0 < a < 1$, akkor $\dfrac{1}{a} > 1 \Rarr \root{n}\of{\frac{1}{a}} \rarr 1$
	$\Rarr \lim(\root{n}\of{a}) = \lim(\dfrac{1}{\root{n}\of{\frac{1}{a}}}) \rarr \dfrac{1}{1} = 1 \checkmark$

## 15. Pozitív szám $m$-edik gyökének előállítása rekurzív módon megadott sorozatok határértékével

**Tétel:**
Legyen $2 \le m \in \N$, Ekkor:

1. $\forall A > 0, \exists! \alpha > 0 : \alpha^n = A$
2. $\forall a_0 > 0, a_{n+1} := \dfrac{1}{m}\left(\dfrac{A}{a_n^{m-1}} + (m-1)a_n\right)\ \ (n \in \N)​$

Az így definiált sorozat konvergens és $\lim(a_n) = \alpha$

**Bizonyítás:**
$a_n > 0\ \ (n \in \N)​$, (ld:: Teljes indukció)
$\Rarr (a_n)​$ alulról korlátos, ill.:
$\dfrac{m}{a_{n+1}} = \left(\dfrac{\dfrac{A}{a_n^{m-1}}+(m-1)a_n}{m}\right) \overset{\text{számtani-mértani}}{\ge} \dfrac{A}{a_n^{m-1}}\cdot \underset{(m-1) db}{a_n \cdot a_n \cdot ... \cdot a_n} = A\ \ (n \in \N)​$
Azaz: $a_1 \ge A, a_2 \ge A, a_3 \ge A, ...​$
Mutassuk meg, hogy az $(a_{n+1})​$ elshiftelt sorozat monoton fogyó, azaz $\forall n \in \N : \dfrac{a_{n+2}}{a_{n+1}} \le 1​$
$\dfrac{a_{n+2}}{a_{n+1}} = \dfrac{1}{a_{n+1}} \cdot \dfrac{1}{m}\left(\dfrac{A}{a_{n+1}^{m-1}} + (m-1)a_{n+1}\right) = \dfrac{1}{m}\left(\dfrac{A}{a_{n+1}^{m}} + (m-1)\right) =​$

$= \dfrac{1}{m}\left(\dfrac{A - a_{n+1}^m}{a_{n+1}^{m}} + m\right) = \dfrac{\overset{\le 0}{A - a_{n+1}^m}}{\underset{\le 0}{m \cdot a_{n+1}^{m}}} + 1 \Rarr \le 1$, monoton fogyó
$\Rarr (a_{n+1})$ korlátos és monoton fogyó $\Rarr$ konvergens $\Rarr \exists \alpha \in \R : \lim(a_{n+1}) = \alpha$

