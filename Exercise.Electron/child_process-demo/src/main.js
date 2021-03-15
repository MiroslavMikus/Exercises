const electron = require('electron')

const { app, BrowserWindow } = electron

let mainWindow

app.on('ready', _ => {
    mainWindow = new BrowserWindow({
        width: 600,
        height: 150
    })

    mainWindow.loadURL(`file://${__dirname}/status.html`)

    // mainWindow.toggleDevTools();

    mainWindow.on('close', _ => {
        mainWindow = null
    })
})