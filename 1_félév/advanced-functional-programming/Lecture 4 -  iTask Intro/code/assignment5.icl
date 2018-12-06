module assignment5

/*
	Advanced Programming.
	Skeleton for assignment 4.
	To be used in a project with the environment iTasks.
	Pieter Koopman, pieter@cs.ru.nl

	Make sure the "iTasks-SDK" folder is in one of the search locations of the executable:
	.
	..
	..\..
	..\..\..
	..\..\..\..
	..\..\..\..\..
	C:\Clean 2.4
	C:\Program Files
	A convenient way to do this is putting this project in a (sub)folder 
	of iTask-SDK in the Clean 2.4 folder.
	
	You can also use the -sdk commandline flag to set the path.
	Example: -sdk C:\Users\johndoe\Desktop\Clean2.4\iTasks-SDK
*/

import iTasks, StdArray

:: Idea	:== Maybe Note //String
:: Name	:== String

:: NumIdea =
	{ note :: Idea
	, numb :: Display Int
	}

:: NamedIdea = { name :: Name, idea :: Idea}
:: NamedIdea2 = { name2 :: Name, idea2 :: NumIdea}
derive class iTask NamedIdea, NumIdea, NamedIdea2

doIdentified :: (Name -> Task x) -> Task x | iTask x
doIdentified task
	=   enterInformation "Enter your name" []
	>>= task

editIdeas :: Name -> Task NamedIdea
editIdeas name
	= enterInformation (name +++ " add your idea") []
	>>= \idea . return {name = name, idea = idea}

editIdeas2 :: Name -> Task NamedIdea2
editIdeas2 name
	= updateInformation (name +++ " add your idea") [] {note = Nothing, numb = Display 7}
	>>= \idea . return {name2 = name, idea2 = idea}

editIdeas3 :: [NamedIdea2] Name -> Task [NamedIdea2]
editIdeas3 list name
	= viewInformation "ideas" [] list
	||- updateInformation (name +++ " add your idea") [] {note = Nothing, numb = Display (length list + 1)}
	>>= \idea. editIdeas3 (list ++ if (useful idea) [{name2 = name, idea2 = idea}] []) name

useful :: NumIdea -> Bool
useful idea = case idea.note of
	Just (Note s) = size s > 5
	_ = False

Start :: *World -> *World
Start world
	= startEngine
		(	doIdentified (editIdeas3 [])
		>>=	viewInformation "The result" []
		)
		world