module Lesson8 where

import Prelude hiding (takeWhile, dropWhile, span, reverse, nub)

count :: (a -> Bool) -> [a] -> Int
count p = length . filter p

takeWhile :: (a -> Bool) -> [a] -> [a]
takeWhile p [] = []
takeWhile p (x:xs)
  | p x       = x : takeWhile p xs 
  | otherwise = []

dropWhile :: (a -> Bool) -> [a] -> [a]
dropWhile p [] = []
dropWhile p l@(x:xs)
  | p x       = dropWhile p xs 
  | otherwise = l


-- recursion with accumulating param

-- more optimal version of reverse
reverse :: [a] -> [a]
reverse = reverse' [] where 
  reverse' :: [a] -> [a] -> [a]
  reverse' done [] = done 
  reverse' done (x:xs) = reverse' (x:done) xs 

-- no duplicates
nub :: Eq a => [a] -> [a]
nub = reverse . nub' [] where 
  nub' :: Eq a => [a] -> [a] -> [a]
  nub' done [] = done 
  nub' done (x:xs)
    | x `elem` done = nub' done xs 
    | otherwise     = nub' (x:done) xs

span :: (a -> Bool) -> [a] -> ([a],[a])
span p xs = (reverse ys, zs) where
  (ys,zs) = span' [] p xs 

  span' :: [a] -> (a -> Bool) -> [a] -> ([a],[a])
  span' done p [] = (done,[])
  span' done p l@(x:xs)
    | p x       = span' (x:done) p xs
    | otherwise = (done,l)  