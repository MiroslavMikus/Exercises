const electron = require('electron');

const ipc = electron.ipcRenderer;

document.getElementById('start').addEventListener('click', _=> {
    ipc.send('countdown-start');
});

ipc.on('countdown', (ent, count)=>{
    document.getElementById('count').innerHTML = count;
})