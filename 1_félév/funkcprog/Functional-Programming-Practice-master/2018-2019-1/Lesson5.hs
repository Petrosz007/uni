module Lesson5 where 

import Prelude hiding (minimum, (++), concat, zip, isPrefixOf, elem, take, drop)

minimum :: Ord a => [a] -> a
minimum (x:xs) = x `min` (minimum xs)

(++) :: [a] -> [a] -> [a]
(++) [] ys = ys
(++) (x:xs) ys = x : (xs ++ ys) 

concat :: [[a]] -> [a]
concat [] = []
concat (x:xs) = x ++ concat xs 

zip :: [a] -> [b] -> [(a,b)]
zip xs [] = []
zip [] ys = []
zip (x:xs) (y:ys) = (x,y) : zip xs ys 

isPrefixOf :: Eq a => [a] -> [a] -> Bool 
isPrefixOf [] _ = True 
isPrefixOf (x:xs) (y:ys) = x == y && isPrefixOf xs ys 

elem :: Eq a => a -> [a] -> Bool 
elem x [] = False 
elem x (y:ys) = x == y || x `elem` ys 

take :: Int -> [a] -> [a]
take 0 xs = []
take n [] = [] 
take n (x:xs)
  | n < 0 = [] 
  | otherwise = x : take (n-1) xs

drop :: Int -> [a] -> [a]
drop 0 xs = xs
drop n [] = [] 
drop n l@(x:xs)
  | n < 0 = l 
  | otherwise = drop (n-1) xs
