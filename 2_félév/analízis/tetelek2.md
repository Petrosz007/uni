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

**Tétel:**

Tekintsük a $\sum a_n​$ sort és tfh.: $\root{n}\of{|a_n|}​$ sorozat konvergens és $A := \lim(\root{n}\of{|a_n|})​$
Ekkor:

1. Ha $0 \le A \lt 1$, akkor $\sum a_n$ abszolút konvergens, tehát konvergens is
2. Ha $1 < A$, akkor $\sum a_n$ divergens
3. Ha $A = 1$, akkor lehet konvergens és divergens is

**Bizonyítás:**

**1,**
 $0 \le A < 1​$ esetén $\exists q \in \R​$, hogy $A < q < 1​$ 
Mivel $\lim(\root{n}\of{|a_n|}) = A​$, ezért:

​	$\exists N \in \N, \forall n \in \N, n \ge N: \root{n}\of{|a_n|} < q < 1​$
$\Rarr\forall n \in \N, n \ge N: |a_n| < q^n​$

Mivel $0 \le A < q < 1$, ezért $\sum q^n$ konvergens $\Rarr \underset{n=N}{\sum} q^n$ konvergens
De: $\underset{n=N}{\sum} q^n$ majorálja a $\underset{n=N}{\sum} |a_n|$ sort
$\Rarr$ (ld.: Majoráló kritérium) $\underset{n=N}{\sum} |a_n|$ konvergens $\Rarr \sum |a_n|$ konvergens
$\Rarr \sum a_n$ abszolút konvergens

**2,**
Tfh.: $A > 1$. Ekkor $\exists q \in \R : 1 < q < A$
$\lim(\root{n}\of{|a_n|}) = A$ miatt:

​	$\exists N \in \N, \forall n \in \N, n \ge N: \root{n}\of{|a_n|} > q​$
$\Rarr \forall n \in \N, n \ge N : |a_n| > q^n > 1​$
$\Rarr \lim(a_n) \ne 0 \Rarr \sum a_n​$ divergens

**3,**
Tekintsük a $\underset{n = 1}{\sum} \dfrac{1}{n^2}​$ konvergens sort és a $\underset{n = 1}{\sum} \dfrac{1}{n}​$ divergens sort.
Ekkor:

​	$\root{n}\of{\left| \dfrac{1}{n^2} \right|} = \left(\dfrac{1}{\root{n}\of{n}}\right)^2 \rarr 1$    $(n \rarr \infty)$

​	$\root{n}\of{\left| \dfrac{1}{n} \right|} = \dfrac{1}{\root{n}\of{n}} \rarr 1​$    $(n \rarr \infty)​$

## 5. A D'Alembert-féle hányados-kritérium

**Tétel:**

Tekintsük a $\sum a_n​$ sort, ahol $a_n \ne 0\ \ (n\in \N)​$ és tfh.: $\left(\dfrac{|a_{n+1}|}{|a_n|}\right)​$ sorozat konvergens és $A := \lim\left(\dfrac{|a_{n+1}|}{|a_n|}\right)​$
Ekkor:

1. Ha $0 \le A \lt 1$, akkor $\sum a_n$ abszolút konvergens, tehát konvergens is
2. Ha $1 < A$, akkor $\sum a_n$ divergens
3. Ha $A = 1​$, akkor lehet konvergens és divergens is

**Bizonyítás:**

**1,**
Tfh.: $A < 1 \Rarr \exists q \in \R : A < q < 1​$
$\lim\left(\dfrac{|a_{n+1}|}{|a_n|}\right) = A​$ miatt:

​	$\exists N \in \N, \forall n \in \N, n \ge N:\dfrac{|a_{n+1}|}{|a_n|} < q​$
Tehát: $|a_{n+1}| < q|a_n|​$
​		$|a_n| < q|a_{n-1}|​$
​		$|a_{n-1}| < q|a_{n-2}|​$
​			...
​		$|a_{n_0+1}| < q|a_{n_0}|​$
$\Rarr \forall n \in \N, n \ge n_0 : |a_{n+1}| < \underset{< q|a_{n+1}|}{\underbrace{q|a_n|}} < q^2|a_{n+1}| < ...  < q^{n+1-n_0}\cdot |a_{n_0}| =​$
$= \underset{= K > 0}{\underbrace{q^{1-n_0} \cdot |a_{n_0}|}} \cdot q^n​$

$\Rarr q \in (0,1) $ miatt $\sum q^n$ konvergens $\Rarr \underset{n=n_0}{\sum} q^n$ konvergens
De! $\underset{n=n_0}{\sum} q^n$ majorálja a $\sum |a_{n+1}|$ sort $\Rarr \underset{n=n_0}{\sum}|a_{n+1}|$ konvergens $\Rarr$
	$\Rarr \sum |a_{n+1}|$ konvergens $\Rarr \sum |a_n|$ konvergens $\Rarr \sum a_n$ abszolút konvergens

**2,**
Tfh.: $A > 1$. Ekkor: $\exists q \in \R : A > q > 1$
$\Rarr \sum n_0 \in \N, \forall n \in \N, n \ge n_0 : \dfrac{|a_{n+1}|}{|a_n|} > q$
$\Rarr |a_{n+1}| > K \cdot q^n > K > 0$    $(n \ge n_0)$
$\Rarr \lim(a_{n+1}) \ne 0 \Rarr \lim(a_n) \ne 0 \Rarr\sum a_n$ divergens

**3,**Tekintsük a $\underset{n = 1}{\sum} \dfrac{1}{n^2}$ konvergens sort és a $\underset{n = 1}{\sum} \dfrac{1}{n}$ divergens sort.
Ekkor:

​	$\dfrac{\left|\dfrac{1}{(n+1)^2}\right|}{\left|\dfrac{1}{n^2}\right|} = \dfrac{n^2}{(n+1)^2} \rarr 1​$    $(n \rarr \infty)​$

​	$\dfrac{\left|\dfrac{1}{n+1}\right|}{\left|\dfrac{1}{n}\right|} = \dfrac{n}{n+1} \rarr 1$    $(n \rarr \infty)​$

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





