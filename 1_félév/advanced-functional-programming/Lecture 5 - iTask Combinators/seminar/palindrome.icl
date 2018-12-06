module palindrome

import StdEnv
import iTasks

palindrome :: Task (Maybe String)
palindrome
	=   enterInformation "Enter a palindrome" []
	>>*
		[OnAction (Action "Continue")
			(ifValue isPalindrome (return o Just))
		,OnAction (Action "Cancel")
			(always (return Nothing))
		]

isPalindrome :: String -> Bool
isPalindrome s = reverse [c\\c<-:s] == [c\\c<-:s]

Start w = doTasks palindrome w
