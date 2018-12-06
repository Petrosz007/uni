module questions

import iTasks, StdArray

:: Question =
	{question :: String
	,answers  :: [String]
	,correct  :: Int
	}

derive class iTask Question

questions :: Shared [Question]
questions = sharedStore "questions" []

nameTask :: (String -> Task a) -> Task a | iTask a
nameTask task
 =    updateInformation "enter your name" [] ""
  >>* [OnAction ActionOk (ifValue (\name.size name > 2) task)]

mainTask :: String -> Task [Question]
mainTask user
  | user == "admin"
  = updateSharedInformation "Admin view" [] questions
  | user == "rinus" || user == "pieter"
  = teacherTask user
  = studentTask user

teacherTask :: String -> Task [Question]
teacherTask user = editSharedList questions

studentTask name // = viewSharedInformation "The questions" [] questions
 =	get questions >>= \list.
 	answer list 0 0
where
	answer [] correct wrong
		= viewInformation (name + " answered " + toString correct + " questions correct and " + toString wrong + " questions wrong") [] []
	answer [q:rest] correct wrong
		=	viewInformation "question" [] q.question
			||- editChoice "select answer" [ChooseFromGrid snd] [(i,a) \\ i <- [0..] & a <- q.answers] Nothing
			>>= \(i,a).
				if (i == q.correct)
					(answer rest (correct + 1) wrong)
					(answer rest correct (wrong + 1))

editSharedList :: (Shared [a]) -> Task [a] | iTask a
editSharedList store
 =		enterChoiceWithShared "Choose an item to edit" [ChooseFromGrid snd] (mapRead (\ps -> [(i,p) \\ p <- ps & i <- [0..]]) store)
	>>*	[ OnAction (Action "Append") (hasValue (showAndDo append))
		, OnAction (Action "Delete") (hasValue (showAndDo delete))
		, OnAction (Action "Edit")   (hasValue (showAndDo edit))
		, OnAction (Action "First")  (always (showAndDo first (0,undef)))
		, OnAction (Action "Clear")  (always (showAndDo append (-1,undef)))
		, OnAction (Action "Quit")   (always (get store))
		]
where
	showAndDo fun ip
	 = viewSharedInformation "In store" [] store
	   ||-  fun ip
	   >>* [ OnValue 					 (hasValue	(\_ -> editSharedList store))
	 	   , OnAction (Action "Cancel") (always	(editSharedList store))
	 	   ]
	append (i,_)
	 =	enterInformation "Add new item" []
		>>=	\n.upd (\ps -> let (begin,end) = splitAt (i+1) ps in (begin ++ [n] ++ end)) store
	delete (i,_)
	 = upd (\ps -> removeAt i ps) store
	first _
	 = enterInformation "Add new item" []
	   >>= \n.upd (\l.[n:l]) store
	edit (i,p)
	 = updateInformation "Edit item" [] p 
	   >>= \p -> 	upd (\ps ->  updateAt i p ps) store

undef = hd []

Start world
// = startEngine
= startEngineWithOptions (\cli options -> defaultEngineCLIOptions cli {options & sessionTime = 1000000000})
  (nameTask mainTask) world

instance + String where + s t = s +++ t
