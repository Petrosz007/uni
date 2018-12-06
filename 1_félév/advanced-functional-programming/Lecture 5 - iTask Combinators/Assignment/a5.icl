module a5

import iTasks

import Data.List
import Data.Func

derive class iTask Function, Question

:: Function = Student | Teacher | Admin
:: Question =
	{ question :: String
	, answers  :: [String]
	, correct  :: Int
	}

questions :: Shared [Question]
questions = sharedStore "questions"
	[{question="Afp cool?"
	 ,answers=["yes", "no"]
	 ,correct=0}
	,{question="Mart the awesomest TA?"
	 ,answers=["yes", "no"]
	 ,correct=0}
	]

Start w = doTasks login w

login :: Task [Question]
login = enterInformation "Enter your function" []
	>>= \function->case function of
		Teacher = teacher
		Admin = admin
		Student = student

student :: Task [Question]
student = get questions
	>>= \qs->sequence (map makeQuestion qs) @ distillResult
	>>= viewInformation "Result" []
	>>* [OnAction (Action "Quit") (always login)]
where
	makeQuestion :: Question -> Task Bool
	makeQuestion q = enterChoice q.question [ChooseFromList snd] (zip2 [0..] q.answers)
		@? \v->case v of
			NoValue = NoValue
			Value (idx, _) _ = Value (idx == q.correct) True

	distillResult :: [Bool] -> String
	distillResult [] = "No questions answered!"
	distillResult b =
		"You had " +++
		toString good +++
		" answers correct and " +++
		toString bad +++
		" answers incorrect which results in a score of: " +++
		toString (good*100 / (good+bad)) +++
		"/100"
	where
		good = length $ filter id b
		bad = length $ filter not b

admin :: Task [Question]
admin = updateSharedInformation "Questions" [] questions

teacher :: Task [Question]
teacher = forever
	$     enterChoiceWithShared "Choose an item to edit" [ChooseFromList id] questions
	>>*
		[ OnAction (Action "Append") (withValue $ Just o append)
		, OnAction (Action "Delete") (withValue $ Just o delete)
		, OnAction (Action "Edit")   (withValue $ Just o edit)
		, OnAction (Action "Clear")  (withValue $ Just o clear)
		, OnAction (Action "First")  (always first)
		, OnAction (Action "Quit")   (always login)
		]
where
	append choice = orCancel (enterInformation () []) \nq->upd (insertAfter choice nq) questions
	delete choice = upd (deleteBy (===) choice) questions
	edit choice   = orCancel (updateInformation () [] choice) $ replace choice
	clear choice  = orCancel (enterInformation () []) $ replace choice
	first         = orCancel (enterInformation () []) \nq->upd (\x->[nq:x]) questions

	replace choice nq = upd (deleteBy (===) choice o insertAfter choice nq) questions

	insertAfter :: a a [a] -> [a] | gEq{|*|} a
	insertAfter after el [] = [el]
	insertAfter after el [e:es]
		| e === after = [e,el:es]
		= insertAfter after el es

	orCancel do done = do >>*
		[ OnAction (Action "Cancel") (always teacher)
		, OnAction (Action "Continue") (withValue (Just o done))
		]
