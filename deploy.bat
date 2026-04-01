@echo off
setlocal enabledelayedexpansion
title GitHub ve Railway Deployment

echo =========================================================
echo       ANTIGRAVITY AI TRADER V2 GITHUB VE RAILWAY        
echo =========================================================
echo.

:: 1. Git'i Başlat
if not exist ".git" (
    echo [BILGI] Git baslatiliyor...
    git init
) else (
    echo [BILGI] Git daha onceden baslatilmis.
)

:: 2. Değişiklikleri Ekle
echo [BILGI] Dosyalar stage alanina ekleniyor...
git add .

:: 3. Commit İşlemi
set /p COMMIT_MSG="Commit mesaji girin (Bos birakirsaniz 'Otomatik Railway Deployment' yazilacak): "
if "!COMMIT_MSG!"=="" set COMMIT_MSG=Otomatik Railway Deployment
git commit -m "!COMMIT_MSG!"
git branch -M main

echo.
:: 4. Origin (Remote) Url Sor ve Ayarla
set /p REPO_URL="Lutfen projenizi gondereceginiz GITHUB REPO URL'sini yapistirin: "

if "!REPO_URL!"=="" (
    echo [HATA] Bir URL girmediginiz icin islem iptal edildi.
    pause
    exit /b
)

git remote add origin !REPO_URL! 2>nul
if %errorlevel% neq 0 (
    echo [BILGI] Origin onceden tanimlari, URL guncelleniyor...
    git remote set-url origin !REPO_URL!
)

echo.
echo [BILGI] Kodlar GitHub'a yukleniyor (Yuklendiğinde Railway otomatik tetiklenecektir)...
git push -u origin main

echo.
echo =========================================================
echo ISLEM TAMAMLANDI!
echo 1) Endise etmeyin, kodlariniz Railway icin GitHub'a gonderildi.
echo 2) Lutfen Railway (veya benzeri platformunuz) uzerinden bu repoya baglanin.
echo 3) Railway'de Worker veya WebAPI hangi projeyi calistirmasini istiyorsaniz Start Command kismina:
echo    "dotnet run --project AntigravityAiTraderV2.Worker" 
echo                     veya
echo    "dotnet run --project AntigravityAiTraderV2.WebAPI"
echo    yazmayi unutmayin.
echo =========================================================
pause
