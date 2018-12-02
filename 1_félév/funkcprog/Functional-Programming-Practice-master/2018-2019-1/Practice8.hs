module Practice8 where

import Prelude hiding (Maybe(..), find, lookup, update, delete)

data Maybe a = Nothing | Just a
  deriving (Show, Eq)

find :: (a -> Bool) -> [a] -> Int
find p = findInd p . zip [0..] where 

  findInd :: (a -> Bool) -> [(Int,a)] -> Int
  findInd _ [] = -1
  findInd p ((ind,e):xs) 
    | p e       = ind
    | otherwise = findInd p xs

findM :: (a -> Bool) -> [a] -> Maybe Int
findM p xs = if ind < 0 then Nothing else Just ind where
  ind = find p xs

isJust :: Maybe a -> Bool 
isJust (Just _) = True
isJust _        = False

isNothing :: Maybe a -> Bool 
isNothing Nothing = True
isNothing _       = False

eqMaybe :: Eq a => Maybe a -> Maybe a -> Bool
eqMaybe (Just x) (Just y) = x == y
eqMaybe Nothing  Nothing  = True
eqMaybe _        _        = False

safeHead :: [a] -> Maybe a
safeHead []     = Nothing
safeHead (x:xs) = Just x

safeTail :: [a] -> Maybe [a]
safeTail []     = Nothing
safeTail (x:xs) = Just xs

lookup :: Eq k => k -> [(k,v)] -> Maybe v
{-
lookup _ [] = Nothing 
lookup k ((a,b):xs)
  | k == a    = Just b
  | otherwise = lookup k xs
-}
lookup k = safeHead . map snd . filter ((==k) . fst)

update :: Eq k => (v -> Maybe v) -> k -> [(k,v)] -> [(k,v)]
update f k [] = []
update f k (x@(key,val):xs)
  | k /= key = x : update f k xs
  | k == key = case f val of 
                 Just val' -> (key,val') : xs
                 Nothing   -> xs

delete :: Eq k => k -> [(k,v)] -> [(k,v)]
delete k = update (const Nothing) k 


data Privilege = User | Admin
  deriving (Show, Eq)

type Username = String
type Password = String
type Database = [((Username,Password), Privilege)]

data Cookie = LoggedOut | LoggedIn Username Privilege 
  deriving (Show, Eq)

-- k ~ (Username,Password)
-- v ~ Privilege
login :: Username -> Password -> Database -> Cookie
login user pass db = case lookup (user,pass) db of 
  Nothing   -> LoggedOut 
  Just priv -> LoggedIn user priv

trfDb :: [((Username,Password), Privilege)] -> [(Username, (Password,Privilege))]
trfDb = map (\((x,y),z) -> (x,(y,z)))

trfDbBack :: [(Username, (Password,Privilege))] -> [((Username,Password), Privilege)]
trfDbBack = map (\(x,(y,z)) -> ((x,y),z))

deleteUser :: Username -> Cookie -> Database -> Database
deleteUser target (LoggedIn _ Admin) db = trfDbBack . delete target . trfDb $ db
deleteUser _ _ db = db


-- practice
-- fromMaybe :: a -> Maybe a -> a

-- maybe :: b -> (a -> b) -> Maybe a -> b 

-- catMaybes :: [Maybe a] -> [a]

-- mapMaybe :: (a -> Maybe b) -> [a] -> [b]

