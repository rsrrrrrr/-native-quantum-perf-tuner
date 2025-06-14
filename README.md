
# Native Quantum Performance Tuner – Starter Kit

> **버전**: 0.1.0 – 2025-06-14

이 패키지는 데모 WebGL 대시보드를 **실제 네이티브 서비스 + Electron UI** 로
승격시키기 위한 **스켈레톤 코드** 입니다.

## 1. 요구 사항
| 도구 | 버전 |
|------|------|
| .NET SDK | 8.x |
| Node.js | ≥ 20 |
| Windows 10/11 (x64) | 또는 Linux (WIP) |

## 2. 빌드 순서 (PowerShell)

```ps1
# 1) 백엔드 컴파일
cd backend
dotnet publish -c Release -r win-x64

# 2) 프런트엔드 설치 + 실행
cd ..\frontend
npm install
npm run start
```

### 설치 패키지 만들기

```ps1
# Electron 패키징 (윈도우 x64 EXE)
npm run pack
```

`build/build.ps1` 스크립트는 위 명령을 자동화합니다.

## 3. 구조

```
backend/   ← C# .NET 8 서비스 (LibreHardwareMonitor + NVML)
frontend/  ← Electron UI (React-ready)
build/     ← CI/패키징 스크립트
```

## 4. 주의
* 실제 전압·클럭 제어 코드는 **벤더 SDK**(NVAPI, ADL, XTU)에 따라 달라집니다.
* 관리자 권한으로 실행해야 센서/오버클럭 API 접근이 가능합니다.
