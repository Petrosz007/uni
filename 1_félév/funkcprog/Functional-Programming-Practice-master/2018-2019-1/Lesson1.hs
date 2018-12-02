module Lesson1 where

-- interpreter as calculcator

-- simple types: Int, Char, Bool

-- simple literals of simple types (and constants)
-- negation for numerical literal
-- numerical literals in Haskell are compicated, we will only consider Ints

five :: Int
five = 5

six :: Int
six = five + 1

zee :: Char
zee = 'z'

true :: Bool
true = True

-- simple functions over simple types:

-- function signature
-- function arity
-- infix functions, operators (unary, binary)

-- Int: (+), (-), (*), negate, div, mod
-- Bool: (&&), (||), not
-- Char: toUpper, toLower

-- simple functions between types:
-- (==), (/=), (<)

-- custom functions:

plusOne :: Int -> Int
plusOne n = n + 1

double :: Int -> Int
double n = 2 * n

quadruple :: Int -> Int
quadruple n = double (double n)





isZero :: Int -> Bool
isZero n = n == 0

nonZero :: Int -> Bool
nonZero n = not (isZero n)

isEven :: Int -> Bool
isEven n = n `mod` 2 == 0

isOdd :: Int -> Bool
isOdd n = n `mod` 2 == 1


returnFirst :: Char -> Int -> Char
returnFirst x y = x



isDivisibleBy :: Int -> Int -> Bool
isDivisibleBy n k = n `mod` k == 0

isDiv4 :: Int -> Bool
isDiv4 n = n `isDivisibleBy` 4

isDiv100 :: Int -> Bool
isDiv100 n = n `isDivisibleBy` 100

isDiv400 :: Int -> Bool
isDiv400 n = n `isDivisibleBy` 400

isLeapYear :: Int -> Bool
isLeapYear n = isDiv4 n && (not (isDiv100 n) || isDiv400 n)
