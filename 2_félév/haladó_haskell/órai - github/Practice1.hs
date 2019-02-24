module Practice1 where

import Prelude hiding (Maybe(..))

-- synonym
-- OverloadedString
type Name = String

-- compile time difference
-- no runtime overhead 
newtype TaggedName = N Name 

-- algebraic data type
-- Nat       ~ type ctor
-- Zero, Suc ~ data ctor
data Nat = Zero | Suc Nat

data Maybe a = Nothing | Just a 
  deriving Show

data Email = Email { _address :: String
                   , _domain  :: String 
                   }
  deriving Show

data Person = Person { _name  :: Name
                     , _age   :: Int
                     , _email :: Maybe Email
                     } 
  deriving Show

{-
_name :: Person -> Name 
_name (Person n _ _) = n 

_age :: Person -> Int 
_age (Person _ age _) = age

_email :: Person -> String
_email (Person _ _ email) = email
-}

-- parameterized
-- unit ~ Nil
-- union ~ |
-- product ~ Cons _ _
data List a = Nil | Cons a (List a) 
  deriving Show

length' :: List a -> Nat
length' Nil = Zero
length' (Cons x xs) = Suc $ length' xs

addNat :: Nat -> Nat -> Nat
addNat Zero n = n 
addNat n Zero = n 
addNat (Suc n) m = Suc (n `addNat` m)

sum' :: List Nat -> Nat
sum' Nil = Zero 
sum' (Cons n xs) = n `addNat` (sum' xs)

instance Show Nat where
  --show :: Nat -> String
  show Zero    = "Zero"
  show (Suc n) = "Suc (" ++ show n ++ ")" 

instance Eq Nat where 
  (==) (Suc n) (Suc m) = n == m
  (==) Zero    Zero    = True
  (==) _       _       = False

instance Ord Nat where 
  (<=) Zero _          = True 
  (<=) (Suc n) (Suc m) = n <= m
  (<=) (Suc _) Zero    = False 

instance Eq a => Eq (List a) where 
  (==) Nil Nil = True
  (==) (Cons x xs) (Cons y ys) = x == y && xs == ys
  (==) _ _ = False