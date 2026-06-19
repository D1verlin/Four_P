@echo off
title UrbanTransit Launcher

echo ======================================================================
echo     UrbanTransit - Intelligent Urban Transit System
echo ======================================================================
echo.
echo Initializing startup...
echo.

:: Check for dotnet
where dotnet >nul 2>nul
if %errorlevel% neq 0 (
    echo [ERROR] .NET SDK not found. Please install .NET 10.0 SDK to run Backend.
    pause
    exit /b 1
)

:: Check for node/npm
where npm >nul 2>nul
if %errorlevel% neq 0 (
    echo [ERROR] Node.js/npm not found. Please install Node.js to run Frontend.
    pause
    exit /b 1
)

echo [1/2] Starting Backend (ASP.NET Core Web API)...
start "UrbanTransit Backend" cmd /k "cd /d %~dp0TransitApi && dotnet run"

echo [2/2] Starting Frontend (React + Vite)...
start "UrbanTransit Frontend" cmd /k "cd /d %~dp0transit-client && npm run dev"

echo.
echo ======================================================================
echo All components started in separate windows!
echo.
echo  - Backend (API):   http://localhost:5000
echo  - Swagger UI:      http://localhost:5000/swagger/index.html
echo  - Frontend:        http://localhost:5173
echo.
echo  Admin Dashboard (http://localhost:5173/admin):
echo  - Login:    admin
echo  - Password: admin
echo.
echo  To stop the services, close the backend and frontend command windows.
echo ======================================================================
echo.
pause
