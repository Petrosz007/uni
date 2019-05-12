# Bizonyítással kért tételek a 2.  zh-n

## 1. A geometriai sor konvergenciája

**Tétel:**

Legyen $q \in \R$. Ekkor ha $\sum q^n$ konvergens $\Leftrightarrow |q| < 1$

Ekkor a részsorösszeg: $\overset{\infty}{\underset{n=0}{\sum}}q^n = \dfrac{1}{1-q}$

**Bizonyítás:**

$q = 1$ esetén:

​		$s_n = \overset{n}{\underset{k=0}{\sum}}1^k = n+1$  $(n \in \N)$ miatt $(s_n)$ divergens $\Rarr$ $\sum q^n$ is divergens

$q \in \R \text{\\}\{1\} $, akkor:

​		$1-q^{n+1} = (1-q)(1+q+q^2+...+q^n)$ miatt
​		$s_n = \overset{n}{\underset{k=0}{\sum}} q^k = 1+q+q^2+...+q^n = \dfrac{1-q^{n+1}}{1-q}\ \ (n \in \N)$

​		Tehát: $(s_n)$ konvergens $\Leftrightarrow$ $(q^{n+1}) = q(q^n)$ konvergens
​		De $(q^n)$ konvergens $\overset{q \ne 1}{\Leftrightarrow} |q| < 1$ 
​		És ekkor:

​			$\overset{\infty}{\underset{n=0}{\sum}}q^n = \lim(s_n) = \lim(\dfrac{1-q^{n+1}}{1-q}) = \dfrac{1}{1-q}$

## 2. A végtelen sorokra vonatkozó Cauchy-féle konvergenciakritérium

**Tétel:**

$\sum a_n$ sor Cauchy, ha:
		$\forall \epsilon > 0, \exists N \in \N, \forall n, m \in \N, m \gt n \ge N : |a_{n+1} + a_{n+2} + ... + a_m | < \epsilon$

$\sum a_n$ sor konvergens $\Leftrightarrow \sum a_n$ Cauchy

**Bizonyítás:**

$\sum a_n$ konvergens $\Leftrightarrow (s_n)$ konvergens $\Leftrightarrow (s_n)$ Cauchy-sorozat, azaz:
		$\forall \epsilon > 0, \exists N \in \N, \forall n,m \in \N, m\gt n \ge N :$
				$\underbrace{|s_m - s_n|} < \epsilon$
			$|a_0 + a_1 + ... + a_{n-1} + a_n + ... a_m - (a_0 + a_1 + ... + a_n)| =$
			$|a_{n+1} + a_{n+2}  + ... + a_m| \Leftrightarrow \sum a_n$ sor Cauchy

## 3. Végtelen sorokra vonatkozó összehasonlító kritériumok

**Tétel:**

Legyen $\sum a_n, \sum b_n$ pozitív tagú sorok, melyekre $\exists N \in \N, \forall n \in \N, n \ge N: 0 \le a_n \le b_n$

1. Ekkor ha $\sum b_n$ konvergens, akkor $\sum a_n$ is konvergens
2. Ekkor ha $\sum a_n$ divergens, akkor $\sum b_n$ is divergens

**Bizonyítás:**

Tfh.: $n \ge N$, legyenek
	$s_n^a := a_N + a_{N+1} + ... a_n$
	$s_n^b := b_N + b_{N+1} + ... + b_n$

Mivel $\forall n \in \N, n \ge N: a_n \le b_n$, ezért $\forall n \in \N, n \ge N: s_n^a \le s_n^b$

1. Ha $\sum b_n$ konvergens $\Rarr$ $(s_n^b)$ korlátos $\overset{a_n \le b_n}{\Rarr}$ $(s_n^a)$ is korlátos

   $\Rarr \underset{n=N}{\sum a_n}$ konvergens $\Rarr \sum a_n$ konvergens

2. $\sum a_n$ divergens $\Rarr \underset{n=N}{\sum} a_n$ divergens $(s_n^a)$ nem felülről korlátos
   $\Rarr (s_n^b)$ sem felülről korlátos $\Rarr \underset{n=N}{\sum} b_n$ divergens $\Rarr \sum b_n$ divergens

## 4. A Cauchy-féle gyökkritérium

## 5. A D'Alembert-féle hányados-kritérium

## 6. Leibniz-típusú sorok konvergenciája

## 7. Minden $[0,1]$-beli szám előállítható tizedestört alakban

## 8. Abszolút konvergens sorok átrendezése

## 9. Abszolút konvergens sorok szorzására vonatkozó tétel

## 10. Hatványsorok konvergenciahalmazára vonatkozó, a konvergenciasugarát meghatározó tétel

## 11. A Cauchy-Hadamard-tétel

## 12. Függvények határértékének egyértelműsége

## 13. A határértékre vonatkozó átviteli elv

## 14. Hatványsorok konvergenciája

## 15. Monoton függvények határértéke





