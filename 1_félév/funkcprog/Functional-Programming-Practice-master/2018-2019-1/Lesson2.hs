module Lesson2 where

-- simple functions

square :: Int -> Int
square n = n * n

distance :: Int -> Int -> Int
distance x y = abs (x - y)



-- currying
-- Notice that these functions are defined without giving argments!
-- They are defined in terms of other functions.
-- simple example: f = g

plusOne :: Int -> Int
plusOne = (1+)

double :: Int -> Int
double = (2*)

distanceFrom5 :: Int -> Int
distanceFrom5 = distance 5



-- precedence (use :i in GHCi)
-- precedence ranges from 0 to 9
-- infix notation ~ precedence of 7
-- function application ~ precedence of 10

expr1, expr2, expr3, expr4, expr5, expr6, expr7 :: Int
expr8, expr9, expr10 :: Bool

expr1 = ((1 + 2) + 3) + 4

expr2 = 2 ^ (3 ^ 5)

expr3 = (2 ^ 3) + (5 * 7)

expr4 = 10 `div` 3 + 10
expr5 = 10 `div` 3 * 10
expr6 = 10 `div` 3 ^ 10
expr7 = div 10 3 ^ 10

expr8 = (True && False) || True

expr9 = (1 == 5) && True

expr10 = (1 == (5 + 7)) && True

exprWithNegation = (-1) + (-2) * (-3)

--incorrectExpr = 1 == 2 == 3 --> 1 == 2 && 2 == 3


-- pattern matching

not' :: Bool -> Bool
not' True = False
not' _    = True

and' :: Bool -> Bool -> Bool
and' True True = True
and' _    _    = False

or' :: Bool -> Bool -> Bool
or' False False = False
or' _     _     = True


-- tuples

tuple1 = (1,2)

tuple2 = (1,'a')

tuple3 = ('a', True)

tuple4 = (1, 'a', True)

distSqrFromOrigo :: (Int, Int) -> Int
distSqrFromOrigo (x,y) = x^2 + y^2

coordDistanceSqr :: (Int, Int) -> (Int, Int) -> Int
coordDistanceSqr (x1,y1) (x2, y2) = (distance x1 y1)^2 + (distance x2 y2)^2


-- fst
first :: (a,b) -> a
first (x,_) = x

-- snd
snd :: (a,b) -> b
snd (_,y) = y

isEvenTuple :: Int -> (Int, Bool)
isEvenTuple n = (n, even n)

triplicate :: a -> (a,a,a)
triplicate x = (x,x,x)

swap :: (a,b) -> (b,a)
swap (x,y) = (y,x)

doubleTheTuple :: (a, b) -> ((a,b), (a,b))
doubleTheTuple x = (x,x)



-- where clause

f :: Int -> Int -> Int
f x y = x^2 + 3*y + 4*x*y

-- reduce redundancy
-- we can reference the arguments in the where clause
g :: Int -> Int -> Int
g x y = f z z
  where z = 3*x + 7*y

-- introduce local helper functions
-- we can shadow already existing arguments
h :: Int -> Int -> Int
h x y = f2 x y + f2 y x
  where f2 x y = f x x + f y y
