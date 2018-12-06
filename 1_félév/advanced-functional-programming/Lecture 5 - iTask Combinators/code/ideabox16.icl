module ideabox16

import iTasks, StdArray
import iTasks.UI.Definition, iTasks.UI.Editor.Builtin
import qualified Data.Map as DM

Start world = startEngine (nameTask mainTask) world
//Start world = startEngine t3 world

:: Idea =
  { idea_name :: String
  , idea_description :: Maybe String
  }

:: NamedIdea =
  { number :: Int
  , idea   :: Idea
  , user   :: String
  }

:: DisplayIdea =
  { num :: Int
  , idea_title :: String
  , owner :: String
  }

derive class iTask Idea, NamedIdea, DisplayIdea

ideas :: Shared [NamedIdea]
ideas = sharedStore "Ideas" []

nameTask :: (String -> Task a) -> Task a | iTask a
nameTask task
 =    updateInformation "enter your name" [] ""
  >>* [OnAction ActionOk (ifValue (\name.size name > 2) task)]

mainTask :: String -> Task [NamedIdea]
mainTask user | user == "admin"
  = updateSharedInformation "admin view" [] ideas
  = userTask user

userTask :: String -> Task [NamedIdea]
userTask user
  =   get ideas >>= \list.
      (enterChoiceWithShared "Current idea list"
        [ChooseFromGrid (\{number,idea,user}.{num=number,idea_title=idea.idea_name,owner=user})]
//        viewSharedInformation "plans" []
        ideas >&^ viewSharedInformation "selected idea" []
        )
  ||- enterInformation ("enter a new idea " +++ user) []
      >>* [ OnAction ActionOk (ifValue (valid list) addIdea)
          , OnAction ActionRefresh (always (userTask user))
          , OnAction ActionQuit (always (get ideas))
          , OnAction ActionDelete (always check)
          ]
where
  valid list idea = size idea.idea_name > 2 && not (isMember idea.idea_name (map (\i=i.idea.idea_name) list))
  addIdea idea 
   =  upd (\list. list ++ [{ idea = idea, user = user, number = length list + 1}]) ideas
    >>| userTask user
  check
    = viewInformation "are you sure to delete all ideas?" [] ""
    >>* [ OnAction ActionYes (always (upd (\list.[]) ideas))
        , OnAction ActionNo  (always (get ideas))
        ]
    >>| userTask user

t1 :: Task [Idea]
t1 = withShared [] \s.show s -||- forever (add s)
where
//	show :: (Shared [Idea]) -> Task [Idea]
	show share = viewSharedInformation "Ideas" [] share
//	add :: (Shared [Idea]) -> Task [Idea]
	add share = enterInformation "New" [] >>= \n.upd (\l.[n:l]) share

myShare :: Shared [Idea]
myShare = sharedStore "myIdeas" []

t2 :: Task [Idea]
t2 = show -||- forever add
where
	show ::Task [Idea]
	show = viewSharedInformation "Ideas" [] myShare
	add :: Task [Idea]
	add = enterInformation "New" [] >>= \n.upd (\l.[n:l]) myShare

t3 :: Task [Idea]
t3 = show -||- forever add
where
	show ::Task [Idea]
	show = viewSharedInformation "Ideas" [] myShare
	add :: Task [Idea]
	add 
		= enterInformation "Name" [] -&&-
		  enterInformation "Desc" [] >>= \(n,d).
		  upd (\l.[{idea_name=n,idea_description=d}:l]) myShare

//noteEditor = UpdateUsing id (const id) (textArea 'DM'.newMap)

/*
userTask :: String -> Task [NamedIdea]
userTask user
  = forever (
      get ideas >>= \list.
      (enterChoiceWithShared "Current idea list"
        [ChooseWith (ChooseFromGrid (\{number,idea,user}.{num=number,idea_title=idea.idea_name,owner=user}))]
//        viewSharedInformation "plans" []
        ideas >&^ viewSharedInformation "selected idea" []
        )
  ||- enterInformation ("enter a new idea " +++ user) []
      >>* [ OnAction ActionOk (ifValue (valid list) addIdea)
          , OnAction (Action "Clear" [ActionKey (unmodified KEY_BACKSPACE)]) (always (get ideas))
          , OnAction ActionQuit (always (throw 0))
          , OnAction ActionDelete (always check)
          ]
          )
where
  valid list idea = size idea.idea_name > 2 && not (isMember idea.idea_name (map (\i=i.idea.idea_name) list))
  addIdea idea 
   =  upd (\list. list ++ [{ idea = idea, user = user, number = length list}]) ideas
  check
    = viewInformation "are you sure to delete all ideas?" [] ""
    >>* [ OnAction ActionYes (always (upd (\list.[]) ideas))
        , OnAction ActionNo  (always (get ideas))
        ]
*/
