module genericMapSol

/*
  Genric map definition for assignment 3 in AFP 2018
  Pieter Koopman, pieter@cs.ru.nl
  September 2018
  
  Use StdEnv or iTask environment.
*/

import StdEnv, StdGeneric

generic gMap a b :: a -> b
gMap{|Int|}         x = x
gMap{|Real|}        x = x
gMap{|UNIT|}        x = x
gMap{|PAIR|}   f g (PAIR x y) = PAIR   (f x) (g y) 
gMap{|EITHER|} f g (LEFT x)   = LEFT   (f x)
gMap{|EITHER|} f g (RIGHT x)  = RIGHT  (g x)
gMap{|CONS|}   f   (CONS x)   = CONS   (f x)
gMap{|OBJECT|} f   (OBJECT x) = OBJECT (f x)

derive gMap [], Bin, (,)

:: Bin a = Leaf | Bin (Bin a) a (Bin a)
t = Bin (Bin Leaf 1 Leaf) 2 (Bin (Bin Leaf 3 Leaf) 4 Leaf)

fac n = prod [1..n]

Start = 
	(gMap{|*->*|} (\x.(x,fac x)) [1..10]
	,gMap{|*->*|} fac t
	,gMap{|*->*->*|} (gMap{|*->*|} fac) (gMap{|*->*|} fac)  ([1..10],t)
	
	,gEq{|*|} [1,2] [1,2]
	,gEq{|*|} [1,2] [1,2,3]
	,gEq{|*->*|} (<) [1,2] [1,2,3]
	,gEq{|*->*|} (<) [1,2] [2,3]
	)

import GenEq

Start2 = 
	(gEq{|*|} [1,2] [3,4]
	,gEq{|*->*|} (<) [1,2] [3,4]
	)
