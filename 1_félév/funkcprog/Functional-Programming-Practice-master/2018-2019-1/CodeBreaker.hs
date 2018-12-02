module CodeBreaker where

import Data.List

blackStones :: Eq a => [a] -> [a] -> Int
blackStones xs ys = length $ filter id $ zipWith (==) xs ys

whiteStones :: Eq a => [a] -> [a] -> Int
whiteStones xs ys = (fst $ foldl f (0,ys) xs) - blackStones xs ys where
  f (n,ys') cur
    | cur `elem` ys' = (n + 1, ys' \\ [cur])
    | otherwise      = (n, ys')

data Code = Code Char Char Char Char

readCode :: String -> Maybe Code
readCode [a,b,c,d] = Just $ Code a b c d
readCode _         = Nothing

toList :: Code -> String
toList (Code a b c d) = [a,b,c,d]

whiteAndBlackStones :: Code -> String -> (Int,Int)
whiteAndBlackStones code guess 
  | length guess /= 4 = (0,0)
  | otherwise = (ws,bs) where

  code' = toList code
  ws    = whiteStones code' guess
  bs    = blackStones code' guess