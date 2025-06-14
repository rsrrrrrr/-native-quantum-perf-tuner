
const { contextBridge, ipcRenderer } = require('electron');
contextBridge.exposeInMainWorld('nativeAPI', {
  onData: (callback) => ipcRenderer.on('native-data', (_, data) => callback(JSON.parse(data)))
});
