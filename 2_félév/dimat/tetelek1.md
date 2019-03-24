# Alapok

## 1. Az unió tulajdonságai

- $A \cup B = B \cup A$
  $A \cup B \Leftrightarrow x \in B \or x \in A \Leftrightarrow x \in A \or x \in B \Leftrightarrow A \cup B$

- $A \cup (B \cup C) = (A \cup B) \cup C​$

  $(A \cup B) \cup C \Leftrightarrow  x \in (A \cup B) \or x \in C \Leftrightarrow$
  $\Leftrightarrow (x \in A \or x \in B) \or x \in C \Leftrightarrow x \in A \or (x \in B \or x \in C) \Leftrightarrow$
  $x \in A \or x \in (B \cup C) \Leftrightarrow A \cup (B \cup C)$

- $A \cup A = A$
  $A \cup A \Leftrightarrow x \in A \or x \in A \Leftrightarrow x \in A \Leftrightarrow A$

- $A \cup \emptyset = A$
  $A \cup \emptyset \Leftrightarrow x \in A \or x \in \emptyset \Leftrightarrow x \in A \Leftrightarrow A$

- $A \subseteq B \Leftrightarrow A \cup B = B$
  $A \subseteq B \Leftrightarrow \forall x : (x \in A \Rarr x \in B) \ \ \checkmark$
  $A \subseteq (A \cup B) \Leftrightarrow \forall x : (x \in A \Rarr (x \in A \or x \in B)) \Leftrightarrow \forall x : (x \in A \Rarr x \in B)\ \checkmark$

## 2. A metszet tulajdonságai

*bizonyítások hasonlóan mint az uniónál*

- $A \cap B = B \cap A$
- $(A \cap B) \cap C = A \cap (B \cap C)$
- $A \cap A = A$
- $A \cap \emptyset = \emptyset$
- $A \subseteq B \Leftrightarrow A \cap B = A$

## 3. Az unió és metszet disztributivitási tulajdonságai

- $A \cap (B \cup C) = (A \cap B) \cup (A \cap C)$
  $A \cap (B \cup C) \Leftrightarrow x \in A \and x \in (B \cup C) \Leftrightarrow x \in A \and (x \in B \or x \in C) \Leftrightarrow$
  $(x \in A \and x \in B) \or (x \in A \and x \in C) \Leftrightarrow x \in (A \cap B) \or x \in (A \cap C)$
  $ \Leftrightarrow (A \cap B) \cup (A \cap C)$

- $A \cup (B \cap C) = (A \cup B) \cap (A \cup C)$
  *az előző alapján*

## -4. Komplementer tulajdonságai

*A fenti bizonyításokhoz hasonlóan, csak fel kell írni kvantorokkal*
$x$ az alaphalmaz / univerzum:

- $ \overline{\emptyset} = x$
- $\overline{x} = \emptyset$
- $A \cup \overline{A} = x$
- $A \cap \overline{A} = \emptyset​$
- $\overline{\overline{A}} = A$
- $A \subseteq B \Leftrightarrow \overline{B} \subseteq \overline{A}$
- $\overline{A \cap B} = \overline{A} \cup \overline{B}$
- $\overline{A \cup B} = \overline{A} \cap \overline{B}$

## 5. Relációk kompozíciójának tulajdonságai

- **Kompozíció asszociatív:**
  $(R \circ S) \circ T = R \circ (S \circ T)$
  $(R \circ S) \circ T \Leftrightarrow (x, w) \in (R \circ S) \circ T \Leftrightarrow \exists y : (x, y) \in T \and (y, w) \in (R \circ S) \Leftrightarrow$
  $\exists y : (x, y) \in T \and \exists z : (y, z) \in S \and (z, w) \in R \Leftrightarrow $
    $\exists z ,\exists y : (x, y) \in T \and (y, z) \in S \and (z, w) \in R \Leftrightarrow $

  $\exists z : (x, z) \in (S  \circ T) \and (z, w) \in R \Leftrightarrow $
  $(x, w) \in R \circ (S  \circ T) \Leftrightarrow $
  $R \circ (S \circ T)$

- **Kompozíció inverze:**
  $(R \circ S)^{-1} = S^{-1} \circ R^{-1}​$
  $(z, x) \in (R \circ S)^{-1} \Leftrightarrow (x, z) \in (R \circ S) \Leftrightarrow ​$
  $\exists y : (x, y) \in S \and (y, z) \in R \Leftrightarrow \exists y : (y, x) \in S^{-1} \and (z, y) \in R^{-1} \Leftrightarrow​$

  $S^{-1} \circ R^{-1}$

## ---6. Állítás, amely kimondja, hogy függvények kompozíciója is függvény



## 7. Állítás, amely kimondja, hogy injektív függvények kompozíciója is injektív

Ha $f$ és $g$ injektív, akkor $f \circ g$ is injektív.

**Bizonyítás:** 
$f$ injektív $\Rarr x_1 \ne x_2 \Leftrightarrow f(x_1) \ne f(x_2)$
$x_1 \ne x_2 \overset{g \text{ injektív}}{\Rarr} g(x_1) \ne g(x_2) \overset{f \text{ injektív}}{\Rarr} f(g(x_1)) \ne f(g(x_2)) \Leftrightarrow (f \circ g)(x_1) \ne (f\circ g)(x_2)$

 # Komplex számok

## 8. Hányados kiszámítása algebrai alakban

Ha $z, w \in \C$ és $z = a + bi, w = c + di$ akkor $\dfrac{z}{w} = \dfrac{ac + bd}{c^2 + d^2} + \dfrac{bc - ad}{c^2 + d^2}i$

**Bizonyítás:**
$\dfrac{z}{w} \Leftrightarrow \dfrac{a + bi}{c + di} \Leftrightarrow \dfrac{a + bi}{c + di} \cdot \dfrac{c - di}{c - di} \Leftrightarrow \dfrac{(a + bi) \cdot (c - di)}{c^2 - d^2 \cdot i^2} \Leftrightarrow \dfrac{ac - adi + bci - adi^2}{c^2 + d^2} \Leftrightarrow$
$\dfrac{ac + bd + i(bc - ad)}{c^2 + d^2} \Leftrightarrow \dfrac{ac + bd}{c^2 + d^2} + \dfrac{bc - ad}{c^2 + d^2}i​$

## ---9. A konjugálás és abszolút érték tulajdonságai

Ha $z,w \in \C$ és $z = a + b i, w = c + d i$, akkor:

- $z \cdot \overline{z} = | z | ^2$
- $\dfrac{1}{z} = \dfrac{\overline{z}}{|z|^2} = \dfrac{a - b i}{a^2 + b^2}$
- $\overline{z_1 \cdot z_2} = \overline{z_1} \cdot \overline{z_2}​$
- $\overline{z_1 + z_2} = \overline{z_1} + \overline{z_2}$
- $|z_1 \cdot z_2 | = |z_1| \cdot |z_2|$
- $|z| = \overline{z}$
- Háromszög egyenlőtlenség:
  $|z_1 + z_2| \le |z_1| + |z_2|$
- $|z \cdot w| = |z| \cdot |w|$
- Háromszög egyenlőtlenség:
  $|z + w| \le |z| + |w|$ 
- $\arg(z_1 \cdot z_2) = \arg z_1 + \arg z_2$
- ??

## 10. Szorzásra vonatkozó Moivre-azonosság

Ha $r_1, r_2 \ne 0​$ és $z_1 = r_1(\cos \varphi_1 + i\sin \varphi_1), z_2 = r_2(\cos\varphi_2 + i\sin \varphi_2)​$, akkor
$z_1 \cdot z_2 = r_1 \cdot r_2(\cos(\varphi_1 + \varphi_2) + i \sin(\varphi_1 + \varphi_2))​$

**Bizonyítás:**
$z_1 \cdot z_2 = r_1(\cos \varphi_1 + i\sin \varphi_1) \cdot r_2(\cos\varphi_2 + i\sin \varphi_2) \Leftrightarrow$
$r_1 \cdot r_2 ((\cos\varphi_1 \cdot \cos \varphi_2 - \sin \varphi_1 \cdot \sin \varphi_2) + (\cos \varphi_1 \cdot \sin \varphi_2 + \cos \varphi_2 \cdot \sin \varphi_1)i) \Leftrightarrow$
$\overset{\text{addíciós képletek}}{\Leftrightarrow} r_1 \cdot r_2 (\cos(\varphi_1 + \varphi_2) + i\sin(\varphi_1 + \varphi_2))$

