
window.nativeAPI.onData((payload) => {
  document.getElementById('cpu').innerText = payload.cpu.load.toFixed(1) + '%';
  document.getElementById('gpu').innerText = payload.gpu.gpuLoad + '%';
});
