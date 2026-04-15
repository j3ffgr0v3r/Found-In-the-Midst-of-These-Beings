@echo off
setlocal

pushd "%~dp0..\.." || exit /b 1

set BUILD_DIR=Builds\WebGL
set DEPLOY_DIR=temp-deploy
set REMOTE_URL=https://github.com/j3ffgr0v3r/Found-In-the-Midst-of-These-Beings.git

if exist "%DEPLOY_DIR%" rmdir /s /q "%DEPLOY_DIR%"
mkdir "%DEPLOY_DIR%"

xcopy "%BUILD_DIR%\*" "%DEPLOY_DIR%\" /E /I /Y
if errorlevel 1 (
    echo Failed to copy build files.
    exit /b 1
)

cd /d "%DEPLOY_DIR%"

git init
if errorlevel 1 exit /b 1

git checkout -b gh-pages
if errorlevel 1 exit /b 1

type nul > .nojekyll

git add .
if errorlevel 1 exit /b 1

git commit -m "Deploy WebGL build"
if errorlevel 1 (
    echo Git commit failed. Maybe there were no changes?
    exit /b 1
)

git remote add origin "%REMOTE_URL%"
if errorlevel 1 exit /b 1

git push -f origin gh-pages
if errorlevel 1 (
    echo Git push failed.
    exit /b 1
)

cd ..
rmdir /s /q "%DEPLOY_DIR%"

popd

echo Deployment complete.
exit /b 0
