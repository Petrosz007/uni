## Mit jelent az $f : A \rarr B$ szimbólum?

$\emptyset \ne A, B$ esetén az $f : A \rarr B$ szimbólum egy olyan függvény, amelyre:
$D_f = A $ és $R_f \subset B$

## Mit jelent az $f \in A \rarr B$ szimbólum?

$\emptyset \ne A, B$ esetén az $f \in A \rarr B$ szimbólum egy olyan függvény, amelyre:
$D_f \subset A $ és $R_f \subset B$

## Hogyan értelmezzük halmaznak függvény által létesített képét?

Legyen $\emptyset \ne A, B, C$  $f: A \rarr B, C \subset A$  Ekkor a $C$ halmaz $f$ által létesített képe:
$f[C] := \{f(x) \in B : x \in C\} \subset B$

## Definiálja halmaznak függvény által létesített ősképét?

Legyen $\emptyset \ne A,B,D$  $f : A \rarr B, D \subset B$  Ekkor $D$ halmaz $f$ által létesített ősképe:
$f^{-1}[D] := \{ x \in A : f(x) \in D \} \subset A$

## Mikor nevezünk egy függvényt invertálhatónak?

$f \in A \rarr B$  invertálható, ha:
$\forall x,y \in D_f, x \ne y : f(x) \ne f(y)$

## Definiálja az inverz függvényt.

Legyen $f : A \rarr B$ invertálható függvény. Ekkor $f$ inverz függvénye $f^{-1}$, ahol $f^{-1} : B \rarr A, D_{f^{-1}} = R_f, R_{f^{-1}} = D_f$ és $B \ni y \longmapsto^{f^{-1}} x \in A$ , amelyre $f(x) = y$

## Írja le az összetett függvény fogalmát.

Legyen $f : A \rarr B, g : C \rarr D$ és $R_g \cap D_f \ne \emptyset$ Ekkor $f$ és $g$ összetett függvénye:
$f \circ g : \{ x \in D_g : g(x) \in D_f \} \rarr B,$   $(f \circ g)(x) := f(g(x))$

