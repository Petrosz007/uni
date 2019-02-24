{-# LANGUAGE FlexibleInstances #-}
module Practice2 where

import Data.Monoid
import Data.Semigroup
import Data.Foldable

data Nat = Zero | Suc Nat 
  deriving Show

zero :: Nat 
zero = Zero 

three :: Nat 
three = Suc (Suc (Suc Zero))

-- neutral element (n)
--  forall x . n ˙op˙ x = x 
--  forall x . x ˙op˙ n = x 
addNat :: Nat -> Nat -> Nat 
addNat Zero n = n
addNat n Zero = n
addNat (Suc n) m = Suc (n `addNat` m) 

-- (n + 1) * m = n * m + m
mulNat :: Nat -> Nat -> Nat 
mulNat Zero n = Zero
mulNat n Zero = Zero
mulNat (Suc n) m = m `addNat` (n `mulNat` m)

instance Num Nat where 
  (+) = addNat
  (*) = mulNat

  -- fromInteger :: Integer -> a (a ~ Nat)
  fromInteger 0 = Zero 
  fromInteger n 
    | n > 0     = Suc (fromInteger $ n-1)
    | otherwise = error "Cannot convert negative integer to Nat" 

  abs = id

  signum Zero = Zero 
  signum _    = Suc Zero

  -- not total
  (-) n Zero = n 
  (-) (Suc n) (Suc m) = n - m

{-
class Semigroup a where
  -- mappend
  -- associative
  (<>) :: a -> a -> a

class Semigroup m => Monoid m where
  mempty :: m 

newtype Sum a     = Sum     { getSum     :: a }
newtype Product a = Product { getProduct :: a }

instance Num a => Semigroup (Sum a) where 
  (<>) = (+)

instance (Num a, Monoid) => Monoid (Sum a) where 
  mempty = 0
-}

data List a = Nil | Cons a (List a) 
  deriving Show

(+++) :: List a -> List a -> List a
(+++) Nil xs = xs
(+++) xs Nil = xs
(+++) (Cons x xs) ys = Cons x (xs +++ ys)

instance Semigroup (List a) where 
  (<>) = (+++)

instance Monoid (List a) where
  mempty = Nil

map' :: (a -> b) -> List a -> List b
map' f Nil = Nil
map' f (Cons x xs) = Cons (f x) (map' f xs)

instance Foldable List where
  foldMap f = fold . map' f

  fold Nil = mempty
  fold (Cons x xs) = x <> fold xs