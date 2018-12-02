@echo Eggyel kijjebbrol kell inditani. (Pl. magabol a pptx-bol.)
cd Eloadas9hez/
cls
@echo A programban minden kiiras a cout-ba tortenik,
@echo igy a cout fajlba iranyitasakor a kerdesek szovege 
@echo is megjelenik a fajlban.
@pause
cls
@echo Konzolos proba... a parameterek legyenek: P=(1,1); Q=(2,2)
@echo Jegyezze meg az eredmenyt!
irany.exe
cls
@echo Ugyanez fajlbol jon... hasonlitsa az elobbihoz!
irany.exe <1_1_2_2.be >1_1_2_2.ki
@echo A kimeneti fajl tartalma ket *-os sor kozott:
@echo *****************************************************************************
@type 1_1_2_2.ki
@echo *****************************************************************************
@pause
cls

irany.exe <1_1_2_1.be >1_1_2_1.ki
@echo A kimeneti fajl tartalma ket *-os sor kozott:
@echo *****************************************************************************
@type 1_1_2_1.ki
@echo *****************************************************************************
@pause
cls

irany.exe <1_1_1_2.be >1_1_1_2.ki
@echo A kimeneti fajl tartalma ket *-os sor kozott:
@echo *****************************************************************************
@type 1_1_1_2.ki
@echo *****************************************************************************
@pause

