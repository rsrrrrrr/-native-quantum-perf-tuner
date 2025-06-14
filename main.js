
const { app, BrowserWindow } = require('electron');
const { spawn } = require('child_process');
const path = require('path');

let backend;
function createWindow () {
  const win = new BrowserWindow({
    width: 1280,
    height: 800,
    webPreferences: {
      preload: path.join(__dirname, 'preload.js')
    }
  });
  win.loadFile('index.html');

  // launch backend exe from publish dir
  const backendPath = process.platform === 'win32'
      ? path.join(__dirname, '..', 'backend', 'bin', 'Release', 'net8.0', 'win-x64', 'publish', 'NativePerfService.exe')
      : 'dotnet'; // adjust for other OS
  backend = spawn(backendPath, [], { stdio: ['ignore', 'pipe', 'inherit'] });

  backend.stdout.on('data', (data) => {
    win.webContents.send('native-data', data.toString());
  });
}

app.whenReady().then(createWindow);
app.on('window-all-closed', () => {
  if (backend) backend.kill();
  if (process.platform !== 'darwin') app.quit();
});
