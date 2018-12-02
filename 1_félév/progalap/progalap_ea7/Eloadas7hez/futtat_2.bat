@echo Eggyel kijjebbrol kell inditani. (Pl. magabol a pptx-bol.)
cd Eloadas9hez/
cls
@echo A programban a lenyegi kiirasok cout-ba tortennek,
@echo a kerdes szovegek kiirasa pedig cerr-be, azaz 
@echo a cout fajlba iranyitasakor a cerr-es output a command ablakban,
@echo a cout-beliek a fajlban jelenik meg.
@pause
cls
@echo Konzolos proba... a parameterek legyenek: P=(1,1); Q=(2,2)
@echo Jegyezze meg az eredmenyt!
irany_2.exe
cls
@echo Ugyanez fajlbol jon... hasonlitsa az elobbihoz!
irany_2.exe <1_1_2_2.be >1_1_2_2.ki
@echo a kimeneti fajl tartalma:
@echo *****************************************************************************
@type 1_1_2_2.ki
@echo *****************************************************************************
@pause
cls

irany_2.exe <1_1_2_1.be >1_1_2_1.ki
@echo a kimeneti fajl tartalma:
@echo *****************************************************************************
@type 1_1_2_1.ki
@echo *****************************************************************************
@pause
cls

irany_2.exe <1_1_1_2.be >1_1_1_2.ki
@echo a kimeneti fajl tartalma:
@echo *****************************************************************************
@type 1_1_1_2.ki
@echo *****************************************************************************
@pause

