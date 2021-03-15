const electron = require('electron');

const countdown = require('./countdown.js')

const app = electron.app
const BrowserWindow = electron.BrowserWindow
const ipc = electron.ipcMain

const windows = [] ;

app.on('ready', _ => {

    [1,2,3,].forEach(_ => {
        let win = new BrowserWindow({
            height: 400,
            width: 400 
        })
        
        win.loadURL(`file://${__dirname}/countdown.html`);
        
        win.on('closed', _ => {

            // Closing window will update windows array as well.
            windows.splice(windows.indexOf(win),1);

            console.log('Closed!');
        })

        windows.push(win);
    })
})

ipc.on('countdown-start', _ =>{
    console.log("Caught countdownt start event");

    countdown(count => {
        console.log("Count ", count);

        windows.forEach(win => {
            win.webContents.send('countdown', count);
        })
    });
});
