module Rationals where

import Data.List

nextRational :: Rational -> Rational
nextRational x = 1 / (2*y + 1 - x)
  where y = fromIntegral (floor x)

positiveRationals :: [Rational]
positiveRationals = iterate nextRational 1

rationals :: [Rational]
rationals = 0 : merge pos neg where 
  pos = positiveRationals 
  neg = map negate pos

  merge :: [a] -> [a] -> [a]
  merge xs ys = concatMap (\(a,b) -> [a,b]) $ zip xs ys
  
rationalIndex :: Rational -> Int
rationalIndex r = ind where 
  indexedRationals = zip [0..] rationals
  Just (ind,_) = find ((==r) . snd) indexedRationals 
  
index :: Eq a => a -> [a] -> Int
index x [] = 0
index x (y:ys)
  | x == y = 0
  | otherwise = 1 + index x ys