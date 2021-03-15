const electron = require('electron')

const { app, globalShortcut, BrowserWindow} = electron

let mainWindow

app.on('ready', _ => {
    mainWindow = new BrowserWindow(
        {
            width: 0,
            height: 0,
            resizable: false,
            frame: false,
        })

    mainWindow.hide()

    mainWindow.loadURL(`file://${__dirname}/capture.html`)

    mainWindow.on('close',_ => {
        mainWindow = null
    })

    globalShortcut.register('Ctrl+Alt+d', _ => {
        mainWindow.webContents.send('capture', app.getPath('pictures'))
    })
})